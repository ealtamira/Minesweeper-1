namespace Minesweeper
{
    public partial class Form1 : Form
    {
        public Image flag = Image.FromFile(@"../../../Images/flag.png");
        public Image[] images = new Image[]
        {
            Image.FromFile(@"../../../Images/0.jpg"),
            Image.FromFile(@"../../../Images/1.png"),
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
        }
        void PictureBox1Load()
        {
            pictureBox1.Height = Height - 200;
            pictureBox1.Width = Width - 200;
            pictureBox1.Location = new Point((Height - pictureBox1.Height) / 2, (Width - pictureBox1.Width) / 2);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Engine.Init(this, 10);
        }
    }
}