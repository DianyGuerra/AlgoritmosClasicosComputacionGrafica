using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DiscretizacionCirculosApp.RecorteLineas
{
    public partial class frmMidpointClip : Form
    {
        Bitmap lienzo;
        Rectangle ventanaRecorte;

        bool dibujandoLinea = false;
        Point pInicio, pFin;
        const int MAX_DEPTH = 18;
        int maxDepthUsada = 0;


        public frmMidpointClip()
        {
            InitializeComponent();
            InicializarLienzoYVentana();
        }

        // ===================== INICIALIZAR LIENZO Y VENTANA ======================
        void InicializarLienzoYVentana()
        {
            lienzo = new Bitmap(picCanvas.Width, picCanvas.Height);
            picCanvas.Image = lienzo;

            int margen = 100;
            ventanaRecorte = new Rectangle(
                margen,
                margen,
                picCanvas.Width - 2 * margen,
                picCanvas.Height - 2 * margen
            );

            DibujarVentana();
        }

        void DibujarVentana()
        {
            using (Graphics g = Graphics.FromImage(lienzo))
            {
                g.Clear(Color.White);

                // Rectángulo visible en color suave
                using (Brush b = new SolidBrush(Color.FromArgb(230, 179, 221, 255)))
                    g.FillRectangle(b, ventanaRecorte);

                // Borde negro de la ventana
                using (Pen p = new Pen(Color.Black, 2))
                    g.DrawRectangle(p, ventanaRecorte);
            }

            picCanvas.Invalidate();
        }

        // ===================== FUNCIONES DE APOYO ======================

        bool PuntoDentro(PointF p)
        {
            return p.X >= ventanaRecorte.Left && p.X <= ventanaRecorte.Right &&
                   p.Y >= ventanaRecorte.Top && p.Y <= ventanaRecorte.Bottom;
        }

        bool SegmentoFueraBBox(PointF a, PointF b)
        {
            float minX = Math.Min(a.X, b.X);
            float maxX = Math.Max(a.X, b.X);
            float minY = Math.Min(a.Y, b.Y);
            float maxY = Math.Max(a.Y, b.Y);

            if (maxX < ventanaRecorte.Left || minX > ventanaRecorte.Right ||
                maxY < ventanaRecorte.Top || minY > ventanaRecorte.Bottom)
                return true;

            return false;
        }

        float Distancia(PointF a, PointF b)
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        // Recorte por subdivisión de punto medio
        void RecortePuntoMedio(PointF a, PointF b, List<Tuple<PointF, PointF>> visibles, int depth)
        {
            // Solo para estadística (no corta la recursión)
            if (depth > maxDepthUsada)
                maxDepthUsada = depth;

            bool insideA = PuntoDentro(a);
            bool insideB = PuntoDentro(b);

            // 1. Ambos dentro → aceptar segmento completo
            if (insideA && insideB)
            {
                visibles.Add(Tuple.Create(a, b));
                return;
            }

            // 2. Bounding box del segmento no toca la ventana → rechazo trivial
            if (SegmentoFueraBBox(a, b))
                return;

            // 3. Hemos subdividido "suficiente" o el segmento es muy pequeño:
            //    aproximamos el corte en lugar de seguir partiendo.
            if (depth >= MAX_DEPTH || Distancia(a, b) < 1.0f)
            {
                // Si al menos una parte está dentro, aproximamos
                if (insideA || insideB)
                {
                    PointF aClamp = ClampToWindow(a);
                    PointF bClamp = ClampToWindow(b);

                    // Si después de ajustar ambos quedan dentro, lo consideramos visible
                    if (PuntoDentro(aClamp) && PuntoDentro(bClamp))
                        visibles.Add(Tuple.Create(aClamp, bClamp));
                }
                return;
            }

            // 4. Subdividimos en el punto medio y seguimos recursivamente
            PointF m = new PointF((a.X + b.X) / 2f, (a.Y + b.Y) / 2f);

            RecortePuntoMedio(a, m, visibles, depth + 1);
            RecortePuntoMedio(m, b, visibles, depth + 1);
        }

        PointF ClampToWindow(PointF p)
        {
            float x = Math.Max(ventanaRecorte.Left,
                     Math.Min(ventanaRecorte.Right, p.X));
            float y = Math.Max(ventanaRecorte.Top,
                     Math.Min(ventanaRecorte.Bottom, p.Y));
            return new PointF(x, y);
        }

        // ===================== EVENTOS DEL PICCANVAS ======================

        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            dibujandoLinea = true;
            pInicio = e.Location;
            pFin = e.Location;
        }

        private void picCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!dibujandoLinea) return;
            // Si quieres previsualizar, aquí podrías guardar pFin y repintar, 
            // pero para simplificar solo lo actualizamos al soltar.
            pFin = e.Location;
        }

        private void picCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (!dibujandoLinea) return;
            dibujandoLinea = false;

            pFin = e.Location;

            PointF a = pInicio;
            PointF b = pFin;

            // ← visibiles AHORA está fuera del using, así lo puedes usar después
            var visibles = new List<Tuple<PointF, PointF>>();

            // reiniciamos la profundidad máxima usada en esta línea
            maxDepthUsada = 0;

            using (Graphics g = Graphics.FromImage(lienzo))
            {
                // 1) Línea completa en ROJO (segmento original)
                using (Pen pRoja = new Pen(Color.Red, 2))
                    g.DrawLine(pRoja, a, b);

                // 2) Calcular los segmentos visibles con Midpoint
                RecortePuntoMedio(a, b, visibles, 0);

                // 3) Dibujar solo los trozos dentro de la ventana en AZUL
                using (Pen pAzul = new Pen(Color.Blue, 3))
                {
                    foreach (var seg in visibles)
                        g.DrawLine(pAzul, seg.Item1, seg.Item2);
                }
            }

            // 4) Info para el label
            float dx = b.X - a.X;
            float dy = b.Y - a.Y;
            double longitud = Math.Sqrt(dx * dx + dy * dy);

            lblInfo.Text = $"Midpoint: L={longitud:F1}px, trozos visibles={visibles.Count}, prof.max={maxDepthUsada}";

            picCanvas.Invalidate();
        }


        // ===================== BOTONES ======================

        private void btnClean_Click(object sender, EventArgs e)
        {
            InicializarLienzoYVentana();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
