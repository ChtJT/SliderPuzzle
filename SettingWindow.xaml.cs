using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using System.Windows.Media; // 引入命名空间

namespace SliderPuzzleGameExtension
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window
    {
        
        public SettingWindow()
        {
            InitializeComponent();
        }
        private void MusicWindow_Click(object sender, RoutedEventArgs e)
        {
            MusicWindow musicWindow = new MusicWindow();
            musicWindow.Closed += (s, args) => this.Show();  // 当设置窗口关闭时，重新显示 StartWindow
            musicWindow.Show();
        }
    }
}
