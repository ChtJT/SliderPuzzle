using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SliderPuzzleGameExtension
{
    public class GameState
    {
        public string DifficultyLevel { get; set; }
        public string ImagePath { get; set; }
        public List<int> Board { get; set; } // 当前棋盘布局
    }
}
