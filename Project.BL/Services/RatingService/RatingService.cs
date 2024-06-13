using Project.BL.Dtos.Rating;
using Project.BL.Dtos.Statuscode;
using Project.BL.Mappers.Mapper;
using Project.DAL.Models;
using Project.DAL.UnitOfWork;
using System.Security.Claims;

namespace Project.BL.Services.RatingService;
public class RatingService : IRatingService
{
    private readonly IUnitRepository _unitRepository;
    private readonly IMapper _mapper;

    public RatingService(IUnitRepository unitRepository, IMapper mapper)
    {
        _unitRepository = unitRepository;
        _mapper = mapper;
    }
    public async Task<StatuscodeDTO> addRating(ClaimsPrincipal user, int productId, RatingInsertDTO insert)
    {
        User? existedUser = await _unitRepository.user.GetUserAsync(user);
        if (existedUser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "Invalid Token");

        Product? product = await _unitRepository.product.Getone(productId);
        if (product == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no product with this id");

        Rating? exiestRating = await _unitRepository.rating.getRating(productId, existedUser.Id);
        if (exiestRating != null)
            return new StatuscodeDTO(Statuscode.BadRequest, "you already rated this product");

        Rating newRate = _mapper.rating.insertToModel(insert, existedUser.Id, productId);
        await _unitRepository.rating.Add(newRate);
        await _unitRepository.SaveChanges();

        return new StatuscodeDTO(Statuscode.Created, null);
    }

    public async Task<StatuscodeDTO> deleteRating(ClaimsPrincipal user, int productId)
    {
        User? existedUser = await _unitRepository.user.GetUserAsync(user);
        if (existedUser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "Invalid Token");

        Product? product = await _unitRepository.product.Getone(productId);
        if (product == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no product with this id");

        Rating? exiestRating = await _unitRepository.rating.getRating(productId, existedUser.Id);
        if (exiestRating == null)
            return new StatuscodeDTO(Statuscode.BadRequest, "You didn't rate this product yet");

        _unitRepository.rating.Delete(exiestRating);
        await _unitRepository.SaveChanges();

        return new StatuscodeDTO(Statuscode.NoContent);
    }

    public async Task<StatuscodeDTO> getAvgRating(int productId)
    {
        double avg = await _unitRepository.rating.getAvrageRate(productId);
        return new StatuscodeDTO(Statuscode.Ok,null,avg);
    }

    public async Task<StatuscodeDTO> getProductRating(int productId, int page, ClaimsPrincipal? user)
    {
        User? existedUser = await _unitRepository.user.GetUserAsync(user);

        Product? product = await _unitRepository.product.Getone(productId);
        if (product == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no product with this id");

        IEnumerable<Rating> productRatingModel = await _unitRepository.rating.getProductRatings(productId, page, existedUser?.Id);
        int ratingTotalPages = await _unitRepository.rating.getRateCount(productId);
        Rating? existingRate = await _unitRepository.rating.getRating(productId, existedUser?.Id);

        ProductRatingDTO productRating = _mapper.rating.toProductRating(productRatingModel, ratingTotalPages, existingRate);
        return new StatuscodeDTO(Statuscode.Ok, null, productRating);
    }

    public async Task<StatuscodeDTO> isproductEligable(int productId, ClaimsPrincipal user)
    {
        User? existedUser = await _unitRepository.user.GetUserAsync(user);
        if (existedUser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "Invalid Token");

        Product? product = await _unitRepository.product.Getone(productId);
        if (product == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no product with this id");

        int? productIdInOrder = await _unitRepository.orderItem.isProductEligible(productId,existedUser.Id);
        bool isEligable = productIdInOrder != 0 && productIdInOrder != null;

        return new StatuscodeDTO(Statuscode.Ok, null, isEligable);
    }

    public async Task<StatuscodeDTO> updateRating(ClaimsPrincipal user, int productId, RatingInsertDTO rating)
    {
        User? existedUser = await _unitRepository.user.GetUserAsync(user);
        if (existedUser == null)
            return new StatuscodeDTO(Statuscode.NotFound, "Invalid Token");

        Product? product = await _unitRepository.product.Getone(productId);
        if (product == null)
            return new StatuscodeDTO(Statuscode.NotFound, "There is no product with this id");

        Rating? exiestRating = await _unitRepository.rating.getRating(productId, existedUser.Id);
        if (exiestRating == null)
            return new StatuscodeDTO(Statuscode.BadRequest, "You didn't rate this product yet");

        exiestRating.rate = rating.rate;
        exiestRating.reviewText = rating.reviewText;
        exiestRating.reviewTitle = rating.reviewtitle;

        _unitRepository.rating.Update(exiestRating);
        await _unitRepository.SaveChanges();

        return new StatuscodeDTO(Statuscode.NoContent);
    }
}