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
    /// Provide custom time input.
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _oldText;
        private string _template = "__:__";

        public MainWindow()
        {
            InitializeComponent();

            InputBox.Text = _template;
            _oldText = _template;

            InputBox.TextChanged += OnTextChanged;
        }


        private void OnTextChanged (object sender, TextChangedEventArgs e)
        {
            InputBox.TextChanged -= OnTextChanged;      //Remove this event

            int carretPos = InputBox.CaretIndex;
            StringBuilder text = new StringBuilder(InputBox.Text);

            string insertedChar = text[carretPos - 1].ToString();
            int insertedNum;
            bool isNumber = Int32.TryParse(insertedChar, out insertedNum);

            if (carretPos > _template.Length || !isNumber)     //If carret in last pos OR inserted char not number, then abort
            {
                InputBox.Text = _oldText;
                InputBox.CaretIndex = carretPos - 1;

                InputBox.TextChanged += OnTextChanged;
                return;
            }

            if (text[carretPos].ToString() == ":")      //If carret right above ':' char, then remove inserted char and place him after
            {
                text.Remove(carretPos - 1, 3);
                text.Insert(carretPos - 1, ":" + insertedChar);

                carretPos ++;
            }
            else    //Standart behavior of program
            {
                text.Remove(carretPos, 1);                
            }

            InputBox.Text = text.ToString();    //Replace old string with new one
            InputBox.CaretIndex = carretPos;

            _oldText = InputBox.Text;       //Mem current standing

            InputBox.TextChanged += OnTextChanged;      //Return event
        }
    }
}
