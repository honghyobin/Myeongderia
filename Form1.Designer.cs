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
            startButton = new Button();
            SuspendLayout();
            // 
            // startButton
            // 
            startButton.BackColor = Color.White;
            startButton.Font = new Font("문체부 돋음체", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 129);
            startButton.ForeColor = Color.SeaGreen;
            startButton.Location = new Point(349, 439);
            startButton.Name = "startButton";
            startButton.Size = new Size(282, 63);
            startButton.TabIndex = 0;
            startButton.Text = "게임 시작";
            startButton.UseVisualStyleBackColor = false;
            startButton.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.StartBackground;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(944, 601);
            Controls.Add(startButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button startButton;
    }
}
