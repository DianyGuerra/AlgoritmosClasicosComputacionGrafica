using System;
using System.Drawing;
using System.Windows.Forms;

namespace DiscretizacionCirculosApp.Relleno
{
    public partial class frmCohenSutherland : Form
    {
        Bitmap lienzo;
        Rectangle ventanaRecorte;   // área visible (ventana)

        bool dibujandoLinea = false;
        PointF pInicio, pFin;

        public frmCohenSutherland()
        {
            InitializeComponent();

            // Crear bitmap y asignarlo al PictureBox
            lienzo = new Bitmap(picCanvas.Width, picCanvas.Height);
            picCanvas.Image = lienzo;

            // Definir ventana de recorte centrada con un margen
            int margen = 100;
            ventanaRecorte = new Rectangle(
                margen,
                margen,
                picCanvas.Width - 2 * margen,
                picCanvas.Height - 2 * margen
            );

            DibujarVentana();

            // Conectar eventos del mouse (si no lo hiciste en el diseñador)
            picCanvas.MouseDown += picCanvas_MouseDown;
            picCanvas.MouseUp += picCanvas_MouseUp;
        }

        // ================== DIBUJAR VENTANA ==================
        void DibujarVentana()
        {
            using (Graphics g = Graphics.FromImage(lienzo))
            {
                g.Clear(Color.Transparent);

                using (Brush b = new SolidBrush(Color.FromArgb(80, 0, 255, 0)))
                using (Pen penVentana = new Pen(Color.White, 2))
                {
                    g.FillRectangle(b, ventanaRecorte);   // área visible
                    g.DrawRectangle(penVentana, ventanaRecorte);
                }
            }

            picCanvas.Invalidate();
        }

        // ================== CÓDIGOS DE REGIÓN ==================
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


        string OutCodeToBits(OutCode code)
        {
            // convierte el enum a int y luego a binario de 4 bits
            int valor = (int)code;
            return Convert.ToString(valor, 2).PadLeft(4, '0');
        }


        // ================== ALGORITMO COHEN–SUTHERLAND ==================
        bool RecorteCohenSutherland(ref PointF p0, ref PointF p1)
        {
            OutCode out0 = ComputeOutCode(p0.X, p0.Y);
            OutCode out1 = ComputeOutCode(p1.X, p1.Y);
            bool accept = false;

            while (true)
            {
                // 1) Trivialmente aceptada
                if ((out0 | out1) == OutCode.Inside)
                {
                    accept = true;
                    break;
                }
                // 2) Trivialmente rechazada
                else if ((out0 & out1) != 0)
                {
                    break;
                }
                // 3) Parcialmente visible → calcular intersección
                else
                {
                    double x = 0, y = 0;
                    OutCode outOut = out0 != OutCode.Inside ? out0 : out1;

                    if ((outOut & OutCode.Top) != 0)
                    {
                        y = ventanaRecorte.Top;
                        x = p0.X + (p1.X - p0.X) *
                            (ventanaRecorte.Top - p0.Y) / (double)(p1.Y - p0.Y);
                    }
                    else if ((outOut & OutCode.Bottom) != 0)
                    {
                        y = ventanaRecorte.Bottom;
                        x = p0.X + (p1.X - p0.X) *
                            (ventanaRecorte.Bottom - p0.Y) / (double)(p1.Y - p0.Y);
                    }
                    else if ((outOut & OutCode.Right) != 0)
                    {
                        x = ventanaRecorte.Right;
                        y = p0.Y + (p1.Y - p0.Y) *
                            (ventanaRecorte.Right - p0.X) / (double)(p1.X - p0.X);
                    }
                    else if ((outOut & OutCode.Left) != 0)
                    {
                        x = ventanaRecorte.Left;
                        y = p0.Y + (p1.Y - p0.Y) *
                            (ventanaRecorte.Left - p0.X) / (double)(p1.X - p0.X);
                    }

                    if (outOut == out0)
                    {
                        p0 = new PointF((float)x, (float)y);
                        out0 = ComputeOutCode(p0.X, p0.Y);
                    }
                    else
                    {
                        p1 = new PointF((float)x, (float)y);
                        out1 = ComputeOutCode(p1.X, p1.Y);
                    }
                }
            }

            return accept;
        }

        // ================== MOUSE: DIBUJAR LÍNEA ==================
        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            // Solo con botón izquierdo
            if (e.Button != MouseButtons.Left) return;

            dibujandoLinea = true;
            pInicio = e.Location;
        }

        private void picCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (!dibujandoLinea) return;
            dibujandoLinea = false;

            pFin = e.Location;

            // Dibuja la línea completa en rojo
            using (Graphics g = Graphics.FromImage(lienzo))
            using (Pen penCompleta = new Pen(Color.Red, 2))
            {
                g.DrawLine(penCompleta, pInicio, pFin);

                // Calcula códigos de región de los extremos ORIGINALES
                OutCode c0 = ComputeOutCode(pInicio.X, pInicio.Y);
                OutCode c1 = ComputeOutCode(pFin.X, pFin.Y);

                // Muestra los códigos en el label
                string bits0 = OutCodeToBits(c0);
                string bits1 = OutCodeToBits(c1);
                lblInfo.Text = $"Cohen-Sutherland\nP0: {bits0}\nP1: {bits1}";

                // Ahora aplica el recorte
                PointF q0 = pInicio;
                PointF q1 = pFin;

                if (RecorteCohenSutherland(ref q0, ref q1))
                {
                    using (Pen penVisible = new Pen(Color.Blue, 3))
                    {
                        g.DrawLine(penVisible, q0, q1);
                    }
                }
            }

            picCanvas.Invalidate();
        }


        // ================== BOTÓN: LIMPIAR ==================
        private void btnClean_Click(object sender, EventArgs e)
        {
            DibujarVentana();
        }

        // ================== BOTÓN: CERRAR ==================
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
