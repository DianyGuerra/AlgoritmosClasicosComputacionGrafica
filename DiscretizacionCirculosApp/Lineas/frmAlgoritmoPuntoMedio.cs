using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscretizacionCirculosApp
{
    public partial class frmAlgoritmoPuntoMedio : Form
    {
        public frmAlgoritmoPuntoMedio()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            picCanvas.Refresh();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {

            // Inicializar constantes
            picCanvas.Refresh();
            Graphics g = picCanvas.CreateGraphics();
            Brush brushPasos = new SolidBrush(Color.Black);
            Brush brushPuntos = new SolidBrush(Color.Red);

            float SF = 10;

            int x0, y0, x1, y1;


            // Validar entradas que sean enteros
            if (!int.TryParse(txtX1.Text, out x0) || x0 < 0 ||
                !int.TryParse(txtY1.Text, out y0) || y0 < 0 ||
                !int.TryParse(txtX2.Text, out x1) || x1 < 0 ||
                !int.TryParse(txtY2.Text, out y1) || y1 < 0)
            {
                MessageBox.Show("Por favor ingresa solo números enteros POSITIVOS (sin negativos).");
                return;
            }


            // --------- ALGORITMO DE PUNTO MEDIO GENERALIZADO ---------

            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);

            int sx = (x1 >= x0) ? 1 : -1; // dirección en X
            int sy = (y1 >= y0) ? 1 : -1; // dirección en Y

            int x = x0;
            int y = y0;

            // Caso 1: |pendiente| <= 1 → avanzamos en X
            if (dx >= dy)
            {
                int d = 2 * dy - dx; // decisión inicial

                for (int i = 0; i <= dx; i++)
                {
                    g.FillRectangle(brushPasos, x * SF, y * SF, 8, 8);

                    if (d > 0)
                    {
                        y += sy;
                        d -= 2 * dx;
                    }

                    d += 2 * dy;
                    x += sx;
                }
            }
            // Caso 2: |pendiente| > 1 → avanzamos en Y
            else
            {
                int d = 2 * dx - dy; // decisión inicial

                for (int i = 0; i <= dy; i++)
                {
                    g.FillRectangle(brushPasos, x * SF, y * SF, 8, 8);

                    if (d > 0)
                    {
                        x += sx;
                        d -= 2 * dy;
                    }

                    d += 2 * dx;
                    y += sy;
                }
            }

            // Dibujo de los puntos inicial y final
            g.FillEllipse(brushPuntos, x0 * SF, y0 * SF, 8, 8);
            g.FillEllipse(brushPuntos, x1 * SF, y1 * SF, 8, 8);

        }
    }
}
