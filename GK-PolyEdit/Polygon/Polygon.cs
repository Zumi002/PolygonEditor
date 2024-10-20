using GK_PolyEdit.Relations;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;
using static GK_PolyEdit.Polygon.Edge;
using GK_PolyEdit.Drawing;

namespace GK_PolyEdit.Polygon
{
    class Polygon
    {

        Vertex startingVertex;
        Vertex? selectedVertex;
        Vertex? contextSelectedVertex;

        Edge? selectedEdge;
        PointF? edgeSelectionPointF;

        BezierControl? selectedBezierControl;
        //PointF? bezierControlSelectionPointF;

        PointF? polygonSelectionPointF;
        bool polygonSelected;

        Pen ePen = new Pen(Color.Black, 2);
        Pen bezierControlEdgePen = new Pen(BezierControl.drawEdgeColor, 2);
        Pen bezierControlPen = new Pen(BezierControl.drawColor, 2);
        Pen bezierControlSelectedPen = new Pen(BezierControl.drawSelectColor, 2);

        Brush vBrush = new SolidBrush(Vertex.drawColor);
        Brush vSelBrush = new SolidBrush(Vertex.selectColor);
        Brush insideBrush = new SolidBrush(Color.FromArgb(255, 170, 201, 242));
        Brush insideSelectedBrush = new SolidBrush(Color.FromArgb(255, 22, 118, 245));

        Font fontForDrawing = new Font("Arial", 9);
        Brush fontBrush = new SolidBrush(Color.Black);

        int vCount;
        public Polygon()
        {
            vCount = 3;
            //ten pierwszy co mamy zrobić
            Vertex v1 = new Vertex(new PointF(60, 60)),
                   v2 = new Vertex(new PointF(120, 200)),
                   v3 = new Vertex(new PointF(60, 250));
            Edge e1 = new Edge(v1, v2),
                 e2 = new Edge(v2, v3),
                 e3 = new Edge(v3, v1);
            startingVertex = v1;

            polygonSelected = false;

            bezierControlEdgePen.DashPattern = new float[] { 5, 5 };

        }
        public void AddVertexOnEdge(Vertex v, Edge e)
        {
            vCount++;
            Edge newPrevEdge = new Edge(e.prevV, v),
                 newNextEdge = new Edge(v, e.nextV);
        }
        public void AddVertex()
        {
            if (selectedEdge != null && edgeSelectionPointF != null)
            {
                Vertex v;
                if (selectedEdge.isBezier)
                {
                    v = new Vertex((PointF)edgeSelectionPointF);
                }
                else
                {
                    v = new Vertex(selectedEdge.ProjectionOnEdge((PointF)edgeSelectionPointF));
                }
                AddVertexOnEdge(v, selectedEdge);
                RelationSolver.FixBeziers(v);
            }
            
        }
        public bool RemoveContextSelectedVertex()
        {
            if (ContextSelectedVertex())
                return RemoveVertex(contextSelectedVertex);
            return false;
        }
        public bool RemoveVertex(Vertex v)
        {
            if (vCount <= 3)
            {
                //nie da się
                return false;
            }
            if (v.id == startingVertex.id)
                startingVertex = startingVertex.nextE.nextV;
            vCount--;
            Vertex nextV = v.nextE.nextV,
                   prevV = v.prevE.prevV;
            Edge newEdge = new Edge(prevV, nextV);
            RelationSolver.FixBeziers(prevV);
            return true;
        }

