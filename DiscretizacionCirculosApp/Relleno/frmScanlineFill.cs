using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DiscretizacionCirculosApp.Relleno
{
    public partial class frmScanlineFill : Form
    {
        Bitmap lienzo;
        Color colorRelleno = Color.Empty;

        // Lista de vértices del polígono generado
        List<Point> poligono = new List<Point>();

        // Rango vertical del polígono
        int minY, maxY;

        // ===== Campos para la animación =====
        bool animando = false;
        int yInicioAnim, yFinAnim, yActualAnim;
        Color colorAnim;

        public frmScanlineFill()
        {
            InitializeComponent();

            // Crear bitmap en blanco
            lienzo = new Bitmap(picCanvas.Width, picCanvas.Height);
            using (Graphics g = Graphics.FromImage(lienzo))
            {
                g.Clear(Color.White);
            }
            picCanvas.Image = lienzo;

            // Mejorar refresco visual
            this.DoubleBuffered = true;

            // Configurar timer de animación
            timerAnim = new Timer();
            timerAnim.Interval = 15;              // velocidad de animación (ms)
            timerAnim.Tick += timerAnim_Tick;
        }

        // ===================== BOTÓN: COLOR ======================
        private void btnPickColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorRelleno = colorDialog1.Color;
                btnPickColor.BackColor = colorRelleno;
            }
        }

        // ===================== BOTÓN: GENERAR POLÍGONO ======================
        private void btnGeneratePolygon_Click(object sender, EventArgs e)
        {
            // Si estaba animando, parar
            if (animando)
            {
                timerAnim.Stop();
                animando = false;
            }

            // Limpiar lienzo
            lienzo = new Bitmap(picCanvas.Width, picCanvas.Height);
            using (Graphics g = Graphics.FromImage(lienzo))
            {
                g.Clear(Color.White);
            }
            picCanvas.Image = lienzo;

            poligono.Clear();

            Random rnd = new Random();

            int n = rnd.Next(6, 13);   // 6 a 12 vértices
            float cx = picCanvas.Width / 2f;
            float cy = picCanvas.Height / 2f;
            float maxR = Math.Min(picCanvas.Width, picCanvas.Height) / 3f;

            minY = picCanvas.Height;
            maxY = 0;

            for (int i = 0; i < n; i++)
            {
                float angulo = (float)(i * (2 * Math.PI / n)) +
                               (float)(rnd.NextDouble() * 0.5 - 0.25);
                float radio = maxR * (0.5f + (float)rnd.NextDouble() * 0.5f);

                int px = (int)(cx + Math.Cos(angulo) * radio);
                int py = (int)(cy + Math.Sin(angulo) * radio);

                poligono.Add(new Point(px, py));

                if (py < minY) minY = py;
                if (py > maxY) maxY = py;
            }

            // Dibujar contorno del polígono
            using (Graphics g = Graphics.FromImage(lienzo))
            using (Pen lapiz = new Pen(Color.Black, 4))   // grosor del borde
            {
                lapiz.LineJoin = System.Drawing.Drawing2D.LineJoin.Round; // esquinas más bonitas
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // suaviza bordes
                g.DrawPolygon(lapiz, poligono.ToArray());
            }

            picCanvas.Invalidate();
        }

        // ===================== UN PASO DE SCANLINE (UNA FILA) ======================
        void ScanlineFillStep(int y, Color relleno)
        {
            List<float> intersecciones = new List<float>();

            // Buscar intersecciones de la línea horizontal y con cada arista
            for (int i = 0; i < poligono.Count; i++)
            {
                Point p1 = poligono[i];
                Point p2 = poligono[(i + 1) % poligono.Count];

                // Ignorar aristas horizontales
                if (p1.Y == p2.Y)
                    continue;

                int yMin = Math.Min(p1.Y, p2.Y);
                int yMax = Math.Max(p1.Y, p2.Y);

                // ¿La fila Y cruza esta arista?
                if (y >= yMin && y < yMax)
                {
                    float x = p1.X + (float)(y - p1.Y) * (p2.X - p1.X) / (float)(p2.Y - p1.Y);
                    intersecciones.Add(x);
                }
            }

            // Si hay menos de 2 intersecciones, no hay nada que rellenar
            if (intersecciones.Count < 2)
                return;

            // Ordenar las intersecciones de izquierda a derecha
            intersecciones.Sort();

            // Rellenar entre pares [x0,x1], [x2,x3], ...
            for (int i = 0; i + 1 < intersecciones.Count; i += 2)
            {
                int xIni = (int)Math.Ceiling(intersecciones[i]);
                int xFin = (int)Math.Floor(intersecciones[i + 1]);

                // Asegurar rangos válidos
                xIni = Math.Max(0, xIni);
                xFin = Math.Min(lienzo.Width - 1, xFin);

                for (int x = xIni; x <= xFin; x++)
                {
                    lienzo.SetPixel(x, y, relleno);
                }
            }
        }

        // ===================== TIMER: ANIMACIÓN ======================
        private void timerAnim_Tick(object sender, EventArgs e)
        {
            if (!animando) return;

            if (yActualAnim > yFinAnim)
            {
                // Terminar animación
                timerAnim.Stop();
                animando = false;

                // Reactivar botones
                btnScanlineFill.Enabled = true;
                btnGeneratePolygon.Enabled = true;
                btnPickColor.Enabled = true;

                return;
            }

            // Rellenar UNA sola fila
            ScanlineFillStep(yActualAnim, colorAnim);
            yActualAnim++;

            picCanvas.Invalidate();
        }

        // ===================== BOTÓN: RELLENAR (INICIA ANIMACIÓN) ======================
        private void btnScanlineFill_Click(object sender, EventArgs e)
        {
            if (colorRelleno == Color.Empty)
            {
                MessageBox.Show("Seleccione un color de relleno.");
                return;
            }

            if (poligono.Count < 3)
            {
                MessageBox.Show("Primero genere un polígono.");
                return;
            }

            // Definir rango de Y a rellenar
            yInicioAnim = Math.Max(0, minY);
            yFinAnim = Math.Min(lienzo.Height - 1, maxY);
            yActualAnim = yInicioAnim;
            colorAnim = colorRelleno;

            // Desactivar botones mientras se anima
            btnScanlineFill.Enabled = false;
            btnGeneratePolygon.Enabled = false;
            btnPickColor.Enabled = false;

            animando = true;
            timerAnim.Start();
        }

        // ===================== BOTÓN: LIMPIAR ======================
        private void btnClean_Click(object sender, EventArgs e)
        {
            if (animando)
            {
                timerAnim.Stop();
                animando = false;
            }

            poligono.Clear();
            lienzo = new Bitmap(picCanvas.Width, picCanvas.Height);
            using (Graphics g = Graphics.FromImage(lienzo))
            {
                g.Clear(Color.White);
            }
            picCanvas.Image = lienzo;
            picCanvas.Invalidate();

            // Reactivar botones por si estaban desactivados
            btnScanlineFill.Enabled = true;
            btnGeneratePolygon.Enabled = true;
            btnPickColor.Enabled = true;
        }

        // ===================== BOTÓN: CERRAR ======================
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
