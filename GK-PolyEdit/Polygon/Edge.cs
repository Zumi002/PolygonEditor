using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GK_PolyEdit.Relations;

namespace GK_PolyEdit.Polygon
{
    class Edge
    {
        static int ID = 0;
        public int id;
        
        public Vertex nextV, prevV;
        public Relation? relation;

        public bool isBezier;

        public BezierControl? prevB, nextB;

        public Edge(Vertex prev,Vertex next)
        {
            nextV = next;
            prevV = prev;
            this.id = ID++;
            prev.SetEdges(prev.prevE, this);
            next.SetEdges(this, next.nextE);
            relation = null;
            isBezier = false;
        }


        public double DistanceSQ((double x , double y) PointF)
        {
            //from https://paulbourke.net/geometry/PointFlineplane/

            PointF proj = ProjectionOnEdge(new PointF((float)PointF.x, (float)PointF.y));

            double dx = proj.X - PointF.x;
            double dy = proj.Y - PointF.y;   

            return dx * dx + dy * dy;
        }
        public double DistanceSQ(PointF PointF)
        {
            return DistanceSQ((PointF.X, PointF.Y));
        }

        public static double DistanceSQ(PointF PointF, PointF A, PointF B)
        {
            PointF proj = ProjectionOnEdge(PointF,A,B);

            double dx = proj.X - PointF.X;
            double dy = proj.Y - PointF.Y;

            return dx * dx + dy * dy;
        }

        public static PointF ProjectionOnEdge(PointF PointF, PointF A, PointF B)
        {
            double px = B.X - A.X;
            double py = B.Y - A.Y;

            double norm = (px * px + py * py);
            double u = ((PointF.X - A.X) * px + (PointF.Y - A.Y) * py) / norm;
            if (u > 1)
            {
                u = 1;
            }
            else if (u < 0)
            {
                u = 0;
            }

            double x = A.X + px * u;
            double y = A.Y + py * u;

            return new PointF((int)x, (int)y);
        }

        public PointF ProjectionOnEdge(PointF PointF)
        {
            PointF A = nextV.pos;
            PointF B = prevV.pos;

            double px = B.X - A.X;
            double py = B.Y - A.Y;

            double norm = (px * px + py * py);
            double u = ((PointF.X - A.X) * px + (PointF.Y - A.Y) * py) / (double)norm;
            if (u > 1)
            {
                u = 1;
            }
            else if (u < 0)
            {
                u = 0;
            }
            
            double x = A.X + px * u;
            double y = A.Y + py * u;

            return new PointF((int)x,(int)y);
        }

        public bool AddRelation(Relation rel)
        {
            Relation? prevRel = relation;
            relation = rel;
            if (relation.name == "B")
            {
                PointF defaultPos = new PointF(50, 50);

                BezierControl A = new BezierControl(defaultPos, this,true),
                              B = new BezierControl(defaultPos, this,false);
                prevB = A;
                nextB = B;
                ((BezierRelation)relation).SetBezierControl(A, B);
                isBezier = true;
            }
            bool canHaveRelation = (!rel.ex)||
                                   ((prevV.prevE.relation==null||prevV.prevE.relation.name != rel.name) &&
                                   (nextV.nextE.relation==null||nextV.nextE.relation.name != rel.name));
            if (canHaveRelation && RelationSolver.TrySolve(prevV))
            {
                isBezier = relation.name == "B";
                return true;
            }
            isBezier = false;
            relation = prevRel;
            if (relation!=null && relation.name == "B")
            {
                isBezier = true;
            }
            return false;
        }

        public void RemoveRelation()
        {
            isBezier = false;
            relation = null;
        }

        public int GetEdgeLength()
        {
            return (int)Math.Sqrt(prevV.DistanceSQ(nextV.pos));
        }
    }
}
