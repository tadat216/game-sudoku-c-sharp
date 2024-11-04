using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;

namespace GameSudoku
{
    internal class GamePlay
    {
        public Cell[,] cell = new Cell[9, 9];
        public Cell clickingCell;
        bool stop = false;
        bool isEror = false;
        int cntHint = 5;

        Color textColor = ColorTranslator.FromHtml("#0072E3");
        Color fixedNumColor = ColorTranslator.FromHtml("#354962");
        Color adjacentCellsColor = ColorTranslator.FromHtml("#E2EBF3");
        Color clickingCellColor = ColorTranslator.FromHtml("#BBDEFB");
        Color backColorEror = ColorTranslator.FromHtml("#F7CFD6");
        Color sameValueColor = ColorTranslator.FromHtml("#C3D7EA");
        Color textErorColor = ColorTranslator.FromHtml("#E55C6C");
        //Note
        bool noteMode = false;

        private readonly Form1 form1;
        public GamePlay(Form1 form1)
        {
            this.form1 = form1;
        }

        public void InitalizateSudokuCells(Panel panel, int amountRemove) //Khoi tao cac o so trong bang sudoku
        {
            panel.BackColor = fixedNumColor;
            Generator gen = new Generator();
            gen.Main(amountRemove); //Số lượng số được ẩn đi
            int sz = 60;
            for (int r = 0, y = 4; r < 9; r++, y += (r % 3 == 0 ? 64 : 62))
            {
                for(int c = 0, x = 4; c < 9; c++, x += (c % 3 == 0 ? 64 : 62))
                {
                    cell[r, c] = new Cell(gen.hiddenNum[r, c], gen.displayNum[r, c], gen.fixedNum[r, c], r, c, (new Label()));
                    panel.Controls.Add(cell[r, c].label);
                    cell[r, c].label.Size = new Size(sz, sz);
                    cell[r, c].label.TextAlign = ContentAlignment.MiddleCenter;
                    cell[r, c].label.Font = new Font("Arial", 25);
                    if (cell[r, c].display != 0) cell[r, c].label.Text = cell[r, c].display.ToString();
                    if (!cell[r, c].fxed){
                        cell[r, c].noteStr = "";
                        cell[r, c].note = false;
                    }
                    cell[r, c].label.ForeColor = (cell[r, c].fxed ? fixedNumColor : textColor);
                    cell[r, c].label.BackColor = Color.White;
                    cell[r, c].label.Location = new Point(x, y);
                    int x1 = x, y1 = y;
                    cell[r, c].label.BringToFront();
                    CellSetClickEvent(cell[r, c]);
                }
            }
        }

        void CellSetClickEvent(Cell c)
        {
            c.label.Click += new EventHandler(new EventHandler((sender, e) => ClickCell(sender, e, c)));
        }

        void ClickCell(object sender, EventArgs e, Cell c)
        {
            if (stop) return;
            ResetColor();
            clickingCell = c;
            ChangeColorCells(clickingCell);
        }

        void ChangeColorCells(Cell currentCell) //Tô màu các ô
        {
            ResetColor(); //Đưa màu của các cell trở về bình thường
            for(int r = 0; r < 9; r++)
            {
                for(int c = 0; c < 9; c++)
                {
                    if (cell[r, c].row == currentCell.row || cell[r, c].col == currentCell.col || cell[r, c].block == currentCell.block)
                    {
                        cell[r, c].label.BackColor = adjacentCellsColor;
                    }
                    if (cell[r, c].display != 0 && cell[r, c].display == currentCell.display) cell[r, c].label.BackColor = sameValueColor;
                }
            }
            CheckEror(); //Kiểm tra lỗi
            currentCell.label.BackColor = clickingCellColor;
            if (!isEror) //Nếu không có lỗi, kiểm tra đã chiến thắng hay chưa
            {
                int cntNum = 0;
                for (int i = 0; i < 9; ++i)
                    for (int j = 0; j < 9; ++j)
                        if (cell[i, j].display > 0) ++cntNum;
                
                if (cntNum == 81) {
                    form1.WinGame(); //Tải thông báo Win Game
                    stop = true;
                }
            }
        }

        void ResetColor()
        {
            for (int r = 0; r < 9; ++r)
                for (int c = 0; c < 9; ++c)
                {
                    cell[r, c].label.BackColor = Color.White;
                    if (!cell[r, c].note) cell[r, c].label.ForeColor = (cell[r, c].fxed ? fixedNumColor : textColor);
                    else cell[r, c].label.ForeColor = Color.Gray;
                }
        }

