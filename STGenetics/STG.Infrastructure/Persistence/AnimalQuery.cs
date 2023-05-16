using STG.Domain.Entity;
using STG.Infrastructure.Databse;
using STG.Infrastructure.Shared.Query;

namespace STG.Infrastructure.Persistence
{
    public class AnimalQuery : Query<Animal>
    {
        public AnimalQuery(AppDbContext context) : base(context)
        {

        }
    }
}
