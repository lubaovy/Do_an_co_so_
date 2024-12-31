namespace KHO
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
            this.entityConnection1 = new System.Data.Entity.Core.EntityClient.EntityConnection();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.ngườiDùngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hàngHóaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kháchHàngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nhàCungCấpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đơnVịTínhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.phieuNhapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.phiếuXuấtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tồnKhoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hóaĐơnToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnThoat = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dVTBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dVTBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripSeparator1,
            this.toolStripDropDownButton2,
            this.toolStripSeparator2,
            this.btnThoat});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1236, 50);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ngườiDùngToolStripMenuItem,
            this.hàngHóaToolStripMenuItem,
            this.kháchHàngToolStripMenuItem,
            this.nhàCungCấpToolStripMenuItem,
            this.đơnVịTínhToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::KHO.Properties.Resources.operational_system;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(116, 47);
            this.toolStripDropDownButton1.Text = "Quản lý kho";
            this.toolStripDropDownButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // ngườiDùngToolStripMenuItem
            // 
            this.ngườiDùngToolStripMenuItem.AutoSize = false;
            this.ngườiDùngToolStripMenuItem.Image = global::KHO.Properties.Resources.teamwork;
            this.ngườiDùngToolStripMenuItem.Name = "ngườiDùngToolStripMenuItem";
            this.ngườiDùngToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.ngườiDùngToolStripMenuItem.Text = "Người dùng";
            this.ngườiDùngToolStripMenuItem.Click += new System.EventHandler(this.ngườiDùngToolStripMenuItem_Click);
            // 
            // hàngHóaToolStripMenuItem
            // 
            this.hàngHóaToolStripMenuItem.Image = global::KHO.Properties.Resources.goods;
            this.hàngHóaToolStripMenuItem.Name = "hàngHóaToolStripMenuItem";
            this.hàngHóaToolStripMenuItem.Size = new System.Drawing.Size(224, 28);
            this.hàngHóaToolStripMenuItem.Text = "Hàng hóa";
            this.hàngHóaToolStripMenuItem.Click += new System.EventHandler(this.hàngHóaToolStripMenuItem_Click);
            // 
            // kháchHàngToolStripMenuItem
            // 
            this.kháchHàngToolStripMenuItem.Image = global::KHO.Properties.Resources.customer;
            this.kháchHàngToolStripMenuItem.Name = "kháchHàngToolStripMenuItem";
            this.kháchHàngToolStripMenuItem.Size = new System.Drawing.Size(224, 28);
            this.kháchHàngToolStripMenuItem.Text = "Khách hàng ";
            this.kháchHàngToolStripMenuItem.Click += new System.EventHandler(this.kháchHàngToolStripMenuItem_Click);
            // 
            // nhàCungCấpToolStripMenuItem
            // 
            this.nhàCungCấpToolStripMenuItem.Image = global::KHO.Properties.Resources.partnership;
            this.nhàCungCấpToolStripMenuItem.Name = "nhàCungCấpToolStripMenuItem";
            this.nhàCungCấpToolStripMenuItem.Size = new System.Drawing.Size(224, 28);
            this.nhàCungCấpToolStripMenuItem.Text = "Nhà cung cấp";
            this.nhàCungCấpToolStripMenuItem.Click += new System.EventHandler(this.nhàCungCấpToolStripMenuItem_Click);
            // 
            // đơnVịTínhToolStripMenuItem
            // 
            this.đơnVịTínhToolStripMenuItem.Name = "đơnVịTínhToolStripMenuItem";
            this.đơnVịTínhToolStripMenuItem.Size = new System.Drawing.Size(224, 28);
            this.đơnVịTínhToolStripMenuItem.Text = "Đơn vị tính";
            this.đơnVịTínhToolStripMenuItem.Click += new System.EventHandler(this.đơnVịTínhToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 50);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.phieuNhapToolStripMenuItem,
            this.phiếuXuấtToolStripMenuItem,
            this.tồnKhoToolStripMenuItem,
            this.hóaĐơnToolStripMenuItem1});
            this.toolStripDropDownButton2.Image = global::KHO.Properties.Resources.analysis;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(85, 47);
            this.toolStripDropDownButton2.Text = "Báo cáo";
            this.toolStripDropDownButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // phieuNhapToolStripMenuItem
            // 
            this.phieuNhapToolStripMenuItem.Image = global::KHO.Properties.Resources.import;
            this.phieuNhapToolStripMenuItem.Name = "phieuNhapToolStripMenuItem";
            this.phieuNhapToolStripMenuItem.Size = new System.Drawing.Size(224, 28);
            this.phieuNhapToolStripMenuItem.Text = "Phiếu nhập";
            this.phieuNhapToolStripMenuItem.Click += new System.EventHandler(this.phieuNhapToolStripMenuItem_Click);
            // 
            // phiếuXuấtToolStripMenuItem
            // 
            this.phiếuXuấtToolStripMenuItem.Image = global::KHO.Properties.Resources.export;
            this.phiếuXuấtToolStripMenuItem.Name = "phiếuXuấtToolStripMenuItem";
            this.phiếuXuấtToolStripMenuItem.Size = new System.Drawing.Size(224, 28);
            this.phiếuXuấtToolStripMenuItem.Text = "Phiếu xuất";
            this.phiếuXuấtToolStripMenuItem.Click += new System.EventHandler(this.phiếuXuấtToolStripMenuItem_Click);
            // 
            // tồnKhoToolStripMenuItem
            // 
            this.tồnKhoToolStripMenuItem.Image = global::KHO.Properties.Resources.warehouse;
            this.tồnKhoToolStripMenuItem.Name = "tồnKhoToolStripMenuItem";
            this.tồnKhoToolStripMenuItem.Size = new System.Drawing.Size(224, 28);
            this.tồnKhoToolStripMenuItem.Text = "Tồn kho";
            this.tồnKhoToolStripMenuItem.Click += new System.EventHandler(this.tồnKhoToolStripMenuItem_Click);
            // 
            // hóaĐơnToolStripMenuItem1
            // 
            this.hóaĐơnToolStripMenuItem1.Image = global::KHO.Properties.Resources.receipt;
            this.hóaĐơnToolStripMenuItem1.Name = "hóaĐơnToolStripMenuItem1";
            this.hóaĐơnToolStripMenuItem1.Size = new System.Drawing.Size(224, 28);
            this.hóaĐơnToolStripMenuItem1.Text = "Hóa đơn";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 50);
            // 
            // btnThoat
            // 
            this.btnThoat.Image = global::KHO.Properties.Resources.log_out;
            this.btnThoat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(58, 47);
            this.btnThoat.Text = "Thoát";
            this.btnThoat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // dVTBindingSource
            // 
            this.dVTBindingSource.DataMember = "DVT";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Imprint MT Shadow", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(321, 279);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(599, 39);
            this.label1.TabIndex = 2;
            this.label1.Text = "PHẦN MỀM QUẢN LÝ KHO HÀNG\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(1236, 738);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phần mềm quản lý kho hàng";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dVTBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource dVTBindingSource;
        private System.Data.Entity.Core.EntityClient.EntityConnection entityConnection1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnThoat;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem ngườiDùngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hàngHóaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kháchHàngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nhàCungCấpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem phieuNhapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tồnKhoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem phiếuXuấtToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem đơnVịTínhToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hóaĐơnToolStripMenuItem1;
    }
}

