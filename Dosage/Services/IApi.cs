using Dosage.Data;

namespace Dosage.Services
{
    public interface IApi
    {
        Task<ResponseApi> Post(string api, object value);
        Task<ResponseApi> Put(string api, object value);
        Task<ResponseApi> Delete(string api, object value);
        Task<ResponseApi> Get(string api, Dictionary<string, string> header);

    }
}
