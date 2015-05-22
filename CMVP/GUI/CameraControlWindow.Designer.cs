namespace CMVP
{
    partial class CameraControlWindow
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
            this.checkBoxDrawTrack = new System.Windows.Forms.CheckBox();
            this.videoStreamPanel = new System.Windows.Forms.Panel();
            this.checkBoxDrawId = new System.Windows.Forms.CheckBox();
            this.o = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.scrollbar_label = new System.Windows.Forms.Label();
            this.threshold_ScrollBar = new System.Windows.Forms.HScrollBar();
            this.checkBoxDrawTails = new System.Windows.Forms.CheckBox();
            this.cameraSettings = new System.Windows.Forms.Button();
            this.checkBoxDrawRefHeading = new System.Windows.Forms.CheckBox();
            this.checkBoxDrawDirection = new System.Windows.Forms.CheckBox();
            this.checkBoxDrawWindows = new System.Windows.Forms.CheckBox();
            this.checkBoxDrawCenters = new System.Windows.Forms.CheckBox();
            this.checkBoxDrawCirkels = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxDrawTrack
            // 
            this.checkBoxDrawTrack.AutoSize = true;
            this.checkBoxDrawTrack.Location = new System.Drawing.Point(6, 19);
            this.checkBoxDrawTrack.Name = "checkBoxDrawTrack";
            this.checkBoxDrawTrack.Size = new System.Drawing.Size(83, 17);
            this.checkBoxDrawTrack.TabIndex = 3;
            this.checkBoxDrawTrack.Text = "Draw tracks";
            this.checkBoxDrawTrack.UseVisualStyleBackColor = true;
            this.checkBoxDrawTrack.CheckedChanged += new System.EventHandler(this.checkBoxDrawTrack_CheckedChanged_1);
            // 
            // videoStreamPanel
            // 
            this.videoStreamPanel.AutoSize = true;
            this.videoStreamPanel.Location = new System.Drawing.Point(214, 12);
            this.videoStreamPanel.Name = "videoStreamPanel";
            this.videoStreamPanel.Size = new System.Drawing.Size(458, 366);
            this.videoStreamPanel.TabIndex = 4;
            this.videoStreamPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.videoStreamPanel_Paint);
            // 
            // checkBoxDrawId
            // 
            this.checkBoxDrawId.AutoSize = true;
            this.checkBoxDrawId.Location = new System.Drawing.Point(6, 42);
            this.checkBoxDrawId.Name = "checkBoxDrawId";
            this.checkBoxDrawId.Size = new System.Drawing.Size(83, 17);
            this.checkBoxDrawId.TabIndex = 5;
            this.checkBoxDrawId.Text = "Draw car ID";
            this.checkBoxDrawId.UseVisualStyleBackColor = true;
            this.checkBoxDrawId.CheckedChanged += new System.EventHandler(this.checkBoxDrawId_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.scrollbar_label);
            this.groupBox2.Controls.Add(this.threshold_ScrollBar);
            this.groupBox2.Controls.Add(this.checkBoxDrawTails);
            this.groupBox2.Controls.Add(this.cameraSettings);
            this.groupBox2.Controls.Add(this.checkBoxDrawRefHeading);
            this.groupBox2.Controls.Add(this.checkBoxDrawDirection);
            this.groupBox2.Controls.Add(this.checkBoxDrawWindows);
            this.groupBox2.Controls.Add(this.checkBoxDrawCenters);
            this.groupBox2.Controls.Add(this.checkBoxDrawCirkels);
            this.groupBox2.Controls.Add(this.checkBoxDrawId);
            this.groupBox2.Controls.Add(this.checkBoxDrawTrack);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(196, 366);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Features";
            // 
            // scrollbar_label
            // 
            this.scrollbar_label.AutoSize = true;
            this.scrollbar_label.Location = new System.Drawing.Point(14, 288);
            this.scrollbar_label.Name = "scrollbar_label";
            this.scrollbar_label.Size = new System.Drawing.Size(54, 13);
            this.scrollbar_label.TabIndex = 13;
            this.scrollbar_label.Text = "Threshold";
            // 
            // threshold_ScrollBar
            // 
            this.threshold_ScrollBar.Location = new System.Drawing.Point(17, 311);
            this.threshold_ScrollBar.Maximum = 255;
            this.threshold_ScrollBar.Minimum = 100;
            this.threshold_ScrollBar.Name = "threshold_ScrollBar";
            this.threshold_ScrollBar.Size = new System.Drawing.Size(160, 15);
            this.threshold_ScrollBar.TabIndex = 12;
            this.threshold_ScrollBar.Value = 100;
            this.threshold_ScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.threshold_ScrollBar_Scroll);
            // 
            // checkBoxDrawTails
            // 
            this.checkBoxDrawTails.AutoSize = true;
            this.checkBoxDrawTails.Location = new System.Drawing.Point(6, 180);
            this.checkBoxDrawTails.Name = "checkBoxDrawTails";
            this.checkBoxDrawTails.Size = new System.Drawing.Size(72, 17);
            this.checkBoxDrawTails.TabIndex = 11;
            this.checkBoxDrawTails.Text = "Draw tails";
            this.checkBoxDrawTails.UseVisualStyleBackColor = true;
            this.checkBoxDrawTails.CheckedChanged += new System.EventHandler(this.checkBoxDrawTails_CheckedChanged);
            // 
            // cameraSettings
            // 
            this.cameraSettings.Location = new System.Drawing.Point(17, 218);
            this.cameraSettings.Name = "cameraSettings";
            this.cameraSettings.Size = new System.Drawing.Size(156, 23);
            this.cameraSettings.TabIndex = 10;
            this.cameraSettings.Text = "Show Camera Settings";
            this.cameraSettings.UseVisualStyleBackColor = true;
            this.cameraSettings.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxDrawRefHeading
            // 
            this.checkBoxDrawRefHeading.AutoSize = true;
            this.checkBoxDrawRefHeading.Location = new System.Drawing.Point(6, 157);
            this.checkBoxDrawRefHeading.Name = "checkBoxDrawRefHeading";
            this.checkBoxDrawRefHeading.Size = new System.Drawing.Size(157, 17);
            this.checkBoxDrawRefHeading.TabIndex = 10;
            this.checkBoxDrawRefHeading.Text = "Draw reference of headings";
            this.checkBoxDrawRefHeading.UseVisualStyleBackColor = true;
            this.checkBoxDrawRefHeading.CheckedChanged += new System.EventHandler(this.checkBoxDrawRefHeading_CheckedChanged);
            // 
            // checkBoxDrawDirection
            // 
            this.checkBoxDrawDirection.AutoSize = true;
            this.checkBoxDrawDirection.Location = new System.Drawing.Point(6, 134);
            this.checkBoxDrawDirection.Name = "checkBoxDrawDirection";
            this.checkBoxDrawDirection.Size = new System.Drawing.Size(144, 17);
            this.checkBoxDrawDirection.TabIndex = 9;
            this.checkBoxDrawDirection.Text = "Draw detected direktions";
            this.checkBoxDrawDirection.UseVisualStyleBackColor = true;
            this.checkBoxDrawDirection.CheckedChanged += new System.EventHandler(this.checkBoxDrawDirection_CheckedChanged);
            // 
            // checkBoxDrawWindows
            // 
            this.checkBoxDrawWindows.AutoSize = true;
            this.checkBoxDrawWindows.Location = new System.Drawing.Point(6, 111);
            this.checkBoxDrawWindows.Name = "checkBoxDrawWindows";
            this.checkBoxDrawWindows.Size = new System.Drawing.Size(95, 17);
            this.checkBoxDrawWindows.TabIndex = 8;
            this.checkBoxDrawWindows.Text = "Draw windows";
            this.checkBoxDrawWindows.UseVisualStyleBackColor = true;
            this.checkBoxDrawWindows.CheckedChanged += new System.EventHandler(this.checkBoxDrawWindows_CheckedChanged);
            // 
            // checkBoxDrawCenters
            // 
            this.checkBoxDrawCenters.AutoSize = true;
            this.checkBoxDrawCenters.Location = new System.Drawing.Point(6, 88);
            this.checkBoxDrawCenters.Name = "checkBoxDrawCenters";
            this.checkBoxDrawCenters.Size = new System.Drawing.Size(137, 17);
            this.checkBoxDrawCenters.TabIndex = 7;
            this.checkBoxDrawCenters.Text = "Draw detected  centers";
            this.checkBoxDrawCenters.UseVisualStyleBackColor = true;
            this.checkBoxDrawCenters.CheckedChanged += new System.EventHandler(this.checkBoxDrawCenters_CheckedChanged);
            // 
            // checkBoxDrawCirkels
            // 
            this.checkBoxDrawCirkels.AutoSize = true;
            this.checkBoxDrawCirkels.Location = new System.Drawing.Point(6, 65);
            this.checkBoxDrawCirkels.Name = "checkBoxDrawCirkels";
            this.checkBoxDrawCirkels.Size = new System.Drawing.Size(129, 17);
            this.checkBoxDrawCirkels.TabIndex = 6;
            this.checkBoxDrawCirkels.Text = "Draw detected circles";
            this.checkBoxDrawCirkels.UseVisualStyleBackColor = true;
            this.checkBoxDrawCirkels.CheckedChanged += new System.EventHandler(this.checkBoxDrawCirkels_CheckedChanged);
            // 
            // CameraControlWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(684, 389);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.videoStreamPanel);
            this.Name = "CameraControlWindow";
            this.Text = "CMVP - Camera Control";
            this.Load += new System.EventHandler(this.CameraControlWindow_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxDrawTrack;
        private System.Windows.Forms.Panel videoStreamPanel;
        private System.Windows.Forms.CheckBox checkBoxDrawId;
        private System.ComponentModel.BackgroundWorker o;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxDrawCirkels;
        private System.Windows.Forms.CheckBox checkBoxDrawDirection;
        private System.Windows.Forms.CheckBox checkBoxDrawWindows;
        private System.Windows.Forms.CheckBox checkBoxDrawCenters;
        private System.Windows.Forms.Button cameraSettings;
        private System.Windows.Forms.CheckBox checkBoxDrawRefHeading;
        private System.Windows.Forms.CheckBox checkBoxDrawTails;
        private System.Windows.Forms.HScrollBar threshold_ScrollBar;
        private System.Windows.Forms.Label scrollbar_label;
    }
}