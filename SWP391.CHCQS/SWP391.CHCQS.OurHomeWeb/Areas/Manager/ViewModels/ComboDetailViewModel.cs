using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.Model;
using Task = SWP391.CHCQS.Model.Task;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels
{
    public class ComboDetailViewModel
    {
        public Combo Combo { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> ConstructionTypeList { get; set; }

        public List<ComboMaterials> ComboMaterials { get; set; }
        public List<ComboTasks> ComboTasks { get; set; }

	}
}
