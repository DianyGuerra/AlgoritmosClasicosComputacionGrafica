using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DiscretizacionCirculosApp.Relleno
{
    public partial class frmLiangBarsky : Form
    {

        Bitmap lienzo;
        // Ventana de recorte (zona visible)
        Rectangle ventana;

        //estados para dibujo de línea con el mouse
        bool dibujandoLinea = false;
        Point pInicio;
        Point pFin;

        public frmLiangBarsky()
        {
            InitializeComponent();

            // Crear bitmap del tamaño del PictureBox
            lienzo = new Bitmap(picCanvas.Width, picCanvas.Height);
            picCanvas.Image = lienzo;

            //dibujar la ventana de recorte
            DibujarVentana();
        }

        // ----------------------------------------------------
        // DIBUJAR LA VENTANA DE RECORTE
        // ----------------------------------------------------
        private void DibujarVentana()
        {
            // Definimos la ventana como un rectángulo centrado
            int margen = 100;
            ventana = new Rectangle(
                margen,
                margen,
                picCanvas.Width - 2 * margen,
                picCanvas.Height - 2 * margen
            );

            using (Graphics g = Graphics.FromImage(lienzo))
            {
                // Fondo blanco para todo el lienzo
                g.Clear(Color.White);

                // Relleno suave para la zona visible
                using (Brush b = new SolidBrush(Color.FromArgb(230, 209, 179, 255)))
                {
                    g.FillRectangle(b, ventana);
                }

                // Borde de la ventana
                using (Pen p = new Pen(Color.White, 2))
                {
                    g.DrawRectangle(p, ventana);
                }
            }

            picCanvas.Invalidate();
        }

        // ----------------------------------------------------
        // DIBUJAR LÍNEA COMPLETA
        // ----------------------------------------------------
        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            dibujandoLinea = true;
            pInicio = e.Location;
        }

        private void picCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (!dibujandoLinea) return;
            dibujandoLinea = false;

            pFin = e.Location;

            using (Graphics g = Graphics.FromImage(lienzo))
            {
                //Dibujar línea completa en ROJO
                using (Pen penCompleta = new Pen(Color.Red, 2))
                {
                    g.DrawLine(penCompleta, pInicio, pFin);
                }

                //Aplicar Liang-Barsky
                PointF p0 = pInicio;
                PointF p1 = pFin;

                if (LiangBarskyClip(ventana, ref p0, ref p1))
                {
                    //Dibujar parte visible en AZUL
                    using (Pen penVisible = new Pen(Color.Blue, 3))
                    {
                        g.DrawLine(penVisible, p0, p1);
                    }
                }
            }

            picCanvas.Invalidate();
        }

        // ----------------------------------------------------
        // ALGORITMO LIANG–BARSKY
        // ----------------------------------------------------
        bool ClipTest(float p, float q, ref float t0, ref float t1)
        {
            if (p == 0)
            {
                // Segmento paralelo a este borde
                // Si q < 0, está totalmente fuera
                if (q < 0) return false;
                return true; // está dentro o sobre el borde
            }

            float r = q / p;

            if (p < 0)
            {
                // Borde de entrada
                if (r > t1) return false;
                if (r > t0) t0 = r;
            }
            else // p > 0
            {
                // Borde de salida
                if (r < t0) return false;
                if (r < t1) t1 = r;
                lblInfo.Text = $"Liang-Barsky: t0={t0:F2}, t1={t1:F2}";
            }
            return true;
        }

        /// <summary>
        /// Recorta un segmento con Liang–Barsky.
        /// Devuelve true si hay parte visible; p0 y p1 se actualizan al tramo dentro de la ventana.
        /// </summary>
        bool LiangBarskyClip(Rectangle win, ref PointF p0, ref PointF p1)
        {
            float x0 = p0.X, y0 = p0.Y;
            float x1 = p1.X, y1 = p1.Y;

            float dx = x1 - x0;
            float dy = y1 - y0;

            float t0 = 0.0f;
            float t1 = 1.0f;

            // Bordes de la ventana
            float xMin = win.Left;
            float xMax = win.Right;
            float yMin = win.Top;
            float yMax = win.Bottom;

            // Test contra cada borde
            if (ClipTest(-dx, x0 - xMin, ref t0, ref t1) &&   // izquierda
                ClipTest(dx, xMax - x0, ref t0, ref t1) &&   // derecha
                ClipTest(-dy, y0 - yMin, ref t0, ref t1) &&   // arriba
                ClipTest(dy, yMax - y0, ref t0, ref t1))     // abajo
            {
                // Hay parte visible
                if (t1 < 1.0f)
                {
                    x1 = x0 + t1 * dx;
                    y1 = y0 + t1 * dy;
                }
                if (t0 > 0.0f)
                {
                    x0 = x0 + t0 * dx;
                    y0 = y0 + t0 * dy;
                }

                p0 = new PointF(x0, y0);
                p1 = new PointF(x1, y1);
                return true;
            }

            // No intersecta la ventana
            return false;
        }

        // ----------------------------------------------------
        // BOTÓN: LIMPIAR
        // ----------------------------------------------------
        private void btnClean_Click(object sender, EventArgs e)
        {
            DibujarVentana();  // vuelve a dibujar ventana y borra líneas
        }

        // ----------------------------------------------------
        // BOTÓN: CERRAR
        // ----------------------------------------------------
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
