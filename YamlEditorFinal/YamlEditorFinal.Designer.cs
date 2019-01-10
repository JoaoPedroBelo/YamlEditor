namespace YamlEditorFinal
{
    partial class YamlEditorFinal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YamlEditorFinal));
            this.materialDivider_DragBar = new MaterialSkin.Controls.MaterialDivider();
            this.label_TopBar = new System.Windows.Forms.Label();
            this.toolStrip_TopMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_OpenFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Undo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Redo = new System.Windows.Forms.ToolStripButton();
            this.toolStrip_TopMenu.SuspendLayout();
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
            this.materialDivider_DragBar.Size = new System.Drawing.Size(707, 23);
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
            this.toolStripButton_Redo});
            this.toolStrip_TopMenu.Location = new System.Drawing.Point(0, 23);
            this.toolStrip_TopMenu.Name = "toolStrip_TopMenu";
            this.toolStrip_TopMenu.Size = new System.Drawing.Size(707, 25);
            this.toolStrip_TopMenu.TabIndex = 3;
            this.toolStrip_TopMenu.Text = "toolStrip_TopMenu";
            // 
            // toolStripButton_OpenFile
            // 
            this.toolStripButton_OpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_OpenFile.Image = global::YamlEditorFinal.Properties.Resources.baseline_folder_white_48;
            this.toolStripButton_OpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_OpenFile.Name = "toolStripButton_OpenFile";
            this.toolStripButton_OpenFile.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_OpenFile.Text = "toolStripButton1";
            // 
            // toolStripButton_Save
            // 
            this.toolStripButton_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Save.Image = global::YamlEditorFinal.Properties.Resources.baseline_save_white_48;
            this.toolStripButton_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Save.Name = "toolStripButton_Save";
            this.toolStripButton_Save.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Save.Text = "toolStripButton1";
            // 
            // toolStripButton_Undo
            // 
            this.toolStripButton_Undo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Undo.Image = global::YamlEditorFinal.Properties.Resources.baseline_undo_white_48;
            this.toolStripButton_Undo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Undo.Name = "toolStripButton_Undo";
            this.toolStripButton_Undo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Undo.Text = "toolStripButton2";
            // 
            // toolStripButton_Redo
            // 
            this.toolStripButton_Redo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Redo.Image = global::YamlEditorFinal.Properties.Resources.baseline_redo_white_48;
            this.toolStripButton_Redo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Redo.Name = "toolStripButton_Redo";
            this.toolStripButton_Redo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Redo.Text = "toolStripButton3";
            // 
            // YamlEditorFinal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 505);
            this.Controls.Add(this.toolStrip_TopMenu);
            this.Controls.Add(this.label_TopBar);
            this.Controls.Add(this.materialDivider_DragBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "YamlEditorFinal";
            this.toolStrip_TopMenu.ResumeLayout(false);
            this.toolStrip_TopMenu.PerformLayout();
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
    }
}

