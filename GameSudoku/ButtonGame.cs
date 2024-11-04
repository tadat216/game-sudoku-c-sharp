using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace GameSudoku
{
    internal class ButtonGame : Button
    {
        Color backColorHover = ColorTranslator.FromHtml("#DCE3ED");
        Color backColorDefaut = ColorTranslator.FromHtml("#EAEEF4");
        Color textColor = ColorTranslator.FromHtml("#0072E3");
        
        public ButtonGame()
        {
            this.BackColor = backColorDefaut;
            this.ForeColor = textColor;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
        }
        void ButtonGame_MouseHover(object sender, MouseEventArgs e)
        {
            this.BackColor = backColorHover;
        }
    }
}
