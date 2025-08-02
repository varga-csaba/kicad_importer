using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KiCadImport
{
    internal class KiCadLibImporter
    {
        public static void ImportKiCadLib(string projectPath, string zipPath, string libName, Action<string> writeToConsole, bool delete)
        {
            var hardwareDir = Directory.GetParent(projectPath);
            var componentsDir = Path.Combine(hardwareDir.FullName, "components");
            var extractDir = Path.Combine(componentsDir, libName);
            var tempDir = Path.Combine(componentsDir, "temp");

            var fpTablePath = Path.Combine(projectPath, "fp-lib-table");
            var symTablePath = Path.Combine(projectPath, "sym-lib-table");

            Directory.CreateDirectory(componentsDir);
            Directory.CreateDirectory(extractDir);
            Directory.CreateDirectory(tempDir);

            ZipFile.ExtractToDirectory(zipPath, tempDir, overwriteFiles: true);
            Console.WriteLine("Extracted ZIP to: " + extractDir);
            writeToConsole("Extracted ZIP to: " + extractDir);

            foreach (var file in Directory.EnumerateFiles(tempDir, "*.*", SearchOption.AllDirectories))
            {
                var ext = Path.GetExtension(file).ToLower();
                if (ext == ".kicad_mod" || ext == ".kicad_sym" || ext == ".step" || ext == ".stp")
                {
                    File.Copy(file, Path.Combine(extractDir, Path.GetFileName(file)), overwrite: true);
                }
            }

            Directory.Delete(tempDir, recursive: true);

            var modFiles = Directory.GetFiles(extractDir, "*.kicad_mod").ToList();
            if (modFiles.Any())
            {
                var shortest = modFiles.OrderBy(f => Path.GetFileName(f).Length).First();
                foreach (var file in modFiles)
                {
                    if (file != shortest)
                    {
                        File.Delete(file);
                        Console.WriteLine("Deleted: " + Path.GetFileName(file));
                        writeToConsole("Deleted: " + Path.GetFileName(file));
                    }
                }
                Console.WriteLine("Kept shortest .kicad_mod file: " + Path.GetFileName(shortest));
                writeToConsole("Kept shortest .kicad_mod file: " + Path.GetFileName(shortest));
            }

            void UpdateFpLibTable()
            {
                if (!File.Exists(fpTablePath))
                {
                    File.WriteAllText(fpTablePath, "(fp_lib_table)\n");
                }

                var content = File.ReadAllText(fpTablePath);
                if (!content.Contains(libName))
                {
                    string entry = $"\n  (lib (name \"{libName}\")(type \"KiCad\")(uri \"${{KIPRJMOD}}/../components/{libName}\")(options \"\")(descr \"\"))\n";
                    content = content.Substring(0, content.Length - 3) + entry + ")\n";
                    File.WriteAllText(fpTablePath, content);
                    Console.WriteLine("Added to fp-lib-table.");
                    writeToConsole("Added to fp-lib-table.");
                }
                else
                {
                    Console.WriteLine("Footprint lib already in fp-lib-table.");
                    writeToConsole("Footprint lib already in fp-lib-table.");
                }
            }

            void UpdateSymLibTable()
            {
                if (!File.Exists(symTablePath))
                {
                    File.WriteAllText(symTablePath, "(sym_lib_table)\n");
                }

                var content = File.ReadAllText(symTablePath);
                var symFile = Directory.GetFiles(extractDir, "*.kicad_sym").FirstOrDefault();
                if (symFile == null)
                    return;

                string relativeSymPath = $"../components/{libName}/{Path.GetFileName(symFile)}";

                if (!content.Contains(libName))
                {
                    string entry = $"\n  (lib (name \"{libName}\")(type \"KiCad\")(uri \"${{KIPRJMOD}}/{relativeSymPath}\")(options \"\")(descr \"\"))\n";
                    content = content.Substring(0, content.Length - 3) + entry + ")\n";
                    File.WriteAllText(symTablePath, content);
                    Console.WriteLine("Added to sym-lib-table.");
                    writeToConsole("Added to sym-lib-table.");
                }
                else
                {
                    Console.WriteLine("Symbol lib already in sym-lib-table.");
                    writeToConsole("Symbol lib already in sym-lib-table.");
                }
            }

            void UpdateFootprintWithStep()
            {
                var stepFile = Directory.EnumerateFiles(extractDir, "*.step", SearchOption.AllDirectories)
                               .Concat(Directory.EnumerateFiles(extractDir, "*.stp", SearchOption.AllDirectories))
                               .FirstOrDefault();

                var modFile = Directory.GetFiles(extractDir, "*.kicad_mod").FirstOrDefault();
                if (stepFile == null || modFile == null) return;

                string content = File.ReadAllText(modFile);
                string stepRelPath = $"../components/{libName}/{Path.GetFileName(stepFile)}";

                string modelEntry = $"  (model \"${{KIPRJMOD}}/{stepRelPath}\"(offset(xyz 0 0 0))(scale(xyz 1 1 1))(rotate(xyz 0 0 0)))\n";

                if (content.Contains("(model"))
                {
                    var parts = Regex.Split(content, @"\(model");
                    content = parts[0] + modelEntry + ") \n";
                }
                else
                {
                    content = content.Substring(0, content.Length - 3) + modelEntry + ")\n";
                }

                File.WriteAllText(modFile, content);
                Console.WriteLine("Added STEP model to footprint.");
                writeToConsole("Added STEP model to footprint.");
            }

            UpdateFpLibTable();
            UpdateSymLibTable();
            UpdateFootprintWithStep();

            if (delete)
            {
                try
                {
                    File.Delete(zipPath);
                    Console.WriteLine("Deleted zip: " + extractDir);
                    writeToConsole("Deleted zip: " + extractDir);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deleting directory: " + ex.Message);
                    writeToConsole("Error deleting directory: " + ex.Message);
                }
            }
            writeToConsole("Import completed successfully.");
        }
    }
}
