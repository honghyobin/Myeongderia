namespace Myeongderia
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            startButton = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)startButton).BeginInit();
            SuspendLayout();
            // 
            // startButton
            // 
            startButton.BackColor = Color.Transparent;
            startButton.BackgroundImage = Properties.Resources.startBtn;
            startButton.BackgroundImageLayout = ImageLayout.Stretch;
            startButton.Location = new Point(346, 438);
            startButton.Name = "startButton";
            startButton.Size = new Size(253, 119);
            startButton.TabIndex = 1;
            startButton.TabStop = false;
            startButton.Click += startButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.None;
            BackgroundImage = Properties.Resources.StartBackground;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(960, 640);
            Controls.Add(startButton);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)startButton).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox pictureBox1;
        private PictureBox startButton;
    }
}
