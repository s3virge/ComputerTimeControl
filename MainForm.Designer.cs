namespace ComputerTimeControl
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.allowedHours = new System.Windows.Forms.NumericUpDown();
            this.allowedMinutes = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.powerOffHours = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.powerOffMinutes = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.allowedHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.allowedMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerOffHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerOffMinutes)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(152, 93);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 32);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(248, 93);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // allowedHours
            // 
            this.allowedHours.Location = new System.Drawing.Point(173, 10);
            this.allowedHours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.allowedHours.Name = "allowedHours";
            this.allowedHours.Size = new System.Drawing.Size(40, 20);
            this.allowedHours.TabIndex = 2;
            // 
            // allowedMinutes
            // 
            this.allowedMinutes.Location = new System.Drawing.Point(258, 10);
            this.allowedMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.allowedMinutes.Name = "allowedMinutes";
            this.allowedMinutes.Size = new System.Drawing.Size(40, 20);
            this.allowedMinutes.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Разрешено работать в сутки";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(217, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "часов";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(304, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "минут";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Віключать через каждіе";
            // 
            // powerOffHours
            // 
            this.powerOffHours.Location = new System.Drawing.Point(173, 45);
            this.powerOffHours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.powerOffHours.Name = "powerOffHours";
            this.powerOffHours.Size = new System.Drawing.Size(40, 20);
            this.powerOffHours.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(219, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "часов";
            // 
            // powerOffMinutes
            // 
            this.powerOffMinutes.Location = new System.Drawing.Point(258, 47);
            this.powerOffMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.powerOffMinutes.Name = "powerOffMinutes";
            this.powerOffMinutes.Size = new System.Drawing.Size(40, 20);
            this.powerOffMinutes.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(304, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "минут";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnSystemAriaIconMouseDoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 146);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.powerOffMinutes);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.powerOffHours);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.allowedMinutes);
            this.Controls.Add(this.allowedHours);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onClosing);
            
            this.Resize += new System.EventHandler(this.OnResize);
            ((System.ComponentModel.ISupportInitialize)(this.allowedHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.allowedMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerOffHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerOffMinutes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown allowedHours;
        private System.Windows.Forms.NumericUpDown allowedMinutes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown powerOffHours;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown powerOffMinutes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