        public DirectBitmap DrawPolygon(DirectBitmap newBmp,bool libDraw)
        {
            Vertex p = startingVertex;

            List<PointF> polygonPointFs = new List<PointF>();
            for (int i = 0; i < vCount; i++)
            {
                polygonPointFs.Add(p.pos);
                if (p.nextE.relation != null && p.nextE.relation.name == "B")
                {
                    BezierRelation br = (BezierRelation)p.nextE.relation;

                    br.CalcAprox(br.GetControlPointFs(p.nextE));
                    polygonPointFs.AddRange(br.aprox);
                }
                p = p.nextE.nextV;
            }
            using (var graphics = Graphics.FromImage(newBmp.Bitmap))
            {

                graphics.Clear(Color.White);
                graphics.FillPolygon(polygonSelected ? insideSelectedBrush : insideBrush, polygonPointFs.ToArray());
                for (int i = 0; i < vCount; i++)
                {
                    Edge drawNow = p.nextE;

                    if (drawNow.relation is BezierRelation)
                    {

                        BezierRelation br = (BezierRelation)drawNow.relation;

                        BezierDrawing.DrawBezier(br.GetControlPointFs(drawNow), newBmp, Color.Black);

                        /* 
                         * Bezier aprox PointFs debug
                         * 
                        BezierRelation br = (BezierRelation)drawNow.relation;
                        br.CalcAprox(con);
                        foreach (PointF pp in br.aprox)
                        {
                            graphics.FillEllipse(vSelBrush, pp.X - 5, pp.Y - 5, 10, 10);
                        }
                        */
                    }
                    else
                    {
                        PointF A = drawNow.prevV.pos;
                        PointF B = drawNow.nextV.pos;
                        
                        if (libDraw)
                            graphics.DrawLine(ePen, A, B);
                        else
                            DrawLineBresenham.DrawLine(newBmp, A, B,ePen);
                        if (drawNow.relation != null)
                        {
                            if (!drawNow.isBezier)
                            {
                                PointF midPonit = new PointF((A.X + B.X) / 2, (A.Y + B.Y) / 2);
                                string relationString = drawNow.relation.name;
                                if(drawNow.relation.name == "L")
                                {
                                    LengthRelation lr = (LengthRelation)drawNow.relation;
                                    relationString = $"{lr.name} - {lr.value}";
                                }
                                graphics.DrawString(relationString, fontForDrawing, fontBrush, midPonit);
                            }
                        }
                    }

                    p = p.nextE.nextV;
                }
                int doubleSize = Vertex.size * 2;
                for (int i = 0; i < vCount; i++)
                {
                    (int x, int y) = ((int)p.pos.X, (int)p.pos.Y);

                    graphics.FillEllipse(p.selected ? vSelBrush : vBrush, x - Vertex.size, y - Vertex.size, doubleSize, doubleSize);

                    p = p.nextE.nextV;
                }
                for (int i = 0; i < vCount; i++)
                {
                    Edge drawNow = p.nextE;

                    if (drawNow.relation is BezierRelation)
                    {
                        BezierRelation br = (BezierRelation)drawNow.relation;
                        PointF[] con = br.GetControlPointFs(drawNow);
                        bool selectedPrevB = drawNow.prevB.selected;
                        bool selectedNextB = drawNow.nextB.selected;
                        graphics.DrawLine(bezierControlEdgePen, con[0], con[1]);
                        graphics.DrawLine(bezierControlEdgePen, con[1], con[2]);
                        graphics.DrawLine(bezierControlEdgePen, con[2], con[3]);

                        graphics.DrawLine(selectedPrevB ? bezierControlSelectedPen : bezierControlPen,
                                                            new PointF(con[1].X - 4, con[1].Y - 4),
                                                            new PointF(con[1].X + 4, con[1].Y + 4));
                        graphics.DrawLine(selectedPrevB ? bezierControlSelectedPen : bezierControlPen,
                                                            new PointF(con[1].X + 4, con[1].Y - 4),
                                                            new PointF(con[1].X - 4, con[1].Y + 4));
                        graphics.DrawLine(selectedNextB ? bezierControlSelectedPen : bezierControlPen,
                                                            new PointF(con[2].X - 4, con[2].Y - 4),
                                                            new PointF(con[2].X + 4, con[2].Y + 4));
                        graphics.DrawLine(selectedNextB ? bezierControlSelectedPen : bezierControlPen,
                                                            new PointF(con[2].X + 4, con[2].Y - 4),
                                                            new PointF(con[2].X - 4, con[2].Y + 4));
                    }
                    p = p.nextE.nextV;
                }
            }

            return newBmp;
        }

