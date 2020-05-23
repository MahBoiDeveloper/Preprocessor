using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Tao.OpenGl;
using Tao.Platform.Windows;
using ModelComponents;

namespace PreprocessorUtils
{
    public enum GradationType
    {
        Angle, Square
    }

    public class Visualizer
    {
        private struct DrawingOperation
        {
            public int paramsList;
            public int mode;
            public float[] pts;
            public float[] cls;
            public int count;

            public DrawingOperation(int drawMode, float[] vertexCordArray, int paramsListId, float[] vertexColorArray = null)
            {
                cls = vertexColorArray;
                paramsList = paramsListId;
                mode = drawMode;
                count = vertexCordArray.Length / 2;
                pts = vertexCordArray;
            }
        }

        private struct TexturedDrawingOperation
        {
            public int texId;
            public float[] pts, tex;
            public int count;
            public TexturedDrawingOperation(int textureId, float[] vertexCordArray, float[] textureCordArray)
            {
                texId = textureId;
                count = vertexCordArray.Length / 2;
                pts = vertexCordArray;
                tex = textureCordArray;
            }
        }
        private const int defaultWidth = 500, defaultHeight = 500;
        private SimpleOpenGlControl drawArea;
        private RectangleF borders = new RectangleF(-10, -10, 20, 20);
        private float proportion;
        public float scale = 1.0f;
        private float offsetx = 0, offsety = 0;
        private double pixelsPerUnit;
        List<float> PCbuf = new List<float>();
        List<float> TXbuf = new List<float>();
        float textProportion = 30f / 15f;
        private Dictionary<Color, int> letters = new Dictionary<Color, int>();
        private Dictionary<char, int> symOffset = new Dictionary<char, int>();
        List<DrawingOperation> drawBuffer = new List<DrawingOperation>();
        List<TexturedDrawingOperation> texturedBuffer = new List<TexturedDrawingOperation>();
        Dictionary<int, Color> matColors = new Dictionary<int, Color>();
        public double pointLocality
        {
            get
            {
                return (7.0) / this.pixelsPerUnit;
            }
        }



        public Visualizer(SimpleOpenGlControl drawArea)
        {
            this.drawArea = drawArea;
            drawArea.InitializeContexts();
            SetViewPort();
            EnableSmooth();
            RefreshBorders();
            for (char s = '0'; s <= '9'; s++)
                symOffset[s] = ((int)s - 48);
            symOffset['p'] = 10;
            symOffset['L'] = 11;
            symOffset['z'] = 12;
            symOffset['n'] = 13;
            symOffset[','] = 14;
        }

        public void DrawBounds(MyNode[] nodes, Color color)
        {
            int count = nodes.Length;
            if (count == 0) return;
            List<MyStraightLine> lines = new List<MyStraightLine>();
            for (int i = 0; i < count; i++)
            {
                MyNode node = nodes[i];
                double pointDiameter = 0.5 * scale;
                // т.к. сюда передаются координаты центра точки, то надо их преобразовать к верхнему левому углу прямоугольника, описывающего эллипс. .NET не умеет рисовать окржуности - что бы нарисовать окружность, мы испльзуем метод DrawEllipse
                // данный метод принимает два параметра - Pen и Rectangle. 
                DrawCircle(new MyCircle(0, new MyPoint(node.X, node.Y, MyPoint.PointType.IsGeometryPoint), (pointDiameter / 2)), color, false, 1.0);

                switch (node.BoundType)
                {
                    case 10: // закрепелен по Х                    
                        //Рисуем линию рядом с кружочком             
                        lines.Add(new MyStraightLine(0, new MyPoint(node.X - pointDiameter / 2, node.Y + pointDiameter / 2, MyPoint.PointType.IsGeometryPoint),
                            new MyPoint(node.X - pointDiameter / 2, node.Y - pointDiameter / 2, MyPoint.PointType.IsGeometryPoint)));

                        //Рисуем штриховочку под линией               
                        for (int j = 0; j < 5; j++)
                        {
                            lines.Add(new MyStraightLine(0, new MyPoint(node.X - pointDiameter / 2, node.Y + pointDiameter / 2 - pointDiameter / 4 * j, MyPoint.PointType.IsGeometryPoint),
                            new MyPoint(node.X - pointDiameter / 2 - pointDiameter / 4, node.Y + pointDiameter / 2 - pointDiameter / 4 * (j - 1), MyPoint.PointType.IsGeometryPoint)));
                        }
                        break;
                    case 1: // закрепелен по У
                        //Рисуем линию под кружочком
                        lines.Add(new MyStraightLine(0, new MyPoint(node.X - pointDiameter / 2, node.Y - pointDiameter / 2, MyPoint.PointType.IsGeometryPoint),
                           new MyPoint(node.X + pointDiameter / 2, node.Y - pointDiameter / 2, MyPoint.PointType.IsGeometryPoint)));

                        //Рисуем штриховочку под линией

                        for (int j = 0; j < 5; j++)
                        {
                            lines.Add(new MyStraightLine(0, new MyPoint(node.X - pointDiameter / 2 + pointDiameter / 4 * (j - 1), node.Y - pointDiameter / 2 - pointDiameter / 4, MyPoint.PointType.IsGeometryPoint),
                            new MyPoint(node.X - pointDiameter / 2 + pointDiameter / 4 * j, node.Y - pointDiameter / 2, MyPoint.PointType.IsGeometryPoint)));
                        }
                        break;
                    case 11:// закрепелен по Х и У
                        //Рисуем линию под кружочком
                        lines.Add(new MyStraightLine(0, new MyPoint(node.X - pointDiameter / 2, node.Y - pointDiameter, MyPoint.PointType.IsGeometryPoint),
                             new MyPoint(node.X + pointDiameter / 2, node.Y - pointDiameter, MyPoint.PointType.IsGeometryPoint)));

                        //Рисуем штриховочку под линией
                        for (int j = 0; j < 5; j++)
                        {
                            lines.Add(new MyStraightLine(0, new MyPoint(node.X - pointDiameter / 2 + pointDiameter / 4 * (j - 1), node.Y - pointDiameter / 2 - pointDiameter * 3.0 / 4.0, MyPoint.PointType.IsGeometryPoint),
                            new MyPoint(node.X - pointDiameter / 2 + pointDiameter / 4 * j, node.Y - pointDiameter, MyPoint.PointType.IsGeometryPoint)));
                        }
                        // рисуем две линии-опоры
                        lines.Add(new MyStraightLine(0, new MyPoint(node.X - pointDiameter / 2, node.Y - pointDiameter, MyPoint.PointType.IsGeometryPoint),
                        new MyPoint(node.X, node.Y, MyPoint.PointType.IsGeometryPoint)));
                        lines.Add(new MyStraightLine(0, new MyPoint(node.X + pointDiameter / 2, node.Y - pointDiameter, MyPoint.PointType.IsGeometryPoint),
                        new MyPoint(node.X, node.Y, MyPoint.PointType.IsGeometryPoint)));
                        break;
                }
            }
            DrawLinesArray(lines.ToArray(), color, false, false, 1.0);
        }

