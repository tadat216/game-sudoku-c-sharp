namespace GameSudoku
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panelNumberButton = new System.Windows.Forms.Panel();
            this.panelControlButton = new System.Windows.Forms.Panel();
            this.panelSudokuBoard = new System.Windows.Forms.Panel();
            this.panelWin = new System.Windows.Forms.Panel();
            this.panelNewGame = new System.Windows.Forms.Panel();
            this.panelSudokuBoard.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelNumberButton
            // 
            this.panelNumberButton.Location = new System.Drawing.Point(604, 198);
            this.panelNumberButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelNumberButton.Name = "panelNumberButton";
            this.panelNumberButton.Size = new System.Drawing.Size(254, 254);
            this.panelNumberButton.TabIndex = 1;
            this.panelNumberButton.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControlButtonUI_Paint);
            // 
            // panelControlButton
            // 
            this.panelControlButton.Location = new System.Drawing.Point(631, 93);
            this.panelControlButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelControlButton.Name = "panelControlButton";
            this.panelControlButton.Size = new System.Drawing.Size(256, 89);
            this.panelControlButton.TabIndex = 3;
            // 
            // panelSudokuBoard
            // 
            this.panelSudokuBoard.BackColor = System.Drawing.Color.White;
            this.panelSudokuBoard.Controls.Add(this.panelWin);
            this.panelSudokuBoard.Location = new System.Drawing.Point(9, 10);
            this.panelSudokuBoard.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelSudokuBoard.Name = "panelSudokuBoard";
            this.panelSudokuBoard.Size = new System.Drawing.Size(568, 568);
            this.panelSudokuBoard.TabIndex = 2;
            this.panelSudokuBoard.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.panelSudokuBoard_PreviewKeyDown);
            // 
            // panelWin
            // 
            this.panelWin.Location = new System.Drawing.Point(78, 111);
            this.panelWin.Name = "panelWin";
            this.panelWin.Size = new System.Drawing.Size(399, 289);
            this.panelWin.TabIndex = 5;
            this.panelWin.Paint += new System.Windows.Forms.PaintEventHandler(this.panelWin_Paint);
            // 
            // panelNewGame
            // 
            this.panelNewGame.Location = new System.Drawing.Point(604, 499);
            this.panelNewGame.Name = "panelNewGame";
            this.panelNewGame.Size = new System.Drawing.Size(252, 79);
            this.panelNewGame.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(870, 591);
            this.Controls.Add(this.panelSudokuBoard);
            this.Controls.Add(this.panelNewGame);
            this.Controls.Add(this.panelControlButton);
            this.Controls.Add(this.panelNumberButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Sudoku";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.panelSudokuBoard.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelSudokuBoard;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panelNumberButton;
        private System.Windows.Forms.Panel panelControlButton;
        private System.Windows.Forms.Panel panelNewGame;
        private System.Windows.Forms.Panel panelWin;
    }
}

