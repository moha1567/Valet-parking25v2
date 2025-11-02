using Newtonsoft.Json;
using System.Linq;

namespace pragueParkingV2.Core.Models
{
    // Enum för olika storlekar på parkeringsplatser
    public enum ParkingSpotSize
    {
        Small,
        Medium,
        Large
    }

    public class ParkingSpot
    {
        [JsonProperty("FirstParkedVehicle")]
        public Vehicle ParkedVehicle => ParkedVehicles.FirstOrDefault();  // Första parkerade fordonet.

        [JsonProperty("ParkedVehiclesList")] // Namn för listan av parkerade fordon
        public List<Vehicle> ParkedVehicles { get; } = new List<Vehicle>();  // Lista över parkerade fordon

        public int CurrentSizeOccupied { get; private set; } = 0;  // Total storlek upptagen på platsen
        public int MaxSize { get; set; } = 4;  // Max storlek 4.

        public int SpotId { get; }

        public ParkingSpot(int spotId)
        {
            SpotId = spotId;
        }


        public bool Park(Vehicle vehicle)
        {
            // Kontrollera om det är en bil eller motorcykel
            if (vehicle is Car)
            {
                // Om det redan finns en bil på platsen, kan ingen ny bil parkera här
                if (ParkedVehicles.OfType<Car>().Any())
                {
                    Console.WriteLine($"The spot {SpotId} is already occupied.");
                    return false;  // Det finns redan en bil, så vi kan inte parkera en annan bil
                }
            }
            else if (vehicle is Motorcycle)
            {
                // Om det redan finns två motorcyklar på platsen, kan ingen mer motorcykel parkera här
                if (ParkedVehicles.OfType<Motorcycle>().Count() >= 2)
                {

                    return false;
                }
            }


            if (CurrentSizeOccupied + vehicle.Size <= MaxSize)
            {
                ParkedVehicles.Add(vehicle);  // Lägg till fordonet i listan
                CurrentSizeOccupied += vehicle.Size;  // Uppdatera upptagen storlek
                return true;
            }
            else
            {

                return false;
            }
        }


        public void RemoveVehicle(Vehicle vehicle)
        {
            if (ParkedVehicles.Remove(vehicle))
            {
                CurrentSizeOccupied -= vehicle.Size;  // Uppdatera när ett fordon tas bort
            }
        }

        public bool IsOccupied => ParkedVehicles.Count > 0;  // Kolla om platsen är upptagen
    }



}