        private double ArcAngle(MyArc arc, MyPoint point) // функция вычисляет угол между осью Х и прямой проходящей через центр дуги и точку на дуге. точка должна лежать на дуге
        {
            double angle = 0;
            MyPoint centerPoint = arc.CenterPoint;
            angle = Math.Atan2(point.Y - centerPoint.Y, point.X - centerPoint.X);
            return angle;
        }

        public Bitmap GetImage()
        {
            Bitmap bmp = new Bitmap(drawArea.Width, drawArea.Height);
            System.Drawing.Imaging.BitmapData data = bmp.LockBits(new Rectangle(new Point(0, 0), bmp.Size), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Gl.glReadPixels(0, 0, drawArea.Width, drawArea.Height, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, data.Scan0);
            bmp.UnlockBits(data);
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return bmp;
        }

        private double PointsDistance(MyPoint p1, MyPoint p2)
        {
            return (double)Math.Pow((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y), 0.5);
        }

        private double PointsDistance(double x1, double y1, double x2, double y2)
        {
            return (double)Math.Pow((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2), 0.5);
        }

        public void DrawArc(MyArc arc, Color color, bool drawId = true, double width = 2.0)
        {
            MyPoint pStart = arc.StartPoint, pEnd = arc.EndPoint, pCenter = arc.CenterPoint;
            if (!arc.Clockwise)
            {
                MyPoint p = pStart;
                pStart = pEnd;
                pEnd = p;
            }
            List<float> nodes = new List<float>();
            float radius = (float)PointsDistance(pCenter, pEnd);
            float startAngle = (float)ArcAngle(arc, pEnd);
            if (startAngle < 0) startAngle += 2.0f * (float)Math.PI;
            float endAngle = (float)ArcAngle(arc, pStart);
            if (endAngle < 0) endAngle += 2.0f * (float)Math.PI;

            float h = (endAngle < startAngle) ? (float)(Math.PI * 2.0 - startAngle + endAngle) : (float)(endAngle - startAngle);

            double[] colArray = colorArray(color);
            int list = Gl.glGenLists(1);
            Gl.glNewList(list, Gl.GL_COMPILE);
            Gl.glLineWidth((float)width);
            Gl.glColor3dv(colArray);
            Gl.glEndList();
            h /= 50f;
            int i = 0;
            for (float angle = startAngle; i <= 50; angle += h, i++)
            {
                if (angle > 2.0 * Math.PI) angle -= (float)(2.0 * Math.PI);
                if (i == 25)
                    if (drawId)
                        DrawString("L" + arc.Id.ToString(), (float)(pCenter.X + (radius) * Math.Cos(angle)), (float)(pCenter.Y + (radius) * Math.Sin(angle)));

                nodes.Add((float)(pCenter.X + radius * Math.Cos(angle)));
                nodes.Add((float)(pCenter.Y + radius * Math.Sin(angle)));
            }
            nodes.Add((float)(pCenter.X + radius * Math.Cos(endAngle)));
            nodes.Add((float)(pCenter.Y + radius * Math.Sin(endAngle)));
            if (drawId) FlushText(color);
            float[] pts = nodes.ToArray();
            int count = nodes.Count / 2;
            drawBuffer.Add(new DrawingOperation(Gl.GL_LINE_STRIP, pts, list));
        }

        public void createText(Color color)
        {
            int texId;
            Bitmap text = new Bitmap(256, 32);
            Graphics g = Graphics.FromImage(text);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.Clear(Color.Transparent);
            g.DrawString("0123456789pLzn,", new Font(FontFamily.GenericMonospace, 20.8f, FontStyle.Bold), new SolidBrush(color), new PointF(-5f, 0f));
            g.Dispose();


            Gl.glGenTextures(1, out texId);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texId);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);

