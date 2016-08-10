namespace ReadExcel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReadDBBook = new System.Windows.Forms.Button();
            this.btnReadMatch = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lbla4 = new System.Windows.Forms.Label();
            this.lbla3 = new System.Windows.Forms.Label();
            this.lbla2 = new System.Windows.Forms.Label();
            this.lbla1 = new System.Windows.Forms.Label();
            this.btnShowConfirmed = new System.Windows.Forms.Button();
            this.btnShowNotConfirmed = new System.Windows.Forms.Button();
            this.btnShowMissing = new System.Windows.Forms.Button();
            this.btnShowChanged = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblBook = new System.Windows.Forms.Label();
            this.lblBLin = new System.Windows.Forms.Label();
            this.lblMapp = new System.Windows.Forms.Label();
            this.lblRoom = new System.Windows.Forms.Label();
            this.lblHotl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEditMapp = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnEditMapp);
            this.groupBox1.Controls.Add(this.btnReadDBBook);
            this.groupBox1.Controls.Add(this.btnReadMatch);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(240, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 279);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Λειτουργία";
            // 
            // btnReadDBBook
            // 
            this.btnReadDBBook.Location = new System.Drawing.Point(12, 123);
            this.btnReadDBBook.Name = "btnReadDBBook";
            this.btnReadDBBook.Size = new System.Drawing.Size(150, 45);
            this.btnReadDBBook.TabIndex = 5;
            this.btnReadDBBook.Text = "3. Διάβασμα Κρατήσεων";
            this.btnReadDBBook.UseVisualStyleBackColor = true;
            this.btnReadDBBook.Click += new System.EventHandler(this.btnReadDBBook_Click);
            // 
            // btnReadMatch
            // 
            this.btnReadMatch.Location = new System.Drawing.Point(12, 72);
            this.btnReadMatch.Name = "btnReadMatch";
            this.btnReadMatch.Size = new System.Drawing.Size(150, 45);
            this.btnReadMatch.TabIndex = 4;
            this.btnReadMatch.Text = "2. Διάβασμα Excel";
            this.btnReadMatch.UseVisualStyleBackColor = true;
            this.btnReadMatch.Click += new System.EventHandler(this.btnReadMatch_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 45);
            this.button1.TabIndex = 3;
            this.button1.Text = "1. Αρχικοποίηση";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.propertyGrid1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(795, 260);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Παράμετροι Λειτουργίας";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 18);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(789, 239);
            this.propertyGrid1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 260);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(795, 279);
            this.panel1.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lbla4);
            this.groupBox4.Controls.Add(this.lbla3);
            this.groupBox4.Controls.Add(this.lbla2);
            this.groupBox4.Controls.Add(this.lbla1);
            this.groupBox4.Controls.Add(this.btnShowConfirmed);
            this.groupBox4.Controls.Add(this.btnShowNotConfirmed);
            this.groupBox4.Controls.Add(this.btnShowMissing);
            this.groupBox4.Controls.Add(this.btnShowChanged);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(480, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(315, 279);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Προβολές Σύγκρισης";
            // 
            // lbla4
            // 
            this.lbla4.AutoSize = true;
            this.lbla4.Location = new System.Drawing.Point(176, 189);
            this.lbla4.Name = "lbla4";
            this.lbla4.Size = new System.Drawing.Size(35, 14);
            this.lbla4.TabIndex = 11;
            this.lbla4.Text = "lbla4";
            // 
            // lbla3
            // 
            this.lbla3.AutoSize = true;
            this.lbla3.Location = new System.Drawing.Point(176, 138);
            this.lbla3.Name = "lbla3";
            this.lbla3.Size = new System.Drawing.Size(35, 14);
            this.lbla3.TabIndex = 10;
            this.lbla3.Text = "lbla3";
            // 
            // lbla2
            // 
            this.lbla2.AutoSize = true;
            this.lbla2.Location = new System.Drawing.Point(176, 87);
            this.lbla2.Name = "lbla2";
            this.lbla2.Size = new System.Drawing.Size(35, 14);
            this.lbla2.TabIndex = 9;
            this.lbla2.Text = "lbla2";
            // 
            // lbla1
            // 
            this.lbla1.AutoSize = true;
            this.lbla1.Location = new System.Drawing.Point(176, 36);
            this.lbla1.Name = "lbla1";
            this.lbla1.Size = new System.Drawing.Size(35, 14);
            this.lbla1.TabIndex = 8;
            this.lbla1.Text = "lbla1";
            // 
            // btnShowConfirmed
            // 
            this.btnShowConfirmed.Location = new System.Drawing.Point(11, 174);
            this.btnShowConfirmed.Name = "btnShowConfirmed";
            this.btnShowConfirmed.Size = new System.Drawing.Size(150, 45);
            this.btnShowConfirmed.TabIndex = 7;
            this.btnShowConfirmed.Text = "Κρατήσεις Ok";
            this.btnShowConfirmed.UseVisualStyleBackColor = true;
            this.btnShowConfirmed.Click += new System.EventHandler(this.btnShowConfirmed_Click);
            // 
            // btnShowNotConfirmed
            // 
            this.btnShowNotConfirmed.Location = new System.Drawing.Point(11, 123);
            this.btnShowNotConfirmed.Name = "btnShowNotConfirmed";
            this.btnShowNotConfirmed.Size = new System.Drawing.Size(150, 45);
            this.btnShowNotConfirmed.TabIndex = 6;
            this.btnShowNotConfirmed.Text = "Καταχωρημένες που\r\nδεν βρέθηκαν στο Excel";
            this.btnShowNotConfirmed.UseVisualStyleBackColor = true;
            this.btnShowNotConfirmed.Click += new System.EventHandler(this.btnShowNotConfirmed_Click);
            // 
            // btnShowMissing
            // 
            this.btnShowMissing.Location = new System.Drawing.Point(11, 72);
            this.btnShowMissing.Name = "btnShowMissing";
            this.btnShowMissing.Size = new System.Drawing.Size(150, 45);
            this.btnShowMissing.TabIndex = 5;
            this.btnShowMissing.Text = "Δεν Βρέθηκαν\r\nστο Genero";
            this.btnShowMissing.UseVisualStyleBackColor = true;
            this.btnShowMissing.Click += new System.EventHandler(this.btnShowMissing_Click);
            // 
            // btnShowChanged
            // 
            this.btnShowChanged.Location = new System.Drawing.Point(11, 21);
            this.btnShowChanged.Name = "btnShowChanged";
            this.btnShowChanged.Size = new System.Drawing.Size(150, 45);
            this.btnShowChanged.TabIndex = 4;
            this.btnShowChanged.Text = "Κρατήσεις με Διαφορές";
            this.btnShowChanged.UseVisualStyleBackColor = true;
            this.btnShowChanged.Click += new System.EventHandler(this.btnShowChanged_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblBook);
            this.groupBox3.Controls.Add(this.lblBLin);
            this.groupBox3.Controls.Add(this.lblMapp);
            this.groupBox3.Controls.Add(this.lblRoom);
            this.groupBox3.Controls.Add(this.lblHotl);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(240, 279);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Εχουν Διαβαστεί";
            // 
            // lblBook
            // 
            this.lblBook.AutoSize = true;
            this.lblBook.Location = new System.Drawing.Point(155, 141);
            this.lblBook.Name = "lblBook";
            this.lblBook.Size = new System.Drawing.Size(49, 14);
            this.lblBook.TabIndex = 9;
            this.lblBook.Text = "lblBook";
            // 
            // lblBLin
            // 
            this.lblBLin.AutoSize = true;
            this.lblBLin.Location = new System.Drawing.Point(155, 111);
            this.lblBLin.Name = "lblBLin";
            this.lblBLin.Size = new System.Drawing.Size(45, 14);
            this.lblBLin.TabIndex = 8;
            this.lblBLin.Text = "lblBLin";
            // 
            // lblMapp
            // 
            this.lblMapp.AutoSize = true;
            this.lblMapp.Location = new System.Drawing.Point(155, 81);
            this.lblMapp.Name = "lblMapp";
            this.lblMapp.Size = new System.Drawing.Size(53, 14);
            this.lblMapp.TabIndex = 7;
            this.lblMapp.Text = "lblMapp";
            // 
            // lblRoom
            // 
            this.lblRoom.AutoSize = true;
            this.lblRoom.Location = new System.Drawing.Point(155, 51);
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Size = new System.Drawing.Size(53, 14);
            this.lblRoom.TabIndex = 6;
            this.lblRoom.Text = "lblRoom";
            // 
            // lblHotl
            // 
            this.lblHotl.AutoSize = true;
            this.lblHotl.Location = new System.Drawing.Point(155, 21);
            this.lblHotl.Name = "lblHotl";
            this.lblHotl.Size = new System.Drawing.Size(45, 14);
            this.lblHotl.TabIndex = 5;
            this.lblHotl.Text = "lblHotl";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "Κρατήσεις από το Genero:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "Κρατήσεις προς έλεγχο:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mappings:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Κωδ. Δωματίων:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Κωδ. Ξενοδοχείων:";
            // 
            // btnEditMapp
            // 
            this.btnEditMapp.Location = new System.Drawing.Point(12, 222);
            this.btnEditMapp.Name = "btnEditMapp";
            this.btnEditMapp.Size = new System.Drawing.Size(150, 45);
            this.btnEditMapp.TabIndex = 6;
            this.btnEditMapp.Text = "Προβολή Mappings";
            this.btnEditMapp.UseVisualStyleBackColor = true;
            this.btnEditMapp.Click += new System.EventHandler(this.btnEditMapp_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 539);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Name = "MainForm";
            this.Text = "BookMapping";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnReadMatch;
        private System.Windows.Forms.Button btnReadDBBook;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblBook;
        private System.Windows.Forms.Label lblBLin;
        private System.Windows.Forms.Label lblMapp;
        private System.Windows.Forms.Label lblRoom;
        private System.Windows.Forms.Label lblHotl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnShowConfirmed;
        private System.Windows.Forms.Button btnShowNotConfirmed;
        private System.Windows.Forms.Button btnShowMissing;
        private System.Windows.Forms.Button btnShowChanged;
        private System.Windows.Forms.Label lbla4;
        private System.Windows.Forms.Label lbla3;
        private System.Windows.Forms.Label lbla2;
        private System.Windows.Forms.Label lbla1;
        private System.Windows.Forms.Button btnEditMapp;
    }
}

