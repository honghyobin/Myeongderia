using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Myeongderia
{
    public partial class CutScene : Form
    {
        private int currentSceneIndex = 0;
        private List<Image> scenes = new List<Image>();
        private PictureBox sceneBox;

        public CutScene()
        {
            InitializeComponent();

            this.Size = new Size(960, 640);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // 1. 전체 화면을 채우는 PictureBox 생성
            sceneBox = new PictureBox();
            sceneBox.Dock = DockStyle.Fill;
            sceneBox.SizeMode = PictureBoxSizeMode.StretchImage; // 자동 크기 맞춤!
            sceneBox.Click += OnSceneClick;
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
            this.Click += OnSceneClick;
        }

        // 5. 클릭할 때마다 다음 컷신 보여주기
        private void OnSceneClick(object sender, EventArgs e)
        {
            currentSceneIndex++;

            if (currentSceneIndex < scenes.Count)
            {
                sceneBox.Image = scenes[currentSceneIndex];
            }
            else
            {
                this.Hide(); // 컷신 창 숨기기
                Difficulty difficulty = new Difficulty(); // 난이도 선택 창 띄우기
                difficulty.StartPosition = FormStartPosition.CenterScreen;
                difficulty.Show();
            }
        }

        private void CutScene_Load(object sender, EventArgs e) { }
    }
}
