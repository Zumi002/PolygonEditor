using GK_PolyEdit.Polygon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_PolyEdit.Relations
{
    class VerticalRelation : Relation
    {
        public VerticalRelation()
        {
            name = "V";
            ex = true;
        }

        public override void Solve(Vertex caller, Vertex other)
        {
            other.SetPos(caller.pos.X, other.pos.Y);
        }

        public override bool IsSatisfied(Vertex caller, Vertex other)
        {
            return caller.pos.X == other.pos.X;
        }
    }
}
