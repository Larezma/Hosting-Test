namespace BackendApi.Contract.Products
{
    public class GetProductsResponse
    {
        public int ProductId { get; set; }

        public string Product1 { get; set; } = null!;

        public decimal Calories { get; set; }

        public decimal ProteinPer { get; set; }

        public decimal FatPer { get; set; }

        public decimal CarbsPer { get; set; }

        public string VitaminsAndMinerals { get; set; } = null!;

        public string NutritionalValue { get; set; } = null!;
    }
}