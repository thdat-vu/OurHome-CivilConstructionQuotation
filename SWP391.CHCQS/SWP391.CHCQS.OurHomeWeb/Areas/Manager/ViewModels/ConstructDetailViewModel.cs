namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels
{
	public class ConstructDetailViewModel
	{
		public string? TypeOfConstruct { get; set; }
		public string? Investment { get; set; }
		public string? Foundation { get; set; }
		public string? Basement { get; set; }
		public string? Roof { get; set; }
		public string? BalconyStatus
		{
			get => IsBalcony? "Yes": "No";
		}
		private bool _isBalcony;

		public bool IsBalcony
		{
			get { return _isBalcony; }
			set { _isBalcony = value; }
		}


		public decimal Width { get; set; }
		public decimal Length { get; set; }
		public int Facade { get; set; }
		public string Alley { get; set; } = null!;
		public int Floor { get; set; }
		public decimal Mezzanine { get; set; }
		public decimal RooftopFloor { get; set; }
		public decimal Garden { get; set; }



	}
}