            // Upload the Bitmap to OpenGL.
            // Do this only when text changes.
            BitmapData data = text.LockBits(new Rectangle(0, 0, text.Width, text.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, text.Width, text.Height, 0, Gl.GL_BGRA, Gl.GL_UNSIGNED_BYTE, data.Scan0);
            text.UnlockBits(data);
            letters.Add(color, texId);
        }

        public void DropArrays()
        {
            int[] lists = drawBuffer.ConvertAll(dOp => dOp.paramsList).ToArray();
            foreach (int id in lists) Gl.glDeleteLists(id, 1);
            texturedBuffer.Clear();
            drawBuffer.Clear();
        }


        public void DrawForces(MyFiniteElementModel model, double scale, Color color, bool showValues = true)
        {
            MyNode[] forcedNodes = model.Nodes.FindAll(n => n.ForceX != 0.0 || n.ForceY != 0.0).ToArray();
            int nodesCount = forcedNodes.Length;
            if (nodesCount == 0) return;
            int count = 6 * nodesCount;
            float[] pts = new float[count * 2];
            Gl.glColor3dv(colorArray(color));

            for (int i = 0; i < nodesCount; i++)
            {
                MyNode node = forcedNodes[i];
                // силу будем рисовать, как линию со стрелочкой на конце. Если сила > 0, то она идет вверх или вправо ( зависит от нагрузки - по X или по Y) 
                // плюс, надо еще масштабировать длину стрелки
                // координаты начальной и конечной точки в пикселях

                float forceLengthX = (float)node.ForceX;
                float forceLengthY = (float)node.ForceY;
                float x1 = (float)node.X;
                float y1 = (float)node.Y;
                float x2 = x1 + forceLengthX * (float)scale; // масштабируем длину стрелки
                float y2 = y1 + forceLengthY * (float)scale; // масштабируем длину стрелки

                pts[(i * 12)] = x1;
                pts[(i * 12) + 1] = y1;
                pts[(i * 12) + 2] = x2;
                pts[(i * 12) + 3] = y2;

                // вычисляем угол нагрузки
                float alfa = (float)Math.Atan2(y2 - y1, x2 - x1);
                // длина стрелочки
                float aSize = (float)Math.Pow(((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)), 0.5) / 10.0f;

                float px1 = x2;
                float py1 = y2;
                float px2 = (float)(x2 - aSize * Math.Cos(alfa + 15.0 * (Math.PI / 180.0)));
                float py2 = (float)(y2 - aSize * Math.Sin(alfa + 15.0 * (Math.PI / 180.0)));
                float px3 = (float)(x2 - aSize * Math.Cos(alfa - 15.0 * (Math.PI / 180.0)));
                float py3 = (float)(y2 - aSize * Math.Sin(alfa - 15.0 * (Math.PI / 180.0)));

                pts[(i * 12) + 4] = px1;
                pts[(i * 12) + 5] = py1;
                pts[(i * 12) + 6] = px2;
                pts[(i * 12) + 7] = py2;

                pts[(i * 12) + 8] = px1;
                pts[(i * 12) + 9] = py1;
                pts[(i * 12) + 10] = px3;
                pts[(i * 12) + 11] = py3;

                if (showValues)
                {
                    float f = (float)Math.Pow((Math.Pow(node.ForceX, 2) + Math.Pow(node.ForceY, 2)), 0.5);
                    double fRound = Mathematics.floor(f, Mathematics.accuracy_medium);
                    DrawString(fRound.ToString(), x2, y2, true);
                }
            }
            int list;
            list = Gl.glGenLists(1);
            Gl.glNewList(list, Gl.GL_COMPILE);
            Gl.glColor3dv(colorArray(color));
            Gl.glLineWidth(1.0f);
            Gl.glEndList();
            drawBuffer.Add(new DrawingOperation(Gl.GL_LINES, pts, list));
            if (showValues) FlushText(color);
        }

        public void DrawDensityPoints(DensityPoint[] points, Color color, double pSize = 5.0, double cWidth = 1.0)
        {
            int count = points.Length;
            if (count == 0) return;
            float[] pts = new float[count * 2];

            double[] vColors = colorArray(color);
            int list = Gl.glGenLists(1);
            Gl.glNewList(list, Gl.GL_COMPILE);
            Gl.glColor3dv(vColors);
            Gl.glPointSize((float)(pSize));
            Gl.glEndList();
            for (int i = 0; i < count; i++)
            {
                pts[i * 2] = (float)points[i].X;
                pts[i * 2 + 1] = (float)points[i].Y;
                DrawString(points[i].val.ToString(), (float)points[i].X, (float)points[i].Y);
                DrawCircle(new MyCircle(0, new MyPoint(points[i].X, points[i].Y), points[i].R), color, false, cWidth);
            }
            drawBuffer.Add(new DrawingOperation(Gl.GL_POINTS, pts, list));
            FlushText(color);
        }

