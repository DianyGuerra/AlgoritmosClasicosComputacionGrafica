using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DiscretizacionCirculosApp.Relleno
{
    public partial class frmSutherlandHodgman : Form
    {
        Bitmap lienzo;
        Rectangle ventanaRecorte;

        // Polígono original (clics del usuario)
        List<PointF> poligonoOriginal = new List<PointF>();

        // Polígono recortado
        List<PointF> poligonoClipped = new List<PointF>();

        public frmSutherlandHodgman()
        {
            InitializeComponent();

            // Crear bitmap en blanco
            lienzo = new Bitmap(picCanvas.Width, picCanvas.Height);
            picCanvas.Image = lienzo;

            // Definir ventana de recorte centrada
            int margen = 80;
            ventanaRecorte = new Rectangle(
                margen,
                margen,
                picCanvas.Width - 2 * margen,
                picCanvas.Height - 2 * margen
            );

            RedibujarTodo();
        }

        // ===================== DIBUJO GENERAL ======================

        void RedibujarTodo()
        {
            using (Graphics g = Graphics.FromImage(lienzo))
            {
                g.Clear(Color.White);

                // Dibujar ventana de recorte
                using (Brush b = new SolidBrush(Color.FromArgb(230, 220, 255, 220)))
                using (Pen p = new Pen(Color.Black, 2))
                {
                    g.FillRectangle(b, ventanaRecorte);
                    g.DrawRectangle(p, ventanaRecorte);
                }

                // ===================== POLÍGONO ORIGINAL (ROJO) =====================

                // 1) Dibujar líneas del polígono original si hay al menos 2 vértices
                if (poligonoOriginal.Count >= 2)
                {
                    using (Pen p = new Pen(Color.Red, 2))
                    {
                        g.DrawLines(p, poligonoOriginal.ToArray());
                        // si quieres cerrar el polígono visualmente:
                        // g.DrawLine(p, poligonoOriginal[poligonoOriginal.Count - 1], poligonoOriginal[0]);
                    }
                }

                // 2) Dibujar SIEMPRE los puntitos en cada vértice (aunque sea solo 1)
                if (poligonoOriginal.Count >= 1)
                {
                    using (Brush b = new SolidBrush(Color.Red))
                    {
                        foreach (var v in poligonoOriginal)
                            g.FillEllipse(b, v.X - 3, v.Y - 3, 6, 6);
                    }
                }

                // ===================== POLÍGONO RECORTADO (AZUL) =====================

                if (poligonoClipped.Count >= 3)
                {
                    using (Brush b = new SolidBrush(Color.FromArgb(160, 80, 80, 255)))
                    using (Pen p = new Pen(Color.Blue, 3))
                    {
                        g.FillPolygon(b, poligonoClipped.ToArray());
                        g.DrawPolygon(p, poligonoClipped.ToArray());
                    }
                }
            }

            picCanvas.Invalidate();
        }


        // ===================== CAPTURA DE VÉRTICES ======================

        // Clic izquierdo: agregar vértice al polígono
        private void picCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            // Cada clic es un vértice nuevo
            poligonoOriginal.Add(e.Location);

            // Como se agregó un punto nuevo, invalida el recorte previo
            poligonoClipped.Clear();

            lblInfo.Text = $"Sutherland–Hodgman: vértices entrada = {poligonoOriginal.Count}";
            RedibujarTodo();
        }

        // ===================== BOTÓN: RECORTAR POLÍGONO ======================

        private void btnClip_Click(object sender, EventArgs e)
        {
            if (poligonoOriginal.Count < 3)
            {
                MessageBox.Show("Primero defina un polígono con al menos 3 vértices (clic izquierdo en el área de dibujo).");
                return;
            }

            // Cerrar polígono de entrada (por si no está 100% cerrado)
            var entrada = new List<PointF>(poligonoOriginal);
            if (entrada[0] != entrada[entrada.Count - 1])
                entrada.Add(entrada[0]);

            poligonoClipped = SutherlandHodgmanClip(entrada, ventanaRecorte);

            lblInfo.Text =
                $"Sutherland–Hodgman: vértices entrada = {poligonoOriginal.Count}, " +
                $"vértices salida = {poligonoClipped.Count}";

            RedibujarTodo();
        }

        // ===================== BOTÓN: LIMPIAR ======================

        private void btnClean_Click(object sender, EventArgs e)
        {
            poligonoOriginal.Clear();
            poligonoClipped.Clear();
            lblInfo.Text = "Sutherland–Hodgman: ";
            RedibujarTodo();
        }

        // ===================== BOTÓN: CERRAR ======================

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // =========================================================
        //         IMPLEMENTACIÓN SUTHERLAND–HODGMAN
        // =========================================================

        enum BordeClip { Left, Right, Top, Bottom }

        List<PointF> SutherlandHodgmanClip(List<PointF> poly, Rectangle rect)
        {
            var salida = new List<PointF>(poly);
            salida = ClipContraBorde(salida, rect, BordeClip.Left);
            salida = ClipContraBorde(salida, rect, BordeClip.Right);
            salida = ClipContraBorde(salida, rect, BordeClip.Top);
            salida = ClipContraBorde(salida, rect, BordeClip.Bottom);
            return salida;
        }

        List<PointF> ClipContraBorde(List<PointF> entrada, Rectangle rect, BordeClip borde)
        {
            var salida = new List<PointF>();
            if (entrada.Count == 0) return salida;

            PointF S = entrada[entrada.Count - 1]; // último vértice
            bool Sinside = EstaDentro(S, rect, borde);

            foreach (var P in entrada)
            {
                bool Pinside = EstaDentro(P, rect, borde);

                if (Pinside)
                {
                    if (Sinside)
                    {
                        // in -> in : solo agrego P
                        salida.Add(P);
                    }
                    else
                    {
                        // out -> in : agrego intersección y luego P
                        PointF I = Interseccion(S, P, rect, borde);
                        salida.Add(I);
                        salida.Add(P);
                    }
                }
                else
                {
                    if (Sinside)
                    {
                        // in -> out : agrego solo intersección
                        PointF I = Interseccion(S, P, rect, borde);
                        salida.Add(I);
                    }
                    // out -> out : no agrego nada
                }

                S = P;
                Sinside = Pinside;
            }

            return salida;
        }

        bool EstaDentro(PointF p, Rectangle rect, BordeClip borde)
        {
            switch (borde)
            {
                case BordeClip.Left:
                    return p.X >= rect.Left;
                case BordeClip.Right:
                    return p.X <= rect.Right;
                case BordeClip.Top:
                    // Recuerda: Y crece hacia abajo
                    return p.Y >= rect.Top;
                case BordeClip.Bottom:
                    return p.Y <= rect.Bottom;
                default:
                    return true;
            }
        }

        PointF Interseccion(PointF S, PointF P, Rectangle rect, BordeClip borde)
        {
            float x = 0, y = 0;
            float dx = P.X - S.X;
            float dy = P.Y - S.Y;

            switch (borde)
            {
                case BordeClip.Left:
                    x = rect.Left;
                    y = S.Y + dy * ((x - S.X) / dx);
                    break;
                case BordeClip.Right:
                    x = rect.Right;
                    y = S.Y + dy * ((x - S.X) / dx);
                    break;
                case BordeClip.Top:
                    y = rect.Top;
                    x = S.X + dx * ((y - S.Y) / dy);
                    break;
                case BordeClip.Bottom:
                    y = rect.Bottom;
                    x = S.X + dx * ((y - S.Y) / dy);
                    break;
            }

            return new PointF(x, y);
        }
    }
}
