using AutoMapper;
using AutoMapper.QueryableExtensions;
using OBL.Core.Exceptions;
using STG.Domain.Domain;
using STG.Domain.Entity;
using STG.Infrastructure.Shared.Query;
using STG.Infrastructure.Shared.Repository;
using STG.Infrastructure.Shared.UnitOfWork;

namespace STG.Service.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IRepository<Animal> _repository;
        private readonly IQuery<Animal> _query;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AnimalService(IRepository<Animal> repository, IQuery<Animal> productQuery, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _query = productQuery;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AnimalDomain>> GetAll()
        {
            var sql = await _query.GetAllAsync();
            var list = sql.ProjectTo<AnimalDomain>(_mapper.ConfigurationProvider);
            return list;
        }

        public async Task<AnimalDomain> GetById(long id)
        {
            var entity = await _query.GetByIdAsync(id);
            return _mapper.Map<AnimalDomain>(entity);
        }

        public async Task Update(IEnumerable<AnimalDomain> animalDomains, CancellationToken cancellationToken = default)
        {
            foreach (var animalDomain in animalDomains)
            {
                var animalEntity = await _query.GetByIdAsync(animalDomain.Id);
                if (animalEntity == null)
                {
                    throw new NotFoundException($"Animal with ID {animalDomain.Id} not found.");
                }

                _mapper.Map(animalDomain, animalEntity);
                await _repository.UpdateAsync(animalEntity);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

    }
}
