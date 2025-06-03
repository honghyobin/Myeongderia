using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Myeongderia
{
    public partial class GameForm : Form
    {
        // 팝업 창이 열려 있는지 여부를 저장하는 변수
        private bool isPopupOpen = false;

        // 현재 열려 있는 팝업 창(PopupForm)의 객체를 저장함
        private PopupForm popup;

        // 어떤 재료 버튼을 눌렀을 때, 기본 그림인지 바뀐 그림인지 나타내기 위해.
        // (빵을 처음 클릭하면 빵 윗부분, 두번째 클릭하면 빵 아랫부분)
        private Dictionary<string, bool> imageToggleStates = new Dictionary<string, bool>();

        public GameForm()
        {
            InitializeComponent();

            // 윈도우 창 크기를 가로 960, 세로 640으로 설정
            this.Size = new Size(960, 640);
        }

        // 클릭한 이미지를 팝업 창에 표시해줌
        // 팝업이 없으면 새로 만들고, 있으면 거기에 그림만 추가함
        private void ShowPopupWithImage(Image img)
        {
            // 팝업이 아직 열려 있지 않은 경우
            if (!isPopupOpen)
            {
                // 새 팝업 창을 만들기
                popup = new PopupForm();
                // 팝업 창의 위치를 현재 폼의 가운데로
                popup.StartPosition = FormStartPosition.CenterParent;
                // 팝업 창이 닫힐 때 실행되는 코드
                // 팝업이 닫히면 isPopupOpen을 false로 바꿔서 다시 열 수 있게.
                popup.FormClosed += (s, e) => { isPopupOpen = false; };
                // 팝업이 열려 있다고 표시
                isPopupOpen = true;
                // 팝업 창을 실제로 화면에 띄움
                popup.Show();
            }

            // 현재 열려 있는 팝업 창에 그림을 추가함
            popup.AddImage(img);
        }

        // 빵 버튼을 눌렀을 때 실행되는 코드
        // 눌렀을 때마다 기본 그림과 다른 그림을 번갈아 보여줌
        private void button1_Click(object sender, EventArgs e)
        {
            string key = "Bread";

            // 처음 눌렀다면 기본값 false로 저장 (기본 그림부터 시작)
            if (!imageToggleStates.ContainsKey(key))
                imageToggleStates[key] = false;

            // false면 기본 Bread.png, true면 Bread1.png
            string filePath = imageToggleStates[key]
                ? @"Resources\Bread1.png"
                : @"Resources\Bread.png";

            // 파일에서 그림을 불러옴
            Image imgToShow = Image.FromFile(filePath);

            // 그림을 팝업 창에 보여줌
            ShowPopupWithImage(imgToShow);

            // 다음에 누를 때는 다른 그림이 보이도록 토글 상태 반전
            imageToggleStates[key] = !imageToggleStates[key];
        }

        // 리소스에 미리 등록된 그림이므로 파일 경로 확인은 필요 없음

        // 패티 버튼 클릭 시 그림 추가
        private void button2_Click(object sender, EventArgs e)
        {
            ShowPopupWithImage(Properties.Resources.Patty);
        }

        // 양파 버튼 클릭 시 그림 추가
        private void button3_Click(object sender, EventArgs e)
        {
            ShowPopupWithImage(Properties.Resources.Onion);
        }

        // 치즈 버튼 클릭 시 그림 추가
        private void button4_Click(object sender, EventArgs e)
        {
            ShowPopupWithImage(Properties.Resources.Cheese);
        }

        // 토마토 버튼 클릭 시 그림 추가
        private void button5_Click(object sender, EventArgs e)
        {
            ShowPopupWithImage(Properties.Resources.Tomato);
        }

        // 양상추 버튼 클릭 시 그림 추가
        private void button6_Click(object sender, EventArgs e)
        {
            ShowPopupWithImage(Properties.Resources.Lettuce);
        }
    }
}
