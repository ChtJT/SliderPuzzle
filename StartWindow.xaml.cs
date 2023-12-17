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
    /// StartWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow settingWindow = new SettingWindow();
            settingWindow.Closed += (s, args) => this.Show();  // 当设置窗口关闭时，重新显示 StartWindow
            settingWindow.Show();
            this.Hide();  // 隐藏当前窗口
        }
        private void GameOptionsWindow_Click(object sender, RoutedEventArgs e)
        {
            GameOptionsWindow gameOptionsWindow = new GameOptionsWindow();
            gameOptionsWindow.Closed += (s, args) => this.Show();  // 当设置窗口关闭时，重新显示 StartWindow
            gameOptionsWindow.Show();
            this.Hide();  // 隐藏当前窗口
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // 关闭窗口
        }
    }

}
