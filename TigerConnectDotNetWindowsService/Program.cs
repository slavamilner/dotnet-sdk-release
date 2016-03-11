using System.ServiceProcess;

namespace TigerConnectDotNetWindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MessageReceiver()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
