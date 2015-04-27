using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

using Zuma.GameEngine;

using PointFDraw = System.Drawing.PointF;
using PointFZuma = Zuma.GameEngine.PointF;

namespace Zuma.DesktopUI
{
    public partial class MainForm : Form
    {
        List<PointFZuma> _points;

        Cursor _selectPointCursor;

        Pen _selectedPen;
        Pen _blackPen = new Pen(Color.Black, 1);
        //Pen _qPen = new Pen(new SolidBrush(Color.Bisque), 3.0F);
        Bitmap _qBbmp = null;
        Pen _qPen = null;
        Brush _qBrush = null;

        FileStream _stream = null;

        public MainForm()
        {
            InitializeComponent();

            InitializeComponentAdv();

            _selectPointCursor = new Cursor(GetType(), "CursorSelectPoint.cur");

            _selectedPen = _blackPen;

            LoadTexturedPen();
        }

        private void LoadTexturedPen()
        {
            try
            {
                _stream = File.Open(@"..\..\Texture.png", FileMode.Open, FileAccess.Read);
                _qBbmp = new Bitmap(_stream);
                _qBrush = new TextureBrush(_qBbmp);
                _qPen = new Pen(_qBrush, 8F);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Fucking bitmap over stream");
            }
        }

        private Graphics GetGraphics()
        {
            return tabPageBezierCurves.CreateGraphics();
        }

        private void InitializeComponentAdv()
        {
            this.panelMainMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGameCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMapDrawing.Dock = System.Windows.Forms.DockStyle.Fill;

            panelMapDrawing.Hide();
            panelGameCanvas.Hide();
            panelMainMenu.Show();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Alt)
            {
                FullScreen(!IsFullScreen());
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Pause();
            }
        }

        private void Pause()
        {
            if (panelGameCanvas.Visible)
                panelGameCanvas.Hide();
            if (panelMapDrawing.Visible)
                panelMapDrawing.Hide();
            panelMainMenu.Show();
        }

