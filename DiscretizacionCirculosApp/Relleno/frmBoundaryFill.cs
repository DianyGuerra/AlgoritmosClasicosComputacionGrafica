using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DiscretizacionCirculosApp.Relleno
{
    public partial class frmBoundaryFill : Form
    {
        // --------------------------------------------------------------------
        // Campos del formulario
        // --------------------------------------------------------------------
        Bitmap lienzo;                      // Bitmap donde se dibuja y se rellena
        Color colorRelleno = Color.Empty;   // Color elegido por el usuario
        readonly Color colorBorde = Color.Black;

        bool dibujando = false;             // Controla si el usuario está trazando la figura
        Point puntoAnterior;                // Posición previa del mouse al dibujar

        Point? puntoSemilla = null;         // Punto inicial para el relleno

        // --- Campos para la ANIMACIÓN ---
        Stack<Point> pilaAnimacion;         // Pila de pixels pendientes de rellenar
        Timer timerAnim;                    // Timer que avanza la animación
        bool animando = false;              // Flag para no lanzar dos animaciones a la vez

        public frmBoundaryFill()
        {
            InitializeComponent();

            // Crear lienzo y asignarlo al PictureBox
            lienzo = new Bitmap(picCanvas.Width, picCanvas.Height);
            picCanvas.Image = lienzo;

            // Configurar timer de animación
            timerAnim = new Timer();
            timerAnim.Interval = 10;                    // velocidad: menor = más rápido
            timerAnim.Tick += TimerAnim_Tick;
        }

        // --------------------------------------------------------------------
        //  DIBUJO A MANO ALZADA (BORDE)
        // --------------------------------------------------------------------
        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            // No permitir dibujar mientras se anima
            if (animando) return;

            dibujando = true;
            puntoAnterior = e.Location;
        }

        private void picCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!dibujando || animando) return;

            // Dibujar líneas gruesas para evitar huecos
            using (Graphics g = Graphics.FromImage(lienzo))
            using (Pen lapiz = new Pen(colorBorde, 4))   // borde de grosor 4
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

            // Si el mouse NO se movió, se toma como clic para seleccionar la semilla
            if (puntoAnterior == e.Location)
            {
                puntoSemilla = e.Location;

                // Marca visual del punto semilla (opcional)
                using (Graphics g = Graphics.FromImage(lienzo))
                using (Brush b = new SolidBrush(Color.Gray))
                {
                    g.FillEllipse(b, e.X - 3, e.Y - 3, 6, 6);
                }

                MessageBox.Show("Punto semilla seleccionado en: " + puntoSemilla);
                picCanvas.Invalidate();
            }
        }

        // --------------------------------------------------------------------
        //  BOTÓN: ESCOGER COLOR
        // --------------------------------------------------------------------
        private void btnPickColor_Click(object sender, EventArgs e)
        {
            if (animando) return;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorRelleno = colorDialog1.Color;
                btnPickColor.BackColor = colorRelleno;
            }
        }

        // --------------------------------------------------------------------
        //  INICIO DEL BOUNDARY FILL ANIMADO
        // --------------------------------------------------------------------
        private void btnBoundaryFill_Click(object sender, EventArgs e)
        {
            if (animando) return;   // ya hay una animación en curso

            // Validaciones
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

            // Verificar que la semilla está dentro del lienzo
            if (x < 0 || x >= lienzo.Width || y < 0 || y >= lienzo.Height)
            {
                MessageBox.Show("El punto semilla está fuera del área de dibujo.");
                return;
            }

            Color colorEnPunto = lienzo.GetPixel(x, y);

            // Evitar iniciar sobre el borde
            if (colorEnPunto.ToArgb() == colorBorde.ToArgb())
            {
                MessageBox.Show("Seleccione un punto dentro de la figura, no sobre el borde.");
                return;
            }

            // Configurar animación
            pilaAnimacion = new Stack<Point>();
            pilaAnimacion.Push(new Point(x, y));

            animando = true;
            // Desactivar botones mientras se anima
            btnBoundaryFill.Enabled = false;
            btnPickColor.Enabled = false;
            btnClean.Enabled = false;

            timerAnim.Start();  // Comenzar animación
        }

        // --------------------------------------------------------------------
        //  TICK DEL TIMER: hace avanzar la animación
        // --------------------------------------------------------------------
        private void TimerAnim_Tick(object sender, EventArgs e)
        {
            if (pilaAnimacion == null || pilaAnimacion.Count == 0)
            {
                // Termina animación
                timerAnim.Stop();
                animando = false;

                // Reactivar botones
                btnBoundaryFill.Enabled = true;
                btnPickColor.Enabled = true;
                btnClean.Enabled = true;

                return;
            }

            // Procesar un bloque de píxeles por tick
            int pixelesPorTick = 400;   // puedes subir/bajar este número
            int procesados = 0;

            while (pilaAnimacion.Count > 0 && procesados < pixelesPorTick)
            {
                Point p = pilaAnimacion.Pop();
                procesados++;

                // Límites
                if (p.X < 0 || p.X >= lienzo.Width || p.Y < 0 || p.Y >= lienzo.Height)
                    continue;

                Color actual = lienzo.GetPixel(p.X, p.Y);

                // NO rellenar si es borde
                if (actual.ToArgb() == colorBorde.ToArgb())
                    continue;

                // NO rellenar si ya está del color de relleno
                if (actual.ToArgb() == colorRelleno.ToArgb())
                    continue;

                // Pintamos el pixel
                lienzo.SetPixel(p.X, p.Y, colorRelleno);

                // Vecinos 4-conectados
                pilaAnimacion.Push(new Point(p.X + 1, p.Y));
                pilaAnimacion.Push(new Point(p.X - 1, p.Y));
                pilaAnimacion.Push(new Point(p.X, p.Y + 1));
                pilaAnimacion.Push(new Point(p.X, p.Y - 1));
            }

            // Actualizar la imagen en pantalla
            picCanvas.Invalidate();
        }

        // --------------------------------------------------------------------
        //  LIMPIAR
        // --------------------------------------------------------------------
        private void btnClean_Click(object sender, EventArgs e)
        {
            if (animando) return;

            lienzo = new Bitmap(picCanvas.Width, picCanvas.Height);
            picCanvas.Image = lienzo;
            puntoSemilla = null;
            picCanvas.Invalidate();
        }

        // --------------------------------------------------------------------
        //  CERRAR
        // --------------------------------------------------------------------
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
