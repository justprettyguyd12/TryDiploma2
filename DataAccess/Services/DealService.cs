using DataAccess.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services;

public class DealService : DbService, IService<Deal>
{
    public ICollection<Deal> GetList()
    {
        return Context.Deals
            .Include(d => d.Client)
            .Include(d => d.Beat)
            .Include(d => d.Contract)
            .Select(s => Mapper.Map<Deal>(s))
            .ToList();
    }

    public Deal? Get(Guid id)
    {
        var entity = Context.Deals.Find(id);

        //отключаем отслеживание, чтобы повторно использовать сервис
        Context.Entry(entity!).State = EntityState.Detached;
            
        return entity is not null 
            ? Mapper.Map<Deal>(entity)
            : null;
    }

    public void Create(Deal item)
    {
        var entity = Mapper.Map<DealEntity>(item);
        Transaction(() => Context.Deals.Add(entity));
    }

    public void Update(Deal item)
    {
        var entity = Mapper.Map<DealEntity>(item);
        Transaction(() => Context.Deals.Update(entity));
    }

    public void Delete(Guid id)
    {
        var entity = Context.Deals.Find(id);
        if (entity is null)
            throw new Exception("Такой сделки нет");
        Transaction(() => Context.Deals.Remove(entity));
    }
}