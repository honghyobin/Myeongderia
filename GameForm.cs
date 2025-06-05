namespace Myeongderia
{
    public partial class GameForm : Form
    {
        //효빈:팝업창이 열려 있는지 확인하기 위한 변수
        private bool isPopupOpen = false;
        private PopupForm popup;
        //효빈:빵의 윗면과 아랫면을 번갈아 추가할때 사용
        private Dictionary<string, bool> imageToggleStates = new Dictionary<string, bool>();
        //효빈:모든 레시피 리스트
        private List<List<string>> allOrders = new List<List<string>>();
        //효빈: 현재 주문된 레시피 리스트
        private List<string> currentOrder = new List<string>();
        //효빈:플레이어가 현재 선택한 재료 리스트
        private List<string> userIngredients = new List<string>();

        private Dictionary<string, int> ingredientPrices = new Dictionary<string, int>
        {   //효빈:재료 가격 목록 리스트
            { "Bread", 600 },
            { "Lettuce", 700 },
            { "Patty", 1500 },
            { "Tomato", 1000 },
            { "Cheese", 500 },
            { "Onion", 1000 }
        };
        //효빈:초기 게임 날짜, 목표 금액, 현재 금액
        private int day = 1;
        private int targetAmount = 30000;
        private int currentAmount = 0;

        // 현우:카운트 다운 관련
        private System.Windows.Forms.Timer countdownTimer;
        private int remainingSeconds = 180;
        private Label timerLabel;

        //효빈:게임 초기설정
        public GameForm()
        {
            InitializeComponent();
            this.Size = new Size(960, 640);//효빈:화면의 크기 설정
            //효빈:레시피 이미지박스 생성하고 위치, 투명배경 설정
            recipePictureBox.BackColor = Color.Transparent;
            recipePictureBox.Parent = this;
            recipePictureBox.BringToFront();
            recipePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            //효빈:손님 이미지박스 생성하고 위치, 사이즈, 투명배경 설정
            customerPictureBox = new PictureBox();
            customerPictureBox.Location = new Point(420, 80);
            customerPictureBox.Size = new Size(300, 300);
            customerPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            customerPictureBox.BackColor = Color.Transparent;
            Controls.Add(customerPictureBox);
            customerPictureBox.BringToFront();
            //효빈:레시피 목록 초기화,랜덤 주문설정, 목표 금액 초기화, 재료 패널 설정
            InitOrders();
            SetRandomOrder();
            UpdateGoalLabel();
            SetupIngredientPanels();
            //효빈:주문라벨이 앞으로 위치하도록 설정
            orderLabel.BringToFront();

            // 현우: 타이머 초기화
            SetupTimer();
        }

        private void SetupTimer() //현우: 타이머 설정
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

        // 현우: 타이머 이벤트
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
        
        //효빈: 재료 선택 패널을 화면에 위치시키는 함수
        private void SetupIngredientPanels()
        {
            panelBread = CreateIngredientPanel(new Point(122, 431), new Size(124, 78), (s, e) => ToggleBread());
            panelPatty = CreateIngredientPanel(new Point(262, 430), new Size(124, 78), (s, e) => AddIngredient("Patty", Properties.Resources.Patty));
            panelOnion = CreateIngredientPanel(new Point(407, 433), new Size(59, 75), (s, e) => AddIngredient("Onion", Properties.Resources.Onion));
            panelCheese = CreateIngredientPanel(new Point(472, 433), new Size(59, 75), (s, e) => AddIngredient("Cheese", Properties.Resources.Cheese));
            panelTomato = CreateIngredientPanel(new Point(551, 433), new Size(79, 72), (s, e) => AddIngredient("Tomato", Properties.Resources.Tomato));
            panelLettuce = CreateIngredientPanel(new Point(640, 430), new Size(83, 75), (s, e) => AddIngredient("Lettuce", Properties.Resources.Lettuce));
        }
        //효빈:재료 선택 패널을 생성하고 클릭 이벤트를 연결하는 함수
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
        //효빈:전체 주문 레시피
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
        //효빈:랜덤으로 주문 레시피와 문장을 화면에 출력하는 함수
        private void SetRandomOrder()
        {
            Random rand = new Random();
            int index = rand.Next(allOrders.Count);//효빈:전체 레시피 중 무작위 인덱스 선택
            currentOrder = allOrders[index];//효빈:현재 주문으로 저장

            orderLabel.Text = $"{string.Join(" → ", currentOrder)}";//효빈: ㅓ레시피 문자열로 변환
            recipePictureBox.Image = (Image)Properties.Resources.ResourceManager.GetObject($"Recipe{index + 1}");

            int personIndex = rand.Next(1, 8);//효빈:손님 이미지 무작위 선택
            customerPictureBox.Image = (Image)Properties.Resources.ResourceManager.GetObject($"Person{personIndex}");
            //효빈:어린 손님(Person3, Person4)이 출력 되면 말풍선과 주문라벨으 아래로 이동
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
            //플레이어 재료 및 팝업창 초기화
            userIngredients.Clear();
            popup?.ClearImages();
            balloonPictureBox.BringToFront();
            orderLabel.BringToFront();
        }
        //효빈:재료 이미지 클릭 시 팝업창을 띄우는 함수
        private void ShowPopupWithImage(Image img)
        {
            if (!isPopupOpen)
            {
                popup = new PopupForm();
                popup.StartPosition = FormStartPosition.CenterParent;//효빈:중앙에 위치
                popup.OnComplete = CheckBurger;//효빈:완료 시 햄버거 레시피 정답 검사
                popup.FormClosed += (s, e) => { isPopupOpen = false; };//팝업창이 종료되면 상태 변경
                isPopupOpen = true;
                popup.Show();
            }
            popup.AddImage(img);//팝업창에 이미지 추가
        }
        //효빈:재료를 클릭하면 재료명, 이미지 전달 함수
        private void AddIngredient(string name, Image img)
        {
            ShowPopupWithImage(img);//효빈:이미지 팝업에 추가
            userIngredients.Add(name);//효빈:재료 목록에 추가
        }
        //효빈: 빵 추가할 때 빵의 윗면과 아랫면을 구별하기 위한 함수
        private void ToggleBread()
        {
            string key = "Bread";
            if (!imageToggleStates.ContainsKey(key))
                imageToggleStates[key] = false;//효빈:빵 상태 초기
            //효빈:번갈아가며 이미지 출력
            string filePath = imageToggleStates[key] ? @"Resources\\Bread1.png" : @"Resources\\Bread.png";
            Image imgToShow = Image.FromFile(filePath);
            ShowPopupWithImage(imgToShow);
            userIngredients.Add("Bread");
            imageToggleStates[key] = !imageToggleStates[key];//효빈:상태 변경
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
            //효빈:목표 달성 시 다음 날로 진행, 3일차가 끝나면 게임 종료
            if (currentAmount >= targetAmount)
            {
                if (day < 3)
                {
                    MessageBox.Show($"축하합니다! {day}일차 목표 달성! {day + 1}일차 시작!");
                    day++;
                    currentAmount = 0;

                    // 현우 타이머 초기화
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

        //효빈: 현재 게임 일차와 금액을 목표에 맞게 라벨에 표시하는 함수
        private void UpdateGoalLabel()
        {
            if (day == 1) targetAmount = 30000;
            else if (day == 2) targetAmount = 40000;
            else if (day == 3) targetAmount = 60000;

            goalLabel.Text = $"{day}일차\n목표 금액: {targetAmount}원\n현재 금액: {currentAmount}원";
        }


    }
}
