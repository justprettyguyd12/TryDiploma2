using DataAccess.Models;
using Domain.Models;
using Domain.Models.Crm;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services;

public class BeatService : DbService, IService<Beat>
{
    public ICollection<Beat> GetList()
    {
        return Context.Beats
            .Include(b => b.Section)
            .Select(b => Mapper.Map<Beat>(b))
            .ToList();
    }

    public Beat? Get(Guid id)
    {
        var entity = Context.Beats.Find(id);
        if (entity is null)
            return null;
        
        //отключаем отслеживание, чтобы повторно использовать сервис
        Context.Entry(entity).State = EntityState.Detached;
            
        return Mapper.Map<Beat>(entity);
    }

    public void Create(Beat item)
    {
        var entity = Mapper.Map<BeatEntity>(item);
        Transaction(() => Context.Beats.Add(entity));
    }

    public void Update(Beat item)
    {
        var entity = Mapper.Map<BeatEntity>(item);
        Transaction(() => Context.Beats.Update(entity));
    }

    public void Delete(Guid id)
    {
        var entity = Context.Beats.Find(id);
        if (entity is null)
            throw new Exception("Бита с таким id не обнаружено");
        Transaction(() => Context.Beats.Remove(entity));
    }
}