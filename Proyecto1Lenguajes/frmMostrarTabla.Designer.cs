namespace Proyecto1Lenguajes
{
    partial class frmMostrarTabla
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
            this.dgvAutomata = new System.Windows.Forms.DataGridView();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnMostrarProceso = new System.Windows.Forms.Button();
            this.btnGenerarApp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAutomata)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAutomata
            // 
            this.dgvAutomata.AllowUserToAddRows = false;
            this.dgvAutomata.AllowUserToDeleteRows = false;
            this.dgvAutomata.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAutomata.BackgroundColor = System.Drawing.Color.Silver;
            this.dgvAutomata.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvAutomata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAutomata.Location = new System.Drawing.Point(119, 103);
            this.dgvAutomata.Name = "dgvAutomata";
            this.dgvAutomata.Size = new System.Drawing.Size(486, 177);
            this.dgvAutomata.TabIndex = 0;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.Maroon;
            this.btnCerrar.BackgroundImage = global::Proyecto1Lenguajes.Properties.Resources.x1;
            this.btnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCerrar.ImageKey = "(ninguno)";
            this.btnCerrar.Location = new System.Drawing.Point(53, 112);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(67, 55);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnMostrarProceso
            // 
            this.btnMostrarProceso.BackColor = System.Drawing.Color.White;
            this.btnMostrarProceso.BackgroundImage = global::Proyecto1Lenguajes.Properties.Resources.ver;
            this.btnMostrarProceso.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMostrarProceso.Location = new System.Drawing.Point(54, 165);
            this.btnMostrarProceso.Name = "btnMostrarProceso";
            this.btnMostrarProceso.Size = new System.Drawing.Size(66, 55);
            this.btnMostrarProceso.TabIndex = 2;
            this.btnMostrarProceso.UseVisualStyleBackColor = false;
            this.btnMostrarProceso.Click += new System.EventHandler(this.btnMostrarProceso_Click);
            // 
            // btnGenerarApp
            // 
            this.btnGenerarApp.BackgroundImage = global::Proyecto1Lenguajes.Properties.Resources.logo_brand_symbol_number_buttons;
            this.btnGenerarApp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGenerarApp.Location = new System.Drawing.Point(54, 218);
            this.btnGenerarApp.Name = "btnGenerarApp";
            this.btnGenerarApp.Size = new System.Drawing.Size(66, 55);
            this.btnGenerarApp.TabIndex = 3;
            this.btnGenerarApp.UseVisualStyleBackColor = true;
            this.btnGenerarApp.Click += new System.EventHandler(this.btnGenerarApp_Click);
            // 
            // frmMostrarTabla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Proyecto1Lenguajes.Properties.Resources.f6QOJjc_nerd_wallpaper;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(741, 486);
            this.Controls.Add(this.btnGenerarApp);
            this.Controls.Add(this.btnMostrarProceso);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.dgvAutomata);
            this.Name = "frmMostrarTabla";
            this.Text = "Autómata";
            this.Load += new System.EventHandler(this.frmMostrarTabla_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAutomata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAutomata;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnMostrarProceso;
        private System.Windows.Forms.Button btnGenerarApp;
    }
}