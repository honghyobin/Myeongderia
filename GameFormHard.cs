using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Myeongderia
{
    public partial class GameFormHard : Form
    {
        private bool isPopupOpen = false;
        private PopupForm popup;
        private Dictionary<string, bool> imageToggleStates = new Dictionary<string, bool>();

        private List<List<string>> allOrders = new List<List<string>>();
        private List<string> currentOrder = new List<string>();
        private List<string> userIngredients = new List<string>();

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

        public GameFormHard()
        {
            InitializeComponent();
            this.Size = new Size(960, 640);

            recipePictureBox.Visible = false;

            // 손님 박스 셋업
            customerPictureBox = new PictureBox
            {
                Location = new Point(420, 80),
                Size = new Size(300, 300),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };
            Controls.Add(customerPictureBox);
            customerPictureBox.BringToFront();

            balloonPictureBox.BringToFront();
            orderLabel.BringToFront();

            InitOrders();
            SetRandomOrder();
            UpdateGoalLabel();
            SetupIngredientPanels();
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

            orderLabel.Text = $"“{orderSentences[index]}”";

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

        private void orderLabel_Click(object sender, EventArgs e) { }

        private void balloonPictureBox_Click(object sender, EventArgs e) { }
    }
}
