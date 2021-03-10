using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace First
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button1_OnClick(object sender, RoutedEventArgs e)
        {
            int number;
            IEnumerable<int> permutations;
            
            try
            {
                if (TextBox1.Text.Length == 0 || TextBox2.Text.Trim().Length == 0)
                    throw new ArgumentNullException();
                if (TextBox2.Text.Any(char.IsLetter))
                    throw new ArgumentException();
                number = Convert.ToInt32(TextBox1.Text);
                permutations = TextBox2.Text.Trim().Split(' ').
                    Select(s => Convert.ToInt32(s)).ToArray();

                var lengthOfNum =
                    number.ToString().Select(c => Convert.ToInt32(char.GetNumericValue(c))).ToArray().Length;
                
                if (permutations.Any(index => index >= lengthOfNum || index < 0))
                    throw new ArgumentOutOfRangeException();
            }
            catch (FormatException)
            {
                ExceptionOnInput("Only integer numbers");
                return;
            }
            catch (OverflowException)
            {
                ExceptionOnInput("Integer overflow");
                return;
            }
            catch (ArgumentNullException)
            {
                ExceptionOnInput("No value");
                return;
            }
            catch (ArgumentException)
            {
                ExceptionOnInput("Incorrect value");
                return;
            }
            
            TextBox3.Text = Permute(number, permutations).ToString();
            TextBox1.Clear(); 
            TextBox2.Clear();
        }

        private static int Permute(int number, IEnumerable<int> permutations)
        {
            var digitsOfNumber = new List<int>();
            
            while (true)
            {
                var remainder = number % 10;
                digitsOfNumber.Add(remainder);
                if (remainder == number) break;
                number /= 10;
            }

            digitsOfNumber.Reverse();
            var permutationsArray = permutations.ToArray();
            var result = 0;

            foreach (var index in permutationsArray)
            {
                result += digitsOfNumber[index];
                result *= 10;
            }
            
            return result / 10;
        }

        private void ExceptionOnInput(string message)
        {
            TextBox1.Clear();
            TextBox2.Clear();
            TextBox3.Text = message;
        }
    }
}