        private void FullScreen(bool fullScreen)
        {
            if (fullScreen)
            {
                if (!IsFullScreen())
                {
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                if (IsFullScreen())
                {
                    FormBorderStyle = FormBorderStyle.Sizable;
                    WindowState = FormWindowState.Normal;
                }
            }
        }

        private bool IsFullScreen()
        {
            return FormBorderStyle == FormBorderStyle.None;
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            FullScreen(true);
            panelMainMenu.Hide();
            panelGameCanvas.Show();
        }

        private void buttonDrawMap_Click(object sender, EventArgs e)
        {
            panelMainMenu.Hide();
            panelMapDrawing.Show();
        }

        private void buttonOpenBezierSVG_Click(object sender, EventArgs e)
        {
            StreamReader reader = null;

            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                DialogResult dlgResult = dialog.ShowDialog();
                if (dlgResult != DialogResult.OK)
                    return;

                reader = new StreamReader(dialog.FileName);
                string pointsFileContent = reader.ReadLine();

                _points = Bezier.AbsoluteBezierPoints(pointsFileContent, true);

                textBoxBezierOriginal.Clear();

                for (int i = 0; i < _points.Count; i++)
                {
                    textBoxBezierOriginal.AppendText(string.Format("{0}, {1}\r\n", _points[i].X, _points[i].Y));
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            DrawBezier();
        }

        private void DrawBezier()
        {
            PointFDraw[] points = new PointFDraw[_points.Count];

            int dX = int.Parse(textBoxDX.Text);
            int dY = int.Parse(textBoxDY.Text);

            textBoxBezierShifted.Text = "m ";

            for (int i = 0; i < points.Length; i++)
            {
                points[i].X = _points[i].X + dX;
                points[i].Y = _points[i].Y + dY;
                textBoxBezierShifted.AppendText(String.Format("{0}:{1} ", points[i].X, points[i].Y));

                if (i == 0)
                    textBoxBezierShifted.AppendText("C ");
            }

            textBoxBezierShifted.Text = textBoxBezierShifted.Text.Replace(',', '.');
            textBoxBezierShifted.Text = textBoxBezierShifted.Text.Replace(':', ',');

            Graphics g = GetGraphics();

            for (int i = 0; i + 3 < points.Length; i += 3)
            {
                g.DrawBezier(_selectedPen, points[i + 0], points[i + 1], points[i + 2], points[i + 3]);
            }
        }

        private void buttonDrawByPoints_Click(object sender, EventArgs e)
        {
            string[] pointsCoords = textBoxBezierBuilt.Lines;
            string[] splitter = { ", " };

            PointFDraw pointTmp = new PointFDraw();

            Graphics g = GetGraphics();
            SolidBrush brush = new SolidBrush(buttonSelectColor.BackColor);

            for (int i = 0; i < pointsCoords.Length - 1; i++)
            {
                string[] p = pointsCoords[i].Split(splitter, StringSplitOptions.None);
                pointTmp.X = float.Parse(p[0]);
                pointTmp.Y = float.Parse(p[1]);
                g.FillRectangle(brush, pointTmp.X, pointTmp.Y, 1, 1);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Graphics g = GetGraphics();

            g.Clear(Color.White);
        }

        private void buttonSelectColor_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                buttonSelectColor.BackColor = colorDialog.Color;
                buttonSelectColor.FlatAppearance.MouseDownBackColor = colorDialog.Color;
                buttonSelectColor.FlatAppearance.MouseOverBackColor = colorDialog.Color;
            }
        }

        private void buttonGetPoints_Click(object sender, EventArgs e)
        {
            textBoxBezierBuilt.Clear();

            PointFDraw[] points = new PointFDraw[_points.Count];

            int dX = int.Parse(textBoxDX.Text);
            int dY = int.Parse(textBoxDY.Text);

            for (int i = 0; i < points.Length; i++)
            {
                points[i].X = _points[i].X + dX;
                points[i].Y = _points[i].Y + dY;
            }

            float step = float.Parse(textBoxStep.Text);

            StringBuilder ppp = new StringBuilder();

            for (int i = 0; i + 3 < points.Length; i += 3)
            {
                GetBezierPoints(ppp, points[i + 0], points[i + 1], points[i + 2], points[i + 3], step);
            }

            textBoxBezierBuilt.Text = ppp.ToString();
        }

        private void buttonRoundPoints_Click(object sender, EventArgs e)
        {
            RoundBezier();
        }

        private void RoundBezier()
        {
            StringBuilder result = new StringBuilder();

            string[] pointsCoords = textBoxBezierBuilt.Lines;
            string[] splitter = { ", " };

            string[] tmpPointCoords = pointsCoords[0].Split(splitter, StringSplitOptions.None);
            float tmpX = (float)Math.Round(float.Parse(tmpPointCoords[0]));
            float tmpY = (float)Math.Round(float.Parse(tmpPointCoords[1]));

            result.Append(tmpX);
            result.Append(", ");
            result.Append(tmpY);
            result.AppendLine();

            for (int i = 0; i < pointsCoords.Length - 1; i++)
            {
                tmpPointCoords = pointsCoords[i].Split(splitter, StringSplitOptions.None);
                float tmpX2 = (float)Math.Round(float.Parse(tmpPointCoords[0]));
                float tmpY2 = (float)Math.Round(float.Parse(tmpPointCoords[1]));
                if (tmpX == tmpX2 && tmpY == tmpY2)
                    continue;
                else
                {
                    tmpX = tmpX2;
                    tmpY = tmpY2;
                    result.Append(tmpX);
                    result.Append(", ");
                    result.Append(tmpY);
                    result.AppendLine();
                }
            }

            textBoxBezierBuilt.Text = result.ToString();
        }

        private void GetBezierPoints(StringBuilder resultString, PointFDraw P0, PointFDraw P1, PointFDraw P2, PointFDraw P3, float step)
        {
            PointFDraw p;

            for (float t = 0.0F; t <= 1.0F; t += step)
            {
                float k0 = (float)Math.Pow(1 - t, 3);
                float k1 = (float)(3 * Math.Pow(1 - t, 2) * t);
                float k2 = (float)(3 * (1 - t) * Math.Pow(t, 2));
                float k3 = (float)Math.Pow(t, 3);

                p = P0.Multiply(k0)
                    .Add(P1.Multiply(k1))
                    .Add(P2.Multiply(k2))
                    .Add(P3.Multiply(k3));

                resultString.Append(p.X.ToString());
                resultString.Append(", ");
                resultString.Append(p.Y.ToString());
                resultString.Append("\r\n");
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1 || tabControl1.SelectedIndex == 2)
            {
                DialogResult result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Stream stream = File.Open(openFileDialog.FileName, FileMode.Open);
                    try
                    {
                        if (tabControl1.SelectedIndex == 1)
                        {
                            pictureBox1.Image = Bitmap.FromStream(stream);
                            tabPagePic1.AutoScrollMinSize = pictureBox1.Image.Size + new System.Drawing.Size(0, 22);
                            toolStripStatusLabel1.Text = openFileDialog.FileName;
                        }
                        else
                        {
                            pictureBox2.Image = Bitmap.FromStream(stream);
                            tabPagePic2.AutoScrollMinSize = pictureBox2.Image.Size + new System.Drawing.Size(0, 22);
                            toolStripStatusLabel2.Text = openFileDialog.FileName;
                        }
                    }
                    finally
                    {
                        stream.Close();
                    }
                }
            }
        }

