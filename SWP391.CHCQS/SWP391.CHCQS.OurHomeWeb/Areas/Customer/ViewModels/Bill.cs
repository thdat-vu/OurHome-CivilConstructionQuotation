namespace SWP391.CHCQS.OurHomeWeb.Areas.Customer.ViewModels
{
	public class Bill
	{
        public List<BillDetail> BillDetails { get; set; }
		public double UnitPrice { get; set; }
		public double TotalPrice { get; set; }
        public double TotalArea { get; set; }
    }
}
