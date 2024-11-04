using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameSudoku
{
    internal class ButtonNumber : ButtonGame
    {
        public ButtonNumber() 
        { 
            this.Width = 80;
            this.Height = this.Width;
            this.Font = new Font("Arial", 30);
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Width, 5, 5));
        }
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
        int nLeftRect, // x-coordinate of upper-left corner
        int nTopRect, // y-coordinate of upper-left corner
        int nRightRect, // x-coordinate of lower-right corner
        int nBottomRect, // y-coordinate of lower-right corner
        int nWidthEllipse, // height of ellipse
        int nHeightEllipse // width of ellipse
        );
        public void Set(string Text, int x, int y)
        {
            this.Text = Text;
            this.Location = new Point(x, y);
        }
    }
}
