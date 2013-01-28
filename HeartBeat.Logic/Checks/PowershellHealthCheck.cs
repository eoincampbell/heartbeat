using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace HeartBeat.Logic.Checks
{
    public class PowershellHealthCheck : HealthCheck
    {
        /// <summary>
        /// Gets the success after failure formatter.
        /// </summary>
        /// <value>
        /// The success after failure formatter.
        /// </value>
        protected override string SuccessAfterFailureFormatter
        {
            get { return "POWERSHELL: Script '{0}' succeeded. It had been failing for {1}."; }
        }

        /// <summary>
        /// Gets the success formatter.
        /// </summary>
        /// <value>
        /// The success formatter.
        /// </value>
        protected override string SuccessFormatter
        {
            get { return "POWERSHELL: Script '{0}' succeeded."; }
        }

        /// <summary>
        /// Gets the failure formatter.
        /// </summary>
        /// <value>
        /// The failure formatter.
        /// </value>
        protected override string FailureFormatter
        {
            get { return "POWERSHELL: Script '{0}' failed."; }
        }

        /// <summary>
        /// Gets to string formatter.
        /// </summary>
        /// <value>
        /// To string formatter.
        /// </value>
        protected override string ToStringFormatter
        {
            get { return "POWERSHELL: {0}"; }
        }

        /// <summary>
        /// This Health Checks Unique Id Formatter
        /// </summary>
        private const string IDENTIFIER = "psscript_{0}";


        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        private string Path { get; set; }

        /// <summary>
        /// Gets or sets the script parameters.
        /// </summary>
        /// <value>
        /// The script parameters.
        /// </value>
        private string ScriptParameters { get; set; }

        public PowershellHealthCheck(string friendlyName, string path, string scriptParameters)
            : base(string.Format(IDENTIFIER, friendlyName), friendlyName)
        {
            Path = path;
            ScriptParameters = scriptParameters;
        }

        /// <summary>
        /// Performs the check.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the check succeeds otherwise, <c>false</c>.
        /// </returns>
        public override bool PerformCheck()
        {

            var runspaceConfiguration = RunspaceConfiguration.Create();


            using (var runSpace = RunspaceFactory.CreateRunspace(runspaceConfiguration))
            {
                runSpace.Open();
                using (var runSpaceInvoker = new RunspaceInvoke(runSpace))
                {
                    runSpaceInvoker.Invoke("Set-ExecutionPolicy Unrestricted");

                    using (var pipeLine = runSpace.CreatePipeline())
                    {
                        var scriptFileCommand = new Command(Path);

                        if (!string.IsNullOrWhiteSpace(ScriptParameters))
                        {
                            ScriptParameters.Split(' ')
                                            .ToList()
                                            .ForEach(
                                                token =>
                                                scriptFileCommand.Parameters.Add(new CommandParameter(null, token)));
                        }

                        pipeLine.Commands.Add(scriptFileCommand);

                        var results = pipeLine.Invoke();
                        
                        if (results.Count > 0)
                        {
                            try
                            {
                                return (bool) results[0].BaseObject;
                            }
                            catch
                            {
                                return false;
                            }
                        }
                        return false;
                    }
                }
            }
        }
    }
}
