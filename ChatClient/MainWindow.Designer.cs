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
            this.TableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.inputField = new System.Windows.Forms.TextBox();
            this.messageBox = new ChatClient.lib.RichTextBoxEx();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.onlineUsersLabel = new System.Windows.Forms.TextBox();
            this.onlineUsersList = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chatroomList = new System.Windows.Forms.ListBox();
            this.chatroomsLabel = new System.Windows.Forms.TextBox();
            this.mainTableLayout.SuspendLayout();
            this.TableLayout.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayout
            // 
            this.mainTableLayout.ColumnCount = 3;
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66F));
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.mainTableLayout.Controls.Add(this.TableLayout, 1, 0);
            this.mainTableLayout.Controls.Add(this.tableLayoutPanel2, 2, 0);
            this.mainTableLayout.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.mainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayout.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayout.Name = "mainTableLayout";
            this.mainTableLayout.RowCount = 1;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTableLayout.Size = new System.Drawing.Size(819, 496);
            this.mainTableLayout.TabIndex = 0;
            // 
            // TableLayout
            // 
            this.TableLayout.ColumnCount = 1;
            this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayout.Controls.Add(this.inputField, 0, 1);
            this.TableLayout.Controls.Add(this.messageBox, 0, 0);
            this.TableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayout.Location = new System.Drawing.Point(139, 0);
            this.TableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.TableLayout.Name = "TableLayout";
            this.TableLayout.RowCount = 2;
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayout.Size = new System.Drawing.Size(540, 496);
            this.TableLayout.TabIndex = 0;
            // 
            // inputField
            // 
            this.inputField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputField.Location = new System.Drawing.Point(5, 471);
            this.inputField.Margin = new System.Windows.Forms.Padding(5);
            this.inputField.Name = "inputField";
            this.inputField.Size = new System.Drawing.Size(530, 20);
            this.inputField.TabIndex = 5;
            this.inputField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SendMessage);
            // 
            // messageBox
            // 
            this.messageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageBox.Location = new System.Drawing.Point(5, 5);
            this.messageBox.Margin = new System.Windows.Forms.Padding(5);
            this.messageBox.Name = "messageBox";
            this.messageBox.ReadOnly = true;
            this.messageBox.Size = new System.Drawing.Size(530, 456);
            this.messageBox.TabIndex = 6;
            this.messageBox.Text = "";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.onlineUsersLabel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.onlineUsersList, 0, 1);
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
            // onlineUsersLabel
            // 
            this.onlineUsersLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.onlineUsersLabel.Location = new System.Drawing.Point(5, 5);
            this.onlineUsersLabel.Margin = new System.Windows.Forms.Padding(5);
            this.onlineUsersLabel.Name = "onlineUsersLabel";
            this.onlineUsersLabel.ReadOnly = true;
            this.onlineUsersLabel.Size = new System.Drawing.Size(130, 20);
            this.onlineUsersLabel.TabIndex = 9;
            this.onlineUsersLabel.Text = "Users online:";
            this.onlineUsersLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // onlineUsersList
            // 
            this.onlineUsersList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.onlineUsersList.FormattingEnabled = true;
            this.onlineUsersList.IntegralHeight = false;
            this.onlineUsersList.Items.AddRange(new object[] {
            "Loading..."});
            this.onlineUsersList.Location = new System.Drawing.Point(5, 35);
            this.onlineUsersList.Margin = new System.Windows.Forms.Padding(5);
            this.onlineUsersList.Name = "onlineUsersList";
            this.onlineUsersList.Size = new System.Drawing.Size(130, 456);
            this.onlineUsersList.TabIndex = 8;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.chatroomList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.chatroomsLabel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(139, 496);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // chatroomList
            // 
            this.chatroomList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatroomList.FormattingEnabled = true;
            this.chatroomList.IntegralHeight = false;
            this.chatroomList.Items.AddRange(new object[] {
            "Loading..."});
            this.chatroomList.Location = new System.Drawing.Point(5, 35);
            this.chatroomList.Margin = new System.Windows.Forms.Padding(5);
            this.chatroomList.Name = "chatroomList";
            this.chatroomList.Size = new System.Drawing.Size(129, 456);
            this.chatroomList.TabIndex = 8;
            // 
            // chatroomsLabel
            // 
            this.chatroomsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatroomsLabel.Location = new System.Drawing.Point(5, 5);
            this.chatroomsLabel.Margin = new System.Windows.Forms.Padding(5);
            this.chatroomsLabel.Name = "chatroomsLabel";
            this.chatroomsLabel.ReadOnly = true;
            this.chatroomsLabel.Size = new System.Drawing.Size(129, 20);
            this.chatroomsLabel.TabIndex = 9;
            this.chatroomsLabel.Text = "Chatrooms:";
            this.chatroomsLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.TableLayout.ResumeLayout(false);
            this.TableLayout.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.TextBox onlineUsersLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ListBox onlineUsersList;
        private System.Windows.Forms.TableLayoutPanel TableLayout;
        private System.Windows.Forms.TextBox inputField;
        private lib.RichTextBoxEx messageBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox chatroomList;
        private System.Windows.Forms.TextBox chatroomsLabel;
    }
}

