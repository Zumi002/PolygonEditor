using GK_PolyEdit.Polygon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_PolyEdit.Relations
{
    class HorizontalRelation : Relation
    {
        public HorizontalRelation() 
        {
            name = "H";
            ex = true;
        }

        public override void Solve(Vertex caller, Vertex other)
        {
            other.SetPos(other.pos.X, caller.pos.Y);
        }

        public override bool IsSatisfied(Vertex caller, Vertex other)
        {
            return caller.pos.Y == other.pos.Y;
        }
    }
}