        public void Flush()
        {
            Gl.glFlush();
            drawArea.SwapBuffers();
            //drawArea.Invalidate();
            //System.GC.Collect(0, GCCollectionMode.Forced);
        }

        public void DrawGrid()
        {
            Gl.glDisable(Gl.GL_BLEND);
            Gl.glDisable(Gl.GL_LINE_SMOOTH);

            double[] bcolor = colorArray(Color.Black);
            double[] gcolor = colorArray(Color.Gray);

            float[] apts = { borders.Left, 0, borders.Right, 0, 0, borders.Bottom, 0, borders.Top };

            int hcount = ((int)borders.Bottom - (int)borders.Top + 1) * 2;
            int vcount = ((int)borders.Right - (int)borders.Left + 1) * 2;

            float[] gpts = new float[(hcount + vcount) * 2];

            int k = 0;
            for (int i = (int)borders.Left; i <= (int)borders.Right; i++)
            {
                gpts[k++] = i; gpts[k++] = borders.Top;
                gpts[k++] = i; gpts[k++] = borders.Bottom;
            }

            for (int i = (int)borders.Top; i <= (int)borders.Bottom; i++)
            {
                gpts[k++] = borders.Left; gpts[k++] = i;
                gpts[k++] = borders.Right; gpts[k++] = i;
            }
            Gl.glLineWidth(2.0f);
            Gl.glColor3dv(bcolor);
            DrawArray(apts, Gl.GL_LINES);
            Gl.glColor3dv(gcolor);
            Gl.glLineWidth(1.0f);
            DrawArray(gpts, Gl.GL_LINES);
            Gl.glEnable(Gl.GL_LINE_SMOOTH);
            Gl.glEnable(Gl.GL_BLEND);
        }

        public void DrawRecord(bool showGrid = false)
        {
            Cls();
            EnableSmooth();
            if (showGrid) DrawGrid();
            Gl.glDrawBuffer(Gl.GL_BACK);
            if (drawBuffer.Count > 0)
            {
                Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
                foreach (DrawingOperation drawOp in drawBuffer)
                {
                    int list = drawOp.paramsList;
                    Gl.glCallList(list);
                    Gl.glVertexPointer(2, Gl.GL_FLOAT, 0, drawOp.pts);
                    if (drawOp.cls != null)
                    {
                        Gl.glColorPointer(3, Gl.GL_FLOAT, 0, drawOp.cls);
                        Gl.glEnableClientState(Gl.GL_COLOR_ARRAY);
                    }
                    Gl.glDrawArrays(drawOp.mode, 0, drawOp.count);
                    if (drawOp.cls != null)
                        Gl.glDisableClientState(Gl.GL_COLOR_ARRAY);
                }
                Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
            }

            if (texturedBuffer.Count > 0)
            {
                double[] colorCash = new double[3];
                Gl.glEnable(Gl.GL_TEXTURE_2D);

                Gl.glGetDoublev(Gl.GL_COLOR_ARRAY, colorCash);
                Gl.glColor3dv(colorArray(Color.White));
                //Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
                Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
                Gl.glEnableClientState(Gl.GL_TEXTURE_COORD_ARRAY);
                foreach (TexturedDrawingOperation tOp in texturedBuffer)
                {

                    Gl.glBindTexture(Gl.GL_TEXTURE_2D, tOp.texId);
                    Gl.glVertexPointer(2, Gl.GL_FLOAT, 0, tOp.pts);
                    Gl.glTexCoordPointer(2, Gl.GL_FLOAT, 0, tOp.tex);
                    Gl.glDrawArrays(Gl.GL_QUADS, 0, tOp.count);
                }
                Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
                Gl.glDisableClientState(Gl.GL_TEXTURE_COORD_ARRAY);
                Gl.glColor3dv(colorCash);
                Gl.glDisable(Gl.GL_TEXTURE_2D);
            }
            Flush();
        }

        public void RefreshBorders()
        {
            drawArea.MakeCurrent();
            SetViewPort();
            double scaleX = (double)drawArea.Width / defaultWidth;
            double scaleY = (double)drawArea.Height / defaultHeight;
            proportion = (float)(scaleX / scaleY);
            borders.Y = (float)((-10.0 * scaleY * scale) + offsety);
            borders.Height = (float)(20.0 * scaleY * scale);
            borders.X = (float)(-10.0 * scaleX * scale + offsetx);
            borders.Width = (float)(20.0 * scaleX * scale);
            // настройка проекции
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            // теперь необходимо корректно настроить 2D ортогональную проекцию
            // в зависимости от того, какая сторона больше
            // мы немного варьируем то, как будет сконфигурированный настройки проекции

            Glu.gluOrtho2D(borders.Left, borders.Right, borders.Top, borders.Bottom);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            pixelsPerUnit = ((double)drawArea.Height / borders.Height);
        }

