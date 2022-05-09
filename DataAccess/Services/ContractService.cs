using DataAccess.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services;

public class ContractService : DbService, IService<Contract>
{
    public ICollection<Contract> GetList()
    {
        return Context.Contracts
            .Include(c => c.Deals)
            .Select(c => Mapper.Map<Contract>(c))
            .ToList();
    }

    public Contract? Get(Guid id)
    {
        var entity = Context.Contracts.Find(id);

        //отключаем отслеживание, чтобы повторно использовать сервис
        Context.Entry(entity!).State = EntityState.Detached;
            
        return entity is not null 
            ? Mapper.Map<Contract>(entity)
            : null;
    }

    public void Create(Contract item)
    {
        var entity = Mapper.Map<ContractEntity>(item);
        Transaction(() => Context.Contracts.Add(entity));
    }

    public void Update(Contract item)
    {
        var entity = Mapper.Map<ContractEntity>(item);
        Transaction(() => Context.Contracts.Update(entity));
    }

    public void Delete(Guid id)
    {
        var entity = Context.Contracts.Find(id);
        if (entity is null)
            throw new Exception("Такого контракта нет");
        Transaction(() => Context.Contracts.Remove(entity));
    }
}