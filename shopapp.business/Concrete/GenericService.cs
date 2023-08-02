using Microsoft.EntityFrameworkCore;
using shopapp.business.Concrete.Mapper;
using shopapp.core.Aspects.Logging;
using shopapp.core.DataAccess.Abstract;
using shopapp.core.DTOs.Concrete;
using shopapp.core.Entity.Abstract;
using System.Linq.Expressions;

namespace shopapp.business.Concrete
{
    public class GenericService<T, TDTO>
        where T : class, IEntity,new()
        where TDTO : class
    {
        private readonly IGenericRepository<T> _genericRepository;
        public GenericService(IGenericRepository<T> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<Response<TDTO>> AddAsync(TDTO entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<T>(entity);
            await _genericRepository.AddAsync(newEntity);
            await _genericRepository.CommitAsync();
            var newDto = ObjectMapper.Mapper.Map<TDTO>(newEntity);
            return Response<TDTO>.Success(newDto, 200);
        }

        public async Task<Response<IEnumerable<TDTO>>> GetAllAsync()
        {
            var products = ObjectMapper.Mapper.Map<List<TDTO>>(await _genericRepository.GetAll().ToListAsync());
            await _genericRepository.CommitAsync();
            return Response<IEnumerable<TDTO>>.Success(products, 200);
        }

        public async Task<Response<TDTO>> GetByIdAsync(int id)
        {
            var product = await _genericRepository.GetByIdAsync(id);
            await _genericRepository.CommitAsync();
            if (product == null)
            {
                return Response<TDTO>.Fail("Id Not Found", 404, true);
            }
            return Response<TDTO>.Success(ObjectMapper.Mapper.Map<TDTO>(product), 200);
        }

        public async Task<Response<NoDataDTO>> Remove(int id)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(id);
            if (isExistEntity == null)
            {
                return Response<NoDataDTO>.Fail("Id Not Found", 404, true);

            }
            _genericRepository.Delete(isExistEntity);
            await _genericRepository.CommitAsync();
            return Response<NoDataDTO>.Success(204);
        }

        public async Task<Response<NoDataDTO>> Update(TDTO entity, int id)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(id);
            if (isExistEntity == null)
            {
                return Response<NoDataDTO>.Fail("Id Not Found", 404, true);

            }
            var updateEntity = ObjectMapper.Mapper.Map<T>(entity);
            _genericRepository.Update(updateEntity);
            await _genericRepository.CommitAsync();
            return Response<NoDataDTO>.Success(204);
        }
        public async Task<Response<IEnumerable<TDTO>>> Where(Expression<Func<T, bool>> predicate)
        {
            var list =await _genericRepository.GetWhere(predicate).ToListAsync();
            await _genericRepository.CommitAsync();
            return Response<IEnumerable<TDTO>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDTO>>(list), 200);
        }
    }
   }
