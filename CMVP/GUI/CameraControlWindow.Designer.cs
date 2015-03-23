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
            this.drawVideoRadioButton = new System.Windows.Forms.RadioButton();
            this.processedVideoRadioButton = new System.Windows.Forms.RadioButton();
            this.checkBoxDrawTrack = new System.Windows.Forms.CheckBox();
            this.videoStreamPanel = new System.Windows.Forms.Panel();
            this.checkBoxDrawId = new System.Windows.Forms.CheckBox();
            this.o = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxDrawRefHeading = new System.Windows.Forms.CheckBox();
            this.checkBoxDrawDirection = new System.Windows.Forms.CheckBox();
            this.checkBoxDrawWindows = new System.Windows.Forms.CheckBox();
            this.checkBoxDrawCenters = new System.Windows.Forms.CheckBox();
            this.checkBoxDrawCirkels = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawVideoRadioButton
            // 
            this.drawVideoRadioButton.AutoSize = true;
            this.drawVideoRadioButton.Checked = true;
            this.drawVideoRadioButton.Location = new System.Drawing.Point(6, 19);
            this.drawVideoRadioButton.Name = "drawVideoRadioButton";
            this.drawVideoRadioButton.Size = new System.Drawing.Size(76, 17);
            this.drawVideoRadioButton.TabIndex = 0;
            this.drawVideoRadioButton.TabStop = true;
            this.drawVideoRadioButton.Text = "Raw video";
            this.drawVideoRadioButton.UseVisualStyleBackColor = true;
            // 
            // processedVideoRadioButton
            // 
            this.processedVideoRadioButton.AutoSize = true;
            this.processedVideoRadioButton.Location = new System.Drawing.Point(6, 42);
            this.processedVideoRadioButton.Name = "processedVideoRadioButton";
            this.processedVideoRadioButton.Size = new System.Drawing.Size(104, 17);
            this.processedVideoRadioButton.TabIndex = 1;
            this.processedVideoRadioButton.Text = "Processed video";
            this.processedVideoRadioButton.UseVisualStyleBackColor = true;
            this.processedVideoRadioButton.CheckedChanged += new System.EventHandler(this.processedVideoRadioButton_CheckedChanged);
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
            this.videoStreamPanel.Location = new System.Drawing.Point(214, 13);
            this.videoStreamPanel.Name = "videoStreamPanel";
            this.videoStreamPanel.Size = new System.Drawing.Size(458, 364);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.drawVideoRadioButton);
            this.groupBox1.Controls.Add(this.processedVideoRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(196, 68);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxDrawRefHeading);
            this.groupBox2.Controls.Add(this.checkBoxDrawDirection);
            this.groupBox2.Controls.Add(this.checkBoxDrawWindows);
            this.groupBox2.Controls.Add(this.checkBoxDrawCenters);
            this.groupBox2.Controls.Add(this.checkBoxDrawCirkels);
            this.groupBox2.Controls.Add(this.checkBoxDrawId);
            this.groupBox2.Controls.Add(this.checkBoxDrawTrack);
            this.groupBox2.Location = new System.Drawing.Point(12, 87);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(196, 291);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Features";
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
            this.checkBoxDrawWindows.Size = new System.Drawing.Size(98, 17);
            this.checkBoxDrawWindows.TabIndex = 8;
            this.checkBoxDrawWindows.Text = "Draw Windows";
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
            this.checkBoxDrawCirkels.Text = "Draw detected cirkels";
            this.checkBoxDrawCirkels.UseVisualStyleBackColor = true;
            this.checkBoxDrawCirkels.CheckedChanged += new System.EventHandler(this.checkBoxDrawCirkels_CheckedChanged);
            // 
            // CameraControlWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 389);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.videoStreamPanel);
            this.Name = "CameraControlWindow";
            this.Text = "CMVP - Camera Control";
            this.Load += new System.EventHandler(this.CameraControlWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton drawVideoRadioButton;
        private System.Windows.Forms.RadioButton processedVideoRadioButton;
        private System.Windows.Forms.CheckBox checkBoxDrawTrack;
        private System.Windows.Forms.Panel videoStreamPanel;
        private System.Windows.Forms.CheckBox checkBoxDrawId;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker o;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxDrawCirkels;
        private System.Windows.Forms.CheckBox checkBoxDrawDirection;
        private System.Windows.Forms.CheckBox checkBoxDrawWindows;
        private System.Windows.Forms.CheckBox checkBoxDrawCenters;
        private System.Windows.Forms.CheckBox checkBoxDrawRefHeading;
    }
}