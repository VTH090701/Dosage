using Blazored.SessionStorage;
using Dosage.Data;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dosage.Services
{
    public interface ICatalogService
    {
        Task<ResponseApi> GetCatalog(string type);
        Task<ResponseApi> PostCatalog(string type, string display);
        Task<ResponseApi> DeleteCatalog(string type, int id);
        Task<ResponseApi> UpdateCatalog(string type, int id,string display);
        Task<ResponseApi?> PostSubtance(SubstanceModel sub); 
        Task<ResponseApi?> UpdateSubtance(long id , string name ,string rou_name, string unit_name, string content);
        Task<ResponseApi?> PostDosage(DosageLimit dosage);
        Task<ResponseApi?> GetDetailsDosage(long Id);
        Task<ResponseApi?> UpdateDosage(long Id,DosageLimit dosage);
        Task<ResponseApi?> GetSynchronized(string token);
        Task<ResponseApi?> DeleteCacheSynchronized(string type);
        Task<ResponseApi> Login(string username, string password);

    }
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorageService;

        public CatalogService(HttpClient http
        , ISessionStorageService sessionStorageService
            )
        {
            _httpClient = http;
            _sessionStorageService = sessionStorageService;

        }

        public async Task<ResponseApi> PostCatalog(string type, string display)
        {
            try
            {
                var request = new
                {
                    data = new
                    {
                        type = type,
                        value = new {     
                            display = display
                        }
                    },
                    
                };
                var jsonContent = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("http://192.168.1.252:6868/api/catalog/add", content);
                response.EnsureSuccessStatusCode();
                ResponseApi? result = await response.Content.ReadFromJsonAsync<ResponseApi>();
                return result!;
            }
            catch (Exception ex)
            {
                ResponseApi response = new ResponseApi()
                {
                    Message = ex.Message,
                };
                return response;
            }
        }
        public async Task<ResponseApi> GetCatalog(string type)
        {
            try
            {
                var para = new
                {
                    type = type
                };
                var jsonContent = JsonConvert.SerializeObject(para);
                byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonContent);
                string base64EncodedString = Convert.ToBase64String(jsonBytes);

                var request = new HttpRequestMessage(HttpMethod.Get, $"http://192.168.1.252:6868/api/catalog/{base64EncodedString}");
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                ResponseApi? result = await response.Content.ReadFromJsonAsync<ResponseApi>();
                return result!;
            }
            catch (Exception ex)
            {
                ResponseApi response = new ResponseApi()
                {
                    Message = ex.Message,
                };
                return response;

            }
        }
        public async Task<ResponseApi> DeleteCatalog(string type, int id)
        {
            try
            {
                var para = new
                {
                    type = type,
                    id = id
                };
                var jsonContent = JsonConvert.SerializeObject(para);
                byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonContent);
                string base64EncodedString = Convert.ToBase64String(jsonBytes);

                var request = new HttpRequestMessage(HttpMethod.Delete, $"http://192.168.1.252:6868/api/catalog/delete/{base64EncodedString}");
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                ResponseApi? result = await response.Content.ReadFromJsonAsync<ResponseApi>();
                return result!;
            }
            catch (Exception ex)
            {
                ResponseApi response = new ResponseApi()
                {
                    Message = ex.Message,
                };
                return response;

            }   
        }
        public async Task<ResponseApi> UpdateCatalog(string type, int id, string display)
        {
            try
            {
                var request = new
                {
                    data = new
                    {
                        type = type,
                        value = new
                        {
                            id = id,
                            display = display
                        }
                    },

                };
                var jsonContent = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync("http://192.168.1.252:6868/api/catalog/update", content);
                response.EnsureSuccessStatusCode();
                ResponseApi? result = await response.Content.ReadFromJsonAsync<ResponseApi>();
                return result!;
            }
            catch (Exception ex)
            {
                ResponseApi response = new ResponseApi()
                {
                    Message = ex.Message,
                };
                return response;
            }
        }
        public async Task<ResponseApi?> PostSubtance(SubstanceModel sub)
        {
            try
            {
                var request = new
                {
                    data = new
                    {
                        sub_name = sub.Sub_name,
                        unit = sub.Unit_name,
                        route = sub.Route_name,
                        content = sub.Content

                    },

                };
                var jsonContent = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("http://192.168.1.252:6868/api/medical/substance/add", content);
                response.EnsureSuccessStatusCode();
                ResponseApi? result = await response.Content.ReadFromJsonAsync<ResponseApi>();
                return result!;
            }
            catch (Exception ex)
            {
                ResponseApi response = new ResponseApi()
                {
                    Message = ex.Message,
                };
                return response;
            }
        }
        public async Task<ResponseApi?> UpdateSubtance(long id, string name, string unit_name , string rou_name, string contentsub)
        {
            try
            {
                var request = new
                {
                    data = new
                    {
                        id = id,
                        sub_name = name,
                        unit = unit_name,
                        route = rou_name,
                        content = contentsub
                    },

                };
                var jsonContent = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync("http://192.168.1.252:6868/api/medical/substance/update", content);
                response.EnsureSuccessStatusCode();
                ResponseApi? result = await response.Content.ReadFromJsonAsync<ResponseApi>();
                return result!;
            }
            catch (Exception ex)
            {
                ResponseApi response = new ResponseApi()
                {
                    Message = ex.Message,
                };
                return response;
            }
        }
        public async Task<ResponseApi?> PostDosage(DosageLimit dosage)
        {
            try
            {
                var request = new
                {
                    data = new
                    {
                        sub_name = dosage.Substance,
                        route = dosage.Route,
                        content = dosage.Content,
                        max_dosage = dosage.Max_dosage,
                        duration = dosage.Duration,
                        time_duration = dosage.Time_duration,
                        unit = dosage.Unit

                    },

                };
                var jsonContent = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("http://192.168.1.252:6868/api/dosage/limit/add", content);
                response.EnsureSuccessStatusCode();
                ResponseApi? result = await response.Content.ReadFromJsonAsync<ResponseApi>();
                return result!;
            }
            catch (Exception ex)
            {
                ResponseApi response = new ResponseApi()
                {
                    Message = ex.Message,
                };
                return response;
            }
        }
        public async Task<ResponseApi?> GetDetailsDosage(long Id)
        {
            try
            {
                var para = new
                {
                    id = Id
                };
                var jsonContent = JsonConvert.SerializeObject(para);
                byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonContent);
                string base64EncodedString = Convert.ToBase64String(jsonBytes);

                var request = new HttpRequestMessage(HttpMethod.Get, $"http://192.168.1.252:6868/api/dosage/limit/info/{base64EncodedString}");
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                ResponseApi? result = await response.Content.ReadFromJsonAsync<ResponseApi>();
                return result!;
            }
            catch (Exception ex)
            {
                ResponseApi response = new ResponseApi()
                {
                    Message = ex.Message,
                };
                return response;

            }
        }
        public async Task<ResponseApi?> UpdateDosage(long Id, DosageLimit dosage)
        {
            try
            {
                var request = new
                {
                    data = new
                    {
                        substance_id = Id,
                        max_dosage = dosage.Max_dosage,
                        duration = dosage.Duration,
                        time_duration = dosage.Time_duration,

                    },
                };
                var jsonContent = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync("http://192.168.1.252:6868/api/dosage/limit/update", content);
                response.EnsureSuccessStatusCode();
                ResponseApi? result = await response.Content.ReadFromJsonAsync<ResponseApi>();
                return result!;
            }
            catch (Exception ex)
            {
                ResponseApi response = new ResponseApi()
                {
                    Message = ex.Message,
                };
                return response;
            }
        }
        public async Task<ResponseApi?> GetSynchronized(string token)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "http://192.168.1.252:6868/api/dosage/substance/synchronized");
                request.Headers.Add("token", token);
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                ResponseApi? result = await response.Content.ReadFromJsonAsync<ResponseApi>();
                return result!;
            }
            catch (Exception ex)
            {
                ResponseApi response = new ResponseApi()
                {
                    Message = ex.Message,
                };
                return response;

            }
        }
        public async Task<ResponseApi?> DeleteCacheSynchronized(string type)
        {
            try
            {
                var para = new
                {
                    type = type
                };
                var jsonContent = JsonConvert.SerializeObject(para);
                byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonContent);
                string base64EncodedString = Convert.ToBase64String(jsonBytes);

                var request = new HttpRequestMessage(HttpMethod.Delete, $"http://192.168.1.252:6868/api/cache/clear/{base64EncodedString}");
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                ResponseApi? result = await response.Content.ReadFromJsonAsync<ResponseApi>();
                return result!;
            }
            catch (Exception ex)
            {
                ResponseApi response = new ResponseApi()
                {
                    Message = ex.Message,
                };
                return response;

            }
        }
        public async Task<ResponseApi> Login(string username, string password)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "http://14.241.182.251:59325/api/user/login");
                Dictionary<string, string> headers = new Dictionary<string, string>
                {
                    { "rd_key" ,"i3uJU1ms+3Sj+2sUtQN2GyqT8gvpHwTLcpnCRDAybQclD8XVQiowmglOtz+PKuY1/Bj6yBgnJzozFhcOLgM6DoKd3rmcZXfJN1QQ+/AoA0/BcElT++FDpmoK1UyirKooNUb33+u1NjhpOIY/o1XXHfU12MK8SW5Y+wR8xbLMvksjgx35bqkDBSeHc1aC2uWDvyu8S7NoGXOOhvrqzdl1XAbWBppXCToPfg/54Lff2XLovSxvOKHzwWjDu4Z9KP4W5VbdDl9WZi7g7Df+5jw7zmnkHqB5oyWlBxTF0HK0nilRn5FN5k1s0LfMIJstuS8Lk5s64ORyLD10hqfD0M6POQ==" },
                    { "user" ,    username },
                    { "password" , password }
                };

                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }

                var response = await _httpClient.SendAsync(request);
                var result = await response.Content.ReadFromJsonAsync<ResponseApi>();
                var responLogin = JsonConvert.DeserializeObject<ResponLogin>(result?.Value.ToString());
                if (result?.Code.ToString() == "200")
                {
                    var data = new
                    {
                        Display = responLogin?.Display,
                        Code = responLogin?.Code,
                        Token = responLogin?.Token
                    };
                    await _sessionStorageService.SetItemAsync("us", data);
                }
                return result!;

            }
            catch (Exception ex)
            {
                ResponseApi response = new ResponseApi()
                {
                    Message = ex.Message,
                };
                return response;

            }
        }

    }
}
