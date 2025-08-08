namespace logifly.winforms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label labelTicketId;
        private System.Windows.Forms.TextBox txtTicketId;
        private System.Windows.Forms.Label labelLogType;
        private System.Windows.Forms.Label labelContent;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Label labelCreatedBy;
        private System.Windows.Forms.TextBox txtCreatedBy;
        private System.Windows.Forms.Button btnAddLog;
        private System.Windows.Forms.ListBox listBoxLogs;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.labelTicketId = new System.Windows.Forms.Label();
            this.txtTicketId = new System.Windows.Forms.TextBox();
            this.labelLogType = new System.Windows.Forms.Label();
            this.labelContent = new System.Windows.Forms.Label();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.labelCreatedBy = new System.Windows.Forms.Label();
            this.txtCreatedBy = new System.Windows.Forms.TextBox();
            this.btnAddLog = new System.Windows.Forms.Button();
            this.listBoxLogs = new System.Windows.Forms.ListBox();
            this.btnGetByTicketId = new System.Windows.Forms.Button();
            this.btnGetAllLogs = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelTicketId
            // 
            this.labelTicketId.AutoSize = true;
            this.labelTicketId.Location = new System.Drawing.Point(20, 20);
            this.labelTicketId.Name = "labelTicketId";
            this.labelTicketId.Size = new System.Drawing.Size(49, 13);
            this.labelTicketId.TabIndex = 0;
            this.labelTicketId.Text = "Ticket Id";
            // 
            // txtTicketId
            // 
            this.txtTicketId.Location = new System.Drawing.Point(251, 20);
            this.txtTicketId.Name = "txtTicketId";
            this.txtTicketId.Size = new System.Drawing.Size(250, 20);
            this.txtTicketId.TabIndex = 1;
            // 
            // labelLogType
            // 
            this.labelLogType.AutoSize = true;
            this.labelLogType.Location = new System.Drawing.Point(20, 60);
            this.labelLogType.Name = "labelLogType";
            this.labelLogType.Size = new System.Drawing.Size(35, 13);
            this.labelLogType.TabIndex = 2;
            this.labelLogType.Text = "Başlık";
            // 
            // labelContent
            // 
            this.labelContent.AutoSize = true;
            this.labelContent.Location = new System.Drawing.Point(20, 100);
            this.labelContent.Name = "labelContent";
            this.labelContent.Size = new System.Drawing.Size(35, 13);
            this.labelContent.TabIndex = 4;
            this.labelContent.Text = "Mesaj";
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(251, 97);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(250, 80);
            this.txtContent.TabIndex = 5;
            // 
            // labelCreatedBy
            // 
            this.labelCreatedBy.AutoSize = true;
            this.labelCreatedBy.Location = new System.Drawing.Point(20, 190);
            this.labelCreatedBy.Name = "labelCreatedBy";
            this.labelCreatedBy.Size = new System.Drawing.Size(59, 13);
            this.labelCreatedBy.TabIndex = 6;
            this.labelCreatedBy.Text = "Created By";
            // 
            // txtCreatedBy
            // 
            this.txtCreatedBy.Location = new System.Drawing.Point(260, 190);
            this.txtCreatedBy.Name = "txtCreatedBy";
            this.txtCreatedBy.Size = new System.Drawing.Size(250, 20);
            this.txtCreatedBy.TabIndex = 7;
            // 
            // btnAddLog
            // 
            this.btnAddLog.Location = new System.Drawing.Point(63, 230);
            this.btnAddLog.Name = "btnAddLog";
            this.btnAddLog.Size = new System.Drawing.Size(100, 30);
            this.btnAddLog.TabIndex = 8;
            this.btnAddLog.Text = "Log Ekle";
            this.btnAddLog.UseVisualStyleBackColor = true;
            this.btnAddLog.Click += new System.EventHandler(this.btnAddLog_Click);
            // 
            // listBoxLogs
            // 
            this.listBoxLogs.FormattingEnabled = true;
            this.listBoxLogs.Location = new System.Drawing.Point(20, 280);
            this.listBoxLogs.Name = "listBoxLogs";
            this.listBoxLogs.Size = new System.Drawing.Size(636, 238);
            this.listBoxLogs.TabIndex = 9;
            // 
            // btnGetByTicketId
            // 
            this.btnGetByTicketId.Location = new System.Drawing.Point(169, 230);
            this.btnGetByTicketId.Name = "btnGetByTicketId";
            this.btnGetByTicketId.Size = new System.Drawing.Size(100, 30);
            this.btnGetByTicketId.TabIndex = 8;
            this.btnGetByTicketId.Text = "Ticket\'a Göre Ara";
            this.btnGetByTicketId.UseVisualStyleBackColor = true;
            this.btnGetByTicketId.Click += new System.EventHandler(this.btnGetByTicketId_Click);
            // 
            // btnGetAllLogs
            // 
            this.btnGetAllLogs.Location = new System.Drawing.Point(275, 230);
            this.btnGetAllLogs.Name = "btnGetAllLogs";
            this.btnGetAllLogs.Size = new System.Drawing.Size(100, 30);
            this.btnGetAllLogs.TabIndex = 8;
            this.btnGetAllLogs.Text = "Tüm Logları Getir";
            this.btnGetAllLogs.UseVisualStyleBackColor = true;
            this.btnGetAllLogs.Click += new System.EventHandler(this.btnGetAllLogs_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(251, 57);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(250, 27);
            this.textBox1.TabIndex = 5;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(684, 529);
            this.Controls.Add(this.labelTicketId);
            this.Controls.Add(this.txtTicketId);
            this.Controls.Add(this.labelLogType);
            this.Controls.Add(this.labelContent);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.labelCreatedBy);
            this.Controls.Add(this.txtCreatedBy);
            this.Controls.Add(this.btnGetAllLogs);
            this.Controls.Add(this.btnGetByTicketId);
            this.Controls.Add(this.btnAddLog);
            this.Controls.Add(this.listBoxLogs);
            this.Name = "Form1";
            this.Text = "Log Ekleme";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button btnGetByTicketId;
        private System.Windows.Forms.Button btnGetAllLogs;
        private System.Windows.Forms.TextBox textBox1;
    }
}

