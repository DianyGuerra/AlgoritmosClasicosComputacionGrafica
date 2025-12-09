using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DiscretizacionCirculosApp.Relleno
{
    public partial class frmFloodFill : Form
    {
        Bitmap lienzo;
        Color colorRelleno = Color.Empty;

        bool dibujando = false;
        Point puntoAnterior;

        Point? puntoSemilla = null;


        public frmFloodFill()
        {
            InitializeComponent();
            lienzo = new Bitmap(picCanvas.Width, picCanvas.Height);
            picCanvas.Image = lienzo;
        }

        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            dibujando = true;
            puntoAnterior = e.Location;
        }

        private void picCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!dibujando) return;

            using (Graphics g = Graphics.FromImage(lienzo))
            using (Pen lapiz = new Pen(Color.Black, 4))
            {
                g.DrawLine(lapiz, puntoAnterior, e.Location);
            }

            puntoAnterior = e.Location;
            picCanvas.Invalidate();
        }

        private void picCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            dibujando = false;

            // SI el mouse no se movió, se toma como clic para semilla
            if (puntoAnterior == e.Location)
            {
                puntoSemilla = e.Location;

                MessageBox.Show("Punto semilla seleccionado en: " + puntoSemilla);

                picCanvas.Invalidate();
            }
        }

        private void btnPickColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorRelleno = colorDialog1.Color;
                btnPickColor.BackColor = colorRelleno;
            }
        }

        //algoritmo de flood fill 
        void FloodFill(int x, int y, Color objetivo, Color reemplazo)
        {
            if (objetivo.ToArgb() == reemplazo.ToArgb())
                return;

            Stack<Point> stack = new Stack<Point>();
            stack.Push(new Point(x, y));

            while (stack.Count > 0)
            {
                Point p = stack.Pop();

                if (p.X < 0 || p.X >= lienzo.Width ||
                    p.Y < 0 || p.Y >= lienzo.Height)
                    continue;

                Color actual = lienzo.GetPixel(p.X, p.Y);

                // Solo rellenamos si coincide con el color objetivo
                if (actual.ToArgb() != objetivo.ToArgb())
                    continue;

                // Pintar pixel
                lienzo.SetPixel(p.X, p.Y, reemplazo);

                stack.Push(new Point(p.X + 1, p.Y));
                stack.Push(new Point(p.X - 1, p.Y));
                stack.Push(new Point(p.X, p.Y + 1));
                stack.Push(new Point(p.X, p.Y - 1));
            }
        }


        private void btnFoodFill_Click(object sender, EventArgs e)
        {
            if (colorRelleno == Color.Empty)
            {
                MessageBox.Show("Primero seleccione un color de relleno.");
                return;
            }

            if (puntoSemilla == null)
            {
                MessageBox.Show("Seleccione un punto dentro de la figura.");
                return;
            }

            int x = puntoSemilla.Value.X;
            int y = puntoSemilla.Value.Y;

            // Color objetivo: fondo o interior
            Color objetivo = lienzo.GetPixel(x, y);

            // No rellenar borde
            if (objetivo.ToArgb() == Color.Black.ToArgb())
            {
                MessageBox.Show("Elija un punto dentro de la figura (no en el borde).");
                return;
            }

            FloodFill(x, y, objetivo, colorRelleno);

            picCanvas.Invalidate();
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
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
