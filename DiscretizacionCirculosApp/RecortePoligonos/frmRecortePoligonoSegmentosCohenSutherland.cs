using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DiscretizacionCirculosApp.RecortePoligonos
{
    public partial class frmRecortePoligonoSegmentosCohenSutherland : Form
    {
        private Bitmap lienzo;
        private Rectangle ventanaRecorte;

        // Polígono original (lista de vértices que el usuario marca con el mouse)
        private List<PointF> poligono = new List<PointF>();

        public frmRecortePoligonoSegmentosCohenSutherland()
        {
            InitializeComponent();

            // Crear bitmap y asignarlo al PictureBox
            lienzo = new Bitmap(picCanvas.Width, picCanvas.Height);
            picCanvas.Image = lienzo;

            // Definir la ventana de recorte (rectángulo visible)
            int margen = 80;
            ventanaRecorte = new Rectangle(
                margen,
                margen,
                picCanvas.Width - 2 * margen,
                picCanvas.Height - 2 * margen
            );

            RedibujarTodo();
        }

        // ======================================================
        //        CÓDIGO DE REGIÓN (COHEN–SUTHERLAND)
        // ======================================================

        [Flags]
        enum OutCode
        {
            Inside = 0,   // 0000
            Left = 1,   // 0001
            Right = 2,   // 0010
            Bottom = 4,   // 0100
            Top = 8    // 1000
        }

        OutCode ComputeOutCode(float x, float y)
        {
            OutCode code = OutCode.Inside;

            if (x < ventanaRecorte.Left)
                code |= OutCode.Left;
            else if (x > ventanaRecorte.Right)
                code |= OutCode.Right;

            if (y < ventanaRecorte.Top)
                code |= OutCode.Top;
            else if (y > ventanaRecorte.Bottom)
                code |= OutCode.Bottom;

            return code;
        }

        // Recorta un segmento [p0,p1] con Cohen–Sutherland.
        // Devuelve true si hay parte visible; p0 y p1 se modifican al tramo recortado.
        bool CohenSutherlandClip(ref PointF p0, ref PointF p1)
        {
            float x0 = p0.X, y0 = p0.Y;
            float x1 = p1.X, y1 = p1.Y;

            OutCode outcode0 = ComputeOutCode(x0, y0);
            OutCode outcode1 = ComputeOutCode(x1, y1);

            bool accept = false;

            while (true)
            {
                if ((outcode0 | outcode1) == OutCode.Inside)
                {
                    // Ambos puntos dentro → aceptar
                    accept = true;
                    break;
                }
                else if ((outcode0 & outcode1) != 0)
                {
                    // Ambos comparten una región fuera → rechazo trivial
                    break;
                }
                else
                {
                    // Hay posibilidad de que una parte sea visible
                    float x = 0, y = 0;
                    OutCode outcodeOut = outcode0 != OutCode.Inside ? outcode0 : outcode1;

                    if ((outcodeOut & OutCode.Top) != 0)
                    {
                        y = ventanaRecorte.Top;
                        x = x0 + (x1 - x0) * (y - y0) / (y1 - y0);
                    }
                    else if ((outcodeOut & OutCode.Bottom) != 0)
                    {
                        y = ventanaRecorte.Bottom;
                        x = x0 + (x1 - x0) * (y - y0) / (y1 - y0);
                    }
                    else if ((outcodeOut & OutCode.Right) != 0)
                    {
                        x = ventanaRecorte.Right;
                        y = y0 + (y1 - y0) * (x - x0) / (x1 - x0);
                    }
                    else if ((outcodeOut & OutCode.Left) != 0)
                    {
                        x = ventanaRecorte.Left;
                        y = y0 + (y1 - y0) * (x - x0) / (x1 - x0);
                    }

                    // Reemplazar el punto que está fuera por la intersección
                    if (outcodeOut == outcode0)
                    {
                        x0 = x; y0 = y;
                        outcode0 = ComputeOutCode(x0, y0);
                    }
                    else
                    {
                        x1 = x; y1 = y;
                        outcode1 = ComputeOutCode(x1, y1);
                    }
                }
            }

            if (accept)
            {
                p0 = new PointF(x0, y0);
                p1 = new PointF(x1, y1);
            }

            return accept;
        }

        // ======================================================
        //                  DIBUJADO GENERAL
        // ======================================================

        void RedibujarTodo()
        {
            using (Graphics g = Graphics.FromImage(lienzo))
            {
                g.Clear(Color.White);

                // Ventana de recorte (área visible)
                using (Brush b = new SolidBrush(Color.FromArgb(230, 220, 255, 220)))
                using (Pen p = new Pen(Color.Green, 2))
                {
                    g.FillRectangle(b, ventanaRecorte);
                    g.DrawRectangle(p, ventanaRecorte);
                }

                // Polígono original en rojo
                if (poligono.Count >= 2)
                {
                    using (Pen p = new Pen(Color.Red, 2))
                    {
                        g.DrawLines(p, poligono.ToArray());
                        // Si quieres que se vea cerrado:
                        // g.DrawLine(p, poligono[poligono.Count - 1], poligono[0]);
                    }
                }

                // Vértices como puntos rojos
                if (poligono.Count >= 1)
                {
                    using (Brush b = new SolidBrush(Color.Red))
                    {
                        foreach (var v in poligono)
                            g.FillEllipse(b, v.X - 3, v.Y - 3, 6, 6);
                    }
                }
            }

            picCanvas.Invalidate();
        }

        // ======================================================
        //          CAPTURA DE VÉRTICES DEL POLÍGONO
        // ======================================================

        private void picCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            poligono.Add(e.Location);

            lblInfo.Text = $"Recorte por segmentos (Cohen–Sutherland): vértices={poligono.Count}";
            RedibujarTodo();
        }

        // ======================================================
        //           BOTÓN: RECORTAR POR SEGMENTOS
        // ======================================================

        private void btnClip_Click(object sender, EventArgs e)
        {
            if (poligono.Count < 2)
            {
                MessageBox.Show("Primero dibuje un polígono haciendo clic para agregar vértices.");
                return;
            }

            int segmentosVisibles = 0;

            using (Graphics g = Graphics.FromImage(lienzo))
            {
                // Dibujar de nuevo el polígono original en rojo
                if (poligono.Count >= 2)
                {
                    using (Pen p = new Pen(Color.Red, 2))
                    {
                        g.DrawLines(p, poligono.ToArray());
                        // g.DrawLine(p, poligono[poligono.Count - 1], poligono[0]);
                    }
                }

                // Recortar cada arista como segmento independiente
                using (Pen pVisible = new Pen(Color.Blue, 3))
                {
                    for (int i = 0; i < poligono.Count - 1; i++)
                    {
                        PointF a = poligono[i];
                        PointF b = poligono[i + 1];

                        PointF p0 = a;
                        PointF p1 = b;

                        if (CohenSutherlandClip(ref p0, ref p1))
                        {
                            g.DrawLine(pVisible, p0, p1);
                            segmentosVisibles++;
                        }
                    }

                    // Opcional: cerrar el polígono y recortar el último lado
                    if (poligono.Count >= 3)
                    {
                        PointF a = poligono[poligono.Count - 1];
                        PointF b = poligono[0];

                        PointF p0 = a;
                        PointF p1 = b;

                        if (CohenSutherlandClip(ref p0, ref p1))
                        {
                            g.DrawLine(pVisible, p0, p1);
                            segmentosVisibles++;
                        }
                    }
                }
            }

            lblInfo.Text =
                $"Recorte por segmentos (Cohen–Sutherland): vértices={poligono.Count}, " +
                $"segmentos visibles={segmentosVisibles}";

            picCanvas.Invalidate();
        }

        // ======================================================
        //                 BOTÓN: LIMPIAR
        // ======================================================

        private void btnClean_Click(object sender, EventArgs e)
        {
            poligono.Clear();
            lienzo = new Bitmap(picCanvas.Width, picCanvas.Height);
            picCanvas.Image = lienzo;

            using (Graphics g = Graphics.FromImage(lienzo))
            {
                g.Clear(Color.White);
                using (Brush b = new SolidBrush(Color.FromArgb(230, 220, 255, 220)))
                using (Pen p = new Pen(Color.Green, 2))
                {
                    g.FillRectangle(b, ventanaRecorte);
                    g.DrawRectangle(p, ventanaRecorte);
                }
            }

            lblInfo.Text = "Recorte por segmentos (Cohen–Sutherland):";
            picCanvas.Invalidate();
        }

        // ======================================================
        //                 BOTÓN: CERRAR
        // ======================================================

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
