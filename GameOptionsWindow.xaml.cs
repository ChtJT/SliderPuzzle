using Microsoft.Win32;
using SliderPuzzle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SliderPuzzleGameExtension
{
    /// <summary>
    /// GameOptionsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GameOptionsWindow : Window
    {
        public GameOptionsWindow()
        {
            InitializeComponent();
        }
        private string _selectedDifficulty;
        private GameWindow gameWindow;
        private void StartNewGame_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_selectedDifficulty))
            {
                gameWindow = new GameWindow(_selectedDifficulty);
                gameWindow.ImagePath = selectedImagePath; // 设置图片路径
                gameWindow.Show();
                //this.Close();
            }
            else
            {
                MessageBox.Show("请先选择难度！");
            }
        }
        private void ContinueGame_Click(object sender, RoutedEventArgs e)
        {
            string currentDifficulty = _selectedDifficulty;
            string imagePath = selectedImagePath;
            string filePath = "D:/VS2022/SliderPuzzle/gameSave.xml";

            GameState gameState = GameDataHelper.LoadGameState(currentDifficulty,imagePath,filePath);

            if (gameState != null)
            {
                // 将从XML中加载的棋盘状态转换为二维数组
                int[,] boardState = ConvertTo2DArray(gameState.Board, gameState.DifficultyLevel);
                // 使用加载的游戏状态创建新的 GameWindow
                GameWindow gameWindow = new GameWindow(gameState.DifficultyLevel,imagePath,boardState);
                gameWindow.Show();
            }
            else
            {
                MessageBox.Show("无法加载游戏状态。", "错误");
            }
        }
        // 这个方法用于将一维列表转换为二维数组，根据难度设置数组的维度
        private int[,] ConvertTo2DArray(List<int> board, string difficultyLevel)
        {
            // 假设 DifficultyLevel 的格式为 "NxM"
            string[] parts = difficultyLevel.Split('x');
            int rows = int.Parse(parts[0]);
            int cols = int.Parse(parts[1]);

            int[,] boardState = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    boardState[i, j] = board[i * cols + j];
                }
            }

            return boardState;
        }
        private void SetDifficulty_Click(object sender, RoutedEventArgs e)
        {
            DifficultySettingWindow difficultyWindow = new DifficultySettingWindow(_selectedDifficulty);
            difficultyWindow.DifficultySelected += difficulty =>
            {
                _selectedDifficulty = difficulty; // 保存选择的难度
            };
            difficultyWindow.ShowDialog();
        }
        private string selectedImagePath = "D:/VS2022/SliderPuzzle/image/1.png"; // 默认图片路径
        private void SetPicture_Click(object sender, RoutedEventArgs e)
        {
            var imageSelectionWindow = new ImageSelectionWindow("D:/VS2022/SliderPuzzle/image");
            if (imageSelectionWindow.ShowDialog() == true)
            {
                HandleNewPicture(imageSelectionWindow.SelectedImagePath);
            }
        }
        // 这个方法将会处理新选择的图片
        private void HandleNewPicture(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad; // 确保图片在加载时被缓存
                bitmap.EndInit();
                selectedImagePath = imagePath;
            }
        }
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            // 关闭当前窗口
            this.Close();
        }
    }
}
