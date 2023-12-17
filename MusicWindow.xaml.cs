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
        public MusicWindow()
        {
            InitializeComponent();
            _musicPlayer = MusicPlayer.Instance;

            // 暂时解除事件处理程序的绑定
            MusicToggleButton.Checked -= MusicToggleButton_Checked;
            MusicToggleButton.Unchecked -= MusicToggleButton_Unchecked;

            // 设置播放开关的状态
            MusicToggleButton.IsChecked = _musicPlayer.IsPlaying;

            // 重新绑定事件处理程序
            MusicToggleButton.Checked += MusicToggleButton_Checked;
            MusicToggleButton.Unchecked += MusicToggleButton_Unchecked;
        }
        private void MusicToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            _musicPlayer.PlayMusic("D:/VS2022/SliderPuzzleGameExtension/music.wav");
        }

        private void MusicToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            _musicPlayer.StopMusic();
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
            this.Close();
        }
    }
}
