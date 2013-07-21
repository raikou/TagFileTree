using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Drawing2D;
using System.Globalization;

namespace TagFileTree
{
	public partial class Form1 : Form
	{
		const int titleWidth = 300;
		private DataStructs fileDataStructs;
		
		public Form1()
		{
			InitializeComponent();
			CustomToolsColor CTool = new CustomToolsColor();
			ToolStripManager.Renderer =	new ToolStripProfessionalRenderer( CTool );
			this.BackColor = CTool.getBackGroundColor();

		}

		//public Form1()
		//{
		//	InitializeComponent();
		//	CustomToolsColor CTool = new CustomToolsColor();
		//	ToolStripManager.Renderer = new ToolStripProfessionalRenderer(CTool);
		//	this.BackColor = CTool.getBackGroundColor();

		//}

		//private void Form1_Load(object sender, EventArgs e)
		//{
		//	fileDataStructs = new DataStructs(@"E:\MyData\ProgramingTempPlace\TagFileTree\TEST");

		//	for (int i = 0; i < fileDataStructs.dataStructs.Count; i++)
		//	{
		//		string[] itemData = { fileDataStructs.dataStructs[i].FileName, fileDataStructs.dataStructs[i].addDate.ToString() };
		//		listView1.Items.Add(new ListViewItem(itemData));
		//		if (i % 2 == 0) listView1.Items[i].BackColor = Color.LightSkyBlue;
		//	}
		//}

		private void Form1_Load(object sender, EventArgs e)
		{
			fileDataStructs = new DataStructs(@"E:\MyData\ProgramingTempPlace\TagFileTree\TEST");

			for (int i = 0; i < fileDataStructs.dataStructs.Count; i++)
			{
				string[] itemData = { fileDataStructs.dataStructs[i].FileName, fileDataStructs.dataStructs[i].addDate.ToString(), "【テスト】【キノセイ】【刻】" };
				listView1.Items.Add(new ListViewItem(itemData));
				if (i % 2 == 0) listView1.Items[i].BackColor = Color.LightSkyBlue;
				//listView2.Items.Add(new ListViewItem(itemData));
				//if (i % 2 == 0) listView2.Items[i].BackColor = Color.LightSkyBlue;

			}


		}


		private void toolStripDropDownButton1_Click(object sender, EventArgs e)
		{
			
		}

		private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{

		}

		private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
		{

		}

