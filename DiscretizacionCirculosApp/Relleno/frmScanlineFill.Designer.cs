namespace DiscretizacionCirculosApp.Relleno
{
    partial class frmScanlineFill
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScanlineFill));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClean = new System.Windows.Forms.Button();
            this.btnScanlineFill = new System.Windows.Forms.Button();
            this.btnPickColor = new System.Windows.Forms.Button();
            this.picCanvas = new System.Windows.Forms.PictureBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGeneratePolygon = new System.Windows.Forms.Button();
            this.timerAnim = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1018, 750);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(121, 57);
            this.btnClose.TabIndex = 34;
            this.btnClose.Text = "Cerrar";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClean
            // 
            this.btnClean.Location = new System.Drawing.Point(1027, 508);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(121, 46);
            this.btnClean.TabIndex = 33;
            this.btnClean.Text = "Limpiar";
            this.btnClean.UseVisualStyleBackColor = true;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnScanlineFill
            // 
            this.btnScanlineFill.Location = new System.Drawing.Point(1027, 456);
            this.btnScanlineFill.Name = "btnScanlineFill";
            this.btnScanlineFill.Size = new System.Drawing.Size(121, 46);
            this.btnScanlineFill.TabIndex = 32;
            this.btnScanlineFill.Text = "Rellenar Figura";
            this.btnScanlineFill.UseVisualStyleBackColor = true;
            this.btnScanlineFill.Click += new System.EventHandler(this.btnScanlineFill_Click);
            // 
            // btnPickColor
            // 
            this.btnPickColor.Location = new System.Drawing.Point(1027, 404);
            this.btnPickColor.Name = "btnPickColor";
            this.btnPickColor.Size = new System.Drawing.Size(121, 46);
            this.btnPickColor.TabIndex = 31;
            this.btnPickColor.Text = "Escoger Color";
            this.btnPickColor.UseVisualStyleBackColor = true;
            this.btnPickColor.Click += new System.EventHandler(this.btnPickColor_Click);
            // 
            // picCanvas
            // 
            this.picCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCanvas.Location = new System.Drawing.Point(12, 12);
            this.picCanvas.Name = "picCanvas";
            this.picCanvas.Size = new System.Drawing.Size(940, 795);
            this.picCanvas.TabIndex = 30;
            this.picCanvas.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.30189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(968, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(266, 29);
            this.label3.TabIndex = 37;
            this.label3.Text = "Algoritmo Scanline Fill";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(962, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 156);
            this.label2.TabIndex = 36;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.22642F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(964, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 24);
            this.label1.TabIndex = 35;
            this.label1.Text = "Instrucciones de uso";
            // 
            // btnGeneratePolygon
            // 
            this.btnGeneratePolygon.Location = new System.Drawing.Point(1027, 353);
            this.btnGeneratePolygon.Name = "btnGeneratePolygon";
            this.btnGeneratePolygon.Size = new System.Drawing.Size(121, 45);
            this.btnGeneratePolygon.TabIndex = 38;
            this.btnGeneratePolygon.Text = "Generar Polígono";
            this.btnGeneratePolygon.UseVisualStyleBackColor = true;
            this.btnGeneratePolygon.Click += new System.EventHandler(this.btnGeneratePolygon_Click);
            // 
            // frmScanlineFill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1304, 821);
            this.Controls.Add(this.btnGeneratePolygon);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.btnScanlineFill);
            this.Controls.Add(this.btnPickColor);
            this.Controls.Add(this.picCanvas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmScanlineFill";
            this.Text = "Algoritmo de relleno ScanLine Fill";
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnClean;
        private System.Windows.Forms.Button btnScanlineFill;
        private System.Windows.Forms.Button btnPickColor;
        private System.Windows.Forms.PictureBox picCanvas;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGeneratePolygon;
        private System.Windows.Forms.Timer timerAnim;
    }
}