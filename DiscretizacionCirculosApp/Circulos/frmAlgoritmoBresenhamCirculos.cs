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
    public partial class frmAlgoritmoBresenhamCirculos : Form
    {
        public frmAlgoritmoBresenhamCirculos()
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
            txtRadius.Clear();
            txtRadius.Focus();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {

            //Inicializar constantes
            picCanvas.Refresh();
            Graphics g = picCanvas.CreateGraphics();
            int cx = picCanvas.Width / 2;
            int cy = picCanvas.Height / 2;
            int radius;

            // Validar entrada
            if (!int.TryParse(txtRadius.Text, out radius) || radius <= 0)
            {
                MessageBox.Show("Por favor ingresa un radio mayor a 0 (solo enteros positivos).");
                return;
            }


            // Algoritmo de Bresenham para círculos
            int x = 0;
            int y = radius;
            int d = 3 - (2 * radius);

            while (x <= y)
            {
                Dibujar8Octantes(g, cx, cy, x, y);

                if (d < 0)
                {
                    d = d + (4 * x) + 6;
                }
                else
                {
                    d = d + 4 * (x - y) + 10;
                    y--;
                }
                x++;
            }
        }

        // Función para dibujar los 8 octantes del círculo
        private void Dibujar8Octantes(Graphics g, int cx, int cy, int x, int y)
        {
            int SF = checkBox1.Checked ? 5 : 1;
            Brush brush = checkBox1.Checked ? Brushes.Red : Brushes.Black;
            int size = checkBox1.Checked ? 4 : 2;

            g.FillRectangle(brush, cx + x * SF, cy + y * SF, size, size);
            g.FillRectangle(brush, cx - x * SF, cy + y * SF, size, size);
            g.FillRectangle(brush, cx + x * SF, cy - y * SF, size, size);
            g.FillRectangle(brush, cx - x * SF, cy - y * SF, size, size);

            g.FillRectangle(brush, cx + y * SF, cy + x * SF, size, size);
            g.FillRectangle(brush, cx - y * SF, cy + x * SF, size, size);
            g.FillRectangle(brush, cx + y * SF, cy - x * SF, size, size);
            g.FillRectangle(brush, cx - y * SF, cy - x * SF, size, size);
        }


    }
}

