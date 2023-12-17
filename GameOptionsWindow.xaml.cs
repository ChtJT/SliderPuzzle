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
            // 实现继续游戏的逻辑
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
        private string ConvertLevelToDifficulty(int level)
        {
            switch (level)
            {
                case 0: return "2x3";
                case 1: return "3x3";
                case 2: return "4x4";
                case 3: return "5x5";
                case 4: return "10x10";
                default: return "unknown";
            }
        }
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            // 关闭当前窗口
            this.Close();
        }

    }
}
