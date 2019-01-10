using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace YamlEditorFinal
{
    public partial class YamlEditorFinal : MaterialForm
    {
        public YamlEditorFinal()
        {
            InitializeComponent();

            // Create a material theme manager and add the form to manage (this)
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );

            //makes the toolstripo buttons bigger
            toolStrip_TopMenu.ImageScalingSize = new Size(36, 36);
            //remove toolstrip border
            toolStrip_TopMenu.Renderer = new ToolStripNoBoder();
            //remove tool strip dots on the left
            
        }
    }

    public partial class ToolStripNoBoder : ToolStripSystemRenderer
    {
        public ToolStripNoBoder(){ }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            //base.OnRenderToolStripBorder(e);
        }
    }
}
