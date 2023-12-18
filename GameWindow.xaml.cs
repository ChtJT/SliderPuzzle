using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SliderPuzzleGameExtension
{

    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private readonly Map _map;
        //private readonly MapPainter _mapPainter;
        private string Difficulty { get; set; }
        public GameWindow(string difficulty, bool shuffle = true)
        {
            InitializeComponent();
            int rows, cols;
            ParseDifficulty(difficulty, out rows, out cols);

            _map = new Map(CreateInitialMap(rows, cols, shuffle));
            MapPainter.DrawMap(_map, MainGrid, _map.IsSolved());
        }
        private void ParseDifficulty(string difficulty, out int rows, out int cols)
        {
            Difficulty = difficulty;
            switch (difficulty)
            {
                case "2x3":
                    rows = 2; cols = 3; break;
                case "3x3":
                    rows = 3; cols = 3; break;
                case "4x4":
                    rows = 4; cols = 4; break;
                case "5x5":
                    rows = 5; cols = 5; break;
                case "10x10":
                    rows = 10; cols = 10; break;
                default:
                    throw new InvalidOperationException("未知难度");
            }
        }
        private int[,] CreateInitialMap(int rows, int cols ,bool shuffle)
        {
            int[,] initialMap = new int[rows, cols];

            // 填充数组
            int num = 1;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (num <= rows * cols -1) // 只填充前row*cols个位置
                    {
                        initialMap[i, j] = num++;
                    }
                    else
                        initialMap[i, j] = 0; // 其余位置设置为0（空格）
                }
            }
            if (shuffle)
            {
                ShuffleMap(initialMap, rows, cols);
            }
            return initialMap;
        }
        private Random _random = new Random();
        private void ShuffleMap(int[,] map, int rows, int cols)
        {
            int blankRow = rows - 1;
            int blankCol = cols - 1;
            int[] dx = new int[] { 0, 1, 0, -1 }; // 移动方向 - 上，右，下，左
            int[] dy = new int[] { -1, 0, 1, 0 };

            int newRow, newCol; // 在循环外声明 newRow 和 newCol

            for (int moves = 0; moves < 100000; moves++) // 执行足够多次移动以打乱拼图
            {
                List<int> possibleMoves = new List<int>();

                for (int i = 0; i < 4; i++) // 检查所有可能的移动方向
                {
                    newRow = blankRow + dy[i];
                    newCol = blankCol + dx[i];

                    if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                    {
                        possibleMoves.Add(i);
                    }
                }

                int move = _random.Next(possibleMoves.Count); // 从可能的移动中随机选择一个
                newRow = blankRow + dy[possibleMoves[move]];
                newCol = blankCol + dx[possibleMoves[move]];

                // 交换空白格与移动方向上的格子
                map[blankRow, blankCol] = map[newRow, newCol];
                map[newRow, newCol] = 0;

                blankRow = newRow; // 更新空白格的位置
                blankCol = newCol;
            }
        }
        private void MainGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key is not (Key.Up or Key.Down or Key.Left or Key.Right)) return;
            _map.Move(e.Key);
            MapPainter.DrawMap(_map, MainGrid, _map.IsSolved());
        }
        private string getDifficultyLevel()
        {
            return Difficulty;
        }
        private GameState GetCurrentGameState()
        {
            var gameState = new GameState
            {
                DifficultyLevel = getDifficultyLevel(),
                Board = new List<int>()
            };

            var currentMap = _map.GetCurrentMap(); // 假设这个方法返回当前地图状态
            for (int i = 0; i < currentMap.GetLength(0); i++)
            {
                for (int j = 0; j < currentMap.GetLength(1); j++)
                {
                    gameState.Board.Add(currentMap[i, j]);
                }
            }

            return gameState;
        }
        private void GameWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GameState gameState = GetCurrentGameState();
            GameDataHelper.SaveGameState(gameState, "../../../gameSave.xml");
        }
    }
}