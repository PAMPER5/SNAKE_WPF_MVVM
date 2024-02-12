using SNAKE_WPF_MVVM.ViewModels;
using System.Windows;

namespace SNAKE_WPF_MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWndVM(this);
        }
    }
}