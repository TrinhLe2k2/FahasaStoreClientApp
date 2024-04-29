namespace FahasaStoreClientApp.Models.ViewModels
{
    public class BookCoverTypeVM
    {
        public BookCoverTypeVM(int coverTypeId, string typeName)
        {
            CoverTypeId = coverTypeId;
            TypeName = typeName;
        }

        public int CoverTypeId { get; set; }
        public string TypeName { get; set; }
    }
}
