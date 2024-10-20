using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace GK_PolyEdit.Polygon
{
    class Vertex
    {
        static int ID = 0;
        public int id;
        public Edge? nextE, prevE;
        public bool selected;

        public bool isStatic;

        public enum Continuity
        {
            G0,
            G1,
            C1
        }
        public Continuity con;

        public PointF pos{ get; private set; }


        public Vertex(PointF pos)
        {
            id = ID++;
            this.pos = pos;
            nextE = null;
            prevE = null;  
            selected = false;
            con = Continuity.G1;
            isStatic = false;
        }

        public void SetPos(float x ,float y)
        {
            pos = new PointF(x,y);
        }
        public void SetPos(PointF PointF)
        {
            pos = PointF;
        }
        public void SetEdges(Edge prev, Edge next)
        {
            nextE = next;
            prevE = prev;
        }

        public static Color drawColor = Color.Blue;
        public static Color selectColor = Color.Red;
        public static int size = 7;

        public double DistanceSQ((int x, int y) PointF)
        {
            double dx = PointF.x - pos.X;
            double dy = PointF.y - pos.Y;
            return dx * dx + dy * dy;
        }
        public double DistanceSQ(PointF PointF)
        {
            double dx = PointF.X - pos.X;
            double dy = PointF.Y - pos.Y;
            return dx * dx + dy * dy;
        }


    }
}
