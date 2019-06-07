namespace ChatClient
{
    partial class MainWindow
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
            this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.chatroomList = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.messageBox = new System.Windows.Forms.RichTextBox();
            this.inputField = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label = new System.Windows.Forms.TextBox();
            this.onlineList = new System.Windows.Forms.ListBox();
            this.mainTableLayout.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayout
            // 
            this.mainTableLayout.ColumnCount = 3;
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66F));
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.mainTableLayout.Controls.Add(this.chatroomList, 0, 0);
            this.mainTableLayout.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.mainTableLayout.Controls.Add(this.tableLayoutPanel2, 2, 0);
            this.mainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayout.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayout.Name = "mainTableLayout";
            this.mainTableLayout.RowCount = 1;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTableLayout.Size = new System.Drawing.Size(819, 496);
            this.mainTableLayout.TabIndex = 0;
            // 
            // chatroomList
            // 
            this.chatroomList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatroomList.FormattingEnabled = true;
            this.chatroomList.Items.AddRange(new object[] {
            "Room 1",
            "Room 2",
            "Room 3"});
            this.chatroomList.Location = new System.Drawing.Point(5, 5);
            this.chatroomList.Margin = new System.Windows.Forms.Padding(5);
            this.chatroomList.Name = "chatroomList";
            this.chatroomList.Size = new System.Drawing.Size(129, 486);
            this.chatroomList.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.messageBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.inputField, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(139, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(540, 496);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // messageBox
            // 
            this.messageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageBox.Enabled = false;
            this.messageBox.Location = new System.Drawing.Point(5, 5);
            this.messageBox.Margin = new System.Windows.Forms.Padding(5);
            this.messageBox.Name = "messageBox";
            this.messageBox.ReadOnly = true;
            this.messageBox.Size = new System.Drawing.Size(530, 456);
            this.messageBox.TabIndex = 6;
            this.messageBox.Text = "";
            // 
            // inputField
            // 
            this.inputField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputField.Location = new System.Drawing.Point(5, 471);
            this.inputField.Margin = new System.Windows.Forms.Padding(5);
            this.inputField.Name = "inputField";
            this.inputField.Size = new System.Drawing.Size(530, 20);
            this.inputField.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.onlineList, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(679, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(140, 496);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label
            // 
            this.label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label.Location = new System.Drawing.Point(5, 5);
            this.label.Margin = new System.Windows.Forms.Padding(5);
            this.label.Name = "label";
            this.label.ReadOnly = true;
            this.label.Size = new System.Drawing.Size(130, 20);
            this.label.TabIndex = 9;
            this.label.Text = "Currently online";
            // 
            // onlineList
            // 
            this.onlineList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.onlineList.FormattingEnabled = true;
            this.onlineList.Items.AddRange(new object[] {
            "Nobody"});
            this.onlineList.Location = new System.Drawing.Point(5, 35);
            this.onlineList.Margin = new System.Windows.Forms.Padding(5);
            this.onlineList.Name = "onlineList";
            this.onlineList.Size = new System.Drawing.Size(130, 456);
            this.onlineList.TabIndex = 8;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 496);
            this.Controls.Add(this.mainTableLayout);
            this.Name = "MainWindow";
            this.Text = "IRC";
            this.mainTableLayout.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox label;
        private System.Windows.Forms.TextBox inputField;
        private System.Windows.Forms.ListBox onlineList;
        private System.Windows.Forms.RichTextBox messageBox;
        private System.Windows.Forms.ListBox chatroomList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}

