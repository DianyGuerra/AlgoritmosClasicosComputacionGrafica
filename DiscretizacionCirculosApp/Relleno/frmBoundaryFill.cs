using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DiscretizacionCirculosApp.Relleno
{
    public partial class frmBoundaryFill : Form
    {
        Bitmap lienzo;                // Bitmap donde se dibuja y se rellena
        Color colorRelleno = Color.Empty;

        bool dibujando = false;       // Controla si el usuario está trazando una línea
        Point puntoAnterior;          // Guarda la posición anterior del mouse al dibujar

        Point? puntoSemilla = null;   // Punto inicial del algoritmo Boundary Fill


        public frmBoundaryFill()
        {
            InitializeComponent();

            // Crear lienzo y asignarlo al PictureBox
            lienzo = new Bitmap(picCanvas.Width, picCanvas.Height);
            picCanvas.Image = lienzo;
        }

        //algoritmo de doundary fill

        void BoundaryFill(int x, int y, Color colorRelleno, Color colorBorde)
        {
            Stack<Point> stack = new Stack<Point>();
            stack.Push(new Point(x, y));

            while (stack.Count > 0)
            {
                Point p = stack.Pop();

                // Evitar indexación fuera del lienzo
                if (p.X < 0 || p.X >= lienzo.Width || p.Y < 0 || p.Y >= lienzo.Height)
                    continue;

                Color actual = lienzo.GetPixel(p.X, p.Y);

                // NO rellenar si el pixel es borde
                if (actual.ToArgb() == colorBorde.ToArgb())
                    continue;

                // NO rellenar si ya está del color de relleno
                if (actual.ToArgb() == colorRelleno.ToArgb())
                    continue;

                // Pintar el pixel actual
                lienzo.SetPixel(p.X, p.Y, colorRelleno);

                // Agregar vecinos (4 direcciones)
                stack.Push(new Point(p.X + 1, p.Y));
                stack.Push(new Point(p.X - 1, p.Y));
                stack.Push(new Point(p.X, p.Y + 1));
                stack.Push(new Point(p.X, p.Y - 1));
            }
        }

        //******************************************************************EVENTOS DE DIBUJO

        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            dibujando = true;
            puntoAnterior = e.Location;
        }

        private void picCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!dibujando) return;

            // Dibujar líneas gruesas para evitar huecos
            using (Graphics g = Graphics.FromImage(lienzo))
            using (Pen lapiz = new Pen(Color.Black, 4))   // borde negro de grosor 4
            {
                g.DrawLine(lapiz, puntoAnterior, e.Location);
            }

            puntoAnterior = e.Location;
            picCanvas.Invalidate();
        }

        private void picCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            dibujando = false;

            // Si el mouse NO se movió, se toma como clic para seleccionar la semilla
            if (puntoAnterior == e.Location)
            {
                puntoSemilla = e.Location;

                MessageBox.Show("Punto semilla seleccionado en: " + puntoSemilla);

                picCanvas.Invalidate();
            }
        }

        //===========================================================
        //                     BOTONES DEL FORMULARIO
        //===========================================================

        private void btnPickColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorRelleno = colorDialog1.Color;
                btnPickColor.BackColor = colorRelleno;
            }
        }

        private void btnBoundaryFill_Click(object sender, EventArgs e)
        {
            // Validaciones antes de rellenar
            if (colorRelleno == Color.Empty)
            {
                MessageBox.Show("Seleccione un color de relleno.");
                return;
            }

            if (puntoSemilla == null)
            {
                MessageBox.Show("Seleccione un punto dentro de la figura.");
                return;
            }

            int x = puntoSemilla.Value.X;
            int y = puntoSemilla.Value.Y;

            Color colorEnPunto = lienzo.GetPixel(x, y);

            // Evitar iniciar sobre el borde
            if (colorEnPunto.ToArgb() == Color.Black.ToArgb())
            {
                MessageBox.Show("Seleccione un punto dentro de la figura, no sobre el borde.");
                return;
            }

            // Ejecutar Boundary Fill
            BoundaryFill(x, y, colorRelleno, Color.Black);

            picCanvas.Invalidate();
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            // Reiniciar el lienzo completamente
            lienzo = new Bitmap(picCanvas.Width, picCanvas.Height);
            picCanvas.Image = lienzo;
            puntoSemilla = null;
            picCanvas.Invalidate();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