        //Select Vertex (or not) based on position of mouse
        public bool SelectVertex(PointF PointF)
        {
            Vertex? v = FindCloseVertex(PointF);
            if (v != null)
            {
                v.selected = true;
                selectedVertex = v;
                polygonSelectionPointF = PointF;
                return true;
            }
            return false;
        }
        public bool ContextSelectVertex(PointF PointF)
        {
            Vertex? v = FindCloseVertex(PointF);
            if (v != null)
            {
                contextSelectedVertex = v;
                return true;
            }
            return false;
        }

        public bool CanSelectVertex(PointF PointF)
        {
            return FindCloseVertex(PointF) != null;
        }

        private Vertex? FindCloseVertex(PointF PointF)
        {
            Vertex p = startingVertex;
            int rad = Vertex.size * Vertex.size;
            for (int i = 0; i < vCount; i++)
            {
                if (p.DistanceSQ(PointF) < rad)
                {
                    return p;
                }

                p = p.nextE.nextV;
            }
            return null;
        }

        public bool CanSelectEdge(PointF PointF)
        {
            return FindCloseEdge(PointF) != null;
        }

        public bool SelectEdge(PointF PointF)
        {
            selectedEdge = FindCloseEdge(PointF);
            edgeSelectionPointF = PointF;
            return selectedEdge != null;
        }

        private Edge? FindCloseEdge(PointF PointF)
        {
            Edge p = startingVertex.nextE;
            int rad = 25;
            for (int i = 0; i < vCount; i++)
            {
                if (p.isBezier)
                {
                    BezierRelation br = (BezierRelation)p.relation;
                    PointF A = p.prevV.pos;
                    foreach (PointF P in br.aprox)
                    {
                        if (Edge.DistanceSQ(PointF, A, P) < rad)
                        {
                            return p;
                        }
                        A = P;
                    }
                    if (Edge.DistanceSQ(PointF, A, p.nextV.pos) < rad)
                    {
                        return p;
                    }
                }
                else if (p.DistanceSQ(PointF) < rad)
                {
                    return p;
                }

                p = p.nextV.nextE;
            }
            return null;
        }

        public bool SelectedVertex()
        {
            if (selectedVertex != null && selectedVertex.selected)
                return true;
            return false;
        }
        public bool ContextSelectedVertex()
        {
            if (contextSelectedVertex != null)
                return true;
            return false;
        }

        public void MoveVertex(PointF PointF)
        {
            PointF previousVertexPointF = selectedVertex.pos;
            selectedVertex.SetPos(PointF);
            if (!RelationSolver.TrySolve(selectedVertex))
            {
                selectedVertex.SetPos(previousVertexPointF);
                MovePolygon(PointF);
            }
            polygonSelectionPointF = PointF;
        }

        public void UnselectVertex()
        {
            if (selectedVertex != null)
                selectedVertex.selected = false;
            selectedVertex = null;
        }

        public bool IsPointFInside(PointF PointF)
        {
            Edge e = startingVertex.nextE;
            bool isInside = false;
            PointF A = e.prevV.pos,
                  B = e.nextV.pos;
            for (int i = 0; i < vCount; i++)
            {

                if (e.relation != null && e.relation.name == "B")
                {
                    BezierRelation br = (BezierRelation)e.relation;

                    br.CalcAprox(br.GetControlPointFs(e));
                    foreach (PointF p in br.aprox)
                    {

                        B = p;
                        if (PointF.Y < A.Y != PointF.Y < B.Y)
                        {
                            double tx = (double)(B.X - A.X) / (B.Y - A.Y);
                            if (PointF.X < A.X + tx * (PointF.Y - A.Y))
                                isInside = !isInside;
                        }
                        A = B;
                    }
                    B = e.nextV.pos;
                }
                if (PointF.Y < A.Y != PointF.Y < B.Y)
                {
                    double tx = (double)(B.X - A.X) / (B.Y - A.Y);
                    if (PointF.X < A.X + tx * (PointF.Y - A.Y))
                        isInside = !isInside;
                }
                A = B;
                e = e.nextV.nextE;
                B = e.nextV.pos;
            }
            return isInside;
        }

