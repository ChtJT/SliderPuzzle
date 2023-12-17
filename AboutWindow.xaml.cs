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

using System.Windows;

namespace SliderPuzzleGameExtension
{
    public partial class AboutWindow : Window
    {
        public AboutWindow(string markdownContent)
        {
            InitializeComponent();
            MarkdownTextBox.Text = markdownContent; // 显示传入的 Markdown 内容
        }
    }
}

