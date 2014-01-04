namespace plC_tcp
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnectPLC = new System.Windows.Forms.Button();
            this.lblIPAdress = new System.Windows.Forms.Label();
            this.txtIPAdress = new System.Windows.Forms.TextBox();
            this.lblCurrPLCConnectionState = new System.Windows.Forms.Label();
            this.lblReadFromDB = new System.Windows.Forms.Label();
            this.btnReadDB = new System.Windows.Forms.Button();
            this.lblValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnConnectPLC
            // 
            this.btnConnectPLC.Location = new System.Drawing.Point(22, 81);
            this.btnConnectPLC.Name = "btnConnectPLC";
            this.btnConnectPLC.Size = new System.Drawing.Size(182, 42);
            this.btnConnectPLC.TabIndex = 0;
            this.btnConnectPLC.Text = "Connect PLC";
            this.btnConnectPLC.UseVisualStyleBackColor = true;
            this.btnConnectPLC.Click += new System.EventHandler(this.btnConnectPLC_Click);
            // 
            // lblIPAdress
            // 
            this.lblIPAdress.AutoSize = true;
            this.lblIPAdress.Location = new System.Drawing.Point(28, 18);
            this.lblIPAdress.Name = "lblIPAdress";
            this.lblIPAdress.Size = new System.Drawing.Size(52, 13);
            this.lblIPAdress.TabIndex = 1;
            this.lblIPAdress.Text = "IP Adress";
            // 
            // txtIPAdress
            // 
            this.txtIPAdress.Location = new System.Drawing.Point(23, 44);
            this.txtIPAdress.Name = "txtIPAdress";
            this.txtIPAdress.Size = new System.Drawing.Size(181, 20);
            this.txtIPAdress.TabIndex = 2;
            this.txtIPAdress.Text = "10.130.6.180";
            // 
            // lblCurrPLCConnectionState
            // 
            this.lblCurrPLCConnectionState.AutoSize = true;
            this.lblCurrPLCConnectionState.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrPLCConnectionState.Location = new System.Drawing.Point(21, 148);
            this.lblCurrPLCConnectionState.Name = "lblCurrPLCConnectionState";
            this.lblCurrPLCConnectionState.Size = new System.Drawing.Size(56, 18);
            this.lblCurrPLCConnectionState.TabIndex = 3;
            this.lblCurrPLCConnectionState.Text = "........";
            // 
            // lblReadFromDB
            // 
            this.lblReadFromDB.AutoSize = true;
            this.lblReadFromDB.Location = new System.Drawing.Point(26, 187);
            this.lblReadFromDB.Name = "lblReadFromDB";
            this.lblReadFromDB.Size = new System.Drawing.Size(74, 13);
            this.lblReadFromDB.TabIndex = 4;
            this.lblReadFromDB.Text = "Read from DB";
            // 
            // btnReadDB
            // 
            this.btnReadDB.Location = new System.Drawing.Point(23, 225);
            this.btnReadDB.Name = "btnReadDB";
            this.btnReadDB.Size = new System.Drawing.Size(180, 43);
            this.btnReadDB.TabIndex = 5;
            this.btnReadDB.Text = "Read DB";
            this.btnReadDB.UseVisualStyleBackColor = true;
            this.btnReadDB.Click += new System.EventHandler(this.btnReadDB_Click);
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(330, 26);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(37, 13);
            this.lblValue.TabIndex = 6;
            this.lblValue.Text = "Value ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 391);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.btnReadDB);
            this.Controls.Add(this.lblReadFromDB);
            this.Controls.Add(this.lblCurrPLCConnectionState);
            this.Controls.Add(this.txtIPAdress);
            this.Controls.Add(this.lblIPAdress);
            this.Controls.Add(this.btnConnectPLC);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnectPLC;
        private System.Windows.Forms.Label lblIPAdress;
        private System.Windows.Forms.TextBox txtIPAdress;
        private System.Windows.Forms.Label lblCurrPLCConnectionState;
        private System.Windows.Forms.Label lblReadFromDB;
        private System.Windows.Forms.Button btnReadDB;
        private System.Windows.Forms.Label lblValue;
    }
}

