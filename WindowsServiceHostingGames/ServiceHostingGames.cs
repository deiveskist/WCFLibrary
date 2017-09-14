using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibraryGame;
using System.IO;

namespace WindowsServiceHostingGames
{
    public partial class ServiceHostingGames : ServiceBase
    {
        ServiceHost host;

        public ServiceHostingGames()
        {
            InitializeComponent();
           
        }

        protected override void OnStart(string[] args)
        {
            host = new ServiceHost(typeof(ServiceGames));
            Uri urlService = new Uri("http://localhost:8081/GameService");
            host.AddServiceEndpoint(typeof(IServiceGames), new BasicHttpBinding(), urlService);

            try
            {
                host.Open();
                
            }
            catch (Exception ex)
            {
                LogFile(ex.Message);

                host.Abort();                
            }

        }

        protected override void OnStop()
        {
            if (host != null)
                host.Close();
        }

        private void LogFile(String Message)
        {
            try
            {
                String sDirectoryLog = AppDomain.CurrentDomain.BaseDirectory + @"\Log\";
                String sFileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

                String sLogFileMessage = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + Message + "\n";
                
                if (!Directory.Exists(sDirectoryLog))
                    Directory.CreateDirectory(sDirectoryLog);


                File.AppendAllText(sDirectoryLog + @"\" + sFileName, sLogFileMessage);
            }
            catch
            {
            }
        }
    }
}
