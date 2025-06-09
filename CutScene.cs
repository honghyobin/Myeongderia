using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WMPLib; // Windows Media Player 참조 필요 (프로젝트에 COM -> Windows Media Player 추가)

namespace Myeongderia
{
    public partial class CutScene : Form
    {
        private int currentSceneIndex = 0;
        private List<Image> scenes = new List<Image>();
        private PictureBox sceneBox;

        private WindowsMediaPlayer bgmPlayer;

        public CutScene()
        {
            InitializeComponent();

            this.Size = new Size(960, 640);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // 1. 전체 화면을 채우는 PictureBox 생성
            sceneBox = new PictureBox();
            sceneBox.Dock = DockStyle.Fill;
            sceneBox.SizeMode = PictureBoxSizeMode.StretchImage;
            sceneBox.Click += (s, e) => AdvanceScene();
            this.Controls.Add(sceneBox);

            // 2. 리소스에서 Scene1~Scene14까지 이미지 불러오기
            for (int i = 1; i <= 14; i++)
            {
                var image = (Image)Properties.Resources.ResourceManager.GetObject($"Scene{i}");
                if (image != null)
                    scenes.Add(image);
            }

            // 3. 첫 번째 컷신 표시
            if (scenes.Count > 0)
                sceneBox.Image = scenes[0];

            // 4. 폼 자체 클릭도 컷신 넘기기로 연결
            this.Click += (s, e) => AdvanceScene();
        }

        // 컷신 넘기기 및 마지막 장면 이후 로직
        private void AdvanceScene()
        {
            currentSceneIndex++;

            if (currentSceneIndex < scenes.Count)
            {
                sceneBox.Image = scenes[currentSceneIndex];
            }
            else
            {
                PlayBackgroundMusic(); // 컷신 종료 시 배경음악 시작

                this.Hide();
                Difficulty difficulty = new Difficulty();
                difficulty.StartPosition = FormStartPosition.CenterScreen;
                difficulty.Show();
            }
        }

        private void PlayBackgroundMusic()
        {
            try
            {
                string bgmPath = Path.Combine(Application.StartupPath, "Resources", "BurgerBeat.mp3");
                bgmPlayer = new WindowsMediaPlayer();
                bgmPlayer.URL = bgmPath;
                bgmPlayer.settings.setMode("loop", true);
                bgmPlayer.controls.play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("배경음악 재생 실패: " + ex.Message);
            }
        }

        private void CutScene_Load(object sender, EventArgs e) { }
    }
}
