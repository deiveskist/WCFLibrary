using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using WcfServiceLibraryGame;

namespace ConsoleHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(ServiceGames));
            Uri urlService = new Uri("http://localhost:8081/GameService");
            host.AddServiceEndpoint(typeof(IServiceGames), new BasicHttpBinding(), urlService);
                      
            try
            {
                host.Open();
                Console.ReadLine();
                host.Close();
            }
            catch(Exception ex)
            {
                host.Abort();
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
       
    }
}
