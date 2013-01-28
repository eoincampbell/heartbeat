using System;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using HeartBeat.Logic.Helpers;

namespace HeartBeat.Service
{
    static class Program
    {
        private const string ConsoleStopMessage = "Press any key to stop!";
        private const string ServiceName = "HeartBeat";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            var service = new Main();
    
            if (Debugger.IsAttached)
            {
                RunInteractive(service, args);
            }
            else if (args.Any())
            {
                switch (string.Concat(args).ToLower())
                {
                    case "--console":
                        RunInteractive(service, args);
                        break;
                    case "--install":
                        InstallService();
                        break;
                    case "--uninstall":
                        UninstallService();
                        break;
                    case "--start":
                        StartService();
                        break;
                    case "--stop":
                        StopService();
                        break;
                    default:
                        DisplayHelpMessage();
                        break;
                }
            }
            else
            {
                if (Environment.UserInteractive)
                {
                    DisplayHelpMessage();
                }
                else
                {
                    ServiceBase.Run(service);
                }
            }
        }

        private static void UninstallService()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new[] {"/u", Assembly.GetExecutingAssembly().Location});
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to uninstall service. See logfile for more information.");
                LogHelper.Critical("**** Failed to uninstall service", ex);
            }
        }

        private static void InstallService()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new[] {Assembly.GetExecutingAssembly().Location});
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to install service. See logfile for more information.");
                LogHelper.Critical("**** Failed to install service", ex);
            }
        }

        private static void DisplayHelpMessage()
        {
            const string message = @"
HeartBeat
A Windows Server System Monitoring Tool.
Version: {0} ({1})

Usage
 HeartBeat.exe [options]

Switches
  --install              Install Heartbeat as a windows service
  --start                Start Heartbeat Windows Service
  --stop                 Stop Heartbeat Windows Service
  --uninstall            Uninstall Heartbeat as a windows service

  --console              Run Heartbeat from the command line
  --help                 Display this message
";
            var ass = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(ass.Location);
            var filever = fvi.FileVersion;
            var version = ass.GetName().Version;


            Console.WriteLine(message, filever, version);

        }

        private static void StartService()
        {
            try
            {
                var service = new ServiceController(ServiceName);
                var timeout = TimeSpan.FromMilliseconds(10000);

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to start service. See logfile for more information.");
                LogHelper.Critical("**** Failed to start service", ex);
            }
        }

        private static void StopService()
        {
            try
            {
                var service = new ServiceController(ServiceName);
                var timeout = TimeSpan.FromMilliseconds(10000);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to stop service. See logfile for more information.");
                LogHelper.Critical("**** Failed to stop service", ex);
            }
        }

        private static void RunInteractive(Main service, string[] args)
        {
            service.InteractiveStart(args);
            Console.WriteLine(ConsoleStopMessage);
            Console.Read();
            service.InteractiveStop();
        }
    }
}
