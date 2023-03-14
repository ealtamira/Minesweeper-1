namespace Minesweeper
{
    public partial class Form1 : Form
    {
        public Image flag = Image.FromFile(@"../../../Images/flag.png");
        public Image unclicked = Image.FromFile(@"../../../Images/unclicked.jpg");
        public Image[] images = new Image[]
        {
            Image.FromFile(@"../../../Images/0.jpg"),
            Image.FromFile(@"../../../Images/1.jpg"),
            Image.FromFile(@"../../../Images/2.jpg"),
            Image.FromFile(@"../../../Images/3.jpg"),
            Image.FromFile(@"../../../Images/4.jpg"),
            Image.FromFile(@"../../../Images/5.jpg"),
            Image.FromFile(@"../../../Images/6.jpg"),
            Image.FromFile(@"../../../Images/7.jpg"),
            Image.FromFile(@"../../../Images/8.jpg"),
            Image.FromFile(@"../../../Images/mine.jpg")
        };
        public Form1()
        {
            InitializeComponent();
            Width = 900;
            Height = 900;
            PictureBox1Load();
            Button1Load();
            Label1Load();
            StartPosition = FormStartPosition.CenterScreen;
        }
        void PictureBox1Load()
        {
            pictureBox1.Height = Height - 200;
            pictureBox1.Width = Width - 200;
            pictureBox1.Location = new Point((Height - pictureBox1.Height) / 2, (Width - pictureBox1.Width) / 2);
        }
        void Button1Load()
        {
            button1.Text = "RESET";
            button1.Height = 50;
            button1.Width = 100;
            button1.Location = new Point((Width - button1.Width) / 2, ((pictureBox1.Location.Y) / 2) - button1.Height / 2);
        }
        void Label1Load()
        {
            label1.Text = "000";
            label1.BackColor = Color.Black;
            label1.Height = 80;
            label1.Width = 150;
            label1.AutoSize = false;
            label1.ForeColor = Color.Red;
            label1.Font = new Font("Arial", 30);
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Location = new Point((Width - button1.Location.X) / 2 - label1.Width + 5 , ((pictureBox1.Location.Y) / 2) - label1.Height / 2);

        }
        public void LabelUpdate()
        {
            label1.Text = (Engine.mines - Engine.flagsplaced).ToString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Engine.Init(this, 10);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Controls.Clear();
            Engine.Reset();
        }
    }
}