using System;
using System.Drawing;
using System.Windows.Forms;

namespace Myeongderia
{
    public partial class PopupForm : Form
    {
        private Panel imagePanel;
        private int currentOffset = 0;

        // GameForm에서 등록하는 콜백 (완료 버튼 누를 때 실행됨)
        public Action OnComplete;

        public PopupForm()
        {
            InitializeComponent();

            this.Text = "재료 추가창";
            this.Size = new Size(400, 650);

            imagePanel = new Panel();
            imagePanel.Dock = DockStyle.Fill;
            imagePanel.AutoScroll = true;
            imagePanel.Padding = Padding.Empty;

            Button doneButton = new Button();
            doneButton.Text = "완료";
            doneButton.Size = new Size(100, 40);
            doneButton.Dock = DockStyle.Bottom;
            doneButton.Click += (s, e) =>
            {
                OnComplete?.Invoke(); // GameForm에서 지정한 함수 실행
                this.Close(); // 팝업 닫기
            };

            this.Controls.Add(imagePanel);
            this.Controls.Add(doneButton);
        }

        // 팝업에 그림 추가
        public void AddImage(Image image)
        {
            PictureBox pic = new PictureBox();
            pic.Image = image;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Size = new Size(80, 80);
            pic.Location = new Point(10, currentOffset);
            pic.BringToFront();
            currentOffset += 78; // 살짝 겹치게

            imagePanel.Controls.Add(pic);
        }

        // 그림 초기화
        public void ClearImages()
        {
            imagePanel.Controls.Clear();
            currentOffset = 0;
        }
    }
}
