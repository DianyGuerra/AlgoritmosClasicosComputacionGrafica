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
    public partial class frmAlgoritmoBresenham : Form
    {
        public frmAlgoritmoBresenham()
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

            //Inicializar constantes
            picCanvas.Refresh();
            Graphics g = picCanvas.CreateGraphics();
            Brush brushPasos = new SolidBrush(Color.Black);
            Brush brushPuntos = new SolidBrush(Color.Red);

            float SF = 10;

            int x0, y0, x1, y1;

            //Validar entradas que sean enteros
            if (!int.TryParse(txtX1.Text, out x0) || x0 < 0 ||
                !int.TryParse(txtY1.Text, out y0) || y0 < 0 ||
                !int.TryParse(txtX2.Text, out x1) || x1 < 0 ||
                !int.TryParse(txtY2.Text, out y1) || y1 < 0)
            {
                MessageBox.Show("Por favor ingresa solo números enteros POSITIVOS (sin negativos).");
                return;
            }


            //Declaración de variables del algoritmo
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;

            //--------- ALGORITMO DE BRESENHAM ---------
            while (true)
            {
                g.FillRectangle(brushPasos, x0 * SF, y0 * SF, 8, 8);

                if (x0 == x1 && y0 == y1) break;

                int e2 = 2 * err;

                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }

                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }

            //Dibujar puntos inicial y final
            g.FillEllipse(brushPuntos, x0 * SF, y0 * SF, 8, 8);
            g.FillEllipse(brushPuntos, x1 * SF, y1 * SF, 8, 8);
        }
    }
}
