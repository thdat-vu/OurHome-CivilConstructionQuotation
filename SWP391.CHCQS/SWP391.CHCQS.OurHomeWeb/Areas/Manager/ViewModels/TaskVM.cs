using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.Model;
using Task = SWP391.CHCQS.Model.Task;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels
{
    public class TaskVM
    {
        public Task Task { get; set; }
        public IEnumerable<SelectListItem> TaskCategoryList { get; set; }
    }
}
