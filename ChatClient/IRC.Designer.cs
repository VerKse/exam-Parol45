namespace ChatClient
{
    partial class IRC
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.chatroomLayout = new System.Windows.Forms.TableLayoutPanel();
            this.chatroomList = new System.Windows.Forms.ListBox();
            this.chatroomsLabel = new System.Windows.Forms.TextBox();
            this.TableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.inputTextBox = new System.Windows.Forms.RichTextBox();
            this.userlistLayout = new System.Windows.Forms.TableLayoutPanel();
            this.onlineUsersLabel = new System.Windows.Forms.TextBox();
            this.onlineUsersList = new System.Windows.Forms.ListBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roomsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messageBox = new ChatClient.lib.RichTextBoxEx();
            this.tableLayoutPanel1.SuspendLayout();
            this.mainTableLayout.SuspendLayout();
            this.chatroomLayout.SuspendLayout();
            this.TableLayout.SuspendLayout();
            this.userlistLayout.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.mainTableLayout, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.menuStrip, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(584, 336);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // mainTableLayout
            // 
            this.mainTableLayout.ColumnCount = 3;
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66F));
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.mainTableLayout.Controls.Add(this.chatroomLayout, 0, 0);
            this.mainTableLayout.Controls.Add(this.TableLayout, 1, 0);
            this.mainTableLayout.Controls.Add(this.userlistLayout, 2, 0);
            this.mainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayout.Location = new System.Drawing.Point(3, 28);
            this.mainTableLayout.Name = "mainTableLayout";
            this.mainTableLayout.RowCount = 1;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 305F));
            this.mainTableLayout.Size = new System.Drawing.Size(578, 305);
            this.mainTableLayout.TabIndex = 1;
            // 
            // chatroomLayout
            // 
            this.chatroomLayout.ColumnCount = 1;
            this.chatroomLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.chatroomLayout.Controls.Add(this.chatroomList, 0, 1);
            this.chatroomLayout.Controls.Add(this.chatroomsLabel, 0, 0);
            this.chatroomLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatroomLayout.Location = new System.Drawing.Point(0, 0);
            this.chatroomLayout.Margin = new System.Windows.Forms.Padding(0);
            this.chatroomLayout.Name = "chatroomLayout";
            this.chatroomLayout.RowCount = 2;
            this.chatroomLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.chatroomLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.chatroomLayout.Size = new System.Drawing.Size(98, 305);
            this.chatroomLayout.TabIndex = 4;
            // 
            // chatroomList
            // 
            this.chatroomList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatroomList.Enabled = false;
            this.chatroomList.FormattingEnabled = true;
            this.chatroomList.IntegralHeight = false;
            this.chatroomList.Items.AddRange(new object[] {
            "Loading..."});
            this.chatroomList.Location = new System.Drawing.Point(5, 35);
            this.chatroomList.Margin = new System.Windows.Forms.Padding(5);
            this.chatroomList.Name = "chatroomList";
            this.chatroomList.Size = new System.Drawing.Size(88, 265);
            this.chatroomList.TabIndex = 8;
            this.chatroomList.SelectedIndexChanged += new System.EventHandler(this.ChooseRoom);
            // 
            // chatroomsLabel
            // 
            this.chatroomsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatroomsLabel.Location = new System.Drawing.Point(5, 5);
            this.chatroomsLabel.Margin = new System.Windows.Forms.Padding(5);
            this.chatroomsLabel.Name = "chatroomsLabel";
            this.chatroomsLabel.ReadOnly = true;
            this.chatroomsLabel.Size = new System.Drawing.Size(88, 20);
            this.chatroomsLabel.TabIndex = 9;
            this.chatroomsLabel.Text = "Chatrooms:";
            this.chatroomsLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TableLayout
            // 
            this.TableLayout.ColumnCount = 1;
            this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayout.Controls.Add(this.messageBox, 0, 0);
            this.TableLayout.Controls.Add(this.inputTextBox, 0, 1);
            this.TableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayout.Location = new System.Drawing.Point(98, 0);
            this.TableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.TableLayout.Name = "TableLayout";
            this.TableLayout.RowCount = 2;
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.TableLayout.Size = new System.Drawing.Size(381, 305);
            this.TableLayout.TabIndex = 0;
            // 
            // inputTextBox
            // 
            this.inputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputTextBox.Enabled = false;
            this.inputTextBox.Location = new System.Drawing.Point(5, 279);
            this.inputTextBox.Margin = new System.Windows.Forms.Padding(5);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(371, 21);
            this.inputTextBox.TabIndex = 7;
            this.inputTextBox.Text = "";
            this.inputTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SendChatMessage);
            this.inputTextBox.MouseHover += new System.EventHandler(this.inputTextBox_MouseHover);
            // 
            // userlistLayout
            // 
            this.userlistLayout.ColumnCount = 1;
            this.userlistLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.userlistLayout.Controls.Add(this.onlineUsersLabel, 0, 0);
            this.userlistLayout.Controls.Add(this.onlineUsersList, 0, 1);
            this.userlistLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userlistLayout.Location = new System.Drawing.Point(479, 0);
            this.userlistLayout.Margin = new System.Windows.Forms.Padding(0);
            this.userlistLayout.Name = "userlistLayout";
            this.userlistLayout.RowCount = 2;
            this.userlistLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.userlistLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.userlistLayout.Size = new System.Drawing.Size(99, 305);
            this.userlistLayout.TabIndex = 1;
            // 
            // onlineUsersLabel
            // 
            this.onlineUsersLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.onlineUsersLabel.Location = new System.Drawing.Point(5, 5);
            this.onlineUsersLabel.Margin = new System.Windows.Forms.Padding(5);
            this.onlineUsersLabel.Name = "onlineUsersLabel";
            this.onlineUsersLabel.ReadOnly = true;
            this.onlineUsersLabel.Size = new System.Drawing.Size(89, 20);
            this.onlineUsersLabel.TabIndex = 9;
            this.onlineUsersLabel.Text = "Users online:";
            this.onlineUsersLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // onlineUsersList
            // 
            this.onlineUsersList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.onlineUsersList.FormattingEnabled = true;
            this.onlineUsersList.IntegralHeight = false;
            this.onlineUsersList.Location = new System.Drawing.Point(5, 35);
            this.onlineUsersList.Margin = new System.Windows.Forms.Padding(5);
            this.onlineUsersList.Name = "onlineUsersList";
            this.onlineUsersList.Size = new System.Drawing.Size(89, 265);
            this.onlineUsersList.TabIndex = 8;
            this.onlineUsersList.SelectedIndexChanged += new System.EventHandler(this.onlineUsersList_SelectedIndexChanged);
            // 
            // menuStrip
            // 
            this.menuStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.roomsToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(584, 24);
            this.menuStrip.TabIndex = 2;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reconnectToolStripMenuItem,
            this.disconnectToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // reconnectToolStripMenuItem
            // 
            this.reconnectToolStripMenuItem.Name = "reconnectToolStripMenuItem";
            this.reconnectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.reconnectToolStripMenuItem.Text = "Reconnect";
            this.reconnectToolStripMenuItem.Click += new System.EventHandler(this.ToolStripReconnectClick);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.ToolStripDisconnectClick);
            // 
            // roomsToolStripMenuItem
            // 
            this.roomsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.leaveToolStripMenuItem});
            this.roomsToolStripMenuItem.Name = "roomsToolStripMenuItem";
            this.roomsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.roomsToolStripMenuItem.Text = "Rooms";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.ToolStripAddRoomClick);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.ToolStripDeleteClick);
            // 
            // leaveToolStripMenuItem
            // 
            this.leaveToolStripMenuItem.Name = "leaveToolStripMenuItem";
            this.leaveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.leaveToolStripMenuItem.Text = "Leave";
            this.leaveToolStripMenuItem.Click += new System.EventHandler(this.ToolStripLeaveClick);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.themeToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // themeToolStripMenuItem
            // 
            this.themeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lightToolStripMenuItem,
            this.darkToolStripMenuItem});
            this.themeToolStripMenuItem.Name = "themeToolStripMenuItem";
            this.themeToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.themeToolStripMenuItem.Text = "Theme";
            // 
            // lightToolStripMenuItem
            // 
            this.lightToolStripMenuItem.Name = "lightToolStripMenuItem";
            this.lightToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.lightToolStripMenuItem.Text = "Standart";
            this.lightToolStripMenuItem.Click += new System.EventHandler(this.setLightTheme);
            // 
            // darkToolStripMenuItem
            // 
            this.darkToolStripMenuItem.Name = "darkToolStripMenuItem";
            this.darkToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.darkToolStripMenuItem.Text = "Dark";
            this.darkToolStripMenuItem.Click += new System.EventHandler(this.setDarkTheme);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // messageBox
            // 
            this.messageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageBox.Location = new System.Drawing.Point(5, 5);
            this.messageBox.Margin = new System.Windows.Forms.Padding(5);
            this.messageBox.Name = "messageBox";
            this.messageBox.ReadOnly = true;
            this.messageBox.Size = new System.Drawing.Size(371, 264);
            this.messageBox.TabIndex = 6;
            this.messageBox.TabStop = false;
            this.messageBox.Text = "";
            this.messageBox.MouseHover += new System.EventHandler(this.messageBox_MouseHover);
            // 
            // IRC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 336);
            this.Controls.Add(this.tableLayoutPanel1);
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(600, 375);
            this.Name = "IRC";
            this.Text = "IRC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IRC_FormClosing);
            this.Load += new System.EventHandler(this.onIrcLoad);
            this.SizeChanged += new System.EventHandler(this.OnRecize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.mainTableLayout.ResumeLayout(false);
            this.chatroomLayout.ResumeLayout(false);
            this.chatroomLayout.PerformLayout();
            this.TableLayout.ResumeLayout(false);
            this.userlistLayout.ResumeLayout(false);
            this.userlistLayout.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.TableLayoutPanel chatroomLayout;
        private System.Windows.Forms.ListBox chatroomList;
        private System.Windows.Forms.TextBox chatroomsLabel;
        private System.Windows.Forms.TableLayoutPanel TableLayout;
        private lib.RichTextBoxEx messageBox;
        private System.Windows.Forms.RichTextBox inputTextBox;
        private System.Windows.Forms.TableLayoutPanel userlistLayout;
        private System.Windows.Forms.TextBox onlineUsersLabel;
        private System.Windows.Forms.ListBox onlineUsersList;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem roomsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkToolStripMenuItem;
    }
}

