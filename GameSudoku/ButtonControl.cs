using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace GameSudoku
{
    internal class ButtonControl : ButtonGame
    {
        Label lab = new Label();
        public ButtonControl()
        {
            this.Width = 60;
            this.Height = this.Width;
            this.BackgroundImageLayout = ImageLayout.Center;
            this.lab.Location = new Point(this.Width / 2, this.Height + 10);
            this.lab.Font = new Font("Arial", 10);
            GraphicsPath p = new GraphicsPath();
            p.AddEllipse(1, 1, this.Width - 1, this.Height - 1); //Làm nút trở thành hình tròn
            this.Region = new Region(p); 
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, 60, 60);
        }
        public void Set(System.Drawing.Image img, int x, int y)
        {
            var resizedImage = new Bitmap(30, 30);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.DrawImage(img, 0, 0, resizedImage.Width, resizedImage.Height);
            }
            this.BackgroundImage = resizedImage;
            this.Location = new Point(x, y);
            this.lab.Text = "Text";
        }
    }
}
