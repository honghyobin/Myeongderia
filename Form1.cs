namespace Myeongderia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //�������� ȭ�� �߾ӿ� ��ġ
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(960, 640);//ȭ�� ũ�� ����
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //�����ϱ� ��ư Ŭ���� ���̵� ȭ������ �̵�
            Difficulty difficulty = new Difficulty();
            //�������� ȭ�� �߾ӿ� ��ġ
            difficulty.StartPosition = FormStartPosition.CenterScreen;
            difficulty.Show();
            this.Hide();
            
        }
    }
}