        public void SetViewPort()
        {
            // очистка окна
            Gl.glClearColor(255, 255, 255, 1);

            // установка порта вывода в соотвествии с размерами элемента drawArea
            Gl.glViewport(0, 0, drawArea.Width, drawArea.Height);
        }

        private void EnableSmooth()
        {
            Gl.glEnable(Gl.GL_POINT_SMOOTH);
            Gl.glEnable(Gl.GL_LINE_SMOOTH);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glHint(Gl.GL_POINT_SMOOTH_HINT, Gl.GL_NICEST);
            Gl.glHint(Gl.GL_LINE_SMOOTH_HINT, Gl.GL_NICEST);
        }

        public void Cls()
        {
            // очищаем буфер цвета
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
        }

        private void DrawArray(float[] pts, int mode)
        {
            int count = pts.Length / 2;

            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);

            // передаем массивы вершин и цветов, указывая количество элементов массива, приходящихся
            // на один визуализируемый элемент (в случае точек - 2 координаты: х и у, в случае цветов - 3 составляющие цвета)

            Gl.glVertexPointer(2, Gl.GL_FLOAT, 0, pts);

            // вызываем функцию glDrawArrays которая позволит нам визуализировать наши массивы, передоав их целиком
            // а не передавая в цикле каждую точку
            Gl.glDrawArrays(mode, 0, count);

            // деактивируем режим использования массивов геометрии и цветов
            Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
        }

        public void MoveBy(int x, int y, bool drawGrid = true)
        {
            RefreshBorders();
            float offsetScaleX = ((float)borders.Width / drawArea.Width);
            float offsetScaleY = ((float)borders.Height / drawArea.Height);
            offsetx -= (float)(x * offsetScaleX);
            offsety += (float)(y * offsetScaleY);
            DrawRecord(drawGrid);
        }

        public PointF GetCenter()
        {
            return new PointF((borders.Right + borders.Left) / 2f, (borders.Top + borders.Bottom) / 2f);
        }

        public void IncreaseScale()
        {
            float k;
            if (scale >= 1.0f) k = scale;
            else k = 1.0f / scale;
            IncreaseScale(ref k);
        }

        public void DecreaseScale()
        {
            float k;
            if (scale >= 1.0f) k = scale;
            else k = 1.0f / scale;
            DecreaseScale(ref  k);
        }

        private void IncreaseScale(ref float k)
        {
            k = (float)Mathematics.floor(k, 0.1);
            if (scale >= 1.0f) k += 0.1f;
            else k -= 0.1f;
            if (scale >= 1.0f) scale = k;
            else scale = 1.0f / k;
        }

        private void DecreaseScale(ref float k)
        {
            k = (float)Mathematics.floor(k, 0.1);
            if (scale > 1.0f) k -= 0.1f;
            else k += 0.1f;
            if (scale > 1.0f) scale = k;
            else scale = 1.0f / k;
        }

        public float ScaleToFit(double _width, double _height)
        {
            float width = (float)_width, height = (float)_height;
            if (width == 0 && height == 0) return 1.0f;
            float prevScale = 1;
            float k = (float)Mathematics.floor((scale >= 1.0f) ? scale : 1.0 / scale, 0.1);
            // максимизируем
            while (borders.Width < width || borders.Height < height)
            {
                prevScale = scale;
                IncreaseScale(ref k);
                RefreshBorders();
            }
            // минимизируем
            while (borders.Width > width && borders.Height > height)
            {
                prevScale = scale;
                DecreaseScale(ref k);
                RefreshBorders();
            }
            scale = prevScale;
            IncreaseScale(ref k);
            RefreshBorders();
            return scale;
        }

        public void MoveTo(double x, double y)
        {
            offsetx = (float)x;
            offsety = (float)y;
            RefreshBorders();
        }

        public double SetScale(float val)
        {
            double dif = scale / val;
            scale = val;
            RefreshBorders();
            return dif;
        }

        public void DrawCircle(MyCircle circle, Color color, bool drawId = false, double width = 2.0)
        {
            int count = 100;
            float[] pts = new float[count * 2];
            double[] colorCash = colorArray(color);
            double h = 2 * Math.PI / (count - 1);
            double alpha;
            for (int i = 0; i < count; i++)
            {
                alpha = h * i;
                pts[i * 2] = (float)(circle.CenterPoint.X + circle.Radius * Math.Cos(alpha));
                pts[i * 2 + 1] = (float)(circle.CenterPoint.Y + circle.Radius * Math.Sin(alpha));
            }
            int list = Gl.glGenLists(1);
            Gl.glNewList(list, Gl.GL_COMPILE);
            Gl.glColor3dv(colorCash);
            Gl.glLineWidth((float)width);
            Gl.glEndList();
            drawBuffer.Add(new DrawingOperation(Gl.GL_LINE_STRIP, pts, list));
        }

