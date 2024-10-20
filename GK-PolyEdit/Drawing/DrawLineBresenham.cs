using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GK_PolyEdit.Drawing
{
    static class DrawLineBresenham
    {
        public static void DrawLine(DirectBitmap bmp,PointF A, PointF B,Pen pen)
        {
            
            int x1 = (int)A.X,
                x2 = (int)B.X, 
                y1 = (int)A.Y, 
                y2 = (int)B.Y;

            int dx, dy, g, h, c;

            dx = x2 - x1;
            if (dx > 0) 
                g = +1;
            else 
                g = -1;

            dx = Math.Abs(dx);

            dy = y2 - y1;

            if (dy > 0)
                h = +1; 
            else 
                h = -1;

            dy = Math.Abs(dy);

            if (dx > dy)
            {
                c = -dx;
                while (x1 != x2)
                {
                    if (x1 > 0 && x1 < bmp.Width && y1 < bmp.Height && y1 > 0)
                    {
                        bmp.SetPixel(x1, y1, pen.Color);
                        if(y1-1>0)
                            bmp.SetPixel(x1, y1-1, pen.Color);
                    }
                    c += 2 * dy;
                    if (c > 0) 
                    { 
                        y1 += h;
                        c -= 2 * dx;
                    }
                    x1 += g;
                }
            }
            else
            {
                c = -dy;
                while (y1 != y2)
                {
                    if (x1 > 0 && x1 < bmp.Width && y1 < bmp.Height && y1 > 0)
                    {
                        bmp.SetPixel(x1, y1, pen.Color);
                        if (x1 - 1 >0)
                            bmp.SetPixel(x1 - 1, y1, pen.Color);
                    }
                    c += 2 * dx;
                    if (c > 0) 
                    { 
                        x1 += g;
                        c -= 2 * dy;
                    }
                    y1 += h;
                }
            }
        }
    
    }
}
