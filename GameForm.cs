using System.Media;

namespace Myeongderia
{
    public partial class GameForm : Form
    {
        private bool isPopupOpen = false;
        private PopupForm popup;
        private Dictionary<string, bool> imageToggleStates = new Dictionary<string, bool>();
        private List<List<string>> allOrders = new List<List<string>>();
        private List<string> currentOrder = new List<string>();
        private List<string> userIngredients = new List<string>();

        private Dictionary<string, int> ingredientPrices = new Dictionary<string, int>
        {
            { "Bread", 600 },
            { "Lettuce", 700 },
            { "Patty", 1500 },
            { "Tomato", 1000 },
            { "Cheese", 500 },
            { "Onion", 1000 }
        };

        private int day = 1;
        private int targetAmount = 30000;
        private int currentAmount = 0;

        private System.Windows.Forms.Timer countdownTimer;
        private int remainingSeconds = 180;
        private Label timerLabel;

        //  효과음 재생기
        private SoundPlayer effectPlayer;
        private SoundPlayer failPlayer; // 실패 효과음 플레이어 추가


        public GameForm()
        {
            InitializeComponent();
            this.Size = new Size(960, 640);

            // 효과음 초기화 (리소스 사용)
            try
            {
                effectPlayer = new SoundPlayer(Properties.Resources.ding);
                effectPlayer.Load();

                failPlayer = new SoundPlayer(Properties.Resources.fail); // 리소스에서 fail.wav 로드
                failPlayer.Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show("효과음 로드 실패: " + ex.Message);
            }



            recipePictureBox.BackColor = Color.Transparent;
            recipePictureBox.Parent = this;
            recipePictureBox.BringToFront();
            recipePictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            customerPictureBox = new PictureBox();
            customerPictureBox.Location = new Point(420, 80);
            customerPictureBox.Size = new Size(300, 300);
            customerPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            customerPictureBox.BackColor = Color.Transparent;
            Controls.Add(customerPictureBox);
            customerPictureBox.BringToFront();

            InitOrders();
            SetRandomOrder();
            UpdateGoalLabel();
            SetupIngredientPanels();

            orderLabel.BringToFront();

            SetupTimer();
        }

        private void SetupTimer()
        {
            timerLabel = new Label();
            timerLabel.AutoSize = true;
            timerLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            timerLabel.ForeColor = Color.White;
            timerLabel.BackColor = Color.Transparent;
            timerLabel.Location = new Point(20, 20);
            timerLabel.Text = FormatTime(remainingSeconds);
            timerLabel.BringToFront();
            this.Controls.Add(timerLabel);

            countdownTimer = new System.Windows.Forms.Timer();
            countdownTimer.Interval = 1000;
            countdownTimer.Tick += CountdownTimer_Tick;
            countdownTimer.Start();
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            remainingSeconds--;
            timerLabel.Text = FormatTime(remainingSeconds);

            if (remainingSeconds <= 0)
            {
                countdownTimer.Stop();
                failPlayer?.Play(); // 실패 음향 재생
                MessageBox.Show("Game Over");

                Form1 mainForm = new Form1();
                mainForm.Show();
                this.Close();
            }
        }

        private string FormatTime(int totalSeconds)
        {
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            return $"{minutes:D2}:{seconds:D2}";
        }

        private void SetupIngredientPanels()
        {
            panelBread = CreateIngredientPanel(new Point(122, 431), new Size(124, 78), (s, e) => ToggleBread());
            panelPatty = CreateIngredientPanel(new Point(262, 430), new Size(124, 78), (s, e) => AddIngredient("Patty", Properties.Resources.Patty));
            panelOnion = CreateIngredientPanel(new Point(407, 433), new Size(59, 75), (s, e) => AddIngredient("Onion", Properties.Resources.Onion));
            panelCheese = CreateIngredientPanel(new Point(472, 433), new Size(59, 75), (s, e) => AddIngredient("Cheese", Properties.Resources.Cheese));
            panelTomato = CreateIngredientPanel(new Point(551, 433), new Size(79, 72), (s, e) => AddIngredient("Tomato", Properties.Resources.Tomato));
            panelLettuce = CreateIngredientPanel(new Point(640, 430), new Size(83, 75), (s, e) => AddIngredient("Lettuce", Properties.Resources.Lettuce));
        }

        private Panel CreateIngredientPanel(Point location, Size size, EventHandler clickHandler)
        {
            Panel panel = new Panel();
            panel.Location = location;
            panel.Size = size;
            panel.BackColor = Color.Transparent;
            panel.Cursor = Cursors.Hand;

            Label transparentLabel = new Label
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            transparentLabel.Click += clickHandler;
            panel.Controls.Add(transparentLabel);

            this.Controls.Add(panel);
            panel.BringToFront();
            return panel;
        }