        private void compareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null || pictureBox2.Image == null)
            {
                MessageBox.Show("Image(s) not loaded");
                return;
            }

            if (pictureBox1.Image.Size != pictureBox2.Image.Size)
            {
                DialogResult result =
                    MessageBox.Show("Images have different sizes.\nContinue comparing?",
                    "", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                    return;
            }

            System.Drawing.Size size = new System.Drawing.Size(
                Math.Min(pictureBox1.Image.Size.Width, pictureBox2.Image.Size.Width) + 2,
                Math.Min(pictureBox1.Image.Size.Height, pictureBox2.Image.Size.Height) + 2);

            Bitmap bmp = new Bitmap(size.Width, size.Height);
            pictureBoxCompare.Image = bmp;
            tabPageComparePictures.AutoScrollMinSize = size + new System.Drawing.Size(0, 22);

            Graphics g = Graphics.FromImage(bmp);
            g.DrawRectangle(new Pen(Color.LightGoldenrodYellow), 0, 0, size.Width - 1, size.Height - 1);

            Cursor = Cursors.WaitCursor;
            toolStripStatusLabelCompare.Text = "Comparing...";

            bool areEqual = true;

            for (int i = 0; i < size.Height - 2; i++)
                for (int j = 0; j < size.Width - 2; j++)
                    if (((Bitmap)pictureBox1.Image).GetPixel(j, i) != ((Bitmap)pictureBox2.Image).GetPixel(j, i))
                    {
                        bmp.SetPixel(j + 1, i + 1, Color.Red);
                        areEqual = false;
                    }

            toolStripStatusLabelCompare.Text = "Ready.";
            Cursor = Cursors.Default;

            if (areEqual)
                MessageBox.Show("No difference encountered");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableOrDisableMenuItems();

            if(tabControl1.SelectedTab == tabPageLines)
            {
                if (pictureBox3.Image == null)
                    pictureBox3.Image = new Bitmap(500, 500);
            }
        }

        private void EnableOrDisableMenuItems()
        {
            openToolStripMenuItem.Enabled = false;
            selectPathStartToolStripMenuItem.Enabled = false;
            compareToolStripMenuItem.Enabled = false;

            if (tabControl1.SelectedIndex == 1)
            {
                openToolStripMenuItem.Enabled = true;
                selectPathStartToolStripMenuItem.Enabled = true;
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                openToolStripMenuItem.Enabled = true;
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                compareToolStripMenuItem.Enabled = true;
            }
        }

        private void selectPathStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectPathStartToolStripMenuItem.Checked)
            {
                selectPathStartToolStripMenuItem.Checked = false;
            }
            else
            {
                selectPathStartToolStripMenuItem.Checked = true;
            }
        }

        private void selectPathStartToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (selectPathStartToolStripMenuItem.Checked)
            {
                Cursor = _selectPointCursor;
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectPathStartToolStripMenuItem.Checked)
            {
                if (pictureBox1.Image != null &&
                    e.X < pictureBox1.Image.Width && e.Y < pictureBox1.Image.Height)
                {
                    toolStripStatusLabelCoordinates.Text = string.Format("{0}, {1}", e.X, e.Y);
                    toolStripStatusLabelColor.BackColor = ((Bitmap)pictureBox1.Image).GetPixel(e.X, e.Y);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (selectPathStartToolStripMenuItem.Checked)
                selectPathStartToolStripMenuItem.Checked = false;
            if (toolStripStatusLabelCoordinates.Text != "...")
                textBoxStartPointCoordinates.Text = toolStripStatusLabelCoordinates.Text;
        }

        private void buttonCalculatePath_Click(object sender, EventArgs e)
        {
            if (textBoxStartPointCoordinates.Text == "...")
            {
                MessageBox.Show("Start point not selected.");
                return;
            }

            textBoxCalculatedPath.Clear();

            string[] start = textBoxStartPointCoordinates.Text.Split(new string[] { ", " }, StringSplitOptions.None);
            int X = int.Parse(start[0]);
            int Y = int.Parse(start[1]);

            ((Bitmap)pictureBox1.Image).Save("_tmp.bmp");
            Bitmap bmp = (Bitmap)Bitmap.FromFile("_tmp.bmp");

            //textBoxCalculatedPath.Text = CalculateBezier((Bitmap)pictureBox1.Image, X, Y);
            textBoxCalculatedPath.Text = CalculateBezier(bmp, X, Y);
        }

        private string CalculateBezier(Bitmap bitmapTmp, int X, int Y)
        {
            StringBuilder result = new StringBuilder();

            Color c = Color.FromArgb(255, 0, 0, 0);
            Color c2 = Color.FromArgb(255, 255, 255, 255);

            bitmapTmp.SetPixel(X, Y, c2);

            result.Append(X);
            result.Append(", ");
            result.Append(Y);
            result.Append("\r\n");

            while (true)
            {
                if (X - 1 > 0 && Y - 1 > 0 && bitmapTmp.GetPixel(X - 1, Y - 1) == c)
                {
                    X = X - 1;
                    Y = Y - 1;
                }
                else if (X - 1 > 0 && Y > 0 && bitmapTmp.GetPixel(X - 1, Y) == c)
                {
                    X = X - 1;
                }
                else if (X - 1 > 0 && Y + 1 < bitmapTmp.Height && bitmapTmp.GetPixel(X - 1, Y + 1) == c)
                {
                    X = X - 1;
                    Y = Y + 1;
                }
                else if (X > 0 && Y - 1 > 0 && bitmapTmp.GetPixel(X, Y - 1) == c)
                {
                    Y = Y - 1;
                }
                else if (X > 0 && Y + 1 < bitmapTmp.Height && bitmapTmp.GetPixel(X, Y + 1) == c)
                {
                    Y = Y + 1;
                }
                else if (X + 1 < bitmapTmp.Width && Y - 1 > 0 && bitmapTmp.GetPixel(X + 1, Y - 1) == c)
                {
                    X = X + 1;
                    Y = Y - 1;
                }
                else if (X + 1 < bitmapTmp.Width && Y > 0 && bitmapTmp.GetPixel(X + 1, Y) == c)
                {
                    X = X + 1;
                }
                else if (X + 1 < bitmapTmp.Width && Y + 1 < bitmapTmp.Height && bitmapTmp.GetPixel(X + 1, Y + 1) == c)
                {
                    X = X + 1;
                    Y = Y + 1;
                }
                else
                {
                    break;
                }

                bitmapTmp.SetPixel(X, Y, c2);

                result.Append(X);
                result.Append(", ");
                result.Append(Y);
                result.Append("\r\n");
            }

            return result.ToString();
        }

        private void buttonPenProperties_Click(object sender, EventArgs e)
        {
            if (_selectedPen == _blackPen && _qPen != null)
            {
                _selectedPen = _qPen;
                buttonTexturedPen.Text = "Black pen";
            }
            else
            {
                _selectedPen = _blackPen;
                buttonTexturedPen.Text = "Textured pen";
            }
        }

        private void buttonReleaseTexturedPen_Click(object sender, EventArgs e)
        {
            if (_stream != null)
            {
                _stream.Close();
                _stream = null;
                _qPen = null;
                _selectedPen = _blackPen;
                buttonTexturedPen.Text = "Textured pen";
            }
            else
            {
                LoadTexturedPen();
            }
        }

        private void btnDrawLine_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(pictureBox3.Image);

            PointFDraw p1 = new PointFDraw(float.Parse(txtX1.Text), float.Parse(txtY1.Text));
            PointFDraw p2 = new PointFDraw(float.Parse(txtX2.Text), float.Parse(txtY2.Text));

            g.DrawLine(new Pen(Color.Black), p1, p2);

            pictureBox3.Invalidate();
        }

        private void btnDrawLineByPoints_Click(object sender, EventArgs e)
        {
            PointFZuma p1 = new PointFZuma(float.Parse(txtX1.Text), float.Parse(txtY1.Text));
            PointFZuma p2 = new PointFZuma(float.Parse(txtX2.Text), float.Parse(txtY2.Text));

            Field field = new Field(400, 350);

            LinePath path = new LinePath(field, p1, p2);

            for(int i = 0; i < path.Points.Length; i++)
            {
                PointFZuma p = path.Points[i];
                ((Bitmap)pictureBox3.Image).SetPixel((int)p.X, (int)p.Y, Color.Red);
            }

            pictureBox3.Invalidate();
        }

        private void btnClearLines_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(pictureBox3.Image);

            g.Clear(Color.Yellow);

            pictureBox3.Invalidate();
        }
    }
}

