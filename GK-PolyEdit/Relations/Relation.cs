using GK_PolyEdit.Polygon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_PolyEdit.Relations
{
    abstract class Relation
    {
        // to identifiy between without reflection
        public string name;
        // to check if can be multiple in a row
        public bool ex;

        public abstract void Solve(Vertex Caller, Vertex Other);
        public abstract bool IsSatisfied(Vertex Caller, Vertex Other);
    }
}
