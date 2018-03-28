namespace KeyboardLighter
{
    partial class Form1
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
            this.red = new System.Windows.Forms.Button();
            this.orange = new System.Windows.Forms.Button();
            this.green = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // red
            // 
            this.red.Location = new System.Drawing.Point(13, 13);
            this.red.Name = "red";
            this.red.Size = new System.Drawing.Size(75, 23);
            this.red.TabIndex = 0;
            this.red.Text = "RED";
            this.red.UseVisualStyleBackColor = true;
            this.red.Click += new System.EventHandler(this.red_Click);
            // 
            // orange
            // 
            this.orange.Location = new System.Drawing.Point(13, 43);
            this.orange.Name = "orange";
            this.orange.Size = new System.Drawing.Size(75, 23);
            this.orange.TabIndex = 1;
            this.orange.Text = "ORANGE";
            this.orange.UseVisualStyleBackColor = true;
            this.orange.Click += new System.EventHandler(this.orange_Click);
            // 
            // green
            // 
            this.green.Location = new System.Drawing.Point(13, 73);
            this.green.Name = "green";
            this.green.Size = new System.Drawing.Size(75, 23);
            this.green.TabIndex = 2;
            this.green.Text = "GREEN";
            this.green.UseVisualStyleBackColor = true;
            this.green.Click += new System.EventHandler(this.green_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(204, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Current Color";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.green);
            this.Controls.Add(this.orange);
            this.Controls.Add(this.red);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button red;
        private System.Windows.Forms.Button orange;
        private System.Windows.Forms.Button green;
        private System.Windows.Forms.Label label1;
    }
}

