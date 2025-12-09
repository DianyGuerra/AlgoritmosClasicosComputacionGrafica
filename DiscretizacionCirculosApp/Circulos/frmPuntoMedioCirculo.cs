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
    public partial class frmPuntoMedioCirculo : Form
    {
        public frmPuntoMedioCirculo()
        {
            InitializeComponent();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {

            //Inicializar constantes
            picCanvas.Refresh();
            Graphics g = picCanvas.CreateGraphics();
            Brush brush = Brushes.Black;
            int cx = picCanvas.Width / 2;
            int cy = picCanvas.Height / 2;
            int radius;


            // Validar entrada
            if (!int.TryParse(txtRadius.Text, out radius) || radius <= 0)
            {
                MessageBox.Show("Por favor ingresa un radio mayor a 0 (solo enteros positivos).");
                return;
            }



            // Algoritmo de Punto Medio para círculos
            int x = 0;
            int y = radius;
            float p = 1 - radius;

            PlotPoint(g, brush, cx, cy, x, y);

            while (x < y)
            {
                x = x + 1;
                if (p<0)
                {
                    p=p+2*x+3;
                }
                else 
                { 
                    y = y - 1;
                    p = p + 2*(x-y) + 5;
                }
                PlotPoint(g, brush, cx, cy, x, y);
            }

        }

        // Función para plotear los 8 octantes
        private void PlotPoint(Graphics g, Brush b, int cx, int cy, int x, int y)
        {
            int SF = checkBox1.Checked ? 5 : 1;
            Brush brush = checkBox1.Checked ? Brushes.Red : Brushes.Black;
            int size = checkBox1.Checked ? 4 : 3;

            g.FillRectangle(brush, cx + x * SF, cy + y * SF, size, size);
            g.FillRectangle(brush, cx - x * SF, cy + y * SF, size, size);
            g.FillRectangle(brush, cx + x * SF, cy - y * SF, size, size);
            g.FillRectangle(brush, cx - x * SF, cy - y * SF, size, size);

            g.FillRectangle(brush, cx + y * SF, cy + x * SF, size, size);
            g.FillRectangle(brush, cx - y * SF, cy + x * SF, size, size);
            g.FillRectangle(brush, cx + y * SF, cy - x * SF, size, size);
            g.FillRectangle(brush, cx - y * SF, cy - x * SF, size, size);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            picCanvas.Refresh();
            txtRadius.Clear();
            txtRadius.Focus();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
