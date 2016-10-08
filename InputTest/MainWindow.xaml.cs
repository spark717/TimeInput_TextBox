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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Diagnostics;

namespace InputTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _oldText = "__:__";

        public MainWindow()
        {
            InitializeComponent();

            InputBox.Text = "__:__";

            InputBox.TextChanged += OnTextChanged;
        }


        private void OnTextChanged (object sender, TextChangedEventArgs e)
        {
            InputBox.TextChanged -= OnTextChanged;

            int carretPos = InputBox.CaretIndex;
            StringBuilder text = new StringBuilder(InputBox.Text);

            string insertedChar = text[carretPos - 1].ToString();
            int insertedNum;
            bool isNumber = Int32.TryParse(insertedChar, out insertedNum);

            if (carretPos > 5 || !isNumber)
            {
                InputBox.Text = _oldText;
                InputBox.CaretIndex = carretPos - 1;

                InputBox.TextChanged += OnTextChanged;
                return;
            }

            if (text[carretPos].ToString() == ":")
            {
                text.Remove(carretPos - 1, 3);
                text.Insert(carretPos - 1, ":" + insertedChar);

                carretPos ++;
            }
            else
            {
                text.Remove(carretPos, 1);                
            }

            InputBox.Text = text.ToString();
            InputBox.CaretIndex = carretPos;

            _oldText = InputBox.Text;

            InputBox.TextChanged += OnTextChanged;
        }
    }
}
