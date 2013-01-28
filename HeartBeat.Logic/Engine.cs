using System.Timers;
using HeartBeat.Logic.Checks;
using HeartBeat.Logic.Configuration;

namespace HeartBeat.Logic
{
    public class Engine
    {
        private readonly Timer _timer;
        private readonly HealthCheckManager _manager;
        
        private static bool _isRunning;
        
        public Engine()
        {
            _timer = new Timer();
            _timer.Elapsed += TimerOnElapsed;

            _manager = new HealthCheckManager();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            if (_isRunning) return;
            
            _isRunning = true;
            try
            {
                _manager.PerformChecks();
            }
            finally
            {
                _isRunning = false;
            }
        }

        public void Start()
        {
            _timer.Interval = HeartBeatSettings.SettingsManager.CheckInterval;
            _timer.Enabled = true;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _timer.Enabled = false;
        }
    }
}
