using System;
using System.ComponentModel;
using System.Configuration.Install;

namespace HeartBeat.Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}
