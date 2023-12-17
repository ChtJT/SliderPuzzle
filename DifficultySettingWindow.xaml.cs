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
    /// DifficultySettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DifficultySettingWindow : Window
    {
        public string SelectedDifficulty { get; private set; }
        // 定义一个事件，用于在选择难度后通知其他窗口
        public event Action<string> DifficultySelected;
        public DifficultySettingWindow(string initialDifficulty)
        {
            InitializeComponent();
            SetInitialDifficulty(initialDifficulty);
        }
        private void SetInitialDifficulty(string initialDifficulty)
        {
            if (!string.IsNullOrEmpty(initialDifficulty))
            {
                // 假设您的 ComboBox 是这样设置的项
                foreach (ComboBoxItem item in DifficultyComboBox.Items)
                {
                    if (item.Content.ToString() == initialDifficulty)
                    {
                        DifficultyComboBox.SelectedItem = item;
                        break;
                    }
                }
            }
        }
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = DifficultyComboBox.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                string selectedDifficulty = selectedItem.Content.ToString();
                DifficultySelected?.Invoke(selectedDifficulty); // 触发事件
                this.Close();
            }
            else
            {
                MessageBox.Show("请选择一个难度");
            }
        }
    }
}
