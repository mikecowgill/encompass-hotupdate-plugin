namespace Lendmatic.HotUpdatePlugin.TopMenu.PluginHotUpdateForm
{
    partial class PluginHotUpdate_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginHotUpdate_Form));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chkUpdateAll = new System.Windows.Forms.CheckBox();
            this.grdPlugins = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPlugins)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.chkUpdateAll, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.grdPlugins, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnRefresh, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(928, 544);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // chkUpdateAll
            // 
            this.chkUpdateAll.AutoSize = true;
            this.chkUpdateAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkUpdateAll.Location = new System.Drawing.Point(770, 3);
            this.chkUpdateAll.Name = "chkUpdateAll";
            this.chkUpdateAll.Size = new System.Drawing.Size(155, 44);
            this.chkUpdateAll.TabIndex = 0;
            this.chkUpdateAll.Text = "Update All Users";
            this.chkUpdateAll.UseVisualStyleBackColor = true;
            // 
            // grdPlugins
            // 
            this.grdPlugins.AllowUserToAddRows = false;
            this.grdPlugins.AllowUserToDeleteRows = false;
            this.grdPlugins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.grdPlugins, 2);
            this.grdPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPlugins.Location = new System.Drawing.Point(3, 53);
            this.grdPlugins.Name = "grdPlugins";
            this.grdPlugins.ReadOnly = true;
            this.grdPlugins.RowHeadersWidth = 62;
            this.grdPlugins.RowTemplate.Height = 28;
            this.grdPlugins.Size = new System.Drawing.Size(922, 488);
            this.grdPlugins.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Location = new System.Drawing.Point(3, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(124, 44);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // PluginHotUpdate_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(928, 544);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PluginHotUpdate_Form";
            this.Text = "Plugin Hot Update";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPlugins)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox chkUpdateAll;
        private System.Windows.Forms.DataGridView grdPlugins;
        private System.Windows.Forms.Button btnRefresh;
    }
}