using Application.Dto;

namespace Application.Services
{
    public interface IService<TDto> where TDto : DtoBase
    {
        Task<TDto?> GetByIdAsync(string id);
        Task<ListDtoResponse<TDto>> ListAsync(int offset, int limit);
        Task AddAsync(TDto dto);
        Task UpdateAsync(TDto dto);
        Task DeleteByIdAsync(string id);
    }
}
