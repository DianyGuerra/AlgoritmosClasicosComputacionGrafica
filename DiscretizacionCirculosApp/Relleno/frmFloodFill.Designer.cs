namespace DiscretizacionCirculosApp.Relleno
{
    partial class frmFloodFill
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFloodFill));
            this.picCanvas = new System.Windows.Forms.PictureBox();
            this.btnPickColor = new System.Windows.Forms.Button();
            this.btnFoodFill = new System.Windows.Forms.Button();
            this.btnClean = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // picCanvas
            // 
            this.picCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCanvas.Location = new System.Drawing.Point(12, 12);
            this.picCanvas.Name = "picCanvas";
            this.picCanvas.Size = new System.Drawing.Size(940, 795);
            this.picCanvas.TabIndex = 15;
            this.picCanvas.TabStop = false;
            this.picCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseDown);
            this.picCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseMove);
            this.picCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseUp);
            // 
            // btnPickColor
            // 
            this.btnPickColor.Location = new System.Drawing.Point(1018, 572);
            this.btnPickColor.Name = "btnPickColor";
            this.btnPickColor.Size = new System.Drawing.Size(121, 46);
            this.btnPickColor.TabIndex = 16;
            this.btnPickColor.Text = "Escoger Color";
            this.btnPickColor.UseVisualStyleBackColor = true;
            this.btnPickColor.Click += new System.EventHandler(this.btnPickColor_Click);
            // 
            // btnFoodFill
            // 
            this.btnFoodFill.Location = new System.Drawing.Point(1018, 624);
            this.btnFoodFill.Name = "btnFoodFill";
            this.btnFoodFill.Size = new System.Drawing.Size(121, 46);
            this.btnFoodFill.TabIndex = 17;
            this.btnFoodFill.Text = "Rellenar Figura";
            this.btnFoodFill.UseVisualStyleBackColor = true;
            this.btnFoodFill.Click += new System.EventHandler(this.btnFoodFill_Click);
            // 
            // btnClean
            // 
            this.btnClean.Location = new System.Drawing.Point(1018, 676);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(121, 46);
            this.btnClean.TabIndex = 18;
            this.btnClean.Text = "Limpiar";
            this.btnClean.UseVisualStyleBackColor = true;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1018, 750);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(121, 57);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "Cerrar";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.30189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(960, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 29);
            this.label1.TabIndex = 20;
            this.label1.Text = "Instrucciones de uso";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(962, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 338);
            this.label2.TabIndex = 21;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // frmFloodFill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1273, 819);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.btnFoodFill);
            this.Controls.Add(this.btnPickColor);
            this.Controls.Add(this.picCanvas);
            this.Name = "frmFloodFill";
            this.Text = "Algoritmo de relleno Flood Fill";
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picCanvas;
        private System.Windows.Forms.Button btnPickColor;
        private System.Windows.Forms.Button btnFoodFill;
        private System.Windows.Forms.Button btnClean;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}