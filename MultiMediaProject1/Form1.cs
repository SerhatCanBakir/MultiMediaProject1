using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiMediaProject1
{
    public partial class Form1 : Form
    {
      
        public Form1()
        {
            InitializeComponent();
        }


       

        


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            ofd.Multiselect = false;
            ofd.Title = "Select photo";

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image=Image.FromFile(ofd.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;        
            }
        }


       
        float offsetX = 0;
        float offsetY = 0;
        float zoom = 1f;
       
        private void DrawImageWithOffset()
        {
            if (pictureBox1.Image != null)
            {
                using (Graphics g = pictureBox1.CreateGraphics())
                {
                    g.Clear(pictureBox1.BackColor); 
                    float FotoWidth = pictureBox1.Width*zoom;
                    float FotoHeight = pictureBox1.Height*zoom;
                  
                    float centerX = (pictureBox1.Width - pictureBox1.Image.Width) / 2 + offsetX;
                    float centerY = (pictureBox1.Height - pictureBox1.Image.Height) / 2 + offsetY;
                    g.DrawImage(pictureBox1.Image, centerX, centerY,FotoWidth,FotoHeight);
                }
            }
        }

        private void zoomIn()
        {
            zoom += 0.2f;
            DrawImageWithOffset();
        }

        private void zoomOut()
        {
            zoom -= 0.2f;
            DrawImageWithOffset();
        }


      
        private void ShiftCenter(float x, float y)
        {
            offsetX += x;
            offsetY += y;
            DrawImageWithOffset(); 
        }



        private void button3_Click(object sender, EventArgs e)
        {
            ShiftCenter(+10, 0);  
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShiftCenter(-10,0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            pictureBox1.Refresh();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShiftCenter(0, 10);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ShiftCenter(0, -10);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            zoomOut();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            zoomIn();
        }
        bool IsPressed = false;
        private Point startPoint = new Point(0,0);
        private Point CHange = new Point(0,0);
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                    IsPressed = true;
                startPoint = e.Location;
                Cursor=Cursors.Hand;

            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsPressed)
            {
                float Dx = e.X - startPoint.X;
                float Dy = e.Y - startPoint.Y;
                Dx /= 50;
                Dy /= 50;
                ShiftCenter(Dx, Dy);
                
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            IsPressed = false;
            Cursor = Cursors.Default; 
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Focus(); // Fare PictureBox üzerindeyken odak ver
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

                if(e.Delta > 0)
            {
                zoom += 0.2f;
                DrawImageWithOffset();
            }
                if(e.Delta < 0)
            {
                zoom -= 0.2f;
                DrawImageWithOffset();
            }
        }

    }
}
