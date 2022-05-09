using DataAccess.Models.Crm;
using Domain.Models.Crm;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services;

public sealed class FunnelService : DbService, IService<Funnel>
{
    /// <summary>
    /// Загружает связанные секции и биты в секциях
    /// </summary>
    /// <returns></returns>
    public ICollection<Funnel> GetList()
    {
        return Context.Funnels
            .Include(f => f.Sections)
            .ThenInclude(s => s.Beats)
            .Select(f => Mapper.Map<Funnel>(f))
            .ToList();
    }

    public Funnel? Get(Guid id)
    {
        var entity = Context.Funnels.Find(id);
        if (entity is null)
            return null;

        Context.Entry(entity).Collection(f => f.Sections).Load();
        //отключаем отслеживание, чтобы повторно использовать сервис
        Context.Entry(entity).State = EntityState.Detached;

        return Mapper.Map<Funnel>(entity);
    }

    public void Create(Funnel item)
    {
        var entity = Mapper.Map<FunnelEntity>(item);
        Transaction(() => Context.Funnels.Add(entity));
    }

    public void Update(Funnel item)
    {
        var entity = Mapper.Map<FunnelEntity>(item);
        Transaction(() => Context.Funnels.Update(entity));
    }

    public void Delete(Guid id)
    {
        var entity = Context.Funnels.Find(id);
        if (entity is null)
            throw new Exception("Такой воронки нет");
        Transaction(() => Context.Funnels.Remove(entity));
    }
}