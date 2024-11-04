using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace GameSudoku
{
    internal class Cell
    {
        public int hidden { get; set; }     // Giá trị bị ẩn đi
        public int display { get; set; }    // Giá trị hiển thị
        public bool fxed { get; set; }      // Fixed = 0 tương ứng với ô cần điền số, ngược lại ô đã cố định
        public int row { get; set; }        // Chỉ số dòng của ô. Giá trị [0, 8].
        public int col { get; set; }        // Chỉ số cột của ô. Giá trị [0, 8].
        public int block { get; set; }      // Chỉ số khối của ô. Giá trị [0, 8].
        public Label label { get; set; }    // Label dùng để hiển thị số tương ứng, cùng các đặc điểm khác của ô.
        public bool note { get; set; }      // Dùng để trả lời ô có phải đang được ghi chú hay không.
        public string noteStr { get; set; } // Các số đang được ghi chú trên ô.

        
        public Cell(int hidden, int display, bool fxed, int row, int col, Label label)
        {
            this.hidden = hidden;
            this.display = display;
            this.fxed = fxed;
            this.row = row;
            this.col = col;
            this.block = row / 3 * 3 + col / 3;
            this.label = label;
            this.note = false;
        }
    }
}
