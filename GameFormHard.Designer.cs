namespace Myeongderia
{
    partial class GameFormHard
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
            goalLabel = new Label();
            orderLabel = new Label();
            recipePictureBox = new PictureBox();
            customerPictureBox = new PictureBox();
            panelBread = new Panel();
            panelPatty = new Panel();
            panelOnion = new Panel();
            panelCheese = new Panel();
            panelTomato = new Panel();
            panelLettuce = new Panel();
            balloonPictureBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)recipePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customerPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)balloonPictureBox).BeginInit();
            SuspendLayout();
            // 
            // goalLabel
            // 
            goalLabel.AutoSize = true;
            goalLabel.Font = new Font("맑은 고딕", 10F);
            goalLabel.Location = new Point(30, 50);
            goalLabel.Name = "goalLabel";
            goalLabel.Size = new Size(103, 19);
            goalLabel.TabIndex = 0;
            goalLabel.Text = "목표 금액 표시";
            // 
            // orderLabel
            // 
            orderLabel.AutoSize = true;
            orderLabel.MaximumSize = new Size(250, 0);
            orderLabel.BackColor = ColorTranslator.FromHtml("#ffc66c");
            orderLabel.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            orderLabel.Location = new Point(235, 118);
            orderLabel.Name = "orderLabel";
            orderLabel.Size = new Size(69, 20);
            orderLabel.TabIndex = 1;
            orderLabel.Text = "주문내역";

            // 
            // recipePictureBox
            // 
            recipePictureBox.BackColor = Color.Transparent;
            recipePictureBox.Location = new Point(755, 248);
            recipePictureBox.Name = "recipePictureBox";
            recipePictureBox.Size = new Size(146, 81);
            recipePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            recipePictureBox.TabIndex = 2;
            recipePictureBox.TabStop = false;
            // 
            // customerPictureBox
            // 
            customerPictureBox.Image = Properties.Resources.Person1;
            customerPictureBox.Location = new Point(578, 317);
            customerPictureBox.Name = "customerPictureBox";
            customerPictureBox.Size = new Size(111, 56);
            customerPictureBox.TabIndex = 17;
            customerPictureBox.TabStop = false;
            // 
            // panelBread
            // 
            panelBread.Location = new Point(122, 431);
            panelBread.Name = "panelBread";
            panelBread.Size = new Size(124, 78);
            panelBread.TabIndex = 18;
            // 
            // panelPatty
            // 
            panelPatty.Location = new Point(262, 430);
            panelPatty.Name = "panelPatty";
            panelPatty.Size = new Size(124, 78);
            panelPatty.TabIndex = 19;
            // 
            // panelOnion
            // 
            panelOnion.Location = new Point(407, 433);
            panelOnion.Name = "panelOnion";
            panelOnion.Size = new Size(59, 75);
            panelOnion.TabIndex = 20;
            // 
            // panelCheese
            // 
            panelCheese.Location = new Point(472, 433);
            panelCheese.Name = "panelCheese";
            panelCheese.Size = new Size(59, 75);
            panelCheese.TabIndex = 21;
            // 
            // panelTomato
            // 
            panelTomato.Location = new Point(551, 433);
            panelTomato.Name = "panelTomato";
            panelTomato.Size = new Size(79, 72);
            panelTomato.TabIndex = 22;
            // 
            // panelLettuce
            // 
            panelLettuce.Location = new Point(640, 430);
            panelLettuce.Name = "panelLettuce";
            panelLettuce.Size = new Size(83, 75);
            panelLettuce.TabIndex = 23;
            // 
            // balloonPictureBox
            // 
            balloonPictureBox.BackColor = Color.Transparent;
            balloonPictureBox.BackgroundImage = Properties.Resources.SpeechBubble;
            balloonPictureBox.BackgroundImageLayout = ImageLayout.Stretch;
            balloonPictureBox.Location = new Point(205, 92);
            balloonPictureBox.Name = "balloonPictureBox";
            balloonPictureBox.Size = new Size(303, 125);
            balloonPictureBox.TabIndex = 24;
            balloonPictureBox.TabStop = false;
            balloonPictureBox.UseWaitCursor = true;
            // 
            // GameFormHard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.GameBackgroundImage;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(944, 601);
            Controls.Add(balloonPictureBox);
            Controls.Add(panelLettuce);
            Controls.Add(panelTomato);
            Controls.Add(panelCheese);
            Controls.Add(panelOnion);
            Controls.Add(panelPatty);
            Controls.Add(panelBread);
            Controls.Add(customerPictureBox);
            Controls.Add(recipePictureBox);
            Controls.Add(orderLabel);
            Controls.Add(goalLabel);
            Name = "GameFormHard";
            Text = "GameFormHard";
            ((System.ComponentModel.ISupportInitialize)recipePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)customerPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)balloonPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label goalLabel;
        private Label orderLabel;
        private PictureBox recipePictureBox;
        private PictureBox customerPictureBox;
        private Panel panelBread;
        private Panel panelPatty;
        private Panel panelOnion;
        private Panel panelCheese;
        private Panel panelTomato;
        private Panel panelLettuce;
        private PictureBox balloonPictureBox;
    }
}