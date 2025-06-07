using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Myeongderia
{
    public partial class Difficulty : Form
    {
        public Difficulty()
        {
            InitializeComponent();
            this.Size = new Size(960, 640);//화면 크기 지정
        }
        private void Difficulty_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            GameForm gameOder = new GameForm();
            gameOder.StartPosition = FormStartPosition.CenterScreen;
            gameOder.Show();
            this.Hide();
        }

        private void hardButton_Click_1(object sender, EventArgs e)
        {
            GameFormHard gameOderHard = new GameFormHard();
            gameOderHard.StartPosition = FormStartPosition.CenterScreen;
            gameOderHard.Show();
            this.Hide();
        }
    }
}
