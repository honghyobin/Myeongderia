using System;
using System.Drawing;
using System.Windows.Forms;

namespace Myeongderia
{
    public partial class PopupForm : Form
    {
        private Panel imagePanel;//효빈:재료 이미지들을 표시할 패널
        private int currentOffset = 0;//이미지들이 세로로 쌓일 때 위치 조정

        public Action OnComplete;
        //효빈:팝업창
        public PopupForm()
        {
            InitializeComponent();
            //효빈:상단 제목과 사이즈
            this.Text = "재료 추가창";
            this.Size = new Size(400, 650);
            //효빈:이미지가 길어지면 스크롤해서 볼 수 있도록
            imagePanel = new Panel();
            imagePanel.Dock = DockStyle.Fill;
            imagePanel.AutoScroll = true;
            imagePanel.Padding = Padding.Empty;
            //효빈:완료 버튼 생성 및 설정
            Button doneButton = new Button();
            doneButton.Text = "완료";
            doneButton.Size = new Size(100, 40);
            doneButton.Dock = DockStyle.Bottom;//효빈:버튼을 아래에 배치
            doneButton.Click += (s, e) =>
            {
                OnComplete?.Invoke();
                this.Close(); //효빈:팝업 닫기
            };
            //효빈:이미지를 보여줄 패널, 완료버튼을 폼에 추가
            this.Controls.Add(imagePanel);
            this.Controls.Add(doneButton);
        }

        //효빈:팝업에 그림 추가
        public void AddImage(Image image)
        {
            PictureBox pic = new PictureBox();
            pic.Image = image;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Size = new Size(80, 80);
            pic.Location = new Point(10, currentOffset);//효빈:세로로 쌓기 위해 위치 지정
            pic.BringToFront();
            currentOffset += 78; //효빈:살짝 겹치게
            //효빈:이미지 패널에 추가
            imagePanel.Controls.Add(pic);
        }

        //효빈:팝업 이미지 제거
        public void ClearImages()
        {
            imagePanel.Controls.Clear();
            currentOffset = 0;
        }
    }
}
