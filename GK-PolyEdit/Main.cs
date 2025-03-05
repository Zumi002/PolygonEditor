using GK_PolyEdit.Drawing;
using GK_PolyEdit.Polygon;
using GK_PolyEdit.Relations;
using GK_PolyEdit.TextFiles;
using Microsoft.VisualBasic.Devices;

namespace GK_PolyEdit
{
    public partial class Main : Form
    {
        GK_PolyEdit.Polygon.Polygon polygon;
        DirectBitmap bitmap;
        bool libDraw = true;
        public Main()
        {
            polygon = new Polygon.Polygon();
            InitializeComponent();
            DrawUi();
        }

        private void MainPanel_Resize(object sender, EventArgs e)
        {
            DrawUi();
        }

        private void DrawUi()
        {
            try
            {
                if (bitmap != null) bitmap.Dispose();
                bitmap = new DirectBitmap(PolygonBox.Width, PolygonBox.Height);
                polygon.DrawPolygon(bitmap, libDraw);
                PolygonBox.Image = bitmap.Bitmap;
            }
            catch (Exception e)
            {
                PolygonBox.Image = bitmap.Bitmap;
            }
        }

        private void PolygonBox_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)//For left button clicks
            {

                if (polygon.SelectBezierControl(e.Location))
                {
                    DrawUi();
                }
                else if (polygon.SelectVertex(e.Location))
                {
                    DrawUi();
                }
                else
                {
                    if (polygon.IsPointFInside(e.Location))
                    {
                        polygon.SelectPolygon(e.Location);
                        DrawUi();
                    }
                }

            }
            else if (e.Button == MouseButtons.Right)//For right button clicks
            {
                if (polygon.ContextSelectVertex(e.Location))
                {
                    VertexEditContextMenuStrip.Show(MousePosition);
                    CheckContinuity(polygon.GetContextSelectedVertexContiniuity());
                }
                else if (polygon.SelectEdge(e.Location))
                {
                    EdgeEditContextMenuStrip.Show(MousePosition);
                }
            }
        }

        private void CheckContinuity(Vertex.Continuity con)
        {
            g0ToolStripMenuItem.Checked = false;
            g1ToolStripMenuItem.Checked = false;
            c1ToolStripMenuItem.Checked = false;

            if (con == Vertex.Continuity.G0)
            {
                g0ToolStripMenuItem.Checked = true;
            }
            else if (con == Vertex.Continuity.G1)
            {
                g1ToolStripMenuItem.Checked = true;
            }
            else if (con == Vertex.Continuity.C1)
            {
                c1ToolStripMenuItem.Checked = true;
            }
        }

        private void PolygonBox_MouseUp(object sender, MouseEventArgs e)
        {
            bool drawAfter = false;

            if (polygon.SelectedVertex())
                drawAfter = true;
            if (polygon.SelectedPolygon())
                drawAfter = true;
            if (polygon.SelectedBezierControl())
                drawAfter = true;

            polygon.UnselectVertex();
            polygon.UnselectPolygon();
            polygon.UnselectBezierControl();

            if (drawAfter)
                DrawUi();
        }

        private void PolygonBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (polygon.SelectedBezierControl())
            {
                polygon.MoveBezierControl(e.Location);
                DrawUi();
                Cursor.Current = Cursors.Hand;
            }
            else if (polygon.SelectedVertex())
            {
                polygon.MoveVertex(e.Location);
                DrawUi();
                Cursor.Current = Cursors.Hand;
            }
            else if (polygon.SelectedPolygon())
            {
                polygon.MovePolygon(e.Location);
                DrawUi();
                Cursor.Current = Cursors.Hand;
            }
            else if (polygon.CanSelectVertex(e.Location))
            {
                Cursor.Current = Cursors.Hand;
            }
            else
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            polygon = new Polygon.Polygon();
            DrawUi();
        }

        private void AddVertexClick(object sender, EventArgs e)
        {
            polygon.AddVertex();
            DrawUi();
        }

        private void AddContextRelation(Relation relation)
        {
            bool res = polygon.AddRelationToContextSelectedEdge(relation);
            if (!res)
            {
                MessageBox.Show("Cannot add this rule", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DrawUi();
        }

        private void removeVertexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            polygon.RemoveContextSelectedVertex();
            DrawUi();
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddContextRelation(new VerticalRelation());
            DrawUi();
        }

        private void removeRelationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            polygon.RemoveRelationFromContextSelectedEdge();
            DrawUi();
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddContextRelation(new HorizontalRelation());
            DrawUi();
        }

        private void lengthToolStripMenuItem_Click(object sender, EventArgs e)
        {

            LengthRelationDialog dialog = new LengthRelationDialog(polygon.GetContextSelectedEdgeLength());
            dialog.ShowDialog();
            if (dialog.DialogResult == DialogResult.Cancel)
            {
                return;
            }
            AddContextRelation(new LengthRelation(dialog.value));
            DrawUi();
        }

        private void makeBezierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddContextRelation(new BezierRelation());
        }

        private void g0ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            polygon.SetContextSelectedVertexContiniuity(Vertex.Continuity.G0);
            DrawUi();
        }

        private void g1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            polygon.SetContextSelectedVertexContiniuity(Vertex.Continuity.G1);
            DrawUi();
        }

        private void c1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            polygon.SetContextSelectedVertexContiniuity(Vertex.Continuity.C1);
            DrawUi();
        }

        private void controlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPopup dial = new HelpPopup("Sterowanie");
            dial.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            libDraw = radioButton2.Checked;
            DrawUi();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            libDraw = radioButton2.Checked;
            DrawUi();
        }

        private void relationImplementationInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPopup dial = new HelpPopup("Relacje");
            dial.ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
