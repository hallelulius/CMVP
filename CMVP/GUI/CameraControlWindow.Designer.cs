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
            this.drawDetectedFeaturesCheckBox = new System.Windows.Forms.CheckBox();
            this.drawTrackCheckBox = new System.Windows.Forms.CheckBox();
            this.videoStreamPanel = new System.Windows.Forms.Panel();
            this.drawCarIDCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // drawVideoRadioButton
            // 
            this.drawVideoRadioButton.AutoSize = true;
            this.drawVideoRadioButton.Location = new System.Drawing.Point(13, 13);
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
            this.processedVideoRadioButton.Location = new System.Drawing.Point(13, 36);
            this.processedVideoRadioButton.Name = "processedVideoRadioButton";
            this.processedVideoRadioButton.Size = new System.Drawing.Size(104, 17);
            this.processedVideoRadioButton.TabIndex = 1;
            this.processedVideoRadioButton.TabStop = true;
            this.processedVideoRadioButton.Text = "Processed video";
            this.processedVideoRadioButton.UseVisualStyleBackColor = true;
            // 
            // drawDetectedFeaturesCheckBox
            // 
            this.drawDetectedFeaturesCheckBox.AutoSize = true;
            this.drawDetectedFeaturesCheckBox.Location = new System.Drawing.Point(13, 60);
            this.drawDetectedFeaturesCheckBox.Name = "drawDetectedFeaturesCheckBox";
            this.drawDetectedFeaturesCheckBox.Size = new System.Drawing.Size(137, 17);
            this.drawDetectedFeaturesCheckBox.TabIndex = 2;
            this.drawDetectedFeaturesCheckBox.Text = "Draw detected features";
            this.drawDetectedFeaturesCheckBox.UseVisualStyleBackColor = true;
            // 
            // drawTrackCheckBox
            // 
            this.drawTrackCheckBox.AutoSize = true;
            this.drawTrackCheckBox.Location = new System.Drawing.Point(13, 83);
            this.drawTrackCheckBox.Name = "drawTrackCheckBox";
            this.drawTrackCheckBox.Size = new System.Drawing.Size(78, 17);
            this.drawTrackCheckBox.TabIndex = 3;
            this.drawTrackCheckBox.Text = "Draw track";
            this.drawTrackCheckBox.UseVisualStyleBackColor = true;
            // 
            // videoStreamPanel
            // 
            this.videoStreamPanel.Location = new System.Drawing.Point(163, 13);
            this.videoStreamPanel.Name = "videoStreamPanel";
            this.videoStreamPanel.Size = new System.Drawing.Size(444, 364);
            this.videoStreamPanel.TabIndex = 4;
            this.videoStreamPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.videoStreamPanel_Paint);
            // 
            // drawCarIDCheckBox
            // 
            this.drawCarIDCheckBox.AutoSize = true;
            this.drawCarIDCheckBox.Location = new System.Drawing.Point(13, 106);
            this.drawCarIDCheckBox.Name = "drawCarIDCheckBox";
            this.drawCarIDCheckBox.Size = new System.Drawing.Size(83, 17);
            this.drawCarIDCheckBox.TabIndex = 5;
            this.drawCarIDCheckBox.Text = "Draw car ID";
            this.drawCarIDCheckBox.UseVisualStyleBackColor = true;
            // 
            // CameraControlWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 389);
            this.Controls.Add(this.drawCarIDCheckBox);
            this.Controls.Add(this.videoStreamPanel);
            this.Controls.Add(this.drawTrackCheckBox);
            this.Controls.Add(this.drawDetectedFeaturesCheckBox);
            this.Controls.Add(this.processedVideoRadioButton);
            this.Controls.Add(this.drawVideoRadioButton);
            this.Name = "CameraControlWindow";
            this.Text = "CMVP - Camera Control";
            this.Load += new System.EventHandler(this.CameraControlWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton drawVideoRadioButton;
        private System.Windows.Forms.RadioButton processedVideoRadioButton;
        private System.Windows.Forms.CheckBox drawDetectedFeaturesCheckBox;
        private System.Windows.Forms.CheckBox drawTrackCheckBox;
        private System.Windows.Forms.Panel videoStreamPanel;
        private System.Windows.Forms.CheckBox drawCarIDCheckBox;
    }
}