namespace BackendApi.Contract.Nutrition
{
    public class CreateNutritionRequest
    {
        public int Product { get; set; }

        public string MeanType { get; set; } = null!;

        public string MeanDeacription { get; set; } = null!;

        public DateTime DateNutrition { get; set; }
    }
}