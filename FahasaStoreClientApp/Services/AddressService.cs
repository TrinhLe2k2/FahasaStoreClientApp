using FahasaStoreClientApp.Interfaces;
using FahasaStoreClientApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FahasaStoreClientApp.Services
{
    public class AddressService : IAddressService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AddressService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IEnumerable<District>> GetListDistrictByProvinceId(int provinceId)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync("https://partner.viettelpost.vn/v2/categories/listDistrict?provinceId=" + provinceId);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                // Chuyển đổi JSON thành đối tượng dynamic
                var jsonResponse = JsonConvert.DeserializeObject<dynamic>(content);

                // Kiểm tra cấu trúc của đối tượng JSON trả về
                if (jsonResponse?.error == false && jsonResponse?.status == 200)
                {
                    var data = jsonResponse?.data as JArray;
                    if (data != null)
                    {
                        var provinces = JsonConvert.DeserializeObject<List<District>>(jsonResponse?.data.ToString());

                        return provinces;
                    }
                }

                return new List<District>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error calling the API: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Province>> GetListProvince()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync("https://partner.viettelpost.vn/v2/categories/listProvince");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                // Chuyển đổi JSON thành đối tượng dynamic
                var jsonResponse = JsonConvert.DeserializeObject<dynamic>(content);

                // Kiểm tra cấu trúc của đối tượng JSON trả về
                if (jsonResponse?.error == false && jsonResponse?.status == 200)
                {
                    var data = jsonResponse?.data as JArray;
                    if (data != null)
                    {
                        // Chuyển đổi mảng JSON thành danh sách các đối tượng Province
                        //var provinces = data.Select(item => new Province
                        //{
                        //    PROVINCE_ID = (int?)item["PROVINCE_ID"],
                        //    PROVINCE_CODE = (string?)item["PROVINCE_CODE"],
                        //    PROVINCE_NAME = (string?)item["PROVINCE_NAME"]
                        //});

                        var provinces = JsonConvert.DeserializeObject<List<Province>>(jsonResponse?.data.ToString());

                        return provinces;
                    }
                }

                return new List<Province>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error calling the API: {ex.Message}");
            }
        }


        public async Task<IEnumerable<Ward>> GetListWardByDistrictId(int districtId)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync("https://partner.viettelpost.vn/v2/categories/listWards?districtId=" + districtId);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                // Chuyển đổi JSON thành đối tượng dynamic
                var jsonResponse = JsonConvert.DeserializeObject<dynamic>(content);

                // Kiểm tra cấu trúc của đối tượng JSON trả về
                if (jsonResponse?.error == false && jsonResponse?.status == 200)
                {
                    var data = jsonResponse?.data as JArray;
                    if (data != null)
                    {
                        var provinces = JsonConvert.DeserializeObject<List<Ward>>(jsonResponse?.data.ToString());

                        return provinces;
                    }
                }

                return new List<Ward>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error calling the API: {ex.Message}");
            }
        }
    }
}
