using DataAccess.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services;

public class ClientService : DbService, IService<Client>
{
    public ICollection<Client> GetList()
    {
        return Context.Clients
            .Include(d => d.Deals)
            .Select(d => Mapper.Map<Client>(d))
            .ToList();
    }

    public Client? Get(Guid id)
    {
        var entity = Context.Clients.Find(id);

        //отключаем отслеживание, чтобы повторно использовать сервис
        Context.Entry(entity!).State = EntityState.Detached;
            
        return entity is not null 
            ? Mapper.Map<Client>(entity)
            : null;
    }

    public void Create(Client item)
    {
        var entity = Mapper.Map<ClientEntity>(item);
        Transaction(() => Context.Clients.Add(entity));
    }

    public void Update(Client item)
    {
        var entity = Mapper.Map<ClientEntity>(item);
        Transaction(() => Context.Clients.Update(entity));
    }

    public void Delete(Guid id)
    {
        var entity = Context.Clients.Find(id);
        if (entity is null)
            throw new Exception("Клиента с таким id не обнаружено");
        Transaction(() => Context.Clients.Remove(entity));
    }
}