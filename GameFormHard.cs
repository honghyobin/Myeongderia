using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Myeongderia
{
    public partial class GameFormHard : Form
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
        //효빈:하드모드 문장 주문 목록
        private List<string> orderSentences = new List<string> 
        {
            "고기만 끼운 기본 버거 부탁해!",
            "그 노란 거만 넣어줘.",
            "풀 위에 빨간 거, 그리고 빵.",
            "고기 위에 치즈 얹어서 빵으로 덮어줘.",
            "눈물 나는 채소랑 잎사귀만 넣은 거 줘.",
            "고기, 빨간 거, 노란 거, 순서대로 넣어줘!",
            "잎사귀에 치즈, 그 위에 토마토 부탁해~",
            "고기 두 장! 치즈도 꼭 넣어줘!",
            "빨간 거에 노란 거, 고기 얹고 마무리해줘.",
            "풀 - 고기 - 치즈 순서로 부탁해~",
            "치즈랑 풀, 그 위에 고기, 양파도 살짝~",
            "고기 치즈 양파 토마토, 다 넣어줘!",
            "풀에 토마토, 그리고 고기랑 치즈!",
            "치즈 → 고기 → 치즈 더! 양파도 얹어줘~",
            "양파, 고기, 토마토, 치즈. 딱 그 순서야.",
            "고기에 토마토랑 치즈, 풀도 얹어서 줘.",
            "풀 → 토마토 → 양파 → 고기 → 치즈!",
            "치즈 → 양파 → 고기 → 풀 → 토마토~",
            "고기 두 장에 치즈, 풀, 양파까지 넣어줘!",
            "풀, 치즈, 양파, 토마토, 그리고 마지막엔 고기!"
        };

        private Dictionary<string, int> ingredientPrices = new Dictionary<string, int>
        {   //효빈:재료 가격 목록
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

        //카운트 다운 관련
        private System.Windows.Forms.Timer countdownTimer;
        private int remainingSeconds = 180; 
        private Label timerLabel;

        //  효과음 재생기
        private SoundPlayer effectPlayer;
        private SoundPlayer failPlayer; // 실패 효과음 플레이어 추가



        //효빈:게임 초기설정
        public GameFormHard()
        {
            InitializeComponent();
            this.Size = new Size(960, 640);//효빈:화면의 크기 설정
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
    

            recipePictureBox.Visible = false; //효빈:하드모드에서 이미지 레시피 안 보임
            //효빈:손님 이미지박스 생성하고 위치, 사이즈, 투명배경 설정
            customerPictureBox = new PictureBox
            {
                Location = new Point(420, 80),
                Size = new Size(300, 300),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };
            Controls.Add(customerPictureBox);
            customerPictureBox.BringToFront();
            //효빈:말풍선, 주문라벨 앞으로 위치
            balloonPictureBox.BringToFront();
            orderLabel.BringToFront();
            //효빈: 레시피 목록 초기화, 첫 주문 설정, 목표금액 초기화, 재료 패널 설정
            InitOrders();
            SetRandomOrder();
            UpdateGoalLabel();
            SetupIngredientPanels();

            // 현우: 타이머 리셋
            SetupTimer();
        }

        //현우: 타이머 디자인 및 기능
        private void SetupTimer()
        {
            timerLabel = new Label();
            timerLabel.AutoSize = true;
            timerLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            timerLabel.ForeColor = Color.White;
            timerLabel.BackColor = Color.Transparent;
            timerLabel.Location = new Point(20, 20); // 좌측 상단
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

        //효빈:재료 패널을 버튼처럼 사용
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
            Panel panel = new Panel
            {
                Location = location,
                Size = size,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };

            Label transparentLabel = new Label
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            transparentLabel.Click += clickHandler;
            panel.Controls.Add(transparentLabel);

            Controls.Add(panel);
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
            int index = rand.Next(allOrders.Count);//효빈: 전체 주문 중 랜덤으로 선택
            currentOrder = allOrders[index];//효빈:선택한 주문을 현재 주문에 넣기

            orderLabel.Text = $"“{orderSentences[index]}”";//효빈:인데스의 문장을 주문 라벨에 출력

            int personIndex = rand.Next(1, 8);//효빈:손님 이미지 랜덤 선택
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

            userIngredients.Clear();//효빈:재료 초기화
            popup?.ClearImages();//효빈:팝업창에 생긴 이미지 초기화
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
            effectPlayer?.Play();
            ShowPopupWithImage(img);//효빈:이미지 팝업에 추가
            userIngredients.Add(name);//효빈:재료 목록에 추가
        }
        //효빈: 빵 추가할 때 빵의 윗면과 아랫면을 구별하기 위한 함수



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
            //효빈:목표 달성 시 다음 날로 진행, 3일차가 끝나면 게임 종료
            if (currentAmount >= targetAmount)
            {
                if (day < 3)
                {
                    MessageBox.Show($"축하합니다! {day}일차 목표 달성! {day + 1}일차 시작!");
                    day++;
                    currentAmount = 0;
                    remainingSeconds = 180;
                    timerLabel.Text = FormatTime(remainingSeconds);
                    countdownTimer.Start();
                }
                else
                {
                    countdownTimer.Stop();
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
