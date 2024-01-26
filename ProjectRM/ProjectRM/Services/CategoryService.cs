using Newtonsoft.Json;
using ProjectRM.datamodels;
using ProjectRM.viewmodels;
using System.Text;

namespace ProjectRM.Services
{
    public class CategoryService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse respon = new VMResponse();

        public CategoryService(IConfiguration _configuration)
        {
            configuration = _configuration;
            RouteAPI = configuration["RouteAPI"];
        }


        public async Task<List<TblCategory>> GetAllData()
        {
            List<TblCategory> data = new List<TblCategory>();

            string apiResponse = await client.GetStringAsync(RouteAPI + "apiCategory/GetAllData");
            data = JsonConvert.DeserializeObject<List<TblCategory>>(apiResponse)!;

            return data;
        }

        public async Task<VMResponse> Create(VMTblCategory dataParam)
        {
            //proses convert dari object ke string
            string json = JsonConvert.SerializeObject(dataParam);

            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var request = await client.PostAsync(RouteAPI + "apiCategory/Save", content);

            if (request.IsSuccessStatusCode)
            {
                var apiRespon = await request.Content.ReadAsStringAsync();

                respon = JsonConvert.DeserializeObject<VMResponse>(apiRespon)!;
            }
            else
            {
                respon.Success = false;
                respon.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
            }

            return respon;
        }

        public async Task<bool> CheckCategoryByName(string name, int id)
        {
            string apiRespon = await client.GetStringAsync(RouteAPI + $"apiCategory/CheckCategoryByName/{name}/{id}");
            bool isExist = JsonConvert.DeserializeObject<bool>(apiRespon);

            return isExist;
        }

        public async Task<TblCategory> GetDataById(int id)
        {
            TblCategory data = new TblCategory();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiCategory/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<TblCategory>(apiResponse)!;
            return data;
        }

        public async Task<VMResponse> Edit(TblCategory dataParam)
        {
            //proses convert dari object ke string
            string json = JsonConvert.SerializeObject(dataParam);

            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var request = await client.PutAsync(RouteAPI + "apiCategory/Edit", content);

            if (request.IsSuccessStatusCode)
            {
                var apiRespon = await request.Content.ReadAsStringAsync();

                respon = JsonConvert.DeserializeObject<VMResponse>(apiRespon)!;
            }
            else
            {
                respon.Success = false;
                respon.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
            }

            return respon;
        }

        public async Task<VMResponse> Delete(int id)
        {
            var request = await client.DeleteAsync(RouteAPI + $"apiCategory/Delete/{id}");

            if (request.IsSuccessStatusCode)
            {
                //proses membaca respon dari api
                var apiRespon = await request.Content.ReadAsStringAsync();

                //proses convert hasil respon dari api ke object
                respon = JsonConvert.DeserializeObject<VMResponse>(apiRespon)!;
            }
            else
            {
                respon.Success = false;
                respon.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
            }

            return respon;
        }
    }
}
