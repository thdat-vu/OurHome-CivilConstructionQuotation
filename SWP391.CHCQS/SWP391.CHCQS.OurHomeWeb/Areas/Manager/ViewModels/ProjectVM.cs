using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.Model;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels
{
	public class ProjectVM
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Location { get; set; }
		public string Scale { get; set; }
		public string Size { get; set; }
		public bool Status { get; set; }
		public string? Description { get; set; }
		public string? Overview { get; set; }
		public DateTime Date { get; set; }
		[Display(Name = "Customer Id")]
		public string? CustomerId { get; set; }
        [Display(Name = "Customer Name")]
        public string? CustomerName { get; set; }

	}
}
