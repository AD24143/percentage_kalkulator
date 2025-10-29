using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp3.ViewModels;

namespace lab_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void NumericOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            var text = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            e.Handled = !IsValidNumericInput(text);
        }

        private void IntegerOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            var text = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            e.Handled = !IsValidIntegerInput(text);
        }

        private static bool IsValidNumericInput(string text)
        {
            if (string.IsNullOrEmpty(text))
                return true;

            // Allow numbers with optional single decimal point
            var regex = new Regex(@"^[0-9]*\.?[0-9]*$");
            return regex.IsMatch(text);
        }

        private static bool IsValidIntegerInput(string text)
        {
            if (string.IsNullOrEmpty(text))
                return true;

            // Allow only integers (positive)
            var regex = new Regex(@"^[0-9]+$");
            return regex.IsMatch(text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Button click is handled by Command binding
            // This handler is kept for any additional logic if needed
        }
    }
}