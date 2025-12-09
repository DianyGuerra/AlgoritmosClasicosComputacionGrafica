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

        // ========= CAMPOS PARA ANIMACIÓN =========
        Timer timerFlood;                    // controla la animación
        Queue<Point> colaPixeles;           // píxeles pendientes por rellenar
        Color colorObjetivoAnim;            // color original que queremos reemplazar
        Color colorRellenoAnim;             // color con el que estamos rellenando
        bool animando = false;              // bandera para bloquear dibujo mientras anima

        public frmFloodFill()
        {
            InitializeComponent();

            lienzo = new Bitmap(picCanvas.Width, picCanvas.Height);
            picCanvas.Image = lienzo;

            // --- Configuración del Timer para animar ---
            timerFlood = new Timer();
            timerFlood.Interval = 1;              // velocidad de animación (ms por tick)
            timerFlood.Tick += timerFlood_Tick;

            colaPixeles = new Queue<Point>();
        }

        // ================== DIBUJAR CON EL MOUSE ==================
        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (animando) return;  // no permitir dibujar mientras se rellena

            dibujando = true;
            puntoAnterior = e.Location;
        }

        private void picCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!dibujando || animando) return;

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
            if (animando) return;

            dibujando = false;

            // SI el mouse no se movió, se toma como clic para semilla
            if (puntoAnterior == e.Location)
            {
                puntoSemilla = e.Location;

                MessageBox.Show("Punto semilla seleccionado en: " + puntoSemilla);

                picCanvas.Invalidate();
            }
        }

        // ================== SELECCIONAR COLOR ==================
        private void btnPickColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorRelleno = colorDialog1.Color;
                btnPickColor.BackColor = colorRelleno;
            }
        }

        // ================== INICIAR FLOOD FILL (ANIMADO) ==================
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

            // --------- Preparar animación ---------
            colaPixeles.Clear();
            colaPixeles.Enqueue(new Point(x, y));

            colorObjetivoAnim = objetivo;
            colorRellenoAnim = colorRelleno;

            animando = true;
            btnFoodFill.Enabled = false;   // para que no disparen otro relleno

            timerFlood.Start();            // comienza la animación
        }

        // ================== TICK DEL TIMER (ANIMACIÓN) ==================
        private void timerFlood_Tick(object sender, EventArgs e)
        {
            // Cuántos píxeles procesar por tick (sube o baja para cambiar la velocidad)
            int pixelsPorTick = 500;
            int procesados = 0;

            while (colaPixeles.Count > 0 && procesados < pixelsPorTick)
            {
                Point p = colaPixeles.Dequeue();

                // Límites
                if (p.X < 0 || p.X >= lienzo.Width || p.Y < 0 || p.Y >= lienzo.Height)
                    continue;

                Color actual = lienzo.GetPixel(p.X, p.Y);

                // Solo rellenar los que siguen siendo del color objetivo
                if (actual.ToArgb() != colorObjetivoAnim.ToArgb())
                    continue;

                // Pintar pixel
                lienzo.SetPixel(p.X, p.Y, colorRellenoAnim);

                // Agregar vecinos
                colaPixeles.Enqueue(new Point(p.X + 1, p.Y));
                colaPixeles.Enqueue(new Point(p.X - 1, p.Y));
                colaPixeles.Enqueue(new Point(p.X, p.Y + 1));
                colaPixeles.Enqueue(new Point(p.X, p.Y - 1));

                procesados++;
            }

            // Redibujar
            picCanvas.Invalidate();

            // Si ya no hay más píxeles por procesar, detener animación
            if (colaPixeles.Count == 0)
            {
                timerFlood.Stop();
                animando = false;
                btnFoodFill.Enabled = true;
            }
        }

        // ================== LIMPIAR ==================
        private void btnClean_Click(object sender, EventArgs e)
        {
            timerFlood.Stop();
            animando = false;
            colaPixeles.Clear();
            puntoSemilla = null;

            lienzo = new Bitmap(picCanvas.Width, picCanvas.Height);
            picCanvas.Image = lienzo;
            picCanvas.Invalidate();
        }

        // ================== CERRAR ==================
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
