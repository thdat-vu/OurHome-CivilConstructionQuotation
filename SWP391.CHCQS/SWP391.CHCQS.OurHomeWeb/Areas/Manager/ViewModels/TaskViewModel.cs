namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels
{
    public partial class TaskViewModel
    {
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