        private double[] colorArray(Color color)
        {
            double[] colArray = new double[3];
            colArray[0] = (double)color.R / 255;
            colArray[1] = (double)color.G / 255;
            colArray[2] = (double)color.B / 255;
            return colArray;
        }

        public void DrawAreas(MyArea[] areas, Color color)
        {
            int count = areas.Length * 16;
            if (count == 0) return;
            double[] vcolor = colorArray(color);
            int list = Gl.glGenLists(1);
            Gl.glNewList(list, Gl.GL_COMPILE);
            Gl.glColor3dv(vcolor);
            Gl.glPointSize(10.0f);
            Gl.glLineWidth(2.0f);
            Gl.glEndList();
            int seclist = Gl.glGenLists(1);
            Gl.glNewList(seclist, Gl.GL_COMPILE);
            Gl.glColor3dv(vcolor);
            Gl.glPointSize(8.0f);
            Gl.glLineWidth(2.0f);
            Gl.glEndList();
            // узлы образующие линии
            float[] pts = new float[count * 2];
            // основные узлы
            float[] mainpts = new float[count];
            // промежуточные узлы
            float[] secpts = new float[count];
            HashSet<int> passed = new HashSet<int>();

            int k = 0, pmk = 0, psk = 0;
            foreach (MyArea area in areas)
            {
                for (int j = 0; j < 9; j++)
                {
                    int i = (j != 8) ? j : 0;
                    float x = (float)area.Nodes[i].X, y = (float)area.Nodes[i].Y;
                    int Id = area.Nodes[i].Id;
                    pts[k++] = x; pts[k++] = y;
                    if (i != 0) { pts[k++] = x; pts[k++] = y; }
                    if (j != 8)
                    {
                        if (!passed.Contains(Id))
                        {
                            if (j % 2 == 0)
                            {
                                mainpts[pmk++] = x;
                                mainpts[pmk++] = y;
                            }
                            else
                            {
                                secpts[psk++] = x;
                                secpts[psk++] = y;
                            }
                            DrawString("n" + Id, x, y);
                            passed.Add(Id);
                        }
                    }
                }
                MyPoint areaCenter = new MyPoint((area.Nodes[1].X + area.Nodes[3].X + area.Nodes[5].X + area.Nodes[7].X) / 4.0,
                                                (area.Nodes[1].Y + area.Nodes[3].Y + area.Nodes[5].Y + area.Nodes[7].Y) / 4.0);
                DrawString("z" + area.Id.ToString(), (float)areaCenter.X, (float)areaCenter.Y);
            }
            Array.Resize(ref mainpts, pmk);
            Array.Resize(ref secpts, psk);
            drawBuffer.Add(new DrawingOperation(Gl.GL_POINTS, mainpts, list));
            drawBuffer.Add(new DrawingOperation(Gl.GL_POINTS, secpts, seclist));
            drawBuffer.Add(new DrawingOperation(Gl.GL_LINES, pts, list));
            FlushText(color);
        }

        public void DrawPointsArray(MyPoint[] points, Color color, bool drawId = true, double pSize = 10.0)
        {
            int count = points.Length;
            if (count == 0) return;
            float[] pts = new float[count * 2];

            double[] vColors = colorArray(color);
            int list = Gl.glGenLists(1);
            Gl.glNewList(list, Gl.GL_COMPILE);
            Gl.glColor3dv(vColors);
            Gl.glPointSize((float)(pSize));
            Gl.glEndList();
            for (int i = 0; i < count; i++)
            {
                pts[i * 2] = (float)points[i].X;
                pts[i * 2 + 1] = (float)points[i].Y;
                if (drawId)
                {
                    DrawString("p" + points[i].Id.ToString(), (float)points[i].X, (float)points[i].Y);
                }
            }
            drawBuffer.Add(new DrawingOperation(Gl.GL_POINTS, pts, list));
            FlushText(color);
        }

        public void DrawString(string str, float x, float y, bool centerX = false, bool centerY = false)
        {
            float size = 0.4f;
            str = str.Replace('.', ',');
            if (centerX) x -= ((float)(str.Length) / 2f) * size * scale;
            if (centerY) y -= ((size * textProportion) * scale) / 2f;
            x += 0.1f * scale;
            y += 0.1f * scale;
            foreach (char sym in str)
            {
                float[] tCords = new float[8];

                tCords[0] = x;
                tCords[1] = y;
                tCords[2] = x + size * scale;
                tCords[3] = y;
                tCords[4] = x + size * scale;
                tCords[5] = y + (size * textProportion) * scale;
                tCords[6] = x;
                tCords[7] = y + (size * textProportion) * scale;
                x += size * scale;
                PCbuf.AddRange(tCords);
                float left = symOffset[sym] * (1.0f / 15f);
                float right = (symOffset[sym] + 1) * (1.0f / 15f);
                float[] pt = { left, 1, right, 1, right, 0, left, 0 };
                TXbuf.AddRange(pt);
            }
        }

        private void FlushText(Color color)
        {
            if (!letters.ContainsKey(color)) createText(color);
            texturedBuffer.Add(new TexturedDrawingOperation(letters[color], PCbuf.ToArray(), TXbuf.ToArray()));
            PCbuf.Clear();
            TXbuf.Clear();
        }

