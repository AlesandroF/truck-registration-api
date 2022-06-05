namespace Truck.Registration.Application.UseCases.Get
{
    public class GetResponse
    {
        public int Id { get; set; } 
        
        public int YearManufacture { get; set; }
        
        public int ModelYear { get; set; }
        
        public string Model { get; set; }
    }
}