using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace GK_PolyEdit.Drawing
{
    static class BezierDrawing
    {
        static PointF Lerp(PointF a, PointF b, double t)
        {
            float dx = b.X - a.X;
            float dy = b.Y - a.Y;

            double X = a.X + dx * t;
            double Y = a.Y + dy * t;

            return new PointF((int)X, (int)Y);
        }
        static public PointF CalcBezier(PointF[] controls, double t)
        {
            double u = 1 - t;
            double tt = t * t;
            double uu = u * u;
            double uuu = uu * u;
            double ttt = tt * t;

            float x = (float)(uuu * controls[0].X + 3 * uu * t * controls[1].X + 3 * u * tt * controls[2].X + ttt * controls[3].X);
            float y = (float)(uuu * controls[0].Y + 3 * uu * t * controls[1].Y + 3 * u * tt * controls[2].Y + ttt * controls[3].Y);

            return new PointF(x, y);
        
        }

        public static PointF[] CalcBezierAprox(PointF[] controls, double step)
        {
            double t = 0;
            double step2 = step * step;
            double step3 = step2 * step;
            List<PointF> list = new List<PointF>();
            double[] AX = { controls[0].X,
                            3*(controls[1].X-controls[0].X),
                            3*(controls[2].X-2*controls[1].X+controls[0].X),
                            controls[3].X-3*controls[2].X+3*controls[1].X-controls[0].X };
            double[] AY = { controls[0].Y,
                            3*(controls[1].Y-controls[0].Y),
                            3*(controls[2].Y-2*controls[1].Y+controls[0].Y),
                            controls[3].Y-3*controls[2].Y+3*controls[1].Y-controls[0].Y };
            double[] PX = { AX[0],
                            AX[3]*step3+AX[2]*step2+AX[1]*step,
                            6*AX[3]*step3+2*AX[2]*step2,
                            6*AX[3]*step3 };
            double[] PY = { AY[0],
                            AY[3]*step3+AY[2]*step2+AY[1]*step,
                            6*AY[3]*step3+2*AY[2]*step2,
                            6*AY[3]*step3 };
            while (t <= 1)
            {
                PointF D = new PointF((float)PX[0], (float)PY[0]);
                t += step;
                PX[0] += PX[1];
                PX[1] += PX[2];
                PX[2] += PX[3];
                PY[0] += PY[1];
                PY[1] += PY[2];
                PY[2] += PY[3];
                list.Add(D);
            }
            return list.ToArray();
        }

        public static void DrawBezier(PointF[] controls, DirectBitmap bmp, Color color)
        {
           
            double t = 0;
            double step = 0.001;
            double step2 = step * step;
            double step3 = step2 * step;  
            
            double[] AX = { controls[0].X,
                            3*(controls[1].X-controls[0].X),
                            3*(controls[2].X-2*controls[1].X+controls[0].X),
                            controls[3].X-3*controls[2].X+3*controls[1].X-controls[0].X };
            double[] AY = { controls[0].Y,
                            3*(controls[1].Y-controls[0].Y),
                            3*(controls[2].Y-2*controls[1].Y+controls[0].Y),
                            controls[3].Y-3*controls[2].Y+3*controls[1].Y-controls[0].Y };
            double[] PX = { AX[0],
                            AX[3]*step3+AX[2]*step2+AX[1]*step,
                            6*AX[3]*step3+2*AX[2]*step2,
                            6*AX[3]*step3 };
            double[] PY = { AY[0],
                            AY[3]*step3+AY[2]*step2+AY[1]*step,
                            6*AY[3]*step3+2*AY[2]*step2,
                            6*AY[3]*step3 };
            while (t<=1)
            {
                PointF D = new PointF((float)PX[0], (float)PY[0]);
                t += step;
                PX[0] += PX[1];
                PX[1] += PX[2];
                PX[2] += PX[3];
                PY[0] += PY[1];
                PY[1] += PY[2];
                PY[2] += PY[3];
                if (D.X < 0||D.X+1>=bmp.Width||D.Y<0||D.Y>=bmp.Height)
                    continue;
                bmp.SetPixel((int)D.X, (int)D.Y, color);
                bmp.SetPixel((int)D.X+1, (int)D.Y, color);   
            }

            
        }
    }
}
