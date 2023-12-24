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
        private string _imagePath;
        //private readonly MapPainter _mapPainter;
        private string Difficulty { get; set; }
        public GameWindow(string difficulty)
        {
            InitializeComponent();
            Difficulty = difficulty;
            int rows, cols;
            ParseDifficulty(difficulty, out rows, out cols);

            _map = new Map(CreateInitialMap(rows, cols));
            _imagePath = "D:/VS2022/SliderPuzzle/image/1.png";
            MapPainter.DrawMap(_map, MainGrid, _map.IsSolved(), _imagePath);
        }
        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            // 这里需要根据你的实际游戏逻辑创建当前状态和目标状态
            Map currentState = GetCurrentState();
            Map goalState = GetGoalState();

            var path = AStarSearch(currentState, goalState);
            if (path != null && path.Count > 1)
            {
                Map nextStep = path[0];
                // 显示下一步提示
                ShowNextStep(nextStep);
            }
            else
            {
                MessageBox.Show("没有可用的移动。");
            }
        }
        private Map GetGoalState()
        {
            int[,] goalBoard = new int[_map.RowCount, _map.ColCount];

            int num = 1;
            for (int row = 0; row < _map.RowCount; row++)
            {
                for (int col = 0; col < _map.ColCount; col++)
                {
                    goalBoard[row, col] = num;
                    num++;
                }
            }
            goalBoard[_map.RowCount - 1, _map.ColCount - 1] = 0; // 最后一个位置为空白

            return new Map(goalBoard);
        }
        private void ShowNextStep(Map nextStep)
        {
            var currentMap = _map.GetCurrentMap();
            var nextMap = nextStep.GetCurrentMap();

            (int currentRow, int currentCol) = FindBlankSpace(currentMap);
            (int nextRow, int nextCol) = FindBlankSpace(nextMap);

            // 判断空白格移动的方向
            string moveDirection = "";
            if (currentRow == nextRow)
            {
                if (currentCol - nextCol == 1)
                    moveDirection = "向左移动";
                else if (currentCol - nextCol == -1)
                    moveDirection = "向右移动";
            }
            else if (currentCol == nextCol)
            {
                if (currentRow - nextRow == 1)
                    moveDirection = "向上移动";
                else if (currentRow - nextRow == -1)
                    moveDirection = "向下移动";
            }
            MessageBox.Show($"下一步提示: {moveDirection}");
        }
        private (int, int) FindBlankSpace(int[,] map)
        {
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    if (map[row, col] == 0)
                    {
                        return (row, col);
                    }
                }
            }
            throw new InvalidOperationException("No blank space found in the map.");
        }
        public List<Map> AStarSearch(Map start, Map goal)
        {
            var openSet = new PriorityQueue<Map, int>();
            var cameFrom = new Dictionary<Map, Map>();
            var costSoFar = new Dictionary<Map, int>();

            openSet.Enqueue(start, 0);
            cameFrom[start] = null;
            costSoFar[start] = 0;

            while (openSet.Count > 0)
            {
                var current = openSet.Dequeue();

                if (current.IsGoal(goal))
                {
                    return ReconstructPath(cameFrom, start, current);
                }

                foreach (var next in current.GetNeighbors())
                {
                    int newCost = costSoFar[current] + 1; // 假设每一步的成本为 1
                    if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                    {
                        costSoFar[next] = newCost;
                        int priority = newCost + Heuristic(next, goal);
                        if (!cameFrom.ContainsKey(next)) // 防止重复添加
                        {
                            openSet.Enqueue(next, priority);
                            cameFrom[next] = current;
                        }
                    }
                }
            }
            return null;
        }
        private Map GetCurrentState()
        {
            // 假设 GameState 类有一个接受 Map 对象的构造函数
            return _map.Clone();
        }

        public int Heuristic(Map state, Map goal)
        {
            int distance = 0;
            for (int row = 0; row < state.RowCount; row++)
            {
                for (int col = 0; col < state.ColCount; col++)
                {
                    int value = state[row, col];
                    if (value != 0)
                    {
                        var (goalRow, goalCol) = FindPosition(value, goal);
                        distance += Math.Abs(row - goalRow) + Math.Abs(col - goalCol);
                    }
                }
            }
            return distance;
        }
        private (int, int) FindPosition(int value, Map board)
        {
            for (int row = 0; row < board.RowCount; row++)
            {
                for (int col = 0; col < board.ColCount; col++)
                {
                    if (board[row, col] == value)
                        return (row, col);
                }
            }
            return (-1, -1); // 不应该发生，如果发生则表示错误
        }
        private List<Map> ReconstructPath(Dictionary<Map, Map> cameFrom, Map start, Map goal)
        {
            List<Map> path = new List<Map>();
            var current = goal;
            while (current != start)
            {
                path.Add(current);
                current = cameFrom[current];
            }
            path.Reverse();
            return path;
        }
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                // 当图片路径被设置时，重新绘制地图
                if (_map != null)
                {
                    MapPainter.DrawMap(_map, MainGrid, _map.IsSolved(), _imagePath);
                }
            }
        }

        public GameWindow(string difficulty, string imagePath, int[,] boardState)
        {
            InitializeComponent();
            int rows, cols;
            ParseDifficulty(difficulty, out rows, out cols);
            // 确保传入的棋盘状态与难度匹配
            if (boardState.GetLength(0) != rows || boardState.GetLength(1) != cols)
            {
                throw new ArgumentException("The board state does not match the specified difficulty.");
            }

            _map = new Map(boardState);
            MapPainter.DrawMap(_map, MainGrid, _map.IsSolved(), imagePath);
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
        private int[,] CreateInitialMap(int rows, int cols)
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
                ShuffleMap(initialMap, rows, cols);
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
            MapPainter.DrawMap(_map, MainGrid, _map.IsSolved(), _imagePath);
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
                ImagePath = getCurrentImagePath(),
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
        private string getCurrentImagePath()
        {
            return ImagePath;
        }
        private void GameWindow_Closing(object sender, EventArgs e)
        {
            GameState gameState = GetCurrentGameState();
            GameDataHelper.SaveGameState(gameState, "D:/VS2022/SliderPuzzle/gameSave.xml");
        }
    }
}