using Newtonsoft.Json;
using ProjectRM.viewmodels;
using ProjectRM.datamodels;
using System.Text;

namespace ProjectRM.Service
{
    public class VariantService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse respon = new VMResponse();

        public VariantService(IConfiguration _configuration) //cara dapatkn alamat API dari front end JSONSETTING FRONT END
        {
            configuration = _configuration;
            RouteAPI = configuration["RouteAPI"];
        }


        public async Task<List<VMTblVariant>> GetAllData()
        {
            List<VMTblVariant> data = new List<VMTblVariant>();
            string apiResponse = await client.GetStringAsync(RouteAPI + "apiVariant/GetAllData");
            data = JsonConvert.DeserializeObject<List<VMTblVariant>>(apiResponse);
            return data;
        }

        public async Task<VMTblVariant> GetDataById(int id)
        {
            VMTblVariant data = new VMTblVariant();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiVariant/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<VMTblVariant>(apiResponse);
            return data;
        }


        public async Task<List<VMTblVariant>> GetDataByIdCategory(int id)
        {
            List<VMTblVariant> data = new List<VMTblVariant>();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiVariant/GetDataByIdCategory/{id}");
            data = JsonConvert.DeserializeObject<List<VMTblVariant>>(apiResponse);
            return data;
        }


        public async Task<bool> CheckByName(string name, int id, int idCategory)
        {
            string apiRespon = await client.GetStringAsync(RouteAPI + $"apiVariant/CheckByName/{name}/{id}/{idCategory}");
            bool isExist = JsonConvert.DeserializeObject<bool>(apiRespon);
            return isExist;
        }


        public async Task<VMResponse> Create(VMTblVariant dataParam)
        {
            //proses convert dari object ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //proses mengubah string menjadi json lalu kirim ke request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //proses memanggil API dan mengirim Body
            var request = await client.PostAsync(RouteAPI + "apiVariant/Save", content);

            if (request.IsSuccessStatusCode)
            {
                //proses membaca respon
                var apiRespon = await request.Content.ReadAsStringAsync();
                //proses convert hasil respon dari API ke Objeect
                respon = JsonConvert.DeserializeObject<VMResponse>(apiRespon);

            }
            else
            {
                respon.Success = false;
                respon.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
            }
            return respon;
        }




        public async Task<VMResponse> Edit(VMTblVariant dataParam)
        {
            //proses convert dari object ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //proses mengubah string menjadi json lalu kirim ke request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //proses memanggil API dan mengirim Body
            var request = await client.PutAsync(RouteAPI + "apiVariant/Edit", content);

            if (request.IsSuccessStatusCode)
            {
                //proses membaca respon
                var apiRespon = await request.Content.ReadAsStringAsync();
                //proses convert hasil respon dari API ke Objeect
                respon = JsonConvert.DeserializeObject<VMResponse>(apiRespon);

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
            var request = await client.DeleteAsync(RouteAPI + $"apiVariant/Delete/{id}");
            if (request.IsSuccessStatusCode)
            {
                var apiRespon = await request.Content.ReadAsStringAsync();
                respon = JsonConvert.DeserializeObject<VMResponse>(apiRespon);
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

