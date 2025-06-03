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
            easyButton = new Button();
            hardButton = new Button();
            SuspendLayout();
            // 
            // easyButton
            // 
            easyButton.Font = new Font("문체부 돋음체", 36F, FontStyle.Regular, GraphicsUnit.Point, 129);
            easyButton.Location = new Point(120, 169);
            easyButton.Name = "easyButton";
            easyButton.Size = new Size(236, 109);
            easyButton.TabIndex = 0;
            easyButton.Text = "Easy";
            easyButton.UseVisualStyleBackColor = true;
            easyButton.Click += easyButton_Click;
            // 
            // hardButton
            // 
            hardButton.Font = new Font("문체부 돋음체", 36F, FontStyle.Regular, GraphicsUnit.Point, 129);
            hardButton.Location = new Point(440, 169);
            hardButton.Name = "hardButton";
            hardButton.Size = new Size(236, 109);
            hardButton.TabIndex = 1;
            hardButton.Text = "Hard";
            hardButton.UseVisualStyleBackColor = true;
            // 
            // Difficulty
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(hardButton);
            Controls.Add(easyButton);
            Name = "Difficulty";
            Text = "Difficulty";
            ResumeLayout(false);
        }

        #endregion

        private Button easyButton;
        private Button hardButton;
    }
}