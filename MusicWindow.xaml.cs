using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
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

namespace SliderPuzzleGameExtension
{
    /// <summary>
    /// MusicWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MusicWindow : Window
    {
        private MusicPlayer _musicPlayer;
        private static MusicWindow _instance;
        public MusicWindow()
        {
            InitializeComponent();
            _musicPlayer = MusicPlayer.Instance;
            // 设置播放开关的状态
            MusicToggleButton.IsChecked = _musicPlayer.IsPlaying;
        }
        public static MusicWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MusicWindow();
                }
                return _instance;
            }
        }
        private void MusicToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            _musicPlayer.PlayMusic("../../../music.wav");
            // 减小或静音视频背景的声音
            Application.Current.Dispatcher.Invoke(() =>
            {
                var mainWindow = Application.Current.MainWindow as StartWindow;
                if (mainWindow != null)
                {
                    mainWindow.VideoBackground.Volume = 0;
                }
            });
        }

        private void MusicToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            _musicPlayer.StopMusic();
            // 恢复视频背景的声音
            Application.Current.Dispatcher.Invoke(() =>
            {
                var mainWindow = Application.Current.MainWindow as StartWindow;
                if (mainWindow != null)
                {
                    mainWindow.VideoBackground.Volume = 1; // 或者设置为原来的音量
                }
            });
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_musicPlayer != null && VolumeSlider != null)
            {
                _musicPlayer.SetVolume(VolumeSlider.Value);
            }
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            // 取消关闭事件并隐藏窗口
            e.Cancel = true;
            this.Hide();
        }
    }
}
