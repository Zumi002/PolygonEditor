using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_PolyEdit.Polygon
{
    class BezierControl
    {
        public bool selected;
        public Edge parent;
        public bool prevSide;
        public PointF pos;

        public BezierControl(PointF pos, Edge parent, bool prevSide)
        {
            this.pos = pos;
            this.parent = parent;
            this.prevSide = prevSide;

            selected = false;
        }
        public static Color drawEdgeColor = Color.DarkGray;
        public static Color drawColor = Color.Gray;
        public static Color drawSelectColor = Color.DarkRed;
        public static int size = 7;

        public void SetPos(PointF PointF)
        {
            PointF parentPointF = GetParentPointF();
            pos = new PointF (PointF.X - parentPointF.X,PointF.Y-parentPointF.Y);
        }

        public PointF GetPos()
        {
            PointF parentPointF = GetParentPointF();
            return new PointF(parentPointF.X+pos.X,parentPointF.Y+pos.Y);
        }

        PointF GetParentPointF()
        {
            PointF parentPointF;
            if (prevSide)
            {
                parentPointF = parent.prevV.pos;
            }
            else
            {
                parentPointF = parent.nextV.pos;
            }
            return parentPointF;
        }
        
        //get parent vertex
        public Vertex GetParentVertex()
        {
            return prevSide?parent.prevV:parent.nextV;
        }

        public double DistanceSQ(PointF PointF)
        {
            PointF p = GetPos();
            double dx = PointF.X - p.X;
            double dy = PointF.Y - p.Y;
            return dx * dx + dy * dy;
        }
    }
}
