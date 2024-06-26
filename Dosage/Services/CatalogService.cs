using Blazored.SessionStorage;
using Dosage.Data;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Radzen;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using System_EMS_1._0.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dosage.Services
{
    public interface ICatalogService
    {
        Task<ResponseApi> GetCatalog(string type);
        Task<ResponseApi> PostCatalog(string type, string display);
        Task<ResponseApi> DeleteCatalog(string type, int id);
        Task<ResponseApi> PutCatalog(string type, int id,string display);
        Task<ResponseApi?> PostSubtance(SubstanceModel sub); 
        Task<ResponseApi?> PutSubtance(long id , string name ,string rou_name, string unit_name, string content);
        Task<ResponseApi?> PostDosage(DosageLimit dosage);
        Task<ResponseApi?> GetDetailsDosage(long Id);
        Task<ResponseApi?> PutDosage(long Id,DosageLimit dosage);
        Task<ResponseApi?> GetSynchronized(string token);
        Task<ResponseApi?> DeleteCacheSynchronized(string type);
        Task<ResponseApi> Login(string username, string password);
        Task<ResponseApi> PostAllergy(AllergyModel allergy);
        Task<ResponseApi> PutAllergy(long id, AllergyModel allergy);

    }
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorageService;
        private readonly IApi callapi;
        private readonly ApiSettingsLogin apiSettingsLogin;

        public CatalogService(
        HttpClient http
        ,ISessionStorageService sessionStorageService
        ,IApi api
        ,IOptions<ApiSettingsLogin> apiSettingsOptions
        )
        {
            _httpClient = http;
            _sessionStorageService = sessionStorageService;
            
            callapi = api;
            apiSettingsLogin = apiSettingsOptions.Value;

        }

        public async Task<ResponseApi> PostCatalog(string type, string display)
        {
    

            var request = new
            {
                data = new
                {
                    type = type,
                    value = new
                    {
                        display = display
                    }
                },

            };
            ResponseApi result = await callapi!.Post("catalog/add", request);
            return result;


        }
        public async Task<ResponseApi> GetCatalog(string type)
        {

            var para = new
            {
                type = type
            };
            var jsonContent = JsonConvert.SerializeObject(para);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonContent);
            string base64EncodedString = Convert.ToBase64String(jsonBytes);

            ResponseApi result = await callapi!.Get($"catalog/{base64EncodedString}", null!);
            return result;
        }
        public async Task<ResponseApi> DeleteCatalog(string type, int id)
        {
       
            var para = new
            {
                type = type,
                id = id
            };
            var jsonContent = JsonConvert.SerializeObject(para);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonContent);
            string base64EncodedString = Convert.ToBase64String(jsonBytes);

            ResponseApi result = await callapi!.Delete($"catalog/delete/{base64EncodedString}", null!);
            return result;
        }
        public async Task<ResponseApi> PutCatalog(string type, int id, string display)
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
            ResponseApi result = await callapi!.Put("catalog/update", request);
            return result;
        }
        public async Task<ResponseApi?> PostSubtance(SubstanceModel sub)
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

            ResponseApi result = await callapi!.Post("medical/substance/add", request);
            return result;

        }
        public async Task<ResponseApi?> PutSubtance(long id, string name, string unit_name , string rou_name, string contentsub)
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

            ResponseApi result = await callapi!.Put("medical/substance/update", request);
            return result;

        }
        public async Task<ResponseApi?> PostDosage(DosageLimit dosage)
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

            ResponseApi result = await callapi!.Post("dosage/limit/add", request);
            return result;

        }
        public async Task<ResponseApi?> GetDetailsDosage(long Id)
        {
           
            var para = new
            {
                        id = Id
            };
            var jsonContent = JsonConvert.SerializeObject(para);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonContent);
            string base64EncodedString = Convert.ToBase64String(jsonBytes);

            ResponseApi result = await callapi!.Get($"dosage/limit/info/{base64EncodedString}", null!);
            return result;
        }
        public async Task<ResponseApi?> PutDosage(long Id, DosageLimit dosage)
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

            ResponseApi result = await callapi!.Put("dosage/limit/update", request);
            return result;
        }
        public async Task<ResponseApi?> GetSynchronized(string token)
        {
       
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("token", token);
            ResponseApi result = await callapi!.Get("dosage/substance/synchronized", header);
            return result;
        }
        public async Task<ResponseApi?> DeleteCacheSynchronized(string type)
        {

            var para = new
            {
                type = type
            };
            var jsonContent = JsonConvert.SerializeObject(para);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonContent);
            string base64EncodedString = Convert.ToBase64String(jsonBytes);

            ResponseApi result = await callapi!.Delete($"cache/clear/{base64EncodedString}", null!);
            return result;
        }
        public async Task<ResponseApi> Login(string username, string password)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{apiSettingsLogin.BaseUrlLogin}:{apiSettingsLogin.PortUrlLogin}/api/user/login");
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

        public async Task<ResponseApi> PostAllergy(AllergyModel allergy)
        {
          
            var request = new
            {
                data = new
                {
                    sub_name = allergy.Sub_name,
                    unit = allergy.Unit,
                    route = allergy.Route,
                    content = allergy.Content,
                    allergy_date = allergy.Allergy_date,
                    symptoms = allergy.Symptoms,
                    severity_level = allergy.Severity_level,
                    treatment_measures = allergy.Treatment_measures,
                    alternative_drugs = allergy.Alternative_drugs
                },

            };

            ResponseApi result = await callapi!.Post("dosage/allergy/add", request);
            return result;
        }
        public async Task<ResponseApi> PutAllergy(long id, AllergyModel allergy)
        {

            var request = new
            {
                data = new
                {
                    substance_id = id,
                    allergy_date = allergy.Allergy_date,
                    symptoms = allergy.Symptoms,
                    severity_level = allergy.Severity_level,
                    treatment_measures = allergy.Treatment_measures,
                    alternative_drugs = allergy.Alternative_drugs
                },

            };

            ResponseApi result = await callapi!.Put("dosage/allergy/update", request);
            return result;
        }


      
    }
}
