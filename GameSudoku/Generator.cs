using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSudoku
{
    public class Generator
    {
        public int[,] hiddenNum = new int[9, 9];
        public int[,] displayNum = new int[9, 9];
        public bool[,] fixedNum = new bool[9, 9];

        void FillDiagonal() // Điền giá trị vào các khối ở đường chéo chính.
        {
            int[] a = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int box = 0; box < 9; box += 4)
            {
                Random random = new Random();
                a = a.OrderBy(x => random.Next()).ToArray(); //Xáo trộn ngẫu nhiên mảng A để điền vào khối.
                int s = 0;
                for (int i = box / 3 * 3; i < box / 3 * 3 + 3; ++i){
                    for (int j = box / 3 * 3; j < box / 3 * 3 + 3; ++j){
                        hiddenNum[i, j] = a[s++];
                        fixedNum[i, j] = true;
                    }
                }
            }
        }
        bool Ok(int i, int j, int num) //Hàm kiểm tra số vừa điền vào ô [i, j] có hợp lệ không
        {
            for (int x = 0; x < 9; ++x){
                if (hiddenNum[i, x] == num) return false;
                if (hiddenNum[x, j] == num) return false;
            }
            for (int r = i / 3 * 3; r < i / 3 * 3 + 3; ++r)
                for (int c = j / 3 * 3; c < j / 3 * 3 + 3; ++c) if (hiddenNum[r, c] == num) return false;
            return true;
        }
        bool Ql(int i, int j){ //Hàm quay lui để khởi tạo giá trịu
            if (i == 9) return true;
            int nxti = (j == 8 ? i + 1 : i);
            int nxtj = (j == 8 ? 0 : j + 1);
            if (fixedNum[i, j]) return Ql(nxti, nxtj);
            for (int num = 1; num < 10; ++num){
                if (Ok(i, j, num)){
                    hiddenNum[i, j] = num;
                    if (Ql(nxti, nxtj)) return true;
                    hiddenNum[i, j] = 0;
                }
            }
            return false;
        }
        void RemoveNumber(int k)
        {
            for (int r = 0; r < 9; ++r)
                for (int c = 0; c < 9; ++c)
                {
                    displayNum[r, c] = hiddenNum[r, c];
                    fixedNum[r, c] = true;
                }
            Random rand = new Random();
            bool[,] vis = new bool[9, 9];
            
            for (int i = 0; i < k; ++i)
            {
                int r, c;

                do
                {
                    r = rand.Next(9); c = rand.Next(9);
                    displayNum[r, c] = 0;
                    fixedNum[r, c] = false;
                } while (vis[r, c] == true);
                vis[r, c] = true;
            }
        }
        public void Main(int k)
        {
            FillDiagonal();
            Ql(0, 0);
            RemoveNumber(k);
        }
    }
}
