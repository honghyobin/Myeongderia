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

        // 카운트다운 타이머 관련 필드
        private System.Windows.Forms.Timer countdownTimer;
        private int remainingSeconds = 180; //타이머 시간
        private Label timerLabel;

        public GameForm()
        {
            InitializeComponent();
            this.Size = new Size(960, 640);

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

            // 타이머 초기화
            SetupTimer();
        }

        private void SetupTimer() //타이머 디자인
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

            orderLabel.Text = $"주문: {string.Join(" → ", currentOrder)}";
            recipePictureBox.Image = (Image)Properties.Resources.ResourceManager.GetObject($"Recipe{index + 1}");

            int personIndex = rand.Next(1, 8);
            customerPictureBox.Image = (Image)Properties.Resources.ResourceManager.GetObject($"Person{personIndex}");

            userIngredients.Clear();
            popup?.ClearImages();
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

        private void AddIngredient(string name, Image img)
        {
            ShowPopupWithImage(img);
            userIngredients.Add(name);
        }

        private void ToggleBread()
        {
            string key = "Bread";
            if (!imageToggleStates.ContainsKey(key))
                imageToggleStates[key] = false;

            string filePath = imageToggleStates[key] ? @"Resources\\Bread1.png" : @"Resources\\Bread.png";

            if (!File.Exists(filePath))
            {
                MessageBox.Show($"이미지 파일이 존재하지 않아요: {filePath}");
                return;
            }

            Image imgToShow = Image.FromFile(filePath);
            ShowPopupWithImage(imgToShow);
            userIngredients.Add("Bread");
            imageToggleStates[key] = !imageToggleStates[key];
        }

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
                    MessageBox.Show($"순서가 다릅니다!\n{i + 1}번째 재료:\n[요청] {currentOrder[i]}\n[입력] {userIngredients[i]}");
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

                    // 타이머 초기화
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
            else if (day == 2) targetAmount = 50000;
            else if (day == 3) targetAmount = 80000;

            goalLabel.Text = $"{day}일차\n목표 금액: {targetAmount}원\n현재 금액: {currentAmount}원";
        }
    }
}