        public MyPoint getClickPoint(int x, int y)
        {
            float clickX = borders.X + borders.Width * x / drawArea.Width;
            float clickY = borders.Y + borders.Height * (drawArea.Height - y) / drawArea.Height;
            return new MyPoint(clickX, clickY, MyPoint.PointType.IsGeometryPoint);
        }

        public void DrawBadElements(MyFiniteElement[] elems, double minAngle, double maxAngle, double minSqr, double maxSqr, GradationType colorGradationType)
        {
            int count = elems.Length * 3;
            if (count == 0) return;
            float[] pts = new float[count * 2];
            float[] cls = new float[count * 3];
            int i = 0, j;
            double midAngle = (maxAngle + minAngle) / 2;
            double difAngle = midAngle - minAngle;
            foreach (MyFiniteElement FE in elems)
            {
                double[] angles = Mathematics.getFEangles(FE);
                double feSqr = Mathematics.GeronLaw(FE.Nodes);
                float[] color = null;
                switch (colorGradationType)
                {
                    case GradationType.Angle:
                        if (minAngle > angles.Min() || maxAngle < angles.Max()) color = new float[] { 1.0f, 0, 0 };
                        else
                        {
                            double howBad = Math.Max(Math.Abs(midAngle - angles.Min()), Math.Abs(midAngle - angles.Max()));
                            color = new float[] { (float)(howBad / difAngle), 1.0f - (float)(howBad / difAngle) / 3, 0 };
                        }
                        break;
                    case GradationType.Square:
                        if (minSqr > feSqr) color = new float[] { 1.0f, 0, 0 };
                        else
                        {
                            double howBad = 1.0f - (feSqr - minSqr) / (maxSqr - minSqr);
                            color = new float[] { (float)(howBad), 1.0f - (float)howBad / 3f, 0 };
                        }
                        break;
                }
                for (j = 0; j < 9; j++)
                    cls[(i * 9) + j] = color[j % 3];
                pts[(i * 6)] = (float)FE.Nodes[0].X;
                pts[(i * 6) + 1] = (float)FE.Nodes[0].Y;
                pts[(i * 6) + 2] = (float)FE.Nodes[1].X;
                pts[(i * 6) + 3] = (float)FE.Nodes[1].Y;
                pts[(i * 6) + 4] = (float)FE.Nodes[2].X;
                pts[(i * 6) + 5] = (float)FE.Nodes[2].Y;
                i++;
            }
            int list = Gl.glGenLists(1);
            Gl.glNewList(list, Gl.GL_COMPILE);
            Gl.glLineWidth((float)(1.0));
            Gl.glEndList();
            drawBuffer.Add(new DrawingOperation(Gl.GL_TRIANGLES, pts, list, cls));
        }

