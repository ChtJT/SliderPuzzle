using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SliderPuzzleGameExtension
{
    public class GameState
    {
        public int DifficultyLevel { get; set; } // 难度用 0-4 表示
        public List<int> Board { get; set; } // 当前棋盘布局
    }
}
