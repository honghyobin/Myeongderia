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

        private void startButton_Click(object sender, EventArgs e)
        {
            //�����ϱ� ��ư Ŭ���� �ƾ� ȭ������ �̵�
            CutScene cutscene= new CutScene();
            //�������� ȭ�� �߾ӿ� ��ġ
            cutscene.StartPosition = FormStartPosition.CenterScreen;
            cutscene.Show();
            this.Hide();
        }
    }
}
