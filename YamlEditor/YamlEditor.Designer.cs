namespace YamlEditor
{
    partial class YamlEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YamlEditor));
            this.materialDivider_DragBar = new MaterialSkin.Controls.MaterialDivider();
            this.label_TopBar = new System.Windows.Forms.Label();
            this.toolStrip_TopMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_OpenFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Undo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Redo = new System.Windows.Forms.ToolStripButton();
            this.panel_MainForm = new System.Windows.Forms.Panel();
            this.splitter_MainForm = new System.Windows.Forms.SplitContainer();
            this.mainTreeView = new System.Windows.Forms.TreeView();
            this.mainImageList = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer_PropertiesGrid = new System.Windows.Forms.SplitContainer();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.propertiesTabPage = new System.Windows.Forms.TabPage();
            this.autoSaveCheckBox = new System.Windows.Forms.CheckBox();
            this.updateButton = new System.Windows.Forms.Button();
            this.valueTextBoxLabel = new System.Windows.Forms.Label();
            this.tagTextBoxLabel = new System.Windows.Forms.Label();
            this.nameTextBoxLabel = new System.Windows.Forms.Label();
            this.tagTextBox = new System.Windows.Forms.TextBox();
            this.valueTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.helpTabPage = new System.Windows.Forms.TabPage();
            this.mainWebBrowser = new System.Windows.Forms.WebBrowser();
            this.splitContainer_Log = new System.Windows.Forms.SplitContainer();
            this.materialFlatButton_LogMenu = new MaterialSkin.Controls.MaterialFlatButton();
            this.materialLabel_Log = new MaterialSkin.Controls.MaterialLabel();
            this.textBox_Log = new System.Windows.Forms.TextBox();
            this.materialContextMenuStrip_LogMenu = new MaterialSkin.Controls.MaterialContextMenuStrip();
            this.toolStripMenuItem_ClearLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton_Validate = new System.Windows.Forms.ToolStripButton();
            this.toolStrip_TopMenu.SuspendLayout();
            this.panel_MainForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitter_MainForm)).BeginInit();
            this.splitter_MainForm.Panel1.SuspendLayout();
            this.splitter_MainForm.Panel2.SuspendLayout();
            this.splitter_MainForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_PropertiesGrid)).BeginInit();
            this.splitContainer_PropertiesGrid.Panel1.SuspendLayout();
            this.splitContainer_PropertiesGrid.Panel2.SuspendLayout();
            this.splitContainer_PropertiesGrid.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.propertiesTabPage.SuspendLayout();
            this.helpTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Log)).BeginInit();
            this.splitContainer_Log.Panel1.SuspendLayout();
            this.splitContainer_Log.Panel2.SuspendLayout();
            this.splitContainer_Log.SuspendLayout();
            this.materialContextMenuStrip_LogMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialDivider_DragBar
            // 
            this.materialDivider_DragBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider_DragBar.Depth = 0;
            this.materialDivider_DragBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.materialDivider_DragBar.Enabled = false;
            this.materialDivider_DragBar.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.materialDivider_DragBar.Location = new System.Drawing.Point(0, 0);
            this.materialDivider_DragBar.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider_DragBar.Name = "materialDivider_DragBar";
            this.materialDivider_DragBar.Size = new System.Drawing.Size(900, 23);
            this.materialDivider_DragBar.TabIndex = 0;
            this.materialDivider_DragBar.Text = "materialDivider1";
            // 
            // label_TopBar
            // 
            this.label_TopBar.AutoSize = true;
            this.label_TopBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(132)))), ((int)(((byte)(213)))));
            this.label_TopBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_TopBar.ForeColor = System.Drawing.Color.White;
            this.label_TopBar.Location = new System.Drawing.Point(1, 1);
            this.label_TopBar.Name = "label_TopBar";
            this.label_TopBar.Size = new System.Drawing.Size(87, 20);
            this.label_TopBar.TabIndex = 2;
            this.label_TopBar.Text = "YamlEditor";
            // 
            // toolStrip_TopMenu
            // 
            this.toolStrip_TopMenu.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip_TopMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip_TopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_OpenFile,
            this.toolStripButton_Save,
            this.toolStripButton_Undo,
            this.toolStripButton_Redo,
            this.toolStripButton_Validate});
            this.toolStrip_TopMenu.Location = new System.Drawing.Point(0, 23);
            this.toolStrip_TopMenu.Name = "toolStrip_TopMenu";
            this.toolStrip_TopMenu.Size = new System.Drawing.Size(900, 25);
            this.toolStrip_TopMenu.TabIndex = 3;
            this.toolStrip_TopMenu.Text = "toolStrip_TopMenu";
            // 
            // toolStripButton_OpenFile
            // 
            this.toolStripButton_OpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_OpenFile.Image = global::YamlEditor.Properties.Resources.baseline_folder_white_48;
            this.toolStripButton_OpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_OpenFile.Name = "toolStripButton_OpenFile";
            this.toolStripButton_OpenFile.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_OpenFile.Text = "Open File";
            this.toolStripButton_OpenFile.Click += new System.EventHandler(this.OnOpen);
            // 
            // toolStripButton_Save
            // 
            this.toolStripButton_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Save.Image = global::YamlEditor.Properties.Resources.baseline_save_white_48;
            this.toolStripButton_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Save.Name = "toolStripButton_Save";
            this.toolStripButton_Save.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Save.Text = "Save File";
            this.toolStripButton_Save.Click += new System.EventHandler(this.toolStripButton_Save_Click);
            // 
            // toolStripButton_Undo
            // 
            this.toolStripButton_Undo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Undo.Image = global::YamlEditor.Properties.Resources.baseline_undo_white_48;
            this.toolStripButton_Undo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Undo.Name = "toolStripButton_Undo";
            this.toolStripButton_Undo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Undo.Text = "Undo";
            this.toolStripButton_Undo.Click += new System.EventHandler(this.onUndoClick);
            // 
            // toolStripButton_Redo
            // 
            this.toolStripButton_Redo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Redo.Image = global::YamlEditor.Properties.Resources.baseline_redo_white_48;
            this.toolStripButton_Redo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Redo.Name = "toolStripButton_Redo";
            this.toolStripButton_Redo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Redo.Text = "Redo";
            this.toolStripButton_Redo.Click += new System.EventHandler(this.onRedoClick);
            // 
            // panel_MainForm
            // 
            this.panel_MainForm.Controls.Add(this.splitter_MainForm);
            this.panel_MainForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_MainForm.Location = new System.Drawing.Point(0, 48);
            this.panel_MainForm.Name = "panel_MainForm";
            this.panel_MainForm.Size = new System.Drawing.Size(900, 452);
            this.panel_MainForm.TabIndex = 4;
            // 
            // splitter_MainForm
            // 
            this.splitter_MainForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitter_MainForm.Location = new System.Drawing.Point(0, 0);
            this.splitter_MainForm.Name = "splitter_MainForm";
            // 
            // splitter_MainForm.Panel1
            // 
            this.splitter_MainForm.Panel1.Controls.Add(this.mainTreeView);
            // 
            // splitter_MainForm.Panel2
            // 
            this.splitter_MainForm.Panel2.Controls.Add(this.splitContainer_PropertiesGrid);
            this.splitter_MainForm.Size = new System.Drawing.Size(900, 452);
            this.splitter_MainForm.SplitterDistance = 298;
            this.splitter_MainForm.TabIndex = 0;
            // 
            // mainTreeView
            // 
            this.mainTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTreeView.ImageIndex = 0;
            this.mainTreeView.ImageList = this.mainImageList;
            this.mainTreeView.Location = new System.Drawing.Point(0, 0);
            this.mainTreeView.Name = "mainTreeView";
            this.mainTreeView.SelectedImageIndex = 0;
            this.mainTreeView.Size = new System.Drawing.Size(298, 452);
            this.mainTreeView.TabIndex = 0;
            this.mainTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnAfterSelect);
            this.mainTreeView.DoubleClick += new System.EventHandler(this.OnDoubleClick);
            // 
            // mainImageList
            // 
            this.mainImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("mainImageList.ImageStream")));
            this.mainImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.mainImageList.Images.SetKeyName(0, "001-price-tag.png");
            this.mainImageList.Images.SetKeyName(1, "003-packing.png");
            this.mainImageList.Images.SetKeyName(2, "004-lock.png");
            this.mainImageList.Images.SetKeyName(3, "002-box.png");
            this.mainImageList.Images.SetKeyName(4, "004-lego-cube-or-square - Copy.png");
            this.mainImageList.Images.SetKeyName(5, "lego_with_link.png");
            this.mainImageList.Images.SetKeyName(6, "005-share-link.png");
            // 
            // splitContainer_PropertiesGrid
            // 
            this.splitContainer_PropertiesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_PropertiesGrid.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_PropertiesGrid.Name = "splitContainer_PropertiesGrid";
            this.splitContainer_PropertiesGrid.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_PropertiesGrid.Panel1
            // 
            this.splitContainer_PropertiesGrid.Panel1.Controls.Add(this.mainTabControl);
            // 
            // splitContainer_PropertiesGrid.Panel2
            // 
            this.splitContainer_PropertiesGrid.Panel2.Controls.Add(this.splitContainer_Log);
            this.splitContainer_PropertiesGrid.Size = new System.Drawing.Size(598, 452);
            this.splitContainer_PropertiesGrid.SplitterDistance = 180;
            this.splitContainer_PropertiesGrid.TabIndex = 0;
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.propertiesTabPage);
            this.mainTabControl.Controls.Add(this.helpTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(598, 180);
            this.mainTabControl.TabIndex = 2;
            // 
            // propertiesTabPage
            // 
            this.propertiesTabPage.Controls.Add(this.updateButton);
            this.propertiesTabPage.Controls.Add(this.valueTextBoxLabel);
            this.propertiesTabPage.Controls.Add(this.tagTextBoxLabel);
            this.propertiesTabPage.Controls.Add(this.nameTextBoxLabel);
            this.propertiesTabPage.Controls.Add(this.tagTextBox);
            this.propertiesTabPage.Controls.Add(this.valueTextBox);
            this.propertiesTabPage.Controls.Add(this.nameTextBox);
            this.propertiesTabPage.Location = new System.Drawing.Point(4, 22);
            this.propertiesTabPage.Name = "propertiesTabPage";
            this.propertiesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.propertiesTabPage.Size = new System.Drawing.Size(590, 154);
            this.propertiesTabPage.TabIndex = 0;
            this.propertiesTabPage.Text = "Property";
            this.propertiesTabPage.UseVisualStyleBackColor = true;
            // 
            // autoSaveCheckBox
            // 
            this.autoSaveCheckBox.AutoSize = true;
            this.autoSaveCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.autoSaveCheckBox.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.autoSaveCheckBox.Location = new System.Drawing.Point(217, 36);
            this.autoSaveCheckBox.Name = "autoSaveCheckBox";
            this.autoSaveCheckBox.Size = new System.Drawing.Size(76, 17);
            this.autoSaveCheckBox.TabIndex = 7;
            this.autoSaveCheckBox.Text = "Auto Save";
            this.autoSaveCheckBox.UseVisualStyleBackColor = false;
            this.autoSaveCheckBox.CheckedChanged += new System.EventHandler(this.autoSaveCheckBox_CheckedChanged);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(57, 94);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 23);
            this.updateButton.TabIndex = 6;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // valueTextBoxLabel
            // 
            this.valueTextBoxLabel.AutoSize = true;
            this.valueTextBoxLabel.Location = new System.Drawing.Point(17, 71);
            this.valueTextBoxLabel.Name = "valueTextBoxLabel";
            this.valueTextBoxLabel.Size = new System.Drawing.Size(34, 13);
            this.valueTextBoxLabel.TabIndex = 5;
            this.valueTextBoxLabel.Text = "Value";
            // 
            // tagTextBoxLabel
            // 
            this.tagTextBoxLabel.AutoSize = true;
            this.tagTextBoxLabel.Location = new System.Drawing.Point(25, 45);
            this.tagTextBoxLabel.Name = "tagTextBoxLabel";
            this.tagTextBoxLabel.Size = new System.Drawing.Size(26, 13);
            this.tagTextBoxLabel.TabIndex = 4;
            this.tagTextBoxLabel.Text = "Tag";
            // 
            // nameTextBoxLabel
            // 
            this.nameTextBoxLabel.AutoSize = true;
            this.nameTextBoxLabel.Location = new System.Drawing.Point(16, 19);
            this.nameTextBoxLabel.Name = "nameTextBoxLabel";
            this.nameTextBoxLabel.Size = new System.Drawing.Size(35, 13);
            this.nameTextBoxLabel.TabIndex = 3;
            this.nameTextBoxLabel.Text = "Name";
            // 
            // tagTextBox
            // 
            this.tagTextBox.Location = new System.Drawing.Point(57, 42);
            this.tagTextBox.Name = "tagTextBox";
            this.tagTextBox.Size = new System.Drawing.Size(100, 20);
            this.tagTextBox.TabIndex = 2;
            // 
            // valueTextBox
            // 
            this.valueTextBox.Location = new System.Drawing.Point(57, 68);
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.Size = new System.Drawing.Size(100, 20);
            this.valueTextBox.TabIndex = 1;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(57, 16);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(100, 20);
            this.nameTextBox.TabIndex = 0;
            // 
            // helpTabPage
            // 
            this.helpTabPage.Controls.Add(this.mainWebBrowser);
            this.helpTabPage.Location = new System.Drawing.Point(4, 22);
            this.helpTabPage.Name = "helpTabPage";
            this.helpTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.helpTabPage.Size = new System.Drawing.Size(590, 154);
            this.helpTabPage.TabIndex = 1;
            this.helpTabPage.Text = "Help";
            this.helpTabPage.UseVisualStyleBackColor = true;
            // 
            // mainWebBrowser
            // 
            this.mainWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainWebBrowser.Location = new System.Drawing.Point(3, 3);
            this.mainWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.mainWebBrowser.Name = "mainWebBrowser";
            this.mainWebBrowser.Size = new System.Drawing.Size(584, 148);
            this.mainWebBrowser.TabIndex = 0;
            // 
            // splitContainer_Log
            // 
            this.splitContainer_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_Log.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer_Log.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_Log.Name = "splitContainer_Log";
            this.splitContainer_Log.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_Log.Panel1
            // 
            this.splitContainer_Log.Panel1.Controls.Add(this.materialFlatButton_LogMenu);
            this.splitContainer_Log.Panel1.Controls.Add(this.materialLabel_Log);
            // 
            // splitContainer_Log.Panel2
            // 
            this.splitContainer_Log.Panel2.Controls.Add(this.textBox_Log);
            this.splitContainer_Log.Size = new System.Drawing.Size(598, 268);
            this.splitContainer_Log.SplitterDistance = 35;
            this.splitContainer_Log.TabIndex = 0;
            // 
            // materialFlatButton_LogMenu
            // 
            this.materialFlatButton_LogMenu.AutoSize = true;
            this.materialFlatButton_LogMenu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialFlatButton_LogMenu.Depth = 0;
            this.materialFlatButton_LogMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.materialFlatButton_LogMenu.Location = new System.Drawing.Point(784, 1);
            this.materialFlatButton_LogMenu.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialFlatButton_LogMenu.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFlatButton_LogMenu.Name = "materialFlatButton_LogMenu";
            this.materialFlatButton_LogMenu.Primary = false;
            this.materialFlatButton_LogMenu.Size = new System.Drawing.Size(30, 36);
            this.materialFlatButton_LogMenu.TabIndex = 1;
            this.materialFlatButton_LogMenu.Text = ". . .";
            this.materialFlatButton_LogMenu.UseVisualStyleBackColor = true;
            this.materialFlatButton_LogMenu.Click += new System.EventHandler(this.materialFlatButton_LogMenu_Click);
            // 
            // materialLabel_Log
            // 
            this.materialLabel_Log.AutoSize = true;
            this.materialLabel_Log.Depth = 0;
            this.materialLabel_Log.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.materialLabel_Log.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel_Log.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel_Log.Location = new System.Drawing.Point(0, 16);
            this.materialLabel_Log.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel_Log.Name = "materialLabel_Log";
            this.materialLabel_Log.Size = new System.Drawing.Size(34, 19);
            this.materialLabel_Log.TabIndex = 0;
            this.materialLabel_Log.Text = "Log";
            // 
            // textBox_Log
            // 
            this.textBox_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Log.Location = new System.Drawing.Point(0, 0);
            this.textBox_Log.Multiline = true;
            this.textBox_Log.Name = "textBox_Log";
            this.textBox_Log.ReadOnly = true;
            this.textBox_Log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Log.Size = new System.Drawing.Size(598, 229);
            this.textBox_Log.TabIndex = 0;
            // 
            // materialContextMenuStrip_LogMenu
            // 
            this.materialContextMenuStrip_LogMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialContextMenuStrip_LogMenu.Depth = 0;
            this.materialContextMenuStrip_LogMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_ClearLog});
            this.materialContextMenuStrip_LogMenu.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialContextMenuStrip_LogMenu.Name = "materialContextMenuStrip_LogMenu";
            this.materialContextMenuStrip_LogMenu.Size = new System.Drawing.Size(125, 26);
            // 
            // toolStripMenuItem_ClearLog
            // 
            this.toolStripMenuItem_ClearLog.Name = "toolStripMenuItem_ClearLog";
            this.toolStripMenuItem_ClearLog.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem_ClearLog.Text = "Clear Log";
            // 
            // toolStripButton_Validate
            // 
            this.toolStripButton_Validate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Validate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Validate.Image")));
            this.toolStripButton_Validate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Validate.Name = "toolStripButton_Validate";
            this.toolStripButton_Validate.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Validate.Text = "Validate";
            this.toolStripButton_Validate.Click += new System.EventHandler(this.toolStripButton_Validate_Click);
            // 
            // YamlEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.autoSaveCheckBox);
            this.Controls.Add(this.panel_MainForm);
            this.Controls.Add(this.toolStrip_TopMenu);
            this.Controls.Add(this.label_TopBar);
            this.Controls.Add(this.materialDivider_DragBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "YamlEditor";
            this.toolStrip_TopMenu.ResumeLayout(false);
            this.toolStrip_TopMenu.PerformLayout();
            this.panel_MainForm.ResumeLayout(false);
            this.splitter_MainForm.Panel1.ResumeLayout(false);
            this.splitter_MainForm.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitter_MainForm)).EndInit();
            this.splitter_MainForm.ResumeLayout(false);
            this.splitContainer_PropertiesGrid.Panel1.ResumeLayout(false);
            this.splitContainer_PropertiesGrid.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_PropertiesGrid)).EndInit();
            this.splitContainer_PropertiesGrid.ResumeLayout(false);
            this.mainTabControl.ResumeLayout(false);
            this.propertiesTabPage.ResumeLayout(false);
            this.propertiesTabPage.PerformLayout();
            this.helpTabPage.ResumeLayout(false);
            this.splitContainer_Log.Panel1.ResumeLayout(false);
            this.splitContainer_Log.Panel1.PerformLayout();
            this.splitContainer_Log.Panel2.ResumeLayout(false);
            this.splitContainer_Log.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Log)).EndInit();
            this.splitContainer_Log.ResumeLayout(false);
            this.materialContextMenuStrip_LogMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialDivider materialDivider_DragBar;
        private System.Windows.Forms.Label label_TopBar;
        private System.Windows.Forms.ToolStrip toolStrip_TopMenu;
        private System.Windows.Forms.ToolStripButton toolStripButton_OpenFile;
        private System.Windows.Forms.ToolStripButton toolStripButton_Save;
        private System.Windows.Forms.ToolStripButton toolStripButton_Undo;
        private System.Windows.Forms.ToolStripButton toolStripButton_Redo;
        private System.Windows.Forms.Panel panel_MainForm;
        private System.Windows.Forms.SplitContainer splitter_MainForm;
        private System.Windows.Forms.TreeView mainTreeView;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage helpTabPage;
        private System.Windows.Forms.SplitContainer splitContainer_PropertiesGrid;
        private System.Windows.Forms.TextBox textBox_Log;
        private System.Windows.Forms.SplitContainer splitContainer_Log;
        private MaterialSkin.Controls.MaterialLabel materialLabel_Log;
        private System.Windows.Forms.WebBrowser mainWebBrowser;
        private System.Windows.Forms.ImageList mainImageList;
        private MaterialSkin.Controls.MaterialFlatButton materialFlatButton_LogMenu;
        private MaterialSkin.Controls.MaterialContextMenuStrip materialContextMenuStrip_LogMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ClearLog;
        private System.Windows.Forms.TabPage propertiesTabPage;
        private System.Windows.Forms.Label valueTextBoxLabel;
        private System.Windows.Forms.Label tagTextBoxLabel;
        private System.Windows.Forms.Label nameTextBoxLabel;
        private System.Windows.Forms.TextBox tagTextBox;
        private System.Windows.Forms.TextBox valueTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.CheckBox autoSaveCheckBox;
        private System.Windows.Forms.ToolStripButton toolStripButton_Validate;
    }
}

