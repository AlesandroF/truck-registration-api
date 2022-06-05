using System;
using Truck.Registration.Domain.Enums;

namespace Truck.Registration.Domain.Entities
{
    public class Truck
    {
        public int YearManufacture { get; set; }
        public int ModelYear { get; set; }
        public TruckModelEnum Model { get; set; }
        public bool Active { get; set; }

        public int Id { get; set; } 
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }
    }
}