        int[] noteIdx = { 0,  2,  4,  6,  8,  10, 12, 14, 16 };

        string ChangeNoteValue(string s, int value)
        {
            char[] str = s.ToCharArray();
            if (str[noteIdx[value - 1]] == ' ') str[noteIdx[value - 1]] = Convert.ToChar(value + '0');
            else str[noteIdx[value - 1]] = ' ';
            s = new string(str);
            return s;
        }
        void ChangeValueCell(Cell cell, char key, bool noteMode = false)
        {
            if (cell.fxed == true || stop) return;
            if (noteMode)
            {
                if (!cell.note)
                {
                    cell.note = true;
                    cell.display = 0;
                    cell.label.Font = new Font("Consolas", 10);
                    cell.label.ForeColor = Color.Gray;
                }
                if (cell.noteStr == "")
                {
                    cell.noteStr = "     \n     \n     ";
                }
                
                cell.noteStr = ChangeNoteValue(cell.noteStr, key - '0');
                cell.label.Text = cell.noteStr;
            }
            else
            {
                cell.label.Font = new Font("Arial", 25);
                cell.label.Text = key.ToString();
                if (cell.note) { cell.note = false; cell.noteStr = ""; }
                cell.display = (key - '0');
            }
            ResetColor();
            ChangeColorCells(clickingCell);
        }

        void CheckEror()
        {
            bool isEror1 = false;
            for(int r = 0; r < 9; ++r)
            {
                for(int c = 0; c < 9; ++c)
                {
                    if (cell[r, c].display == 0) continue;
                    if (cell[r, c].display != cell[r, c].hidden) cell[r, c].label.ForeColor = textErorColor;
                    for (int r1 = 0; r1 < 9; ++r1)
                    {
                        for (int c1 = 0; c1 < 9; ++c1)
                        {
                            if(r == r1 && c == c1) continue;
                            if ((r == r1 && c != c1) || (r != r1 && c == c1) || cell[r, c].block == cell[r1, c1].block)
                            if (cell[r, c].display == cell[r1, c1].display)
                            {
                                cell[r1, c1].label.BackColor = backColorEror;
                                isEror1 = true;
                                if (cell[r, c].fxed == false) cell[r, c].label.ForeColor = textErorColor;
                            }
                        }
                    }
                }
            }
            isEror = isEror1;
        }
        void DeleteValueCell(Cell cell)
        {
            if (cell == null || cell.fxed == true || stop) return;
            if(cell.label.Text == string.Empty) return;
            cell.label.Text = string.Empty;
            cell.display = 0;
            cell.noteStr = string.Empty;
            ResetColor();
            ChangeColorCells(clickingCell);
        }
        void UpdateCell(char key, bool noteMode = false)
        {
            if ((clickingCell.display != (int)(key - '0')) || noteMode)
            {
                ChangeValueCell(clickingCell, (char)key, noteMode);
            }
            else 
            {
                DeleteValueCell(clickingCell); 
            }
            //ResetColor();
            //ChangeColorCells(clickingCell);
        }

        //..........................................
        //.............keyboard control.............
        //..........................................

        public void SetValueCellKeyboard(Keys key)
        {
            if (clickingCell == null || stop) return;
  
            if ((char)key >= '1' && (char)key <= '9')
            {
                UpdateCell((char)key, noteMode);
            }
            else if (key == Keys.Back)
            {
                DeleteValueCell(clickingCell);
            }

            if ((key == Keys.W || key == Keys.Up)&& clickingCell.row > 0)
            {
                clickingCell = cell[clickingCell.row - 1, clickingCell.col];
            }
            else if (key == Keys.S && clickingCell.row < 8)
            {
                clickingCell = cell[clickingCell.row + 1, clickingCell.col];
            }
            else if (key == Keys.D && clickingCell.col < 8)
            {
                clickingCell = cell[clickingCell.row, clickingCell.col + 1];
            }
            else if (key == Keys.A && clickingCell.col > 0)
            {
                clickingCell = cell[clickingCell.row, clickingCell.col - 1];
            }
            ChangeColorCells(clickingCell);
        }
        //.............button number.............
        ButtonNumber[] buttonNum = new ButtonNumber[9];
        public void DisplayNumberButton(Panel panel) //Hiển thị giao diện các nút
        {
            for(int i = 0, x = 0, y = 0; i < 9; ++i)
            {
                buttonNum[i] = new ButtonNumber();
                panel.Controls.Add(buttonNum[i]);
                buttonNum[i].Text = (i+1).ToString();
                buttonNum[i].Location = new Point(x, y);
                buttonNum[i].Margin = new Padding(0, 0, 0, 0);
                x += 84;
                if (i % 3 == 2) { y += 84; x = 0; }
                buttonNum[i].SendToBack();
                buttonNum[i].Click += new EventHandler(ButtonNumber_Click);
            }    
        }

