namespace FFXIII2MusicVolumeSlider
{
    partial class CoreForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoreForm));
            this.AppImgPictureBox = new System.Windows.Forms.PictureBox();
            this.PathTextBox = new System.Windows.Forms.TextBox();
            this.VoGroupBox = new System.Windows.Forms.GroupBox();
            this.JpVoRadiobutton = new System.Windows.Forms.RadioButton();
            this.EnVoRadiobutton = new System.Windows.Forms.RadioButton();
            this.FileSystemGroupBox = new System.Windows.Forms.GroupBox();
            this.NovaRadioButton = new System.Windows.Forms.RadioButton();
            this.PackedRadioButton = new System.Windows.Forms.RadioButton();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.SliderTrackBar = new System.Windows.Forms.TrackBar();
            this.SliderValuesLabel = new System.Windows.Forms.Label();
            this.TxtAboveSliderLabel = new System.Windows.Forms.Label();
            this.SetVolumeButton = new System.Windows.Forms.Button();
            this.AboutLinkLabel = new System.Windows.Forms.LinkLabel();
            this.HelpLinkLabel = new System.Windows.Forms.LinkLabel();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.TxtAbovePathBoxlabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AppImgPictureBox)).BeginInit();
            this.VoGroupBox.SuspendLayout();
            this.FileSystemGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // AppImgPictureBox
            // 
            this.AppImgPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("AppImgPictureBox.Image")));
            this.AppImgPictureBox.Location = new System.Drawing.Point(13, 13);
            this.AppImgPictureBox.Name = "AppImgPictureBox";
            this.AppImgPictureBox.Size = new System.Drawing.Size(535, 211);
            this.AppImgPictureBox.TabIndex = 0;
            this.AppImgPictureBox.TabStop = false;
            // 
            // PathTextBox
            // 
            this.PathTextBox.Location = new System.Drawing.Point(13, 265);
            this.PathTextBox.Name = "PathTextBox";
            this.PathTextBox.Size = new System.Drawing.Size(466, 20);
            this.PathTextBox.TabIndex = 1;
            // 
            // VoGroupBox
            // 
            this.VoGroupBox.Controls.Add(this.JpVoRadiobutton);
            this.VoGroupBox.Controls.Add(this.EnVoRadiobutton);
            this.VoGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VoGroupBox.Location = new System.Drawing.Point(13, 326);
            this.VoGroupBox.Name = "VoGroupBox";
            this.VoGroupBox.Size = new System.Drawing.Size(200, 81);
            this.VoGroupBox.TabIndex = 2;
            this.VoGroupBox.TabStop = false;
            this.VoGroupBox.Text = "Voice Over Settings :";
            // 
            // JpVoRadiobutton
            // 
            this.JpVoRadiobutton.AutoSize = true;
            this.JpVoRadiobutton.Location = new System.Drawing.Point(7, 45);
            this.JpVoRadiobutton.Name = "JpVoRadiobutton";
            this.JpVoRadiobutton.Size = new System.Drawing.Size(98, 19);
            this.JpVoRadiobutton.TabIndex = 1;
            this.JpVoRadiobutton.TabStop = true;
            this.JpVoRadiobutton.Text = "Japanese VO";
            this.JpVoRadiobutton.UseVisualStyleBackColor = true;
            // 
            // EnVoRadiobutton
            // 
            this.EnVoRadiobutton.AutoSize = true;
            this.EnVoRadiobutton.Location = new System.Drawing.Point(7, 20);
            this.EnVoRadiobutton.Name = "EnVoRadiobutton";
            this.EnVoRadiobutton.Size = new System.Drawing.Size(85, 19);
            this.EnVoRadiobutton.TabIndex = 0;
            this.EnVoRadiobutton.TabStop = true;
            this.EnVoRadiobutton.Text = "English VO";
            this.EnVoRadiobutton.UseVisualStyleBackColor = true;
            // 
            // FileSystemGroupBox
            // 
            this.FileSystemGroupBox.Controls.Add(this.NovaRadioButton);
            this.FileSystemGroupBox.Controls.Add(this.PackedRadioButton);
            this.FileSystemGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileSystemGroupBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.FileSystemGroupBox.Location = new System.Drawing.Point(348, 326);
            this.FileSystemGroupBox.Name = "FileSystemGroupBox";
            this.FileSystemGroupBox.Size = new System.Drawing.Size(200, 81);
            this.FileSystemGroupBox.TabIndex = 3;
            this.FileSystemGroupBox.TabStop = false;
            this.FileSystemGroupBox.Text = "File System Settings :";
            // 
            // NovaRadioButton
            // 
            this.NovaRadioButton.AutoSize = true;
            this.NovaRadioButton.Location = new System.Drawing.Point(7, 45);
            this.NovaRadioButton.Name = "NovaRadioButton";
            this.NovaRadioButton.Size = new System.Drawing.Size(118, 19);
            this.NovaRadioButton.TabIndex = 1;
            this.NovaRadioButton.TabStop = true;
            this.NovaRadioButton.Text = "Nova / Unpacked";
            this.NovaRadioButton.UseVisualStyleBackColor = true;
            // 
            // PackedRadioButton
            // 
            this.PackedRadioButton.AutoSize = true;
            this.PackedRadioButton.Location = new System.Drawing.Point(7, 20);
            this.PackedRadioButton.Name = "PackedRadioButton";
            this.PackedRadioButton.Size = new System.Drawing.Size(114, 19);
            this.PackedRadioButton.TabIndex = 0;
            this.PackedRadioButton.TabStop = true;
            this.PackedRadioButton.Text = "Default / Packed";
            this.PackedRadioButton.UseVisualStyleBackColor = true;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(486, 264);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(62, 23);
            this.BrowseButton.TabIndex = 4;
            this.BrowseButton.Text = "Browse...";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // SliderTrackBar
            // 
            this.SliderTrackBar.LargeChange = 1;
            this.SliderTrackBar.Location = new System.Drawing.Point(144, 476);
            this.SliderTrackBar.Name = "SliderTrackBar";
            this.SliderTrackBar.Size = new System.Drawing.Size(265, 45);
            this.SliderTrackBar.TabIndex = 5;
            // 
            // SliderValuesLabel
            // 
            this.SliderValuesLabel.AutoSize = true;
            this.SliderValuesLabel.Location = new System.Drawing.Point(151, 507);
            this.SliderValuesLabel.Name = "SliderValuesLabel";
            this.SliderValuesLabel.Size = new System.Drawing.Size(256, 13);
            this.SliderValuesLabel.TabIndex = 6;
            this.SliderValuesLabel.Text = "0      1      2      3      4      5      6      7      8      9     10";
            // 
            // TxtAboveSliderLabel
            // 
            this.TxtAboveSliderLabel.AutoSize = true;
            this.TxtAboveSliderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.TxtAboveSliderLabel.Location = new System.Drawing.Point(215, 447);
            this.TxtAboveSliderLabel.Name = "TxtAboveSliderLabel";
            this.TxtAboveSliderLabel.Size = new System.Drawing.Size(129, 16);
            this.TxtAboveSliderLabel.TabIndex = 7;
            this.TxtAboveSliderLabel.Text = "Music Volume Slider";
            // 
            // SetVolumeButton
            // 
            this.SetVolumeButton.Location = new System.Drawing.Point(235, 546);
            this.SetVolumeButton.Name = "SetVolumeButton";
            this.SetVolumeButton.Size = new System.Drawing.Size(90, 37);
            this.SetVolumeButton.TabIndex = 8;
            this.SetVolumeButton.Text = "Set Volume";
            this.SetVolumeButton.UseVisualStyleBackColor = true;
            this.SetVolumeButton.Click += new System.EventHandler(this.SetVolumeButton_Click);
            // 
            // AboutLinkLabel
            // 
            this.AboutLinkLabel.AutoSize = true;
            this.AboutLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.AboutLinkLabel.Location = new System.Drawing.Point(10, 615);
            this.AboutLinkLabel.Name = "AboutLinkLabel";
            this.AboutLinkLabel.Size = new System.Drawing.Size(42, 16);
            this.AboutLinkLabel.TabIndex = 9;
            this.AboutLinkLabel.TabStop = true;
            this.AboutLinkLabel.Text = "About";
            this.AboutLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AboutLinkLabel_LinkClicked);
            // 
            // HelpLinkLabel
            // 
            this.HelpLinkLabel.AutoSize = true;
            this.HelpLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.HelpLinkLabel.Location = new System.Drawing.Point(512, 615);
            this.HelpLinkLabel.Name = "HelpLinkLabel";
            this.HelpLinkLabel.Size = new System.Drawing.Size(36, 16);
            this.HelpLinkLabel.TabIndex = 10;
            this.HelpLinkLabel.TabStop = true;
            this.HelpLinkLabel.Text = "Help";
            this.HelpLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLinkLabel_LinkClicked);
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionLabel.Location = new System.Drawing.Point(267, 615);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(29, 15);
            this.VersionLabel.TabIndex = 11;
            this.VersionLabel.Text = "v1.0";
            // 
            // TxtAbovePathBoxlabel
            // 
            this.TxtAbovePathBoxlabel.AutoSize = true;
            this.TxtAbovePathBoxlabel.Location = new System.Drawing.Point(13, 248);
            this.TxtAbovePathBoxlabel.Name = "TxtAbovePathBoxlabel";
            this.TxtAbovePathBoxlabel.Size = new System.Drawing.Size(223, 13);
            this.TxtAbovePathBoxlabel.TabIndex = 12;
            this.TxtAbovePathBoxlabel.Text = "FINAL FANTASY XIII-2 launcher file location :";
            // 
            // CoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 638);
            this.Controls.Add(this.TxtAbovePathBoxlabel);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.HelpLinkLabel);
            this.Controls.Add(this.AboutLinkLabel);
            this.Controls.Add(this.SetVolumeButton);
            this.Controls.Add(this.TxtAboveSliderLabel);
            this.Controls.Add(this.SliderValuesLabel);
            this.Controls.Add(this.SliderTrackBar);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.FileSystemGroupBox);
            this.Controls.Add(this.VoGroupBox);
            this.Controls.Add(this.PathTextBox);
            this.Controls.Add(this.AppImgPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CoreForm";
            this.Text = "Final Fantasy XIII-2 - Music Volume Slider";
            ((System.ComponentModel.ISupportInitialize)(this.AppImgPictureBox)).EndInit();
            this.VoGroupBox.ResumeLayout(false);
            this.VoGroupBox.PerformLayout();
            this.FileSystemGroupBox.ResumeLayout(false);
            this.FileSystemGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox AppImgPictureBox;
        private System.Windows.Forms.TextBox PathTextBox;
        private System.Windows.Forms.GroupBox VoGroupBox;
        private System.Windows.Forms.GroupBox FileSystemGroupBox;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.TrackBar SliderTrackBar;
        private System.Windows.Forms.Label SliderValuesLabel;
        private System.Windows.Forms.Label TxtAboveSliderLabel;
        private System.Windows.Forms.Button SetVolumeButton;
        private System.Windows.Forms.RadioButton JpVoRadiobutton;
        private System.Windows.Forms.RadioButton EnVoRadiobutton;
        private System.Windows.Forms.RadioButton NovaRadioButton;
        private System.Windows.Forms.RadioButton PackedRadioButton;
        private System.Windows.Forms.LinkLabel AboutLinkLabel;
        private System.Windows.Forms.LinkLabel HelpLinkLabel;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label TxtAbovePathBoxlabel;
    }
}