using GK_PolyEdit.Polygon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GK_PolyEdit.Drawing;

namespace GK_PolyEdit.Relations
{
    class BezierRelation : Relation
    {
        public PointF[] aprox;
        public BezierControl prevB, nextB;
        
        public BezierRelation() 
        {
            name = "B";
            ex = false;
        }

        public void SetBezierControl(BezierControl prevB, BezierControl nextB)
        {
            this.nextB = nextB;
            this.prevB = prevB;
        }

        public override bool IsSatisfied(Vertex Caller, Vertex Other)
        {
            if (Caller.con == Vertex.Continuity.G0)
                return true;

            BezierControl bc;
            if (Caller.id == prevB.GetParentVertex().id)
                bc = prevB;
            else
                bc = nextB;


            if (!bc.selected)
                return true;

            Edge opEdge = bc.prevSide ? Caller.prevE : Caller.nextE;
            
            if (opEdge.isBezier)
                return true;

            PointF A = bc.GetPos();
            PointF B = Caller.pos;
            PointF C = Other.pos;

            if(Caller.con == Vertex.Continuity.G1)
                return ArePointFsColinear(A, B, C);

            //no need to check C1 because other PointF can only be in one place then
            return true;
        }

        public override void Solve(Vertex Caller, Vertex Other)
        {
            //for now it only needs to fix G1 cont


            BezierControl bc;
            if (Caller.id == prevB.GetParentVertex().id)
                bc = prevB;
            else
                bc = nextB;

            Edge opEdge = bc.prevSide ? Caller.prevE : Caller.nextE;

            PointF A = bc.GetPos();
            PointF B = Caller.pos;
            PointF C = Other.pos;

            

            Other.SetPos(G1PointF(A,B,C,SolveOption.BezierToVertex));

            return;
        }
        public void SolveForBezier(BezierControl calB)
        {
            if (calB.selected)
                return;
            Vertex p = calB.GetParentVertex();
            Edge opEdge = calB.prevSide ? p.prevE : p.nextE;
            PointF B = p.pos;
            if (opEdge.isBezier)
            {
                BezierControl opB = calB.prevSide ? p.prevE.nextB : p.nextE.prevB;
                PointF set = calB.GetPos();
                if(p.con == Vertex.Continuity.G1)
                {
                    set = G1PointF(opB.GetPos(), B, calB.GetPos(),SolveOption.BezierToBezier);
                }
                else if (p.con == Vertex.Continuity.G0)
                {
                    set = G0PointF(opB.GetPos(), B, calB.GetPos(), SolveOption.BezierToBezier);
                }
                else if (p.con == Vertex.Continuity.C1)
                {
                    set = C1PointF(opB.GetPos(), B, calB.GetPos(), SolveOption.BezierToBezier);
                }
                calB.SetPos(set);
            }
            else
            {
                Vertex opV = calB.prevSide ? p.prevE.prevV : p.nextE.nextV;
                PointF set = calB.GetPos();
                if (p.con == Vertex.Continuity.G1)
                {
                    set = G1PointF(opV.pos, B, calB.GetPos(), SolveOption.VertexToBezier);
                }
                else if (p.con == Vertex.Continuity.G0)
                {
                    set = G0PointF(opV.pos, B, calB.GetPos(), SolveOption.VertexToBezier);
                }
                else if (p.con == Vertex.Continuity.C1)
                {
                    set = C1PointF(opV.pos, B, calB.GetPos(), SolveOption.VertexToBezier);
                }
                calB.SetPos(set);
            }
        }
        public void SolveFromBezier(BezierControl calB)
        {
            Vertex p = calB.GetParentVertex();
            p.isStatic = true;
            Edge opEdge = calB.prevSide ? p.prevE : p.nextE;
            PointF B = p.pos;
            if(opEdge.isBezier)
            {
                BezierControl opB = calB.prevSide ? p.prevE.nextB : p.nextE.prevB;
                PointF set = calB.GetPos();
                if (p.con == Vertex.Continuity.G1)
                {
                    set = G1PointF(calB.GetPos(), B, opB.GetPos(), SolveOption.BezierToBezier);
                }
                else if (p.con == Vertex.Continuity.G0)
                {
                    set = G0PointF(calB.GetPos(), B, opB.GetPos(), SolveOption.BezierToBezier);
                }
                else if (p.con == Vertex.Continuity.C1)
                {
                    set = C1PointF(calB.GetPos(), B, opB.GetPos(), SolveOption.BezierToBezier);
                    
                }
                opB.SetPos(set);
            }
            else
            {
                Vertex opV = calB.prevSide ? p.prevE.prevV : p.nextE.nextV;
                PointF set = calB.GetPos();
                if (p.con == Vertex.Continuity.G1)
                {
                    set = G1PointF(calB.GetPos(), B, opV.pos, SolveOption.BezierToVertex);
                }
                else if (p.con == Vertex.Continuity.G0)
                {
                    set = G0PointF(calB.GetPos(), B, opV.pos, SolveOption.BezierToVertex);
                }
                else if (p.con == Vertex.Continuity.C1)
                {
                    set = C1PointF(calB.GetPos(), B, opV.pos, SolveOption.BezierToVertex);
                    opV.isStatic = true;
                }
                opV.SetPos(set);
            }
        }

        public bool ArePointFsColinear(PointF A, PointF B, PointF C)
        {
            double area = A.X * (B.Y - C.Y) + 
                       B.X * (C.Y - A.Y) + 
                       C.Y * (A.Y - B.Y);
            return Math.Abs(area) < 1e-9;
        }

        public void CalcAprox(PointF[] controls)
        {

            aprox = BezierDrawing.CalcBezierAprox(controls, 0.05);
        }

        public enum SolveOption
        {
            BezierToBezier,
            BezierToVertex,
            VertexToBezier
        }
        PointF C1PointF(PointF A, PointF MID, PointF B, SolveOption opt)
        {
            double X = 0, Y = 0;
            double dx = A.X - MID.X;
            double dy = A.Y - MID.Y;
            if (opt == SolveOption.BezierToBezier)
            {
                X = MID.X - (int)dx;
                Y = MID.Y - (int)dy;
            }
            else if(opt == SolveOption.BezierToVertex)
            {
                X = MID.X - (int)(3 * dx);
                Y = MID.Y - (int)(3 * dy);
            }
            else if(opt == SolveOption.VertexToBezier)
            {
                X = MID.X - (int)(dx / 3);
                Y = MID.Y - (int)(dy / 3);
            }
            return new PointF((float)X, (float)Y);
        }
        PointF G0PointF(PointF A, PointF MID, PointF B, SolveOption opt)
        {
            return B;
        }
        PointF G1PointF(PointF A, PointF MID, PointF B, SolveOption opt)
        {

            double angleAMID = Math.Atan2(MID.Y - A.Y, MID.X - A.X);

            double distanceMIDB = Math.Sqrt(Math.Pow(B.X - MID.X, 2) + Math.Pow(B.Y - MID.Y, 2));

            double X = MID.X + (distanceMIDB * Math.Cos(angleAMID));
            double Y = MID.Y + (distanceMIDB * Math.Sin(angleAMID));

            return new PointF((float)X, (float)Y);
        }


        public PointF[] GetControlPointFs(Edge e)
        {
            return new PointF[]{ e.prevV.pos, e.prevB.GetPos(), e.nextB.GetPos(),e.nextV.pos };
        }
    }
}