        void ButtonNumber_Click(object sender, EventArgs e)
        {
            if (clickingCell == null || stop) return;
            if (clickingCell.fxed) return;
            ButtonNumber a = (ButtonNumber)sender;
            if (noteMode) //Nếu đang là là chế độ ghi chú
            {
                UpdateCell(a.Text[0], noteMode);
                return;
            }
            if (clickingCell.label.Text != a.Text) UpdateCell(a.Text[0], noteMode); //Nếu giá trị trên ô bằng với giá trị nút vừa click thì xóa
            else DeleteValueCell(clickingCell);
        }
        //.............button option.............
        public void DisplayButtonOption(Panel panel)
        {
            //Erase Button
            CreateButtonOption(panel, "Erase", 0, Properties.Resources.eraser_icon, EraseBtn_Click);
            //Note Button
            CreateButtonOption(panel, "Note", panel.Width / 4, Properties.Resources.note_icon, NoteBtn_Click);
            //Hint Button
            CreateButtonOption(panel, "Hint", panel.Width / 4 * 2, Properties.Resources.hint_icon, HintBtn_Click);
        }
        public delegate void ButtonOptionClickHandler(object sender, EventArgs e);
        Label labHint;
        public void CreateButtonOption(Panel panel, string Name, int xLocation, System.Drawing.Image img, ButtonOptionClickHandler BtnHandler)
        {
            ButtonControl btn = new ButtonControl();
            PanelControlButton pBtn = new PanelControlButton(btn.Width, panel.Height, Name, xLocation);
            btn.Set(img, 0, 0);
            pBtn.Controls.Add(btn);
            panel.Controls.Add(pBtn);
            btn.Click += new EventHandler(BtnHandler);
            if(Name == "Hint")
            {
                labHint = new Label();
                labHint.Text = cntHint.ToString();
                labHint.BackColor = Color.Transparent;
                labHint.Width = 20;
                labHint.Height = labHint.Width;
                labHint.ForeColor = Color.White;
                labHint.Font = new Font("Arial", 10);
                labHint.TextAlign = ContentAlignment.MiddleCenter;
                labHint.BackColor = ColorTranslator.FromHtml("#0072E3");
                labHint.Location = new Point(pBtn.Width-labHint.Width,0);

                var path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(0, 0, labHint.Width, labHint.Height);

                labHint.Region = new Region(path);

                pBtn.Controls.Add(labHint);
                labHint.BringToFront();
            }
        }

        //Erase Button
        void EraseBtn_Click(object sender, EventArgs e)
        {
            DeleteValueCell(clickingCell);
        }
        // Note Button
        void NoteBtnOn(ButtonControl btn)
        {
            var resizedImage = new Bitmap(60, 60);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.DrawImage(Properties.Resources.note_icon_on, 0, 0, resizedImage.Width, resizedImage.Height);
            }
            btn.BackgroundImage = resizedImage;
        }
        void NoteBtn_Click(Object sender, EventArgs e)
        {
            if (stop) return;
            noteMode = !noteMode;
            if (noteMode)
            {
                NoteBtnOn((ButtonControl)sender);
            }
            else
            {
                ButtonControl btn = (ButtonControl)sender;
                var resizedImage = new Bitmap(30, 30);
                using (Graphics g = Graphics.FromImage(resizedImage))
                {
                    g.DrawImage(Properties.Resources.note_icon, 0, 0, resizedImage.Width, resizedImage.Height);
                }
                btn.BackgroundImage = resizedImage;
            }    
        }

        //Hint Button
        void HintBtn_Click(Object sender, EventArgs e)
        {
            if (stop) return;
            if (clickingCell == null || clickingCell.fxed || cntHint == 0) return;
            --cntHint;
            labHint.Text = cntHint.ToString();
            clickingCell.display = clickingCell.hidden;
            clickingCell.label.Text = clickingCell.display.ToString();
            clickingCell.label.Font = new Font("Arial", 25);
            clickingCell.label.ForeColor = fixedNumColor;
            clickingCell.fxed = true;
            clickingCell.note = false;
            ChangeColorCells(clickingCell);
        }
    }
}
