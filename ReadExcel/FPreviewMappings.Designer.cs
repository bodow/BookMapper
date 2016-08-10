namespace ReadExcel
{
    partial class FPreviewMappings
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboHotel = new System.Windows.Forms.ComboBox();
            this.bsHotels = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dg1 = new System.Windows.Forms.DataGridView();
            this.codeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsRooms = new System.Windows.Forms.BindingSource(this.components);
            this.dg2 = new System.Windows.Forms.DataGridView();
            this.bsMappings = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDo = new System.Windows.Forms.Button();
            this.roomReferenceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mappedToDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsHotels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsRooms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMappings)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(759, 44);
            this.label1.TabIndex = 1;
            this.label1.Text = "Προβολή και Διορθώσεις Αντιστοιχίσεων";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboHotel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(759, 50);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ξενοδοχείο";
            // 
            // cboHotel
            // 
            this.cboHotel.DataSource = this.bsHotels;
            this.cboHotel.DisplayMember = "DashedDescription";
            this.cboHotel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHotel.FormattingEnabled = true;
            this.cboHotel.Location = new System.Drawing.Point(99, 17);
            this.cboHotel.Name = "cboHotel";
            this.cboHotel.Size = new System.Drawing.Size(395, 22);
            this.cboHotel.TabIndex = 5;
            this.cboHotel.ValueMember = "Code";
            // 
            // bsHotels
            // 
            this.bsHotels.DataSource = typeof(ReadExcel.DataClasses.Hotel);
            this.bsHotels.CurrentChanged += new System.EventHandler(this.bsHotels_CurrentChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 94);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dg1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dg2);
            this.splitContainer1.Size = new System.Drawing.Size(759, 434);
            this.splitContainer1.SplitterDistance = 191;
            this.splitContainer1.SplitterWidth = 12;
            this.splitContainer1.TabIndex = 3;
            // 
            // dg1
            // 
            this.dg1.AllowUserToAddRows = false;
            this.dg1.AllowUserToDeleteRows = false;
            this.dg1.AllowUserToResizeRows = false;
            this.dg1.AutoGenerateColumns = false;
            this.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codeDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn});
            this.dg1.DataSource = this.bsRooms;
            this.dg1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg1.Location = new System.Drawing.Point(0, 0);
            this.dg1.MultiSelect = false;
            this.dg1.Name = "dg1";
            this.dg1.ReadOnly = true;
            this.dg1.RowHeadersVisible = false;
            this.dg1.Size = new System.Drawing.Size(759, 191);
            this.dg1.TabIndex = 0;
            // 
            // codeDataGridViewTextBoxColumn
            // 
            this.codeDataGridViewTextBoxColumn.DataPropertyName = "Code";
            this.codeDataGridViewTextBoxColumn.HeaderText = "Code";
            this.codeDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.codeDataGridViewTextBoxColumn.Name = "codeDataGridViewTextBoxColumn";
            this.codeDataGridViewTextBoxColumn.ReadOnly = true;
            this.codeDataGridViewTextBoxColumn.Width = 70;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.MinimumWidth = 100;
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            this.descriptionDataGridViewTextBoxColumn.Width = 500;
            // 
            // bsRooms
            // 
            this.bsRooms.DataSource = typeof(ReadExcel.DataClasses.Room);
            // 
            // dg2
            // 
            this.dg2.AllowUserToAddRows = false;
            this.dg2.AllowUserToDeleteRows = false;
            this.dg2.AutoGenerateColumns = false;
            this.dg2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.roomReferenceDataGridViewTextBoxColumn,
            this.mappedToDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn1});
            this.dg2.DataSource = this.bsMappings;
            this.dg2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg2.Location = new System.Drawing.Point(0, 0);
            this.dg2.Name = "dg2";
            this.dg2.RowHeadersVisible = false;
            this.dg2.Size = new System.Drawing.Size(759, 231);
            this.dg2.TabIndex = 0;
            // 
            // bsMappings
            // 
            this.bsMappings.AllowNew = false;
            this.bsMappings.DataSource = typeof(ReadExcel.DataClasses.RoomJoin);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 481);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(759, 47);
            this.panel1.TabIndex = 4;
            // 
            // btnDo
            // 
            this.btnDo.Location = new System.Drawing.Point(133, 8);
            this.btnDo.Name = "btnDo";
            this.btnDo.Size = new System.Drawing.Size(500, 30);
            this.btnDo.TabIndex = 0;
            this.btnDo.Text = "Μεταβολή αντιστοίχισης, σύμφωνα με τις επιλεγμένες γραμμές";
            this.btnDo.UseVisualStyleBackColor = true;
            this.btnDo.Click += new System.EventHandler(this.btnDo_Click);
            // 
            // roomReferenceDataGridViewTextBoxColumn
            // 
            this.roomReferenceDataGridViewTextBoxColumn.DataPropertyName = "RoomReference";
            this.roomReferenceDataGridViewTextBoxColumn.HeaderText = "Referenced as";
            this.roomReferenceDataGridViewTextBoxColumn.MinimumWidth = 30;
            this.roomReferenceDataGridViewTextBoxColumn.Name = "roomReferenceDataGridViewTextBoxColumn";
            this.roomReferenceDataGridViewTextBoxColumn.ReadOnly = true;
            this.roomReferenceDataGridViewTextBoxColumn.Width = 336;
            // 
            // mappedToDataGridViewTextBoxColumn
            // 
            this.mappedToDataGridViewTextBoxColumn.DataPropertyName = "MappedTo";
            this.mappedToDataGridViewTextBoxColumn.HeaderText = "MappedTo";
            this.mappedToDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.mappedToDataGridViewTextBoxColumn.Name = "mappedToDataGridViewTextBoxColumn";
            this.mappedToDataGridViewTextBoxColumn.ReadOnly = true;
            this.mappedToDataGridViewTextBoxColumn.Width = 80;
            // 
            // descriptionDataGridViewTextBoxColumn1
            // 
            this.descriptionDataGridViewTextBoxColumn1.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn1.HeaderText = "DB Description";
            this.descriptionDataGridViewTextBoxColumn1.MinimumWidth = 30;
            this.descriptionDataGridViewTextBoxColumn1.Name = "descriptionDataGridViewTextBoxColumn1";
            this.descriptionDataGridViewTextBoxColumn1.ReadOnly = true;
            this.descriptionDataGridViewTextBoxColumn1.Width = 336;
            // 
            // FPreviewMappings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 528);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Name = "FPreviewMappings";
            this.Text = "FPreviewMappings";
            this.Load += new System.EventHandler(this.FPreviewMappings_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsHotels)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsRooms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMappings)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboHotel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dg1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource bsHotels;
        private System.Windows.Forms.BindingSource bsRooms;
        private System.Windows.Forms.DataGridView dg2;
        private System.Windows.Forms.BindingSource bsMappings;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnDo;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomReferenceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mappedToDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn1;
    }
}