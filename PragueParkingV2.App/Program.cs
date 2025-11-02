using PragueParkingV2.Core.Services;
using System.Text;

namespace pragueParkingV2.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;


            // Ladda konfigurationen från fil.
            var configManager = new ConfigurationManager();
            var config = configManager.LoadConfig();

            var garage = new ParkingGarage(100, config, configManager);
            garage.LoadParkedVehicles();


            // Skapa DisplayManager och starta huvudmenyn
            var display = new DisplayManager(garage);

            AppDomain.CurrentDomain.ProcessExit += (s, e) => garage.SaveParkedVehicles();

            display.ShowMainMenu();
        }
    }
}
