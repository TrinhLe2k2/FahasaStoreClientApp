using FahasaStoreClientApp.Models;

namespace FahasaStoreClientApp.Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<Province>> GetListProvince();
        Task<IEnumerable<District>> GetListDistrictByProvinceId(int provinceId);
        Task<IEnumerable<Ward>> GetListWardByDistrictId(int districtId);
    }
}
