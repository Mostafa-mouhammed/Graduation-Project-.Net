using Project.BL.Dtos.Orders;
using Project.BL.Dtos.Statuscode;
using Project.BL.Mappers.Mapper;
using Project.DAL.Models;
using Project.DAL.UnitOfWork;
using Stripe.Checkout;
using Stripe;
using System.Security.Claims;
using Project.BL.Dtos.Brand;

namespace Project.BL.Services.OrderService;
public class OrderService : IOrderService
{
    private readonly IUnitRepository _unitRepository;
    private readonly IMapper _mapper;

    public OrderService(IUnitRepository unitRepository, IMapper mapper)
    {
        _unitRepository = unitRepository;
        _mapper = mapper;
    }

    public async Task<StatuscodeDTO> cancelOrder(ClaimsPrincipal user, int orderId)
    {
        /* check if the user is exist and check the token */
        User? existedUser = await _unitRepository.user.GetUserAsync(user);
        if (existedUser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "Invalid Token");

        Order? Order = await _unitRepository.order.Getone(orderId);
        if (Order == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no such an order linked with this user");
        Order.status = OrderStatus.Cancelled;

        /* return each item quantity back to the stock */
        await returnQtyStockBackAfterCancellation(orderId);
        await _unitRepository.SaveChanges();
        return new StatuscodeDTO(Statuscode.NoContent);

    }

    public async Task<StatuscodeDTO> falidPayment(string userId, int orderId)
    {
        Order? existedOrder = await _unitRepository.order.CheckOrder(userId, orderId);
        if (existedOrder == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no such an order linked with this user");

        if (existedOrder.status != OrderStatus.PendingPayment)
            return new StatuscodeDTO(Statuscode.NotFound, $"Order status is already {existedOrder.status}");

        existedOrder.status = OrderStatus.Cancelled;

        /* return each item quantity back to the stock */
        await returnQtyStockBackAfterCancellation(orderId);
        await _unitRepository.SaveChanges();

        return new StatuscodeDTO(Statuscode.Redirect, null, $"http://localhost:4200/payment/failed");
    }


    public async Task<StatuscodeDTO> succsessPayment(string userId, int orderId)
    {
        Order? existedOrder = await _unitRepository.order.CheckOrder(userId, orderId);

        if (existedOrder == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no such an order linked with this user");

        if (existedOrder.status != OrderStatus.PendingPayment)
            return new StatuscodeDTO(Statuscode.NotFound, $"Order status is already {existedOrder.status}");

        existedOrder.status = OrderStatus.Pending;

        await _unitRepository.SaveChanges();

        return new StatuscodeDTO(Statuscode.Redirect, null, $"http://localhost:4200/payment/success");
    }

    public async Task<StatuscodeDTO> placeOrder(ClaimsPrincipal user, string address)
    {
        /* check if the user is exist and check the token */
        User? existedUser = await _unitRepository.user.GetUserAsync(user);
        if (existedUser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "Invalid Token");

        /* check if the user's cart is exist */
        Cart? cart = await _unitRepository.cart.getCartByUserId(existedUser.Id);
        if (cart == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no cart linked with you");

        /* check if there is a products in the user cart or the cart is empty */
        if (cart.cartProducts.Count() <= 0)
            return new StatuscodeDTO(Statuscode.NotFound, "your cart is empty");


        /* create new order and assign it with the products in the cart and the user id */
        Order newOrder = _mapper.order.createDefaultOrder(cart, address);

        /* add the order and save changes to get the order id from the database identity */
        await _unitRepository.order.Add(newOrder);
        await _unitRepository.SaveChanges();

        /* convert cart products to order items */
        IEnumerable<OrderItem> orderItems = _mapper.orderItem.cartProductsToOrderItemlist(cart.cartProducts, newOrder.Id);

        /* assign the converted order items it to the order created */
        _unitRepository.orderItem.addOrderItemsList(orderItems);

        /* empty the cart after creating order */
        _unitRepository.cartProduct.deleteAllbyCartId(cart.Id);

        /* decrease the product stock quantity by the taken quantity of the order items */
        await adjustStockQtyAfterPayment(newOrder.Items);
        await _unitRepository.SaveChanges();


        /* Check the products in all carts that match those in the order,
        If the quantity exceeds the stock limit, reduce it to match the stock limit */
        await adjustCartItemsQuantityAfterPayment(newOrder.Items);

        /* save the changes and create stripe payment session and return session url to the user */
        Session session = createStripeSession(existedUser.Id, cart, newOrder.Id);

        await _unitRepository.SaveChanges();

        return new StatuscodeDTO(Statuscode.Ok, null, session.Url);
    }

    public async Task<StatuscodeDTO> viewOrdersHistory(ClaimsPrincipal user, int page,string sort)
    {
        User? existedUser = await _unitRepository.user.GetUserAsync(user);
        if (existedUser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "Invalid Token");

        IEnumerable<Order> ordersModel = await _unitRepository.order.getOrdersHistory(existedUser.Id, page, sort);
        int orderCount = await _unitRepository.order.GetTotalPagesbyOrders(existedUser.Id);
        int totalPages = getTotalPages(orderCount, 5);
        OrderPaginationRead orderPagination = _mapper.order.listModelToPagingation(ordersModel, totalPages);

        return new StatuscodeDTO(Statuscode.Ok, null, orderPagination);
    }

    Session createStripeSession(string userId, Cart cart,int orderId)
    {
        StripeConfiguration.ApiKey = "sk_test_51OoqmoFEukyPARz9iHniLJZCKXAZ4bOaDGDAEsS0VcPdiULDZJLYy38PrtpXL7b7O9rAOeKEX0Jq30rUk1Zu71j000Bp7JydvH";

        // Create a list to store SessionLineItemOptions for each product
        var lineItems = new List<SessionLineItemOptions>();

        // Iterate over the list of products
        foreach (var item in cart.cartProducts)
        {
            // Create a SessionLineItemOptions object for the current product
                var lineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "USD",
                        UnitAmount = (int)item.Product.Price * 100, // Convert unit price to cents
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name,
                            Images = new List<string> { item.Product.Image }
                        }
                    },
                    Quantity = item.CartProductQuantity,

                };

            // Add the SessionLineItemOptions to the lineItems list
            lineItems.Add(lineItem);
        }

        var options = new SessionCreateOptions
        {
            Mode = "payment", // Specify the mode parameter as "payment"
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = lineItems,
            SuccessUrl = $"https://localhost:7173/api/Order/PaymentSucsses?userId={userId}&orderId={orderId}",
            CancelUrl = $"https://localhost:7173/api/Order/PaymentFailed?userId={userId}&orderId={orderId}",
            ExpiresAt = DateTime.UtcNow.AddMinutes(35),
        };

        var service = new SessionService();
        var session = service.Create(options);
        return session;
    }
    public async Task adjustStockQtyAfterPayment(IEnumerable<OrderItem> orderItems)
    {
        var products = await _unitRepository.product.getProductsByOrderItems(orderItems);
        foreach (var product in products)
        {
            OrderItem orderitem = orderItems.FirstOrDefault(oi => oi.ProductId == product.Id);
            product.Quantity -= orderitem.ItemQuantity;
        }
    }
    public async Task adjustCartItemsQuantityAfterPayment(IEnumerable<OrderItem> orderItems)
    {
        var cartProducts = await _unitRepository.cartProduct.getCartProductsByOrderItems(orderItems);
        foreach (var cartproduct in cartProducts)
        {
            cartproduct.CartProductQuantity = cartproduct.Product.Quantity;
        }
    }
    public async Task<IEnumerable<OrderReadDTO>> GetAllOrders()
    {
        IEnumerable<Order> orders = await _unitRepository.order.GetAll();
        IEnumerable<OrderReadDTO> ordersDTO = _mapper.order.listModelToRead(orders);
        return ordersDTO;
    }

    public async Task<StatuscodeDTO> UpdateOrderStatus(int orderId, OrderStatus newStatus)
    {
        Order? order = await _unitRepository.order.Getone(orderId);
        if (order == null)
            return new StatuscodeDTO(Statuscode.NotFound, "Order not found.");

        order.status = newStatus;
        await _unitRepository.SaveChanges();

        return new StatuscodeDTO(Statuscode.Ok);
    }

    public async Task returnQtyStockBackAfterCancellation(int orderId)
    {
        var orderItems = await _unitRepository.orderItem.getOrderItemsWithProducts(orderId);
        foreach (var orderItem in orderItems)
        {
            orderItem.product.Quantity += orderItem.ItemQuantity;
        }
    }

    int getTotalPages(int count, int limit)
    {
        return (int)Math.Ceiling(((double)count / limit));
    }

    public async Task<StatuscodeDTO> GetDeliveredOrdersCount(ClaimsPrincipal user)
    {
        /* check if the user is exist and check the token */
        User? existedUser = await _unitRepository.user.GetUserAsync(user);
        if (existedUser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "Invalid Token");

        int count = await _unitRepository.order.GetDeliveredOrdersCount(existedUser.Id);
        return new StatuscodeDTO(Statuscode.Ok,null, count);
    }
}
