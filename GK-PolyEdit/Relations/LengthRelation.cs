using GK_PolyEdit.Polygon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace GK_PolyEdit.Relations
{
    class LengthRelation : Relation
    {

        public int value = 0;
        static double err = 2;
        public LengthRelation(int v)
        {
            name = "L";
            ex = false;

            value = v;
        }
        public override bool IsSatisfied(Vertex Caller, Vertex Other)
        {
            double dist = Math.Sqrt(Caller.DistanceSQ(Other.pos));
            return Math.Abs(dist-value) < err;
        }

        public override void Solve(Vertex Caller, Vertex Other)
        {
            double dx = Other.pos.X - Caller.pos.X;
            double dy = Other.pos.Y - Caller.pos.Y;
            double dist = Math.Sqrt(Caller.DistanceSQ(Other.pos));
            double scale = value / dist;

            double X = Caller.pos.X + dx * scale;
            double Y = Caller.pos.Y + dy * scale;


            PointF newOtherPos = new PointF((int)X, (int)Y);
            Other.SetPos(newOtherPos);
        }
    }
}
