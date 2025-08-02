using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace KiCadImport
{
    public class StepModelHandler
    {
        private readonly string pythonExePath;
        private readonly string stepToStlScriptPath;

        public StepModelHandler(string pythonExePath, string scriptPath)
        {
            this.pythonExePath = pythonExePath;
            this.stepToStlScriptPath = scriptPath;
        }

        public bool ConvertStepToStl(string stepPath, string stlOutputPath, out string error)
        {
            error = string.Empty;

            var psi = new ProcessStartInfo
            {
                FileName = pythonExePath,
                Arguments = $"\"{stepToStlScriptPath}\" \"{stepPath}\" \"{stlOutputPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                using (var process = Process.Start(psi))
                {
                    process.WaitForExit();

                    string stdout = process.StandardOutput.ReadToEnd();
                    string stderr = process.StandardError.ReadToEnd();

                    if (process.ExitCode != 0)
                    {
                        error = $"Script error: {stderr}";
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
