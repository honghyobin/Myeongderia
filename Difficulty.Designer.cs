namespace Myeongderia
{
    partial class Difficulty
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
            easyButton = new PictureBox();
            hardButton = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)easyButton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)hardButton).BeginInit();
            SuspendLayout();
            // 
            // easyButton
            // 
            easyButton.BackColor = Color.Transparent;
            easyButton.BackgroundImage = Properties.Resources.easyBtn;
            easyButton.BackgroundImageLayout = ImageLayout.Stretch;
            easyButton.Location = new Point(182, 435);
            easyButton.Name = "easyButton";
            easyButton.Size = new Size(185, 104);
            easyButton.TabIndex = 2;
            easyButton.TabStop = false;
            easyButton.Click += pictureBox1_Click;
            // 
            // hardButton
            // 
            hardButton.BackColor = Color.Transparent;
            hardButton.BackgroundImage = Properties.Resources.hardBtn;
            hardButton.BackgroundImageLayout = ImageLayout.Stretch;
            hardButton.Location = new Point(542, 435);
            hardButton.Name = "hardButton";
            hardButton.Size = new Size(185, 104);
            hardButton.TabIndex = 3;
            hardButton.TabStop = false;
            hardButton.Click += hardButton_Click_1;
            // 
            // Difficulty
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.DifficultyBackground;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(944, 601);
            Controls.Add(hardButton);
            Controls.Add(easyButton);
            Name = "Difficulty";
            Text = "Difficulty";
            Load += Difficulty_Load;
            ((System.ComponentModel.ISupportInitialize)easyButton).EndInit();
            ((System.ComponentModel.ISupportInitialize)hardButton).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox easyButton;
        private PictureBox hardButton;
    }
}