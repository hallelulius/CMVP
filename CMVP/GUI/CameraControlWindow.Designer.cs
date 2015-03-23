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
            this.o = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cameraSettings = new System.Windows.Forms.Button();
            this.checkBoxDrawDirection = new System.Windows.Forms.CheckBox();
            this.checkBoxDrawTriangles = new System.Windows.Forms.CheckBox();
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
            // drawDetectedFeaturesCheckBox
            // 
            this.drawDetectedFeaturesCheckBox.AutoSize = true;
            this.drawDetectedFeaturesCheckBox.Location = new System.Drawing.Point(6, 65);
            this.drawDetectedFeaturesCheckBox.Name = "drawDetectedFeaturesCheckBox";
            this.drawDetectedFeaturesCheckBox.Size = new System.Drawing.Size(137, 17);
            this.drawDetectedFeaturesCheckBox.TabIndex = 2;
            this.drawDetectedFeaturesCheckBox.Text = "Draw detected features";
            this.drawDetectedFeaturesCheckBox.UseVisualStyleBackColor = true;
            // 
            // drawTrackCheckBox
            // 
            this.drawTrackCheckBox.AutoSize = true;
            this.drawTrackCheckBox.Location = new System.Drawing.Point(6, 19);
            this.drawTrackCheckBox.Name = "drawTrackCheckBox";
            this.drawTrackCheckBox.Size = new System.Drawing.Size(78, 17);
            this.drawTrackCheckBox.TabIndex = 3;
            this.drawTrackCheckBox.Text = "Draw track";
            this.drawTrackCheckBox.UseVisualStyleBackColor = true;
            // 
            // videoStreamPanel
            // 
            this.videoStreamPanel.Location = new System.Drawing.Point(214, 13);
            this.videoStreamPanel.Name = "videoStreamPanel";
            this.videoStreamPanel.Size = new System.Drawing.Size(458, 364);
            this.videoStreamPanel.TabIndex = 4;
            this.videoStreamPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.videoStreamPanel_Paint);
            // 
            // drawCarIDCheckBox
            // 
            this.drawCarIDCheckBox.AutoSize = true;
            this.drawCarIDCheckBox.Location = new System.Drawing.Point(6, 42);
            this.drawCarIDCheckBox.Name = "drawCarIDCheckBox";
            this.drawCarIDCheckBox.Size = new System.Drawing.Size(83, 17);
            this.drawCarIDCheckBox.TabIndex = 5;
            this.drawCarIDCheckBox.Text = "Draw car ID";
            this.drawCarIDCheckBox.UseVisualStyleBackColor = true;
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
            this.groupBox2.Controls.Add(this.cameraSettings);
            this.groupBox2.Controls.Add(this.checkBoxDrawDirection);
            this.groupBox2.Controls.Add(this.checkBoxDrawTriangles);
            this.groupBox2.Controls.Add(this.checkBoxDrawCenters);
            this.groupBox2.Controls.Add(this.checkBoxDrawCirkels);
            this.groupBox2.Controls.Add(this.drawDetectedFeaturesCheckBox);
            this.groupBox2.Controls.Add(this.drawCarIDCheckBox);
            this.groupBox2.Controls.Add(this.drawTrackCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 87);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(196, 291);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Features";
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
            // checkBoxDrawDirection
            // 
            this.checkBoxDrawDirection.AutoSize = true;
            this.checkBoxDrawDirection.Location = new System.Drawing.Point(6, 159);
            this.checkBoxDrawDirection.Name = "checkBoxDrawDirection";
            this.checkBoxDrawDirection.Size = new System.Drawing.Size(144, 17);
            this.checkBoxDrawDirection.TabIndex = 9;
            this.checkBoxDrawDirection.Text = "Draw detected direktions";
            this.checkBoxDrawDirection.UseVisualStyleBackColor = true;
            this.checkBoxDrawDirection.CheckedChanged += new System.EventHandler(this.checkBoxDrawDirection_CheckedChanged);
            // 
            // checkBoxDrawTriangles
            // 
            this.checkBoxDrawTriangles.AutoSize = true;
            this.checkBoxDrawTriangles.Location = new System.Drawing.Point(6, 135);
            this.checkBoxDrawTriangles.Name = "checkBoxDrawTriangles";
            this.checkBoxDrawTriangles.Size = new System.Drawing.Size(138, 17);
            this.checkBoxDrawTriangles.TabIndex = 8;
            this.checkBoxDrawTriangles.Text = "Draw detected triangles";
            this.checkBoxDrawTriangles.UseVisualStyleBackColor = true;
            this.checkBoxDrawTriangles.CheckedChanged += new System.EventHandler(this.checkBoxDrawTriangles_CheckedChanged);
            // 
            // checkBoxDrawCenters
            // 
            this.checkBoxDrawCenters.AutoSize = true;
            this.checkBoxDrawCenters.Location = new System.Drawing.Point(6, 111);
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
            this.checkBoxDrawCirkels.Location = new System.Drawing.Point(6, 88);
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
        private System.Windows.Forms.CheckBox drawDetectedFeaturesCheckBox;
        private System.Windows.Forms.CheckBox drawTrackCheckBox;
        private System.Windows.Forms.Panel videoStreamPanel;
        private System.Windows.Forms.CheckBox drawCarIDCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker o;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxDrawCirkels;
        private System.Windows.Forms.CheckBox checkBoxDrawDirection;
        private System.Windows.Forms.CheckBox checkBoxDrawTriangles;
        private System.Windows.Forms.CheckBox checkBoxDrawCenters;
        private System.Windows.Forms.Button cameraSettings;
    }
}