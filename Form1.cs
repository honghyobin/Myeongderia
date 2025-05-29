namespace Myeongderia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //페이지를 화면 중앙에 배치
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //시작하기 버튼 클릭시 난이도 화면으로 이동
            Difficulty difficulty = new Difficulty();
            //페이지를 화면 중앙에 배치
            difficulty.StartPosition = FormStartPosition.CenterScreen;
            difficulty.Show();
            this.Hide();
            
        }
    }
}
