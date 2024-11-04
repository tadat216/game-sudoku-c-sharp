using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameSudoku
{
    public partial class Form1 : Form
    {
        GamePlay gamePlay;
        public Form1()
        {
            InitializeComponent();
            InitialLizateWinGame();
            LevelSelect();
        }
        Button[] level = new Button[4];
        int amountRemove;
        Panel panelLevelSelect = new Panel();
        void LevelSelect()
        {
            this.Controls.Add(panelLevelSelect);
            panelLevelSelect.Width = this.Width;
            panelLevelSelect.Height = this.Height;
            panelLevelSelect.BringToFront();
            string[] s = { "Easy", "Medium", "Hard", "Expert" };
            Color[] clr = { Color.Green, Color.Orange, Color.Red, Color.Purple };
            
            int y = 150;
            for (int i = 0; i < 4; ++i)
            {
                level[i] = new Button();
                panelLevelSelect.Controls.Add(level[i]);
                level[i].Size = new Size(200, 50);
                //level[i].BringToFront();
                level[i].Font = new Font("Arial Rounded MT", 20);
                level[i].Location = new Point(this.Width/2-100, y); y += 55;
                level[i].Click += new EventHandler(Click_level);
                level[i].Text = s[i];
                level[i].FlatStyle = FlatStyle.Flat;
                level[i].FlatAppearance.BorderColor = clr[i];
                level[i].FlatAppearance.BorderSize = 2;
            }
        }
        void Click_level(object sender, EventArgs e)
        {
            string str = ((Button)(sender)).Text;
            if (str[str.Length - 1] == 'y') amountRemove = 20;
            if (str[str.Length - 1] == 'm') amountRemove = 30;
            if (str[str.Length - 1] == 'd') amountRemove = 40;
            if (str[str.Length - 1] == 't') amountRemove = 50;
            LoadGame(amountRemove);
        }

        void LoadGame(int amountRemove)
        {
            
            gamePlay = new GamePlay(this);
            panelLevelSelect.Visible = false;
            panelWin.Visible = false;
            gamePlay.DisplayNumberButton(panelNumberButton);
            gamePlay.InitalizateSudokuCells(panelSudokuBoard, amountRemove);
            gamePlay.DisplayButtonOption(panelControlButton);
            DisplayNewGameButton(panelNewGame, 0, 0, panelNewGame.Width, panelNewGame.Height);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            int centerX = Screen.PrimaryScreen.Bounds.Width / 2;
            int centerY = Screen.PrimaryScreen.Bounds.Height / 2;
            this.Location = new Point(centerX - this.Width / 2, centerY - this.Height / 2);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            gamePlay.SetValueCellKeyboard(e.KeyCode);
        }

        private void panelControlButtonUI_Paint(object sender, PaintEventArgs e)
        {

        }

        Color buttonBackColorDefault = ColorTranslator.FromHtml("#EAEEF4");
        Color buttonBackColorHover = ColorTranslator.FromHtml("#DCE3ED");

        private void panelSudokuBoard_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            gamePlay.SetValueCellKeyboard(e.KeyCode);
        }

        void DisplayNewGameButton(Panel panel, int x, int y, int width, int height)
        {
            ButtonGame newGameBtn = new ButtonGame();
            panel.Controls.Add(newGameBtn);
            newGameBtn.Width = width;
            newGameBtn.Height = height;
            newGameBtn.Text = "NEW GAME";
            newGameBtn.Font = new Font("Arial", 20);
            newGameBtn.Click += new EventHandler(newGameBtn_click);
            newGameBtn.Location = new Point(x, y);
        }

        void newGameBtn_click(object sender, EventArgs e) 
        {
            Application.Restart(); //Khi bấm nút NewGame thì reset game mới
        }

        void InitialLizateWinGame()
        {
            panelWin.Visible = false;
            DisplayNewGameButton(panelWin, panelWin.Width / 2 - 100, panelWin.Width / 2 - 60, 200, 60);
            Label winText = new Label();
            winText.Text = "Congratulation, You win!";
            winText.Width = 380;
            winText.Height = 50;
            winText.Font = new Font("Arial", 25);
            winText.Location = new Point(panelWin.Width / 2 - winText.Width / 2, (int)(panelWin.Width * 0.2));
            panelWin.Controls.Add(winText);
            panelWin.BackColor = Color.White;
        }
        public void WinGame()
        {
            panelNumberButton.Visible = false;
            panelControlButton.Visible = false;
            panelNewGame.Visible = false;
            panelWin.BringToFront();
            panelWin.Visible = true;
        }

        private void panelWin_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panelWin.ClientRectangle, Color.DarkBlue, ButtonBorderStyle.Solid);
        }
    }
}
