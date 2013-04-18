using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;

namespace TagFileTree
{
	class CustomToolsColor : ProfessionalColorTable
	{
		//	背景色の設定
		private Color BackGroundColor = Color.LightGray;
		public Color getBackGroundColor(){ return this.BackGroundColor; }

		#region 【ToolStripの色設定】
			//ToolStripのグラデーションの色を指定
			public override Color ToolStripGradientBegin {
				get{
					return Color.Black;
				}
			}
			public override Color ToolStripGradientMiddle {
				get	{
					return Color.Black;
				}
			}
			public override Color ToolStripGradientEnd {
				get	{
					return Color.Gray;
				}
			}

			//	パネルの下線部分の色設定
			public override Color ToolStripBorder
			{
				get {
					return this.BackGroundColor;
					//return Color.Gray;
				}
			}
		#endregion

		#region 【ToolStripPanelの色設定】
			//ToolStripPanelのグラデーションの色を指定
			public override Color ToolStripPanelGradientBegin {
				get	{
					return Color.Gold;
				}
			}
			public override Color ToolStripPanelGradientEnd {
				get	{
					return Color.Ivory;
				}
			}
		#endregion
	}
}
