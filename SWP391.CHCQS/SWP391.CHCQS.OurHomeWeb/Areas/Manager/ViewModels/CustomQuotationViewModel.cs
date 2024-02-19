
namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels
{
    public partial class CustomQuotationViewModel
    {
        //public string Id { get; } = null!;
        //public DateTime Date { get; }
        //public string? Acreage { get; }
        //public string Location { get; } = null!;
        //public bool Status { get; }
        //public string? Description { get; }
        //public decimal Total { get; }
        //public string SellerId { get; } = null!;
        //public string SellerName { get; }
        //public string EngineerId { get; } = null!;
        //public string EngineerName { get; }
        //public string ManagerId { get; } = null!;
        //public string ManagerName { get; }
        //public string RequestId { get; } = null!;
        //public ConstructDetailViewModel ConstructDetail { get; }
        //public ICollection<CustomQuotationTaskViewModel> CustomQuotationTask { get; set; }
        //public ICollection<MaterialDetailViewModel> MaterialDetail { get; set; }
        public CustomQuotationViewModel(string id, DateTime date, string? acreage, string location, bool status, string? description, decimal total, string sellerId, string sellerName, string engineerId, string engineerName, string managerId, string managerName, string requestId, ConstructDetailViewModel constructDetail, ICollection<CustomQuotationTaskViewModel> customQuotationTask, ICollection<MaterialDetailViewModel> materialDetail)
        {
            Id = id;
            Date = date;
            Acreage = acreage;
            Location = location;
            Status = status;
            Description = description;
            Total = total;
            SellerId = sellerId;
            SellerName = sellerName;
            EngineerId = engineerId;
            EngineerName = engineerName;
            ManagerId = managerId;
            ManagerName = managerName;
            RequestId = requestId;
            ConstructDetail = constructDetail;
            CustomQuotationTask = customQuotationTask;
            MaterialDetail = materialDetail;
        }
        public CustomQuotationViewModel()
        {
            
        }
    }
}
