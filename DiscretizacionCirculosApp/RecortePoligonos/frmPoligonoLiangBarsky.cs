using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DiscretizacionCirculosApp.RecortePoligonos
{
    public partial class frmPoligonoLiangBarsky : Form
    {
        Bitmap lienzo;

        // Ventana de recorte (rectángulo visible)
        Rectangle ventanaRecorte;

        // Vértices del polígono dibujado por el usuario
        List<PointF> vertices = new List<PointF>();

        public frmPoligonoLiangBarsky()
        {
            InitializeComponent();
            InicializarLienzoYVentana();
        }

        // ---------------------------------------------------------
        // Inicializar lienzo y rectángulo de recorte
        // ---------------------------------------------------------
        void InicializarLienzoYVentana()
        {
            // Crear bitmap en blanco
            lienzo = new Bitmap(picCanvas.Width, picCanvas.Height);
            using (Graphics g = Graphics.FromImage(lienzo))
            {
                g.Clear(Color.White);
            }
            picCanvas.Image = lienzo;

            // Ventana centrada con márgenes simétricos
            int margenX = 80;   // izquierda/derecha
            int margenY = 60;   // arriba/abajo

            ventanaRecorte = new Rectangle(
                margenX,
                margenY,
                picCanvas.Width - 2 * margenX,
                picCanvas.Height - 2 * margenY
            );

            RedibujarTodo();
        }

        // Dibuja ventana + polígono
        void RedibujarTodo()
        {
            using (Graphics g = Graphics.FromImage(lienzo))
            {
                g.Clear(Color.White);

                // Fondo verde de la ventana
                using (Brush b = new SolidBrush(Color.FromArgb(220, 240, 255, 200)))
                {
                    g.FillRectangle(b, ventanaRecorte);
                }

                // Borde de la ventana
                using (Pen borde = new Pen(Color.DarkGreen, 2))
                {
                    g.DrawRectangle(borde, ventanaRecorte);
                }

                // Polígono en rojo (si hay al menos 2 puntos)
                if (vertices.Count > 1)
                {
                    using (Pen pRoja = new Pen(Color.Red, 2))
                    {
                        g.DrawLines(pRoja, vertices.ToArray());
                        // Unir último con primero para cerrar la figura visualmente
                        g.DrawLine(pRoja, vertices[vertices.Count - 1], vertices[0]);
                    }
                }

                // Marcar vértices con puntito rojo
                using (Brush br = new SolidBrush(Color.Red))
                {
                    foreach (var v in vertices)
                    {
                        g.FillEllipse(br, v.X - 3, v.Y - 3, 6, 6);
                    }
                }
            }

            picCanvas.Invalidate();
        }

        // ---------------------------------------------------------
        // Click en el canvas: agregar vértice
        // ---------------------------------------------------------
        private void picCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            // Agregamos el punto tal cual (puede estar dentro o fuera)
            vertices.Add(e.Location);
            lblInfo.Text = $"Vértices: {vertices.Count}";

            RedibujarTodo();
        }

        // ---------------------------------------------------------
        // Algoritmo Liang–Barsky (recorte de un segmento)
        // Devuelve true si hay parte visible y los dos nuevos puntos c0, c1
        // ---------------------------------------------------------
        bool LiangBarskyClip(Rectangle win, PointF p0, PointF p1,
                             out PointF c0, out PointF c1,
                             out float t0, out float t1)
        {
            float dx = p1.X - p0.X;
            float dy = p1.Y - p0.Y;

            t0 = 0f;
            t1 = 1f;

            bool ClipTest(float p, float q, ref float t0Ref, ref float t1Ref)
            {
                if (p == 0)
                {
                    // Segmento paralelo: si está fuera, no hay intersección
                    return q >= 0;
                }

                float r = q / p;

                if (p < 0)
                {
                    // Posible entrada
                    if (r > t1Ref) return false;
                    if (r > t0Ref) t0Ref = r;
                }
                else
                {
                    // p > 0 → posible salida
                    if (r < t0Ref) return false;
                    if (r < t1Ref) t1Ref = r;
                }
                return true;
            }

            // Bordes de la ventana (Left, Right, Top, Bottom)
            if (ClipTest(-dx, p0.X - win.Left, ref t0, ref t1) &&
                ClipTest(dx, win.Right - p0.X, ref t0, ref t1) &&
                ClipTest(-dy, p0.Y - win.Top, ref t0, ref t1) &&
                ClipTest(dy, win.Bottom - p0.Y, ref t0, ref t1))
            {
                if (t1 < t0)
                {
                    c0 = c1 = PointF.Empty;
                    return false;
                }

                c0 = new PointF(p0.X + t0 * dx, p0.Y + t0 * dy);
                c1 = new PointF(p0.X + t1 * dx, p0.Y + t1 * dy);
                return true;
            }

            c0 = c1 = PointF.Empty;
            return false;
        }

        // ---------------------------------------------------------
        // Botón RECORTAR
        // ---------------------------------------------------------
        private void btnClip_Click(object sender, EventArgs e)
        {
            if (vertices.Count < 3)
            {
                MessageBox.Show("Dibuje al menos 3 puntos para formar un polígono.");
                return;
            }

            // Redibujar fondo + polígono original en rojo
            RedibujarTodo();

            int segmentosVisibles = 0;

            using (Graphics g = Graphics.FromImage(lienzo))
            using (Pen pAzul = new Pen(Color.Blue, 3))
            {
                // Recorrer cada arista del polígono
                for (int i = 0; i < vertices.Count; i++)
                {
                    PointF a = vertices[i];
                    PointF b = vertices[(i + 1) % vertices.Count];

                    if (LiangBarskyClip(ventanaRecorte, a, b,
                                        out PointF c0, out PointF c1,
                                        out float t0, out float t1))
                    {
                        g.DrawLine(pAzul, c0, c1);
                        segmentosVisibles++;
                    }
                }
            }

            lblInfo.Text = $"Liang-Barsky polígonos: vértices={vertices.Count}, " +
                           $"segmentos visibles={segmentosVisibles}";

            picCanvas.Invalidate();
        }

        // ---------------------------------------------------------
        // Botón LIMPIAR
        // ---------------------------------------------------------
        private void btnClean_Click(object sender, EventArgs e)
        {
            vertices.Clear();
            InicializarLienzoYVentana();
            lblInfo.Text = "Liang-Barsky polígonos listo.";
        }

        // ---------------------------------------------------------
        // Botón CERRAR
        // ---------------------------------------------------------
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
