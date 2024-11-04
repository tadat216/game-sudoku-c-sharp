using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GameSudoku
{
    internal class PanelControlButton : Panel
    {
        Label lab = new Label();
        public PanelControlButton(int witdh, int height, string Text, int xLocation)
        {
            this.Width = witdh;
            this.Height = height;
            this.Controls.Add(lab);
            this.lab.Text = Text;
            this.lab.Font = new Font("Arial", 12);
            this.lab.Width = witdh;
            this.lab.ForeColor = ColorTranslator.FromHtml("#0072E3");
            this.lab.TextAlign = ContentAlignment.MiddleCenter;
            this.lab.Location = new Point(0, (int)(0.7f*height));
            this.Location = new Point(xLocation, 0);
        }
    }
}
