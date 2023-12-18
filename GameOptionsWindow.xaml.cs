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
            // 假设您有一个方法来获取当前选择的难度
            string currentDifficulty = _selectedDifficulty;

            // 构造 XML 文件路径
            string filePath = "../../../gameSave.xml";

            // 从 XML 文件中加载游戏状态
            GameState gameState = GameDataHelper.LoadGameState(currentDifficulty, filePath);

            if (gameState != null)
            {
                // 使用加载的游戏状态创建新的 GameWindow
                GameWindow gameWindow = new GameWindow(gameState.DifficultyLevel, false);
                gameWindow.Show();
                // gameWindow.ApplyGameState(gameState);
            }
            else
            {
                MessageBox.Show("无法加载游戏状态。", "错误");
            }
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
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            // 关闭当前窗口
            this.Close();
        }

    }
}
