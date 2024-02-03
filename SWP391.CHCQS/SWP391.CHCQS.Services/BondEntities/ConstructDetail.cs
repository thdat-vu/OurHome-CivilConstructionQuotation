namespace SWP391.CHCQS.Services.BondEntities
{
    public class ConstructDetail
    {
        public string Id { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }

        public int Facade { get; set; }

        public string Alley { get; set; }
        public int Floor   { get; set; }
        public int Room { get; set; }
        public double Mezzanine { get; set; }
        public double RooftopFloor { get; set; }
        public bool Balcony { get; set; }
        public double Garden { get; set; }

        //investment type
        public string? InvestTypeId { get; set; }
        public InvestmentType? InvestType { get; set; }

        //foundation type
        public string? FoundationTypeId { get; set; }
        public FoundationType? FoundationType { get; set; }
        //Rooftop type
        public string? RooftopTypeId { get; set; }
        public RooftopType? RooftopType { get; set; }

        //BasementType
        public string? BasementTypeId { get; set; }
        public BasementType? BasementType { get; set; }
        //Construction Type
        public string? ConstrucTypeId { get; set; }
        public ConstructionType? ConstrucType { get; set; }
    }
}