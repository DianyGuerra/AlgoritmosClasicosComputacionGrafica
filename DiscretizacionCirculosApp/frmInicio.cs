using DiscretizacionCirculosApp.Relleno;
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
    public partial class frmInicio : Form
    {
        public frmInicio()
        {
            InitializeComponent();
        }

        private void algoritmoDDAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAlgoritmoDDA frm = new frmAlgoritmoDDA();
            frm.MdiParent = this;
            frm.Show();
        }

        private void algoritmoBresenhamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAlgoritmoBresenham frm = new frmAlgoritmoBresenham();
            frm.MdiParent = this;
            frm.Show();
        }

        private void algoritmoDelPuntoMedioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAlgoritmoPuntoMedio frm = new frmAlgoritmoPuntoMedio();
            frm.MdiParent = this;
            frm.Show();
        }

        private void discretizacionDeCirculoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPuntoMedioCirculo frm = new frmPuntoMedioCirculo();
            frm.MdiParent = this;
            frm.Show();
        }

        private void algoritmoDDAParCirculosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmParametricoPolarCirculos frm = new frmParametricoPolarCirculos();
            frm.MdiParent = this;
            frm.Show();
        }

        private void algoritmoBresenhamToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAlgoritmoBresenhamCirculos frm = new frmAlgoritmoBresenhamCirculos();
            frm.MdiParent = this;
            frm.Show();
        }

        private void floodFillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFloodFill frm = new frmFloodFill();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
