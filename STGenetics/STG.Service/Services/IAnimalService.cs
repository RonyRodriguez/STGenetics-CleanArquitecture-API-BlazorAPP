using STG.Domain.Domain;

namespace STG.Service.Services
{
    public interface IAnimalService
    {
        Task<AnimalDomain> GetById(long id);

        Task<IEnumerable<AnimalDomain>> GetAll();

        Task Update(IEnumerable<AnimalDomain> animalDomains, CancellationToken cancellationToken = default);
    }
}
