using Lab4.Model;

namespace lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IBusinessLogic businessLogic = new BusinessLogic();
            //businessLogic.DeleteAirport("TEST");
            foreach (Airport airport in businessLogic.GetAirports())
            {
                Console.WriteLine(airport.Id+", "+airport.City+", "+airport.DateVisted+", "+ airport.Rating);
            }
            Console.WriteLine("Hello, World!");
        }
    }
}