		private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void DrowColorText(string changeText, DrawListViewItemEventArgs e, Color fontColor, Brush BackGroundColor)
		{
			//TODO:位置の調整をする

			//グリッドをOFFにするとなんか、以下が表示されなくなるので、グリッドオフでの表示に対応する。
			
			//適切な色で背景を描画する。
			//e.DrawBackground();
			e.DrawFocusRectangle();

			//string changeText = text;
			string addChangeText = "  ";
			string txt = e.Item.Text;
			string txt2 = txt.Replace(changeText, "\n" + addChangeText + changeText + addChangeText + "\n");


			Rectangle rec = e.Bounds;
			//rec.X += 0;
			//rec.Height += 0;
			//rec.Width -= 0;
			Rectangle fullLine = e.Bounds; //行のRECT
			Graphics g = e.Graphics;
			Color defForeColor = Color.Black; //文字色のデフォルト
			Color foreColor = fontColor;//色を変える箇所の文字列


			//選択時の動作
			if ((e.State & ListViewItemStates.Selected) != 0)
			{
				// Draw the background and focus rectangle for a selected item.
				//アイテムが選択された際の色と範囲を設定している
				Rectangle rect = e.Bounds;
				e.Graphics.FillRectangle(Brushes.BlueViolet, rect);
				e.DrawFocusRectangle();
				defForeColor = Color.White;
			}
			else
			{
				//偶数行の色を変える。
				if (e.ItemIndex % 2 == 1)
				{
					e.Graphics.FillRectangle(Brushes.SkyBlue, fullLine);
				}
			}

			foreach (string str in txt2.Split( '\n' ))
			{
				//文字幅を計算する
				rec.Width = TextRenderer.MeasureText(g, str, listView1.Font, new Size(int.MaxValue, int.MinValue), TextFormatFlags.NoPadding).Width;

				//タグ表示部
				if (str.Equals(addChangeText + changeText + addChangeText))
				{
					foreColor = Color.Red;
					Color backColor = Color.Blue;//背景色
					//背景描画
					e.Graphics.FillRectangle(Brushes.Black, rec);
					//rec.X += 1;
					//rec.Y += 1;
					//rec.Width -= 2;
					//rec.Height -= 2;
					//背景描画
					e.Graphics.FillRectangle(Brushes.Blue, rec);
					//ここで文字列の描画を行う。
					TextRenderer.DrawText(g, str, listView1.Font, rec, foreColor);
					rec.Location = new Point(rec.X + 2, rec.Y);
				}
				else //タグ以外表示部
				{
					foreColor = defForeColor;
					//ここで文字列の描画を行う。
					TextRenderer.DrawText(g, str, listView1.Font, rec, foreColor);
				}

				//次の表示位置へ移動
				rec.Location = new Point(rec.X + rec.Width + 1, rec.Y);
			}

			//表示場所を次の位置にする。
			rec.Location = new Point(rec.X + rec.Width + 1, rec.Y);
			ListViewItem lvs = listView1.Items[e.ItemIndex];
			for (int i = 1; i < lvs.SubItems.Count; i++)
			{
				rec.X = titleWidth + lvs.Position.X;
				rec.Y = lvs.Position.Y;
				string text = lvs.SubItems[i].Text;
				rec.Width = TextRenderer.MeasureText(g, text, listView1.Font, new Size(int.MaxValue, int.MinValue), TextFormatFlags.NoPadding).Width;
				TextRenderer.DrawText(g, text, listView1.Font, rec, foreColor);
			}
			rec.Location = new Point(rec.X + rec.Width + 1, rec.Y);

			//フォーカスを示す四角形を描画
			e.DrawFocusRectangle();
		}


