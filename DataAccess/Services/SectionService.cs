using DataAccess.Models.Crm;
using Domain.Models.Crm;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services;

public class SectionService : DbService, IService<Section>
{
    public ICollection<Section> GetList()
    {
        return Context.Sections
            .Include(s => s.Beats)
            .Include(s => s.Funnel)
            .Select(s => Mapper.Map<Section>(s))
            .ToList();
    }

    public Section? Get(Guid id)
    {
        var entity = Context.Sections.Find(id);
        if (entity is null)
            return null;

        Context.Entry(entity).Collection(s => s.Beats).Load();
        Context.Entry(entity).Reference(s => s.Funnel).Load();
        //отключаем отслеживание, чтобы повторно использовать сервис
        Context.Entry(entity).State = EntityState.Detached;

        return Mapper.Map<Section>(entity);

    }

    public void Create(Section item)
    {
        var entity = Mapper.Map<SectionEntity>(item);
        Transaction(() => Context.Sections.Add(entity));
    }

    public void Update(Section item)
    {
        var entity = Mapper.Map<SectionEntity>(item);
        Transaction(() => Context.Sections.Update(entity));
    }

    public void Delete(Guid id)
    {
        var entity = Context.Sections.Find(id);
        if (entity is null)
            throw new Exception("Такой секции нет");
        Transaction(() => Context.Sections.Remove(entity));
    }
}