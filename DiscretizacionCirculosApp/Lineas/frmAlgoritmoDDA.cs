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
    public partial class frmAlgoritmoDDA : Form
    {
        public frmAlgoritmoDDA()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {

            //Inicializar constantes
            picCanvas.Refresh();
            Graphics g = picCanvas.CreateGraphics();
            Pen pen = new Pen(Color.Black);
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


            //Algoritmo DDA

            int dx = x1 - x0;
            int dy = y1 - y0;
            int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));

            float xIncrement = dx / (float)steps;
            float yIncrement = dy / (float)steps;

            float x = x0;
            float y = y0;

            for (int i = 0; i <= steps; i++)
            {
                g.FillRectangle(brushPasos, x * SF, y * SF, 8, 8);

                x += xIncrement;
                y += yIncrement;

            }

            //Dibujar puntos inicial y final
            g.FillEllipse(brushPuntos, x0 * SF, y0 * SF, 8, 8);
            g.FillEllipse(brushPuntos, x1 * SF, y1 * SF, 8, 8);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            picCanvas.Refresh();
        }
    }
}
