using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace KiCadImport
{

    public partial class Form1 : Form
    {
        public delegate bool WindowEnumCallback(int hwnd, int lparam);

        public Action<string> GetConsoleWriter()
        {
            return msg => WriteToTextBox(rtbConsole, msg);
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumWindows(WindowEnumCallback lpEnumFunc, int lParam);

        [DllImport("user32.dll")]
        public static extern void GetWindowText(int h, StringBuilder s, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(int h);

        [DllImport("user32.dll", SetLastError = true)]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        [DllImportAttribute("User32.dll")]
        private static extern IntPtr SetForegroundWindow(int hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumChildWindows(int hWndParent, WindowEnumCallback lpEnumFunc, int lParam);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(int hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool PostMessage(int hWnd, uint Msg, int wParam, int lParam);

        private List<WindowInfo> Windows = new List<WindowInfo>();
        private string[] shouldBeSaved = { "Schematic Editor", "PCB Editor", "Symbol Editor", "Footprint Editor" };
        private const uint WM_CLOSE = 0x0010;

        public Form1()
        {
            InitializeComponent();
            LoadPreviousSettings();
        }

        // Callback for EnumWindows
        private bool AddWnd(int hwnd, int lparam)
        {
            if (IsWindowVisible(hwnd))
            {
                StringBuilder sb = new StringBuilder(255);
                GetWindowText(hwnd, sb, sb.Capacity);
                Windows.Add(new WindowInfo(hwnd, sb.ToString()));
            }
            return true;
        }

        public void CloseWindow(int hwnd)
        {
            PostMessage(hwnd, WM_CLOSE, 0, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EnumWindows(new WindowEnumCallback(this.AddWnd), 0);
        }

        public void WriteToTextBox(RichTextBox tb, string msg)
        {
            tb.Invoke(new Action(() => tb.Text += DateTime.Now.ToString("HH:mm:ss") + ": " + msg + Environment.NewLine));
        }

        private void reset_kicad()
        {
            WriteToTextBox(rtbConsole, "Caching KiCad windows...");
            Windows.Clear();
            EnumWindows(new WindowEnumCallback(this.AddWnd), 0);
            foreach (WindowInfo w in Windows)
            {
                // Cycle through the KiCad windows, save them and then close them
                foreach (string title in shouldBeSaved)
                {
                    if (w.Title.Contains(title))
                    {
                        SetForegroundWindow(w.Hwnd);
                        SendKeys.SendWait("^s");
                        WriteToTextBox(rtbConsole, $"Saved {w.Title}");
                        Thread.Sleep(100);
                        WriteToTextBox(rtbConsole, $"Closing {w.Title}");
                        CloseWindow(w.Hwnd);
                    }
                }
            }

            Windows.Clear();
            EnumWindows(new WindowEnumCallback(this.AddWnd), 0);
            foreach (WindowInfo w in Windows)
            {
                if (w.Title.Contains("KiCad 8.0"))
                {
                    SetForegroundWindow(w.Hwnd);
                    Thread.Sleep(100);
                    CloseWindow(w.Hwnd);
                }
            }
            System.Diagnostics.Process.Start(tbKicadExe.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reset_kicad();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count == 0)
            {
                MessageBox.Show("Please scan a folder first.");
                return;
            }

            if (!Directory.Exists(tbProjectFolder.Text))
            {
                MessageBox.Show("Please select a downloads folder.");
                return;
            }

            foreach (var c in flowLayoutPanel1.Controls)
            {
                if (c is Panel rowPanel)
                {
                    CheckBox checkBox = rowPanel.Controls[0] as CheckBox;
                    TextBox textBox = rowPanel.Controls[1] as TextBox;
                    Label label = rowPanel.Controls[2] as Label;
                    if (checkBox.Checked && !string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        string zipPath = Path.Combine(tbDownloadsFolder.Text, label.Text);
                        string kicadProjectPath = tbProjectFolder.Text;
                        string libName = Path.GetFileNameWithoutExtension(textBox.Text);
                        try
                        {
                            bool delete = cbRemove.Checked;
                            KiCadLibImporter.ImportKiCadLib(kicadProjectPath, zipPath, libName, GetConsoleWriter(), delete);
                            ScanFolder();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void rtbConsole_TextChanged(object sender, EventArgs e)
        {
            rtbConsole.SelectionStart = rtbConsole.Text.Length;
            rtbConsole.ScrollToCaret();
        }

        private void btnProjectFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tbProjectFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnDownloadsFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tbDownloadsFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnKicadExe_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tbKicadExe.Text = openFileDialog1.FileName;
            }
        }

        private void btnScanFolder_Click(object sender, EventArgs e)
        {
            ScanFolder();
        }

        private void ScanFolder()
        {
            if (!Directory.Exists(tbDownloadsFolder.Text))
            {
                MessageBox.Show("Please select a downloads folder.");
                return;
            }
            flowLayoutPanel1.Controls.Clear();
            string[] zipFiles = Directory.GetFiles(tbDownloadsFolder.Text, "*.zip");
            foreach (string zipFile in zipFiles)
            {
                if (Path.GetFileName(zipFile).StartsWith("ul_") || Path.GetFileName(zipFile).StartsWith("LIB_"))
                {
                    AddZipEntry(Path.GetFileName(zipFile));
                }
            }
        }

        private void AddZipEntry(string fileName)
        {
            Panel rowPanel = new Panel
            {
                Width = flowLayoutPanel1.Width - 25,
                Height = 30
            };

            CheckBox checkBox = new CheckBox
            {
                Location = new Point(5, 5),
                Width = 20
            };

            TextBox textBox = new TextBox
            {
                Location = new Point(30, 5),
                Width = 100
            };

            Label label = new Label
            {
                Text = fileName,
                Location = new Point(150, 8),
                AutoSize = true
            };

            rowPanel.Controls.Add(checkBox);
            rowPanel.Controls.Add(textBox);
            rowPanel.Controls.Add(label);

            flowLayoutPanel1.Controls.Add(rowPanel);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var settings = new PreviousSettings
            {
                ProjectFolder = tbProjectFolder.Text,
                DownloadsFolder = tbDownloadsFolder.Text,
                KicadExe = tbKicadExe.Text,
            };

            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText("PreviousSettings.json", json);
        }

        private void LoadPreviousSettings()
        {
            if (!Path.Exists("PreviousSettings.json"))
            {
                return;
            }
            string json = File.ReadAllText("PreviousSettings.json");
            if (!string.IsNullOrEmpty(json))
            {
                try
                {
                    PreviousSettings settings = JsonConvert.DeserializeObject<PreviousSettings>(json);
                    tbProjectFolder.Text = settings.ProjectFolder;
                    tbDownloadsFolder.Text = settings.DownloadsFolder;
                    tbKicadExe.Text = settings.KicadExe;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading previous settings: " + ex.Message);
                }
            }
        }
    }

    public class PreviousSettings
    {
        public string ProjectFolder { get; set; }
        public string DownloadsFolder { get; set; }
        public string KicadExe { get; set; }
    }
}