        public void DrawElements(MyFiniteElement[] elems, Color color, bool showFEnumbers = true, bool showMaterials = false)
        {
            int count = elems.Length * 6;
            if (count == 0) return;
            float[] pts = new float[count * 2];
            double[] colorCash = colorArray(color);
            List<float> PCmats = new List<float>();
            List<float> TXmats = new List<float>();
            int i = 0;
            List<float> PCtmp = new List<float>();
            List<float> TXtmp = new List<float>();

            foreach (MyFiniteElement FE in elems)
            {
                if (!showMaterials)
                {
                    pts[(i * 12)] = (float)FE.Nodes[0].X;
                    pts[(i * 12) + 1] = (float)FE.Nodes[0].Y;
                    pts[(i * 12) + 2] = (float)FE.Nodes[1].X;
                    pts[(i * 12) + 3] = (float)FE.Nodes[1].Y;

                    pts[(i * 12) + 4] = (float)FE.Nodes[1].X;
                    pts[(i * 12) + 5] = (float)FE.Nodes[1].Y;
                    pts[(i * 12) + 6] = (float)FE.Nodes[2].X;
                    pts[(i * 12) + 7] = (float)FE.Nodes[2].Y;


                    pts[(i * 12) + 8] = (float)FE.Nodes[2].X;
                    pts[(i * 12) + 9] = (float)FE.Nodes[2].Y;
                    pts[(i * 12) + 10] = (float)FE.Nodes[0].X;
                    pts[(i * 12) + 11] = (float)FE.Nodes[0].Y;
                }

                float x1 = (float)FE.Nodes[0].X;
                float y1 = (float)FE.Nodes[0].Y;
                float x2 = (float)FE.Nodes[1].X;
                float y2 = (float)FE.Nodes[1].Y;
                float x3 = (float)FE.Nodes[2].X;
                float y3 = (float)FE.Nodes[2].Y;
                if (showFEnumbers) DrawString(FE.Id.ToString(), (x1 + x2 + x3) / 3, (y1 + y2 + y3) / 3, true, true);
                i++;
            }
            if (!showMaterials)
            {
                int list = Gl.glGenLists(1);
                Gl.glNewList(list, Gl.GL_COMPILE);
                Gl.glColor3dv(colorCash);
                Gl.glLineWidth((float)(1.0));
                Gl.glEndList();
                drawBuffer.Add(new DrawingOperation(Gl.GL_LINES, pts, list));
            }
            else
            {
                Random crnd = new Random();
                PCtmp = PCbuf;
                PCbuf = PCmats;
                TXtmp = TXbuf;
                TXbuf = TXmats;

                List<int> matIds = new List<int>();
                matIds.Add(0);
                foreach (MyFiniteElement fe in elems)
                    if (!matIds.Contains(fe.MaterialPropertyID)) matIds.Add(fe.MaterialPropertyID);
                foreach (int id in matIds)
                {
                    MyFiniteElement[] matElems = new List<MyFiniteElement>(elems).FindAll(f => f.MaterialPropertyID == id).ToArray();
                    count = elems.Length * 6;
                    i = 0;
                    float[] mpts = new float[count * 2];
                    foreach (MyFiniteElement FE in matElems)
                    {

                        mpts[(i * 12)] = (float)FE.Nodes[0].X;
                        mpts[(i * 12) + 1] = (float)FE.Nodes[0].Y;
                        mpts[(i * 12) + 2] = (float)FE.Nodes[1].X;
                        mpts[(i * 12) + 3] = (float)FE.Nodes[1].Y;

                        mpts[(i * 12) + 4] = (float)FE.Nodes[1].X;
                        mpts[(i * 12) + 5] = (float)FE.Nodes[1].Y;
                        mpts[(i * 12) + 6] = (float)FE.Nodes[2].X;
                        mpts[(i * 12) + 7] = (float)FE.Nodes[2].Y;

                        mpts[(i * 12) + 8] = (float)FE.Nodes[2].X;
                        mpts[(i * 12) + 9] = (float)FE.Nodes[2].Y;
                        mpts[(i * 12) + 10] = (float)FE.Nodes[0].X;
                        mpts[(i * 12) + 11] = (float)FE.Nodes[0].Y;

                        float x1 = (float)FE.Nodes[0].X;
                        float y1 = (float)FE.Nodes[0].Y;
                        float x2 = (float)FE.Nodes[1].X;
                        float y2 = (float)FE.Nodes[1].Y;
                        float x3 = (float)FE.Nodes[2].X;
                        float y3 = (float)FE.Nodes[2].Y;
                        if (showMaterials)
                        {
                            DrawString(FE.MaterialPropertyID.ToString(), (x1 + x2 + x3) / 3, (y1 + y2 + y3) / 3, true, true);
                        }
                        i++;
                    }
                    if (!matColors.ContainsKey(id))
                    {
                        byte[] clr = new byte[3];
                        crnd.NextBytes(clr);
                        matColors.Add(id, Color.FromArgb(clr[0], clr[1], clr[2]));
                    }
                    double[] clrCash = colorArray(matColors[id]);
                    int list = Gl.glGenLists(1);
                    Gl.glNewList(list, Gl.GL_COMPILE);
                    Gl.glColor3dv(clrCash);
                    Gl.glLineWidth((float)(1.0));
                    Gl.glEndList();
                    drawBuffer.Add(new DrawingOperation(Gl.GL_LINES, mpts, list));
                    FlushText(matColors[id]);
                }
                PCbuf = PCtmp;
                TXbuf = TXtmp;
            }
            if (showFEnumbers) FlushText(color);
        }

        public void DrawFE(MyFiniteElementModel model, Color color, bool showFEnumbers = true, bool showFEnodeNumbers = true, bool showMaterials = false)
        {
            DrawElements(model.FiniteElements.ToArray(), color, showFEnumbers, showMaterials);
            if (showFEnodeNumbers)
            {
                Gl.glColor3dv(colorArray(Color.Green));
                model.Nodes.ForEach(n => DrawString(n.Id.ToString(), (float)n.X, (float)n.Y));
                FlushText(Color.Green);
            }
        }

        public void DrawLinesArray(MyStraightLine[] lines, Color color, bool drawId = true, bool isAreaLine = false, double width = 2.0)
        {
            int count = lines.Length * 2;
            if (count == 0) return;
            float[] pts = new float[count * 2];
            double[] vColors = colorArray(color);
            int list = Gl.glGenLists(1);
            Gl.glNewList(list, Gl.GL_COMPILE);
            Gl.glColor3dv(vColors);
            Gl.glLineWidth((float)(width));
            Gl.glEndList();
            for (int i = 0; i < lines.Length; i++)
            {
                MyStraightLine line = lines[i];
                pts[i * 4] = (float)line.StartPoint.X;
                pts[i * 4 + 1] = (float)line.StartPoint.Y;
                pts[i * 4 + 2] = (float)line.EndPoint.X;
                pts[i * 4 + 3] = (float)line.EndPoint.Y;
                if (drawId) DrawString("L" + line.Id.ToString(), (float)(line.EndPoint.X + line.StartPoint.X) / 2.0f, (float)(line.EndPoint.Y + line.StartPoint.Y) / 2.0f);
            }
            if (drawId) FlushText(color);
            drawBuffer.Add(new DrawingOperation(Gl.GL_LINES, pts, list));
        }
    }
}
