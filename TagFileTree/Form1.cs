using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagFileTree
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			CustomToolsColor CTool = new CustomToolsColor();
			ToolStripManager.Renderer =	new ToolStripProfessionalRenderer( CTool );
			this.BackColor = CTool.getBackGroundColor();
		}

		private void toolStripDropDownButton1_Click(object sender, EventArgs e)
		{
			
		}

		private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{

		}
	}
}
