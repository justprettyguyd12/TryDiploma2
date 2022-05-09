using DataAccess.Context;
using Domain.Models.Crm;

namespace DataAccess.Services;

public interface IService<T>
{
    ICollection<T> GetList();
    T? Get(Guid id);
    void Create(T item);
    void Update(T item);
    void Delete(Guid id);
}