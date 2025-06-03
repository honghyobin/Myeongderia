using System;
using System.Drawing;
using System.Windows.Forms;

namespace Myeongderia
{
    public partial class PopupForm : Form
    {
        private Panel imagePanel;

        // 재료 이미지를 아래로 겹쳐서 쌓기 위한 현재 Y 위치설정
        private int currentOffset = 0;

        public PopupForm()
        {
            InitializeComponent();

            this.Text = "재료 추가창";         // 창의 제목
            this.Size = new Size(400, 650);    // 창 크기

            // 그림들을 담을 패널 생성
            imagePanel = new Panel();
            imagePanel.Dock = DockStyle.Fill;      // 화면 전체를 차지
            imagePanel.AutoScroll = true;          // 이미지가 많아지면 스크롤
            imagePanel.Padding = Padding.Empty;    // 여백 없게.

            // "완료" 버튼 생성 - 누르면 창을 닫음
            Button doneButton = new Button();
            doneButton.Text = "완료";
            doneButton.Size = new Size(100, 40);        // 버튼 크기
            doneButton.Dock = DockStyle.Bottom;         // 아래쪽에 붙이기
            doneButton.Click += (s, e) => this.Close(); // 버튼 클릭 시 창 닫기

            // 만든 이미지 패널과 버튼을 폼에 추가
            this.Controls.Add(imagePanel);  // 그림 올라갈 공간
            this.Controls.Add(doneButton);  // 완료 버튼
        }

        // 외부에서 그림(Image)을 받아서 창에 추가하는 함수
        public void AddImage(Image image)
        {
            PictureBox pic = new PictureBox();
            pic.Image = image;                              // 실제 보여줄 이미지 설정
            pic.SizeMode = PictureBoxSizeMode.StretchImage; // 그림이 박스에 맞게 자동 조절
            pic.Size = new Size(80, 70);                    // 그림 크기 고정

            // 그림 위치를 직접 지정
            pic.Location = new Point(10, currentOffset);

            // 다음 그림은 살짝 겹치도록 현재 위치를 약간만 이동
            currentOffset += 70;

            // 이미지 패널에 그림(PictureBox)을 추가
            imagePanel.Controls.Add(pic);
        }
    }
}
