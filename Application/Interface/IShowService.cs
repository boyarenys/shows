using Domain.Entities;
using Newtonsoft.Json.Linq;

namespace Application.Interface
{
    public interface IShowService
    {
        Task<JArray> GetAllAsync();
        Task<JObject> GetByIdAsync(int id);
        //Task<Network> GetNetworkByIdAsync(int id);
        Task ProcessShowsAsync(IEnumerable<Show> showsDto);
    }
}
