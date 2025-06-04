namespace Myeongderia
{
    partial class GameForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            orderLabel = new Label();
            recipePictureBox = new PictureBox();
            goalLabel = new Label();
            panelBread = new Panel();
            panelPatty = new Panel();
            panelOnion = new Panel();
            panelCheese = new Panel();
            panelTomato = new Panel();
            panelLettuce = new Panel();
            customerPictureBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)recipePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customerPictureBox).BeginInit();
            SuspendLayout();
            // 
            // orderLabel
            // 
            orderLabel.AutoSize = true;
            orderLabel.Location = new Point(386, 158);
            orderLabel.Name = "orderLabel";
            orderLabel.Size = new Size(55, 15);
            orderLabel.TabIndex = 3;
            orderLabel.Text = "주문내역";
            // 
            // recipePictureBox
            // 
            recipePictureBox.BackColor = Color.Transparent;
            recipePictureBox.Location = new Point(755, 248);
            recipePictureBox.Name = "recipePictureBox";
            recipePictureBox.Size = new Size(146, 81);
            recipePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            recipePictureBox.TabIndex = 1;
            recipePictureBox.TabStop = false;
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
            // panelBread
            // 
            panelBread.Location = new Point(122, 431);
            panelBread.Name = "panelBread";
            panelBread.Size = new Size(124, 78);
            panelBread.TabIndex = 10;
            // 
            // panelPatty
            // 
            panelPatty.Location = new Point(262, 430);
            panelPatty.Name = "panelPatty";
            panelPatty.Size = new Size(124, 78);
            panelPatty.TabIndex = 11;
            // 
            // panelOnion
            // 
            panelOnion.Location = new Point(407, 433);
            panelOnion.Name = "panelOnion";
            panelOnion.Size = new Size(59, 75);
            panelOnion.TabIndex = 12;
            // 
            // panelCheese
            // 
            panelCheese.Location = new Point(472, 433);
            panelCheese.Name = "panelCheese";
            panelCheese.Size = new Size(59, 75);
            panelCheese.TabIndex = 13;
            // 
            // panelTomato
            // 
            panelTomato.Location = new Point(551, 433);
            panelTomato.Name = "panelTomato";
            panelTomato.Size = new Size(79, 72);
            panelTomato.TabIndex = 14;
            // 
            // panelLettuce
            // 
            panelLettuce.Location = new Point(640, 430);
            panelLettuce.Name = "panelLettuce";
            panelLettuce.Size = new Size(83, 75);
            panelLettuce.TabIndex = 15;
            // 
            // customerPictureBox
            // 
            customerPictureBox.Image = Properties.Resources.Person1;
            customerPictureBox.Location = new Point(578, 317);
            customerPictureBox.Name = "customerPictureBox";
            customerPictureBox.Size = new Size(111, 56);
            customerPictureBox.TabIndex = 16;
            customerPictureBox.TabStop = false;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.GameBackgroundImage;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(944, 601);
            Controls.Add(customerPictureBox);
            Controls.Add(panelLettuce);
            Controls.Add(panelTomato);
            Controls.Add(panelCheese);
            Controls.Add(panelOnion);
            Controls.Add(panelPatty);
            Controls.Add(panelBread);
            Controls.Add(goalLabel);
            Controls.Add(recipePictureBox);
            Controls.Add(orderLabel);
            Name = "GameForm";
            Text = "GameOder";
            ((System.ComponentModel.ISupportInitialize)recipePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)customerPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label orderLabel;
        private Label goalLabel;        
        private PictureBox recipePictureBox;
        private Panel panelBread;
        private Panel panelPatty;
        private Panel panelOnion;
        private Panel panelCheese;
        private Panel panelTomato;
        private Panel panelLettuce;
        private PictureBox customerPictureBox;
    }

}

