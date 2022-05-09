using DataAccess.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services;

public class BagService : DbService, IService<ShoppingBag>
{
    public ICollection<ShoppingBag> GetList()
    {
        return Context.Bags
            .Include(b => b.Beats)
            .Select(b => Mapper.Map<ShoppingBag>(b))
            .ToList();
    }

    public ShoppingBag? Get(Guid id)
    {
        var entity = Context.Bags.Find(id);

        //отключаем отслеживание, чтобы повторно использовать сервис
        Context.Entry(entity!).State = EntityState.Detached;
            
        return entity is not null 
            ? Mapper.Map<ShoppingBag>(entity)
            : null;
    }

    public void Create(ShoppingBag item)
    {
        var entity = Mapper.Map<ShoppingBagEntity>(item);
        Transaction(() => Context.Bags.Add(entity));
    }

    public void Update(ShoppingBag item)
    {
        var entity = Mapper.Map<ShoppingBagEntity>(item);
        Transaction(() => Context.Bags.Update(entity));
    }

    public void Delete(Guid id)
    {
        var entity = Context.Bags.Find(id);
        if (entity is null)
            throw new Exception("Такой корзины нет");
        Transaction(() => Context.Bags.Remove(entity));
    }
}