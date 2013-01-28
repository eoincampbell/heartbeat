using System;
using System.ServiceProcess;
using HeartBeat.Logic;
using HeartBeat.Logic.Helpers;

namespace HeartBeat.Service
{
    public partial class Main : ServiceBase
    {
        private readonly Engine _engine;

        public Main()
        {
            InitializeComponent();

            _engine = new Engine();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                LogHelper.Information("HeartBeat Service Starting...");
                _engine.Start();
                LogHelper.Information("HeartBeat Service Started...");
            }
            catch (Exception e)
            {
                LogHelper.Information("HeartBeat Service Could Not Be Started...", e);
                Stop();
            }
        }

        protected override void OnStop()
        {
            try
            {
                LogHelper.Information("HeartBeat Service Stopping...");
                _engine.Stop();
                LogHelper.Information("HeartBeat Service Stopped...");
            }
            catch (Exception e)
            {
                LogHelper.Information("HeartBeat Service could not be stopped...", e);
            }    
        }

        public void InteractiveStart(string[] args)
        {
            OnStart(args);
        }

        public void InteractiveStop()
        {
            OnStop();
        }
    }
}