        private void InitOrders()
        {
            allOrders = new List<List<string>>
            {
                new List<string> { "Bread", "Patty", "Bread" },
                new List<string> { "Bread", "Cheese", "Bread" },
                new List<string> { "Bread", "Lettuce", "Tomato", "Bread" },
                new List<string> { "Bread", "Patty", "Cheese", "Bread" },
                new List<string> { "Bread", "Onion", "Lettuce", "Bread" },
                new List<string> { "Bread", "Patty", "Tomato", "Cheese", "Bread" },
                new List<string> { "Bread", "Lettuce", "Cheese", "Tomato", "Bread" },
                new List<string> { "Bread", "Patty", "Patty", "Cheese", "Bread" },
                new List<string> { "Bread", "Tomato", "Cheese", "Patty", "Bread" },
                new List<string> { "Bread", "Lettuce", "Patty", "Cheese", "Bread" },
                new List<string> { "Bread", "Cheese", "Lettuce", "Patty", "Onion", "Bread" },
                new List<string> { "Bread", "Patty", "Cheese", "Onion", "Tomato", "Bread" },
                new List<string> { "Bread", "Lettuce", "Tomato", "Patty", "Cheese", "Bread" },
                new List<string> { "Bread", "Cheese", "Patty", "Cheese", "Onion", "Bread" },
                new List<string> { "Bread", "Onion", "Patty", "Tomato", "Cheese", "Bread" },
                new List<string> { "Bread", "Patty", "Tomato", "Cheese", "Lettuce", "Bread" },
                new List<string> { "Bread", "Lettuce", "Tomato", "Onion", "Patty", "Cheese", "Bread" },
                new List<string> { "Bread", "Cheese", "Onion", "Patty", "Lettuce", "Tomato", "Bread" },
                new List<string> { "Bread", "Patty", "Patty", "Cheese", "Lettuce", "Onion", "Bread" },
                new List<string> { "Bread", "Lettuce", "Cheese", "Onion", "Tomato", "Patty", "Bread" }
            };
        }

        private void SetRandomOrder()
        {
            Random rand = new Random();
            int index = rand.Next(allOrders.Count);
            currentOrder = allOrders[index];
            orderLabel.Text = $"{string.Join(" → ", currentOrder)}";
            recipePictureBox.Image = (Image)Properties.Resources.ResourceManager.GetObject($"Recipe{index + 1}");

            int personIndex = rand.Next(1, 8);
            customerPictureBox.Image = (Image)Properties.Resources.ResourceManager.GetObject($"Person{personIndex}");

            if (personIndex == 3 || personIndex == 4)
            {
                balloonPictureBox.Location = new Point(balloonPictureBox.Location.X, 190);
                orderLabel.Location = new Point(orderLabel.Location.X, 220);
            }
            else
            {
                balloonPictureBox.Location = new Point(balloonPictureBox.Location.X, 92);
                orderLabel.Location = new Point(orderLabel.Location.X, 120);
            }

            userIngredients.Clear();
            popup?.ClearImages();
            balloonPictureBox.BringToFront();
            orderLabel.BringToFront();
        }

        private void ShowPopupWithImage(Image img)
        {
            if (!isPopupOpen)
            {
                popup = new PopupForm();
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.OnComplete = CheckBurger;
                popup.FormClosed += (s, e) => { isPopupOpen = false; };
                isPopupOpen = true;
                popup.Show();
            }
            popup.AddImage(img);
        }

        //  효과음 즉시 재생
        private void AddIngredient(string name, Image img)
        {
            effectPlayer?.Play();
            ShowPopupWithImage(img);
            userIngredients.Add(name);
        }

        private void ToggleBread()
        {
            string key = "Bread";
            if (!imageToggleStates.ContainsKey(key))
                imageToggleStates[key] = false;

            // 이미지 리소스에서 선택
            Image imgToShow = imageToggleStates[key]
                ? Properties.Resources.Bread1
                : Properties.Resources.Bread;

            effectPlayer?.Play();
            ShowPopupWithImage(imgToShow);
            userIngredients.Add("Bread");

            imageToggleStates[key] = !imageToggleStates[key];
        }

        //효빈: 플레이어가 만든 버거 레시피와 비교하는 함수

        private void CheckBurger()
        {
            if (userIngredients.Count != currentOrder.Count)
            {
                MessageBox.Show("재료 개수가 다릅니다!");
                SetRandomOrder();
                return;
            }

            for (int i = 0; i < currentOrder.Count; i++)
            {
                if (userIngredients[i] != currentOrder[i])
                {
                    MessageBox.Show($"순서가 다릅니다!");
                    SetRandomOrder();
                    return;
                }
            }

            int earned = 0;
            foreach (var item in userIngredients)
            {
                if (ingredientPrices.ContainsKey(item))
                    earned += ingredientPrices[item];
            }
            currentAmount += earned;

            MessageBox.Show($"정답! 수익: +{earned}원");

            if (currentAmount >= targetAmount)
            {
                if (day < 3)
                {
                    MessageBox.Show($"축하합니다! {day}일차 목표 달성! {day + 1}일차 시작!");
                    day++;
                    currentAmount = 0;

                    remainingSeconds = 180;
                    timerLabel.Text = FormatTime(remainingSeconds);
                    countdownTimer.Stop();
                    countdownTimer.Start();
                }
                else
                {
                    MessageBox.Show("축하합니다! 3일차까지 모두 완료했습니다!");
                    this.Close();
                }
            }

            UpdateGoalLabel();
            SetRandomOrder();
        }

        private void UpdateGoalLabel()
        {
            if (day == 1) targetAmount = 30000;
            else if (day == 2) targetAmount = 40000;
            else if (day == 3) targetAmount = 60000;

            goalLabel.Text = $"{day}일차\n목표 금액: {targetAmount}원\n현재 금액: {currentAmount}원";
        }
    }
}
