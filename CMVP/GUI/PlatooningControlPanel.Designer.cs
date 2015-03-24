namespace CMVP
{
    partial class PlatooningControlPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.carToFollowLabel = new System.Windows.Forms.Label();
            this.carToFollowIDDropDown = new System.Windows.Forms.ComboBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // carToFollowLabel
            // 
            this.carToFollowLabel.AutoSize = true;
            this.carToFollowLabel.Location = new System.Drawing.Point(5, 7);
            this.carToFollowLabel.Name = "carToFollowLabel";
            this.carToFollowLabel.Size = new System.Drawing.Size(68, 13);
            this.carToFollowLabel.TabIndex = 0;
            this.carToFollowLabel.Text = "Car to follow:";
            // 
            // carToFollowIDDropDown
            // 
            this.carToFollowIDDropDown.FormattingEnabled = true;
            this.carToFollowIDDropDown.Location = new System.Drawing.Point(79, 4);
            this.carToFollowIDDropDown.Name = "carToFollowIDDropDown";
            this.carToFollowIDDropDown.Size = new System.Drawing.Size(69, 21);
            this.carToFollowIDDropDown.TabIndex = 1;
            this.carToFollowIDDropDown.DropDown += new System.EventHandler(this.carToFollowIDDropDown_DropDown);
            this.carToFollowIDDropDown.SelectedIndexChanged += new System.EventHandler(this.carToFollowIDDropDown_SelectedIndexChanged);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(102, 32);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(22, 13);
            this.statusLabel.TabIndex = 2;
            this.statusLabel.Text = "NA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Platooning status:";
            // 
            // PlatooningControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.carToFollowIDDropDown);
            this.Controls.Add(this.carToFollowLabel);
            this.Name = "PlatooningControlPanel";
            this.Size = new System.Drawing.Size(200, 138);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label carToFollowLabel;
        private System.Windows.Forms.ComboBox carToFollowIDDropDown;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label label1;
    }
}
