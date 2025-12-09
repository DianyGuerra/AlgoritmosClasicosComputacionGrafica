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
    public partial class frmParametricoPolarCirculos : Form
    {
        public frmParametricoPolarCirculos()
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
            int SF = 1;     // Factor de escala

            // Validar entrada
            if (!int.TryParse(txtRadius.Text, out radius) || radius <= 0)
            {
                MessageBox.Show("Por favor ingresa un radio mayor a 0 (solo enteros positivos).");
                return;
            }


            // Algoritmo Paramétrico Polar para círculos
            float x = radius;
            float y = 0;

            float step = 1f / radius;
            float theta = 0;

            while (theta <= 2 * Math.PI)
            {
                x = (float)(radius * Math.Cos(theta));
                y = (float)(radius * Math.Sin(theta));

                if (checkBox1.Checked)
                {
                    SF = 5;
                    g.FillEllipse(Brushes.Red, cx + (int)x * SF, cy + (int)y * SF, 4, 4);
                }
                else
                {
                    g.FillEllipse(Brushes.Black, cx + (int)x * SF, cy + (int)y * SF, 4, 4);
                }

                theta += step;
            }
        }


    }
}
