using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms;
using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
using System.Windows.Forms.Integration;

namespace KiCadImport
{
    public partial class ModelViewerForm : Form
    {
        private ElementHost elementHost;
        private HelixViewport3D viewport;

        public ModelViewerForm()
        {
            InitializeComponent();
            InitializeViewer();
        }
        private void InitializeViewer()
        {
            viewport = new HelixViewport3D();
            viewport.Camera.Position = new Point3D(0, 0, 100);
            viewport.Children.Add(new SunLight());
            viewport.Camera.LookDirection = new Vector3D(0, 0, -100); // Look toward origin
            viewport.Camera.UpDirection = new Vector3D(0, 1, 0);

            elementHost = new ElementHost
            {
                Dock = DockStyle.Fill,
                Child = viewport
            };

            this.Controls.Add(elementHost);
        }
        public void LoadModel(string stlPath)
        {
            var reader = new StLReader();
            var model = reader.Read(stlPath);

            viewport.Children.Clear();
            viewport.Children.Add(new SunLight());
            viewport.Children.Add(new ModelVisual3D { Content = model });
        }
    }
}
