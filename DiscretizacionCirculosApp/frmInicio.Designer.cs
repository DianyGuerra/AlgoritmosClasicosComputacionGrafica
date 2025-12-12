namespace DiscretizacionCirculosApp
{
    partial class frmInicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.lineasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algoritmoDDAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algoritmoBresenhamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algoritmoDelPuntoMedioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.círculoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discretizacionDeCirculoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algoritmoDDAParCirculosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algoritmoBresenhamToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.algoritmosDeRellenoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.floodFillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanLineFillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recorteDeLíneasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algoritmoCohenSutherlandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algoritmoLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algoritmoCyrusBeckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recorteDePolígonosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sutherlandHodgmanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algoritmoDeRecorteDePolígonosPorSegmentosUsandoCohenSutherlandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recorteDePolígonosPorSegmentosUsandoLiangBarskyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lineasToolStripMenuItem,
            this.círculoToolStripMenuItem,
            this.algoritmosDeRellenoToolStripMenuItem,
            this.recorteDeLíneasToolStripMenuItem,
            this.recorteDePolígonosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // lineasToolStripMenuItem
            // 
            this.lineasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.algoritmoDDAToolStripMenuItem,
            this.algoritmoBresenhamToolStripMenuItem,
            this.algoritmoDelPuntoMedioToolStripMenuItem});
            this.lineasToolStripMenuItem.Name = "lineasToolStripMenuItem";
            this.lineasToolStripMenuItem.Size = new System.Drawing.Size(159, 21);
            this.lineasToolStripMenuItem.Text = "Discretización de Líneas";
            // 
            // algoritmoDDAToolStripMenuItem
            // 
            this.algoritmoDDAToolStripMenuItem.Name = "algoritmoDDAToolStripMenuItem";
            this.algoritmoDDAToolStripMenuItem.Size = new System.Drawing.Size(241, 24);
            this.algoritmoDDAToolStripMenuItem.Text = "Algoritmo DDA";
            this.algoritmoDDAToolStripMenuItem.Click += new System.EventHandler(this.algoritmoDDAToolStripMenuItem_Click);
            // 
            // algoritmoBresenhamToolStripMenuItem
            // 
            this.algoritmoBresenhamToolStripMenuItem.Name = "algoritmoBresenhamToolStripMenuItem";
            this.algoritmoBresenhamToolStripMenuItem.Size = new System.Drawing.Size(241, 24);
            this.algoritmoBresenhamToolStripMenuItem.Text = "Algoritmo Bresenham";
            this.algoritmoBresenhamToolStripMenuItem.Click += new System.EventHandler(this.algoritmoBresenhamToolStripMenuItem_Click);
            // 
            // algoritmoDelPuntoMedioToolStripMenuItem
            // 
            this.algoritmoDelPuntoMedioToolStripMenuItem.Name = "algoritmoDelPuntoMedioToolStripMenuItem";
            this.algoritmoDelPuntoMedioToolStripMenuItem.Size = new System.Drawing.Size(241, 24);
            this.algoritmoDelPuntoMedioToolStripMenuItem.Text = "Algoritmo del Punto Medio";
            this.algoritmoDelPuntoMedioToolStripMenuItem.Click += new System.EventHandler(this.algoritmoDelPuntoMedioToolStripMenuItem_Click);
            // 
            // círculoToolStripMenuItem
            // 
            this.círculoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.discretizacionDeCirculoToolStripMenuItem,
            this.algoritmoDDAParCirculosToolStripMenuItem,
            this.algoritmoBresenhamToolStripMenuItem1});
            this.círculoToolStripMenuItem.Name = "círculoToolStripMenuItem";
            this.círculoToolStripMenuItem.Size = new System.Drawing.Size(169, 21);
            this.círculoToolStripMenuItem.Text = "Discretización de Círculos";
            // 
            // discretizacionDeCirculoToolStripMenuItem
            // 
            this.discretizacionDeCirculoToolStripMenuItem.Name = "discretizacionDeCirculoToolStripMenuItem";
            this.discretizacionDeCirculoToolStripMenuItem.Size = new System.Drawing.Size(248, 24);
            this.discretizacionDeCirculoToolStripMenuItem.Text = "Algoritmo Punto Medio";
            this.discretizacionDeCirculoToolStripMenuItem.Click += new System.EventHandler(this.discretizacionDeCirculoToolStripMenuItem_Click);
            // 
            // algoritmoDDAParCirculosToolStripMenuItem
            // 
            this.algoritmoDDAParCirculosToolStripMenuItem.Name = "algoritmoDDAParCirculosToolStripMenuItem";
            this.algoritmoDDAParCirculosToolStripMenuItem.Size = new System.Drawing.Size(248, 24);
            this.algoritmoDDAParCirculosToolStripMenuItem.Text = "Algoritmo Paramétrico Polar";
            this.algoritmoDDAParCirculosToolStripMenuItem.Click += new System.EventHandler(this.algoritmoDDAParCirculosToolStripMenuItem_Click);
            // 
            // algoritmoBresenhamToolStripMenuItem1
            // 
            this.algoritmoBresenhamToolStripMenuItem1.Name = "algoritmoBresenhamToolStripMenuItem1";
            this.algoritmoBresenhamToolStripMenuItem1.Size = new System.Drawing.Size(248, 24);
            this.algoritmoBresenhamToolStripMenuItem1.Text = "Algoritmo Bresenham";
            this.algoritmoBresenhamToolStripMenuItem1.Click += new System.EventHandler(this.algoritmoBresenhamToolStripMenuItem1_Click);
            // 
            // algoritmosDeRellenoToolStripMenuItem
            // 
            this.algoritmosDeRellenoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.floodFillToolStripMenuItem,
            this.bToolStripMenuItem,
            this.scanLineFillToolStripMenuItem});
            this.algoritmosDeRellenoToolStripMenuItem.Name = "algoritmosDeRellenoToolStripMenuItem";
            this.algoritmosDeRellenoToolStripMenuItem.Size = new System.Drawing.Size(150, 21);
            this.algoritmosDeRellenoToolStripMenuItem.Text = "Algoritmos de Relleno";
            // 
            // floodFillToolStripMenuItem
            // 
            this.floodFillToolStripMenuItem.Name = "floodFillToolStripMenuItem";
            this.floodFillToolStripMenuItem.Size = new System.Drawing.Size(156, 24);
            this.floodFillToolStripMenuItem.Text = "Flood Fill";
            this.floodFillToolStripMenuItem.Click += new System.EventHandler(this.floodFillToolStripMenuItem_Click);
            // 
            // bToolStripMenuItem
            // 
            this.bToolStripMenuItem.Name = "bToolStripMenuItem";
            this.bToolStripMenuItem.Size = new System.Drawing.Size(156, 24);
            this.bToolStripMenuItem.Text = "Boundary Fill";
            this.bToolStripMenuItem.Click += new System.EventHandler(this.bToolStripMenuItem_Click);
            // 
            // scanLineFillToolStripMenuItem
            // 
            this.scanLineFillToolStripMenuItem.Name = "scanLineFillToolStripMenuItem";
            this.scanLineFillToolStripMenuItem.Size = new System.Drawing.Size(156, 24);
            this.scanLineFillToolStripMenuItem.Text = "ScanLine Fill";
            this.scanLineFillToolStripMenuItem.Click += new System.EventHandler(this.scanLineFillToolStripMenuItem_Click);
            // 
            // recorteDeLíneasToolStripMenuItem
            // 
            this.recorteDeLíneasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.algoritmoCohenSutherlandToolStripMenuItem,
            this.algoritmoLToolStripMenuItem,
            this.algoritmoCyrusBeckToolStripMenuItem});
            this.recorteDeLíneasToolStripMenuItem.Name = "recorteDeLíneasToolStripMenuItem";
            this.recorteDeLíneasToolStripMenuItem.Size = new System.Drawing.Size(124, 21);
            this.recorteDeLíneasToolStripMenuItem.Text = "Recorte de Líneas";
            // 
            // algoritmoCohenSutherlandToolStripMenuItem
            // 
            this.algoritmoCohenSutherlandToolStripMenuItem.Name = "algoritmoCohenSutherlandToolStripMenuItem";
            this.algoritmoCohenSutherlandToolStripMenuItem.Size = new System.Drawing.Size(256, 24);
            this.algoritmoCohenSutherlandToolStripMenuItem.Text = "Algoritmo Cohen - Sutherland";
            this.algoritmoCohenSutherlandToolStripMenuItem.Click += new System.EventHandler(this.algoritmoCohenSutherlandToolStripMenuItem_Click);
            // 
            // algoritmoLToolStripMenuItem
            // 
            this.algoritmoLToolStripMenuItem.Name = "algoritmoLToolStripMenuItem";
            this.algoritmoLToolStripMenuItem.Size = new System.Drawing.Size(256, 24);
            this.algoritmoLToolStripMenuItem.Text = "Algoritmo Liang - Barsky";
            this.algoritmoLToolStripMenuItem.Click += new System.EventHandler(this.algoritmoLToolStripMenuItem_Click);
            // 
            // algoritmoCyrusBeckToolStripMenuItem
            // 
            this.algoritmoCyrusBeckToolStripMenuItem.Name = "algoritmoCyrusBeckToolStripMenuItem";
            this.algoritmoCyrusBeckToolStripMenuItem.Size = new System.Drawing.Size(256, 24);
            this.algoritmoCyrusBeckToolStripMenuItem.Text = "Algoritmo Punto Medio";
            this.algoritmoCyrusBeckToolStripMenuItem.Click += new System.EventHandler(this.algoritmoCyrusBeckToolStripMenuItem_Click);
            // 
            // recorteDePolígonosToolStripMenuItem
            // 
            this.recorteDePolígonosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sutherlandHodgmanToolStripMenuItem,
            this.algoritmoDeRecorteDePolígonosPorSegmentosUsandoCohenSutherlandToolStripMenuItem,
            this.recorteDePolígonosPorSegmentosUsandoLiangBarskyToolStripMenuItem});
            this.recorteDePolígonosToolStripMenuItem.Name = "recorteDePolígonosToolStripMenuItem";
            this.recorteDePolígonosToolStripMenuItem.Size = new System.Drawing.Size(147, 21);
            this.recorteDePolígonosToolStripMenuItem.Text = "Recorte de polígonos";
            // 
            // sutherlandHodgmanToolStripMenuItem
            // 
            this.sutherlandHodgmanToolStripMenuItem.Name = "sutherlandHodgmanToolStripMenuItem";
            this.sutherlandHodgmanToolStripMenuItem.Size = new System.Drawing.Size(537, 24);
            this.sutherlandHodgmanToolStripMenuItem.Text = "Sutherland - Hodgman";
            this.sutherlandHodgmanToolStripMenuItem.Click += new System.EventHandler(this.sutherlandHodgmanToolStripMenuItem_Click);
            // 
            // algoritmoDeRecorteDePolígonosPorSegmentosUsandoCohenSutherlandToolStripMenuItem
            // 
            this.algoritmoDeRecorteDePolígonosPorSegmentosUsandoCohenSutherlandToolStripMenuItem.Name = "algoritmoDeRecorteDePolígonosPorSegmentosUsandoCohenSutherlandToolStripMenuItem";
            this.algoritmoDeRecorteDePolígonosPorSegmentosUsandoCohenSutherlandToolStripMenuItem.Size = new System.Drawing.Size(537, 24);
            this.algoritmoDeRecorteDePolígonosPorSegmentosUsandoCohenSutherlandToolStripMenuItem.Text = "Algoritmo de recorte de polígonos por segmentos usando Cohen–Sutherland";
            this.algoritmoDeRecorteDePolígonosPorSegmentosUsandoCohenSutherlandToolStripMenuItem.Click += new System.EventHandler(this.algoritmoDeRecorteDePolígonosPorSegmentosUsandoCohenSutherlandToolStripMenuItem_Click);
            // 
            // recorteDePolígonosPorSegmentosUsandoLiangBarskyToolStripMenuItem
            // 
            this.recorteDePolígonosPorSegmentosUsandoLiangBarskyToolStripMenuItem.Name = "recorteDePolígonosPorSegmentosUsandoLiangBarskyToolStripMenuItem";
            this.recorteDePolígonosPorSegmentosUsandoLiangBarskyToolStripMenuItem.Size = new System.Drawing.Size(537, 24);
            this.recorteDePolígonosPorSegmentosUsandoLiangBarskyToolStripMenuItem.Text = "Recorte de polígonos por segmentos usando Liang–Barsky";
            this.recorteDePolígonosPorSegmentosUsandoLiangBarskyToolStripMenuItem.Click += new System.EventHandler(this.recorteDePolígonosPorSegmentosUsandoLiangBarskyToolStripMenuItem_Click);
            // 
            // frmInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmInicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Algoritmos clásicos de Computación Gráfica";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem lineasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem algoritmoDDAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem algoritmoBresenhamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem algoritmoDelPuntoMedioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem círculoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem discretizacionDeCirculoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem algoritmoDDAParCirculosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem algoritmoBresenhamToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem algoritmosDeRellenoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem floodFillToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scanLineFillToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recorteDeLíneasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem algoritmoCohenSutherlandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem algoritmoLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem algoritmoCyrusBeckToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recorteDePolígonosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sutherlandHodgmanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem algoritmoDeRecorteDePolígonosPorSegmentosUsandoCohenSutherlandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recorteDePolígonosPorSegmentosUsandoLiangBarskyToolStripMenuItem;
    }
}

