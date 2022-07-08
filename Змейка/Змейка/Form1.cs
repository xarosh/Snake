using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Змейка
{
    public partial class Form1 : Form
    {
        private int rI, rJ,sl,ry;
        private PictureBox fruit;
        private PictureBox[] snake = new PictureBox[400];
        private Label labelScore;
        private Label labelHScore;
      //  private Label controls;
        private int dirX, dirY;
        private int _width = 900;
        private int _height = 800;
        private int _sizeOfSides = 40;
        private int hscore = 0;
        private int score=0;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Snake";
            this.Width = _width;
            this.Height = _height;
        }
        private void Start()
        {
            dirX = 1;
            dirY = 0;
            labelScore = new Label();
            labelScore.Text = "Score: 0";
            labelScore.Location = new Point(810, 10);
            labelHScore = new Label();
            labelHScore.Text = "HighScore: 0";
            labelHScore.Location = new Point(805, 40);
          //  lab = new Label();
            this.Controls.Add(labelHScore);
            this.Controls.Add(labelScore);
            snake[0] = new PictureBox();
            snake[0].Location = new Point(201, 201);
            snake[0].Size = new Size(_sizeOfSides - 1, _sizeOfSides - 1);

            var golova = new Bitmap(Змейка.Properties.Resources.golova);
            snake[0].Image = golova;
           // snake[0].BackColor = Color.Blue;
            this.Controls.Add(snake[0]);
            fruit = new PictureBox();
            fruit.Size = new Size(_sizeOfSides, _sizeOfSides);
            _generateMap();
            _generateFruit();
            timer.Tick += new EventHandler(_update);
            timer.Interval = sl;
            timer.Start();
            this.KeyDown += new KeyEventHandler(OKP);
        }
        private void _generateFruit()
        {
            Random ran = new Random();
            ry = ran.Next(1, 4);
            var strawberry = new Bitmap(Змейка.Properties.Resources.strawberry);
            var vishna = new Bitmap(Змейка.Properties.Resources.vishna);
            var grusha = new Bitmap(Змейка.Properties.Resources.grusha);
            if (ry == 1)
            {
                fruit.Image = grusha;
            }
            if (ry == 2)
            {
                fruit.Image = strawberry;
            }
            if (ry == 3)
            {
                fruit.Image = vishna;
            }
            Random r = new Random();
            rI = r.Next(0, _height - _sizeOfSides);
            int tempI = rI % _sizeOfSides;
            rI -= tempI;
            rJ = r.Next(0, _height - _sizeOfSides);
            int tempJ = rJ % _sizeOfSides;
            rJ -= tempJ;
            rI++;
            rJ++;
            fruit.Location = new Point(rI, rJ);
            this.Controls.Add(fruit);
        }
        private void _checkBorders()
        {
            if (snake[0].Location.X < 0)
            {
                for(int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirX = 1;
            }
            if (snake[0].Location.X > _height)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirX = -1;
            }
            if (snake[0].Location.Y < 0)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirY = 1;
            }
            if (snake[0].Location.Y > _height-40)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                labelScore.Text = "Score: " + score;
                dirY = -1;
            }
        }
        private void _eatItself()
        {
            for(int _i = 1; _i < score; _i++)
            {
                if (snake[0].Location == snake[_i].Location)
                {
                    for (int _j = _i; _j <= score; _j++)
                        this.Controls.Remove(snake[_j]);
                    score = score - (score - _i + 1);
                    

                    
                }
            }
        }
        
        private void _eatFruit()
        {
            if (snake[0].Location.X ==rI&& snake[0].Location.Y == rJ)
            {
                labelScore.Text = "Score: " + ++score;
                snake[score] = new PictureBox();
                snake[score].Location = new Point(snake[score-1].Location.X+40*dirX, snake[score - 1].Location.Y - 40 * dirY);
                snake[score].Size = new Size(_sizeOfSides-1, _sizeOfSides-1);
                var telo = new Bitmap(Змейка.Properties.Resources.telo);
                snake[score].Image = telo;
              //  snake[score].BackColor = Color.Blue;
                this.Controls.Add(snake[score]);
                _generateFruit();
            }
        }

        private void _generateMap()
        {
            for(int i = 0; i < _width / _sizeOfSides-2; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(0, _sizeOfSides * i);
                pic.Size = new Size(_width - 100, 1);
                this.Controls.Add(pic);
            }
            for (int i = 0; i <= _height / _sizeOfSides; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(_sizeOfSides * i,0);
                pic.Size = new Size(1,_width-140);
                this.Controls.Add(pic);
            }
        }

        private void _moveSnake()
        {
            for(int i = score; i >= 1; i--)
            {
                snake[i].Location = snake[i - 1].Location;
            }
            snake[0].Location = new Point(snake[0].Location.X + dirX * (_sizeOfSides), snake[0].Location.Y + dirY * (_sizeOfSides));
            _eatItself();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            groupBox.Visible = false;
            groupBox.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;
            radioButton4.Enabled = false;
            button1.Enabled = false;
            button1.Visible = false;
            Start();
            label1.Visible = false;
            label1.Enabled = false;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            sl = 300;
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            sl = 170;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox.Visible = true;
            groupBox.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            radioButton3.Enabled = true;
            radioButton4.Enabled = true;
            button1.Visible = true;
            button1.Enabled = true;
            button2.Visible = false;
            button2.Enabled = false;
            button3.Visible = false;
            button3.Enabled = false;
            // snake[0].Dispose();
            //   fruit.Dispose();
            dirX = 0;
            dirY = 0;
            this.Controls.Remove(fruit);
            this.Controls.Remove(labelHScore);
            this.Controls.Remove(labelScore);
            this.Controls.Remove(snake[0]);
            // this.Controls.Remove(snake[]);
            for (int _i = 1; _i <= score; _i++)
            {
                this.Controls.Remove(snake[_i]);
            }
           // this.Controls.Remove(snake[_100]);
            score = 0;
            hscore = 0;
            timer.Tick -= new EventHandler(_update);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button2.Enabled = false;
            timer.Start();
           // st = 0;
            button3.Visible = false;
            button3.Enabled = false;
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            sl = 70;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            sl = 500;
        }

        private void _update(Object myObject,EventArgs eventArgs)
        {
            if(score>= hscore)
            {
                labelHScore.Text = "HighScore: "+ hscore.ToString();
                hscore = score;
            }
            if (dirY == -1)
            {
                var golovav = new Bitmap(Змейка.Properties.Resources.golovav);
                snake[0].Image = golovav;
            }
            if (dirY == 1)
            {
                var golova = new Bitmap(Змейка.Properties.Resources.golova);
                snake[0].Image = golova;
            }
            if (dirX == -1)
            {
                var goloval = new Bitmap(Змейка.Properties.Resources.goloval);
                snake[0].Image = goloval;
            }
            if (dirX == 1)
            {
                var golovap = new Bitmap(Змейка.Properties.Resources.golovap);
                snake[0].Image = golovap;
            }
            _checkBorders();
            _eatFruit();
            _moveSnake();
        }

        private void OKP(object sender,KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    dirX = 1;
                    dirY = 0;
                    break;
                case "Left":
                    dirX = -1;
                    dirY = 0;
                    break;
                case "Up":
                    dirY = -1;
                    dirX = 0;
                    break;
                case "Down":
                    dirY = 1;
                    dirX = 0;
                    break;
                case "Space":
                        button2.Visible = true;
                        button2.Enabled = true;
                        timer.Stop();
                        button3.Visible = true;
                        button3.Enabled = true;
                    
                    break;
            }
        }

    }
}
