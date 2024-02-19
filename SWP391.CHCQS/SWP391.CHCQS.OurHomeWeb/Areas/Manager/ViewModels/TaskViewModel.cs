namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels
{
    public partial class TaskViewModel
    {
        //public string Id { get; set; } = null!;
        //public string Name { get; set; } = null!;
        //public string? Description { get; set; }
        //public decimal UnitPrice { get; set; }
        //public bool Status { get; set; }
        //public string CategoryId { get; set; } = null!;
        //public string CategoryName { get; set; }
        public TaskViewModel(string id, string name, string? description, decimal unitPrice, bool status, string categoryId, string categoryName)
        {
            Id = id;
            Name = name;
            Description = description;
            UnitPrice = unitPrice;
            Status = status;
            CategoryId = categoryId;
            CategoryName = categoryName;
        }
        public TaskViewModel()
        {
                
        }
    }
}