        public void SelectPolygon(PointF PointF)
        {
            polygonSelectionPointF = PointF;
            polygonSelected = true;
        }



        public void UnselectPolygon()
        {
            polygonSelected = false;
        }

        public bool SelectedPolygon()
        {
            return polygonSelected;
        }

        public void MovePolygon(PointF PointF)
        {
            if (polygonSelectionPointF == null)
                return;

            PointF p = (PointF)polygonSelectionPointF,
                  diff = new PointF(PointF.X - p.X, PointF.Y - p.Y);

            polygonSelectionPointF = PointF;

            Vertex v = startingVertex;
            for (int i = 0; i < vCount; i++)
            {
                v.SetPos(new PointF(diff.X + v.pos.X, diff.Y + v.pos.Y));

                v = v.nextE.nextV;
            }
        }

        public bool AddRelationToContextSelectedEdge(Relation rel)
        {
            if (selectedEdge == null)
            {
                return false;
            }
            return selectedEdge.AddRelation(rel);
        }

        public void RemoveRelationFromContextSelectedEdge()
        {
            selectedEdge.RemoveRelation();
        }

        public int GetContextSelectedEdgeLength()
        {
            return selectedEdge.GetEdgeLength();
        }

        //selecting bezier control
        public bool SelectBezierControl(PointF PointF)
        {
            BezierControl? bc = FindCloseBezierControl(PointF);
            if (bc != null)
            {
                bc.selected = true;
                selectedBezierControl = bc;
                polygonSelectionPointF = PointF;
                return true;
            }
            return false;
        }

        public bool CanSelectBezierControl(PointF PointF)
        {
            return FindCloseVertex(PointF) != null;
        }

        private BezierControl? FindCloseBezierControl(PointF PointF)
        {
            Edge p = startingVertex.nextE;
            int rad = Vertex.size * Vertex.size;
            for (int i = 0; i < vCount; i++)
            {
                if (p.isBezier)
                {
                    //może if czy selected krawędź?
                    if (p.prevB.DistanceSQ(PointF) < rad)
                    {
                        return p.prevB;
                    }
                    if (p.nextB.DistanceSQ(PointF) < rad)
                    {
                        return p.nextB;
                    }
                }

                p = p.nextV.nextE;
            }
            return null;
        }
        public void UnselectBezierControl()
        {
            if (selectedBezierControl != null)
            {
                selectedBezierControl.selected = false;
            }
            selectedBezierControl = null;
        }
        public bool SelectedBezierControl()
        {
            return selectedBezierControl != null;
        }
        //moving bezier control
        public void MoveBezierControl(PointF PointF)
        {
            PointF previousBezierControlPointF = selectedBezierControl.GetPos();
            selectedBezierControl.SetPos(PointF);
            
            if (!RelationSolver.TrySolve(selectedBezierControl))
            {
                selectedBezierControl.SetPos(previousBezierControlPointF);
                MovePolygon(PointF);
            }
            
            polygonSelectionPointF = PointF;
        }

        public Vertex.Continuity GetContextSelectedVertexContiniuity()
        {
            return contextSelectedVertex.con;
        }

        public void SetContextSelectedVertexContiniuity(Vertex.Continuity con)
        {
            if (contextSelectedVertex == null)
                return;
            contextSelectedVertex.con = con;
            RelationSolver.FixBeziers(contextSelectedVertex);
        }

    }
}
