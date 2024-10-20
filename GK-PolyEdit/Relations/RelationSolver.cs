using GK_PolyEdit.Polygon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_PolyEdit.Relations
{
    static class RelationSolver
    {
        public static List<PointF> previousPosition = new List<PointF>();
        public static bool TrySolve(Vertex fromVertex)
        {

            SaveLastPos(fromVertex);
            return SolveCore(fromVertex); ;
        }

        public static void SaveLastPos(Vertex fromVertex)
        {

            previousPosition = new List<PointF>();
            Vertex p = fromVertex;
            do
            {
                previousPosition.Add(p.pos);
                p = p.nextE.nextV;
            }
            while (p.id != fromVertex.id);
        }

        public static bool SolveCore(Vertex fromVertex)
        {
            Queue<Vertex> queue = new Queue<Vertex>();

            queue.Enqueue(fromVertex);
            int fixes = 0;
            while (queue.Count > 0)
            {
                Vertex v = queue.Dequeue();
                if (v.nextE.relation != null && !v.nextE.relation.IsSatisfied(v, v.nextE.nextV))
                {
                    Vertex fixing = v.nextE.isBezier ? v.prevE.prevV : v.nextE.nextV;
                    if (!v.nextE.nextV.selected && !v.nextE.nextV.isStatic) v.nextE.relation.Solve(v, fixing);
                    queue.Enqueue(fixing);
                    if (v.nextE.isBezier)
                        queue.Enqueue(v);
                }
                if (v.prevE.relation != null && !v.prevE.relation.IsSatisfied(v, v.prevE.prevV))
                {
                    Vertex fixing = v.prevE.isBezier ?v.nextE.nextV: v.prevE.prevV;
                    if (!v.prevE.prevV.selected && !v.prevE.prevV.isStatic) v.prevE.relation.Solve(v, fixing);
                    queue.Enqueue(fixing);
                    if (v.prevE.isBezier)
                        queue.Enqueue(v);
                }
                fixes++;
                if (fixes > 100)
                {
                    int i = 0;
                    Vertex p = fromVertex;
                    do
                    {
                        p.SetPos(previousPosition[i]);
                        p = p.nextE.nextV;
                        i++;
                        p.isStatic = false;
                    }
                    while (p.id != fromVertex.id);
                    return false;
                }
                
            }
            FixBeziers(fromVertex);
            return true;
        }

        public static bool TrySolve(BezierControl bc)
        {
            Vertex fromVertex = bc.GetParentVertex();
            fromVertex = bc.prevSide ? fromVertex.prevE.prevV : fromVertex.nextE.nextV;
            SaveLastPos(fromVertex);
            BezierRelation br = (BezierRelation)bc.parent.relation;
            br.SolveFromBezier(bc);
            
            return SolveCore(fromVertex);
        }

        public static void FixBeziers(Vertex fromVertex)
        {
            Vertex p = fromVertex;
            do
            {
                if (p.nextE.isBezier)
                {
                    BezierRelation br = (BezierRelation)p.nextE.relation;
                    br.SolveForBezier(p.nextE.nextB);
                    br.SolveForBezier(p.nextE.prevB);
                }
                p.isStatic = false;
                p = p.nextE.nextV;
            }
            while (p.id != fromVertex.id);
        }
    }
}
