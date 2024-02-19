namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels
{
    public partial class ConstructDetailViewModel
    {
        public ConstructDetailViewModel(string quotationId, decimal width, decimal length, int facade, string alley, int floor, int room, decimal mezzanine, decimal rooftopFloor, bool balcony, decimal garden, string constructionTypeName, string investmentTypeName, string foundationTypeName, string rooftopTypeName, string basementTypeName)
        {
            QuotationId = quotationId;
            Width = width;
            Length = length;
            Facade = facade;
            Alley = alley;
            Floor = floor;
            Room = room;
            Mezzanine = mezzanine;
            RooftopFloor = rooftopFloor;
            Balcony = balcony;
            Garden = garden;
            ConstructionTypeName = constructionTypeName;
            InvestmentTypeName = investmentTypeName;
            FoundationTypeName = foundationTypeName;
            RooftopTypeName = rooftopTypeName;
            BasementTypeName = basementTypeName;
        }
        public ConstructDetailViewModel()
        {
            
        }
    }
}
