namespace CD_Teka
{
    partial class Form3
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonSnimiPromene = new System.Windows.Forms.Button();
            this.buttonObrisi = new System.Windows.Forms.Button();
            this.buttonGotovo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 13);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(684, 336);
            this.dataGridView1.TabIndex = 0;
            // 
            // buttonSnimiPromene
            // 
            this.buttonSnimiPromene.Location = new System.Drawing.Point(13, 356);
            this.buttonSnimiPromene.Name = "buttonSnimiPromene";
            this.buttonSnimiPromene.Size = new System.Drawing.Size(75, 53);
            this.buttonSnimiPromene.TabIndex = 1;
            this.buttonSnimiPromene.Text = "SNIMI";
            this.buttonSnimiPromene.UseVisualStyleBackColor = true;
            this.buttonSnimiPromene.Click += new System.EventHandler(this.buttonSnimiPromene_Click);
            // 
            // buttonObrisi
            // 
            this.buttonObrisi.Location = new System.Drawing.Point(95, 356);
            this.buttonObrisi.Name = "buttonObrisi";
            this.buttonObrisi.Size = new System.Drawing.Size(75, 53);
            this.buttonObrisi.TabIndex = 2;
            this.buttonObrisi.Text = "OBRISI";
            this.buttonObrisi.UseVisualStyleBackColor = true;
            this.buttonObrisi.Click += new System.EventHandler(this.buttonObrisi_Click);
            // 
            // buttonGotovo
            // 
            this.buttonGotovo.Location = new System.Drawing.Point(177, 356);
            this.buttonGotovo.Name = "buttonGotovo";
            this.buttonGotovo.Size = new System.Drawing.Size(75, 53);
            this.buttonGotovo.TabIndex = 3;
            this.buttonGotovo.Text = "GOTOVO";
            this.buttonGotovo.UseVisualStyleBackColor = true;
            this.buttonGotovo.Click += new System.EventHandler(this.buttonGotovo_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 421);
            this.Controls.Add(this.buttonGotovo);
            this.Controls.Add(this.buttonObrisi);
            this.Controls.Add(this.buttonSnimiPromene);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonSnimiPromene;
        private System.Windows.Forms.Button buttonObrisi;
        private System.Windows.Forms.Button buttonGotovo;
    }
}