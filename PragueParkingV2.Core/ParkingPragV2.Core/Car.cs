namespace pragueParkingV2.Core.Models
{
    public class Car : Vehicle
    {
        public Car(string licensePlate) : base(licensePlate)
        {
            Size = 4; // Bilstorlek är 4.
        }

        public override decimal CalculateParkingFee()
        {
            var totalTimeParked = (DateTime.Now - ParkingTime).TotalMinutes;
            if (totalTimeParked <= FreeMinutes) return 0;

            var hoursParked = (decimal)Math.Ceiling((totalTimeParked - FreeMinutes) / 60);
            return hoursParked * 20M; 
        }
    }
}
