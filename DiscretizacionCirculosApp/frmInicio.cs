using DiscretizacionCirculosApp.RecorteLineas;
using DiscretizacionCirculosApp.RecortePoligonos;
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

        private void bToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBoundaryFill frm = new frmBoundaryFill();
            frm.MdiParent = this;
            frm.Show();
        }

        private void scanLineFillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScanlineFill frm = new frmScanlineFill();
            frm.MdiParent = this;
            frm.Show();
        }

        private void algoritmoCohenSutherlandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCohenSutherland frm = new frmCohenSutherland();
            frm.MdiParent = this;
            frm.Show();
        }

        private void algoritmoLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLiangBarsky frm = new frmLiangBarsky();
            frm.MdiParent = this;
            frm.Show();
        }

        private void algoritmoCyrusBeckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMidpointClip frm = new frmMidpointClip();
            frm.MdiParent = this;
            frm.Show();
        }

        private void sutherlandHodgmanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSutherlandHodgman frm = new frmSutherlandHodgman();
            frm.MdiParent = this;
            frm.Show();
        }

        private void weilerAthertonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRecortePoligonoSegmentos frm = new frmRecortePoligonoSegmentos();
            frm.MdiParent = this;
            frm.Show();
        }

        private void algoritmoDeRecorteDePolígonosPorSegmentosUsandoCohenSutherlandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRecortePoligonoSegmentos frm = new frmRecortePoligonoSegmentos();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
