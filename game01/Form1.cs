using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game01
{
    public partial class Form1 : Form
    {
        private Point pos;
        private bool dragging;
        private bool lose = false;
        private int coinsNumber = 0;
        public int rrr = 45;

        public Form1()
        {
            InitializeComponent();
            bg1.MouseDown += MouseClickDown;
            bg1.MouseUp += MouseClickUp;
            bg1.MouseMove += MouseClickMove;

            bg4.MouseDown += MouseClickDown;
            bg4.MouseUp += MouseClickUp;
            bg4.MouseMove += MouseClickMove;

            labelLoose.Visible = false;
            btnRestart.Visible = false;

            KeyPreview = true;
        }

        private void MouseClickDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            pos.X = e.X;
            pos.Y = e.Y;
        }
        private void MouseClickUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        private void MouseClickMove(object sender, MouseEventArgs e)
        {
            if(dragging == true)
            {
                Point currentPoint = PointToScreen(new Point(e.X, e.Y));
                this.Location = new Point(currentPoint.X - pos.X, currentPoint.Y - pos.Y + bg1.Top);
            }
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int speed = 5;
            bg1.Top += speed;
            bg4.Top += speed;

            int enemySpeed = 7;
            enemy1.Top += enemySpeed;
            enemy2.Top += enemySpeed;

            coin.Top += speed;

            if (bg1.Top >= 600)
            {
                bg1.Top = 0;
                bg4.Top = -550;
            }

            if (enemy1.Top >= 650)
            {
                enemy1.Top = -100;
                Random rand = new Random();
                enemy1.Left = rand.Next(100, 300);
            }

            if (enemy2.Top >= 650)
            {
                enemy2.Top = -200;
                Random rand = new Random();
                enemy2.Left = rand.Next(300, 600);
            }

            if (coin.Top >= 600)
            {
                coin.Top = -50;
                Random rand = new Random();
                coin.Left = rand.Next(100, 700);
            }

            if(banana.Bounds.IntersectsWith(enemy1.Bounds) || banana.Bounds.IntersectsWith(enemy2.Bounds))
            {
                timer.Enabled = false;
                labelLoose.Visible = true;
                btnRestart.Visible = true;
                lose = true;
            }

            if (banana.Bounds.IntersectsWith(coin.Bounds))
            {
                coinsNumber++;
                labelCoins.Text = "Coins: " + coinsNumber.ToString();
                coin.Top = -50;
                Random rand = new Random();
                coin.Left = rand.Next(100, 600);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (lose) return;
            int speed = 10;
            if(banana.Left>100 && banana.Left < 700)
            {
                if (e.KeyCode == Keys.Left)
                    banana.Left -= 10;
                else if (e.KeyCode == Keys.Right)
                    banana.Left += 10;
            }

        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            enemy1.Top = -130;
            enemy2.Top = -300;
            labelLoose.Visible = false;
            btnRestart.Visible = false;
            timer.Enabled = true;
            lose = false;
            coinsNumber = 0;
            labelCoins.Text = "Coins: 0";
            coin.Top = -500;
        }
    }
}
