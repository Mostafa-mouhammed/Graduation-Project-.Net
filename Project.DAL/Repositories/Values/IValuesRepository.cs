using Project.DAL.Models;
using Project.DAL.Repositories.Generic;

namespace Project.DAL.Repositories.ValuesRepository;
public interface IValuesRepository : IGenericRepository<Values>
{
    Task<Values> getValuebyName(string valueName);
    Task<IEnumerable<Values>> getValuesInList(IEnumerable<int> valuesIds);

}