		/// <summary>
		/// アイテムの背景色を変更する
		/// タグ表示を追加する・・・が表示が一部消えてしまって使えないので、ゴミ
		/// 参考：http://www.kisoplus.com/sample/owner/itibu.html
		/// 参考：http://amonution.sblo.jp/article/48035059.html
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
		{
			DrowColorText("(一般コミック)", e, Color.Black, Brushes.Blue);

			////TODO:位置の調整をする
			////適切な色で背景を描画する。
			////e.DrawBackground();
			//e.DrawFocusRectangle();
			////string txt = ((ListBox)sender).Items[e.ItemIndex].ToString();
			//string changeText = "(一般コミック)";
			//string txt = e.Item.Text;
			//string txt2 = txt.Replace( changeText, "\n"+changeText+"\n");


			//Rectangle rec = e.Bounds;
			//rec.X += 20;
			//rec.Height += 0;
			//rec.Width -= 0;
			//Rectangle fullLine = e.Bounds; //行のRECT
			//Graphics g = e.Graphics;
			//Color defForeColor = Color.Black; //文字色のデフォルト
			//Color foreColor = defForeColor;//文字色


			////選択時の動作
			//if ((e.State & ListViewItemStates.Selected) != 0)
			//{
			//	// Draw the background and focus rectangle for a selected item.
			//	//アイテムが洗濯された際の色と範囲を設定している
			//	Rectangle rect = e.Bounds;
			//	e.Graphics.FillRectangle(Brushes.Blue, rect);
			//	e.DrawFocusRectangle();
			//	defForeColor = Color.White;
			//}else{
			//	//偶数行の色を変える。
			//	if (e.ItemIndex % 2 == 1){
			//		e.Graphics.FillRectangle(Brushes.SkyBlue, fullLine);
			//	}
			//}
			
			//foreach (string str in txt2.Split('\n'))
			//{
			//	//文字幅を計算する
			//	rec.Width = TextRenderer.MeasureText(g, str, listView1.Font, new Size(int.MaxValue, int.MinValue), TextFormatFlags.NoPadding).Width;

			//	//タグ表示部
			//	if (str.Equals(changeText))
			//	{
			//		foreColor = Color.Red;
			//		Color backColor = Color.Blue;//背景色
			//		//背景描画
			//		e.Graphics.FillRectangle(Brushes.Black, rec);
			//		rec.X += 1;
			//		rec.Y += 1;
			//		rec.Width -= 2;
			//		rec.Height -= 2;
			//		//背景描画
			//		e.Graphics.FillRectangle(Brushes.Blue, rec);
			//		//ここで文字列の描画を行う。
			//		TextRenderer.DrawText(g, str, listView1.Font, rec, foreColor);
			//		rec.Location = new Point(rec.X + 2, rec.Y);
			//	}
			//	else //タグ以外表示部
			//	{
			//		foreColor = defForeColor;
			//		//ここで文字列の描画を行う。
			//		TextRenderer.DrawText(g, str, listView1.Font, rec, foreColor);
			//	}

			//	//次の表示位置へ移動
			//	rec.Location = new Point(rec.X + rec.Width + 1, rec.Y);
			//}

			////表示場所を次の位置にする。
			//rec.Location = new Point(rec.X + rec.Width+1, rec.Y);
			//ListViewItem lvs = listView1.Items[ e.ItemIndex ];
			//for( int i=1; i<lvs.SubItems.Count; i++){
			//	rec.X = titleWidth + lvs.Position.X;
			//	rec.Y = lvs.Position.Y;
			//	string text = lvs.SubItems[i].Text;
			//	rec.Width = TextRenderer.MeasureText(g, text, listView1.Font, new Size(int.MaxValue, int.MinValue), TextFormatFlags.NoPadding).Width;
			//	TextRenderer.DrawText(g, text, listView1.Font, rec, foreColor);
			//}
			//rec.Location = new Point(rec.X + rec.Width + 1, rec.Y);
			
			////フォーカスを示す四角形を描画
			//e.DrawFocusRectangle();

		}

		private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
		{
			e.DrawDefault = true;
		}

		private void listView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
		{
			TextFormatFlags flags = TextFormatFlags.Left;

			//TODO：文字列表示の設定

			using (StringFormat sf = new StringFormat()){
				// Store the column text alignment, letting it default
				// to Left if it has not been set to Center or Right.
				switch (e.Header.TextAlign){
					case HorizontalAlignment.Center:
						sf.Alignment = StringAlignment.Center;
						flags = TextFormatFlags.HorizontalCenter;
						break;
					case HorizontalAlignment.Right:
						sf.Alignment = StringAlignment.Far;
						flags = TextFormatFlags.Right;
						break;
				}

				
				// Draw the text and background for a subitem with a 
				// negative value. 
				double subItemValue;
				if (e.ColumnIndex > 0 && Double.TryParse( e.SubItem.Text, NumberStyles.Currency, NumberFormatInfo.CurrentInfo, out subItemValue) && subItemValue < 0)
				{
					// Unless the item is selected, draw the standard 
					// background to make it stand out from the gradient.
					if ((e.ItemState & ListViewItemStates.Selected) == 0){
						e.DrawBackground();
					}

					// Draw the subitem text in red to highlight it. 
					e.Graphics.DrawString(e.SubItem.Text, listView1.Font, Brushes.Blue, e.Bounds, sf);

					//描画位置を取得する。
					Rectangle rec = e.Bounds;
					Brush brush = Brushes.DarkBlue;
					rec.Width += rec.Width + 10;
					e.Graphics.DrawString(e.SubItem.Text, listView1.Font, brush, rec, sf);

					return;
				}



				// Draw normal text for a subitem with a nonnegative 
				// or nonnumerical value.
				//e.DrawText(flags);
			}

		}

	}
}
