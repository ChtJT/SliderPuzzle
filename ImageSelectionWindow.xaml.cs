using System;
using System.Collections.Generic;
using System.IO;
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

namespace SliderPuzzle
{
    /// <summary>
    /// ImageSelectionWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ImageSelectionWindow : Window
    {
        public string SelectedImagePath { get; private set; }

        public ImageSelectionWindow(string folderPath)
        {
            InitializeComponent();
            LoadImages(folderPath);
        }

        private void LoadImages(string folderPath)
        {
            var imagePaths = Directory.GetFiles(folderPath, "*.png")
                               .Select(filePath => new Uri(filePath, UriKind.RelativeOrAbsolute))
                               .ToList();

            ImagesListBox.ItemsSource = imagePaths;
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (ImagesListBox.SelectedItem is Uri selectedUri)
            {
                SelectedImagePath = selectedUri.OriginalString;
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("请选择一张图片。");
            }
        }
    }

}
