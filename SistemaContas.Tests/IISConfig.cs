using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;

namespace SistemaContas.Initializer
{
    public class IISConfig
    {

        const int iisPort = 10941;
        private string _applicationName;
        private Process _iisProcess;

        public IISConfig(string applicationName)
        {
            _applicationName = applicationName;
        }


        public void finalizarIIS()
        {
            Process[] processo = Process.GetProcessesByName("iisexpress.exe");
            if (processo.Length > 0)
            {
                processo[0].Kill();
            }
            else if (_iisProcess != null && _iisProcess.HasExited == false)
            {
                _iisProcess.Kill();
            }
        }



        public void StartIIS()
        {
            finalizarIIS();
            var applicationPath = GetApplicationPath(_applicationName);
            string programFiles = "";
            if (IntPtr.Size == 4)
                programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            else
                programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);


            _iisProcess = new Process();
            _iisProcess.StartInfo.FileName = programFiles + @"\IIS Express\iisexpress.exe";
            _iisProcess.StartInfo.Arguments = string.Format("/path:\"{0}\" /port:{1}", applicationPath, iisPort);
            _iisProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            _iisProcess.Start();
        }


        protected virtual string GetApplicationPath(string applicationName)
        {
            var solutionFolder = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory))));
            return Path.Combine(solutionFolder, applicationName);
        }


        public string GetAbsoluteUrl(string relativeUrl)
        {
            if (!relativeUrl.StartsWith("/"))
            {
                relativeUrl = "/" + relativeUrl;
            }
            return String.Format("http://localhost:{0}{1}", iisPort, relativeUrl);
        }


    }
}

