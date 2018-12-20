

namespace GrandmothersDishes.Services.GrandmothersDishes.ViewModels.Foods
{
    public class DetailsDishViewModel 
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Calories { get; set; }

        public string DishType { get; set; }

        public string ImageUrl { get; set; }
        
        public decimal Price { get; set; }
        
    }
}
