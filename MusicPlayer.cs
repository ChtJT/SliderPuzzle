using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SliderPuzzleGameExtension
{
    public class MusicPlayer
    {
        private static MusicPlayer _instance;
        private MediaPlayer _mediaPlayer;
        public bool IsPlaying { get; private set; }
        // 私有构造函数确保外部不能直接实例化
        private MusicPlayer()
        {
            _mediaPlayer = new MediaPlayer();
        }

        // 公共静态方法提供全局访问点
        public static MusicPlayer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MusicPlayer();
                }
                return _instance;
            }
        }

        public void PlayMusic(string filePath)
        {
            _mediaPlayer.Open(new Uri(filePath, UriKind.RelativeOrAbsolute));
            _mediaPlayer.Play();
            IsPlaying = true;
        }

        public void StopMusic()
        {
            _mediaPlayer.Stop();
            IsPlaying = false;
        }

        public void SetVolume(double volume)
        {
            _mediaPlayer.Volume = volume;
        }
    }
}
