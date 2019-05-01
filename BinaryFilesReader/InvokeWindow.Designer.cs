namespace BinaryFilesReader
{
    partial class InvokeWindow
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
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.buttonInvoke = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnName,
            this.ColumnType,
            this.ColumnValue});
			this.dataGridView.Location = new System.Drawing.Point(12, 12);
			this.dataGridView.MultiSelect = false;
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.Size = new System.Drawing.Size(610, 237);
			this.dataGridView.TabIndex = 0;
			this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.CellEditFinished);
			// 
			// ColumnName
			// 
			this.ColumnName.HeaderText = "Parameter name";
			this.ColumnName.Name = "ColumnName";
			this.ColumnName.ReadOnly = true;
			// 
			// ColumnType
			// 
			this.ColumnType.HeaderText = "Parameter type";
			this.ColumnType.Name = "ColumnType";
			this.ColumnType.ReadOnly = true;
			// 
			// ColumnValue
			// 
			this.ColumnValue.HeaderText = "Value";
			this.ColumnValue.Name = "ColumnValue";
			this.ColumnValue.Visible = false;
			// 
			// buttonInvoke
			// 
			this.buttonInvoke.Enabled = false;
			this.buttonInvoke.Location = new System.Drawing.Point(547, 255);
			this.buttonInvoke.Name = "buttonInvoke";
			this.buttonInvoke.Size = new System.Drawing.Size(75, 23);
			this.buttonInvoke.TabIndex = 1;
			this.buttonInvoke.Text = "Invoke";
			this.buttonInvoke.UseVisualStyleBackColor = true;
			this.buttonInvoke.Click += new System.EventHandler(this.InvokeClicked);
			// 
			// InvokeWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(637, 290);
			this.Controls.Add(this.buttonInvoke);
			this.Controls.Add(this.dataGridView);
			this.Name = "InvokeWindow";
			this.Text = "InvokeWindow";
			this.Load += new System.EventHandler(this.Initialized);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnValue;
        public System.Windows.Forms.DataGridView dataGridView;
        public System.Windows.Forms.Button buttonInvoke;
    }
}