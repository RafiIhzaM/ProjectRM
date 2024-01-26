using Newtonsoft.Json;
using System.Text;
using ProjectRM.viewmodels;

namespace ProjectRM.Service
{
    public class ProductService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse respon = new VMResponse();

        public ProductService(IConfiguration _configuration) //cara dapatkn alamat API dari front end JSONSETTING FRONT END
        {
            configuration = _configuration;
            RouteAPI = configuration["RouteAPI"];
        }

        public async Task<List<VMTblProduct>> GetAllData()
        {
            List<VMTblProduct> data = new List<VMTblProduct>();

            //menjdapatkan api 
            string apiRespon = await client.GetStringAsync(RouteAPI + "apiProduct/GetAllData");
            //mengubah string menjadi object list 
            data = JsonConvert.DeserializeObject<List<VMTblProduct>>(apiRespon);
            return data;
        }

        public async Task<bool> CheckByName(string name, int id, int idVariant)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiProduct/CheckByName/{name}/{id}/{idVariant}");
            bool isExist = JsonConvert.DeserializeObject<bool>(apiResponse);
            return isExist;
        }

        public async Task<VMResponse> Create(VMTblProduct dataParam)
        {
            //proses convert dari object ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //proses mengubah string menjadi json lalu kirim ke request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //proses memanggil API dan mengirim Body
            var request = await client.PostAsync(RouteAPI + "apiProduct/Save", content);

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

        public async Task<VMTblProduct> GetDataById(int id)
        {
            VMTblProduct data = new VMTblProduct();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiProduct/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<VMTblProduct>(apiResponse);
            return data;
        }


        public async Task<VMResponse> Edit(VMTblProduct dataParam)
        {
            //proses convert dari object ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //proses mengubah string menjadi json lalu kirim ke request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //proses memanggil API dan mengirim Body
            var request = await client.PutAsync(RouteAPI + "apiProduct/Edit", content);

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
            var request = await client.DeleteAsync(RouteAPI + $"apiProduct/Delete/{id}");
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

