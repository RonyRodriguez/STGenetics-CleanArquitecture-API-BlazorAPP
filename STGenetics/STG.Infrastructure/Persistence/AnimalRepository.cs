using STG.Domain.Entity;
using STG.Infrastructure.Databse;
using STG.Infrastructure.Shared.Repository;
using STG.Infrastructure.Shared.UnitOfWork;

namespace STG.Infrastructure.Persistence
{
    public class AnimalRepository : Repository<Animal>
    {
        public AnimalRepository(AppDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        { }
    }
}
