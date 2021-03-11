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
            uint number;
            IEnumerable<byte> permutations;
            
            try
            {
                if (TextBox1.Text.Length == 0 || TextBox2.Text.Trim().Length == 0)
                    throw new ArgumentNullException();
                if (TextBox2.Text.Any(char.IsLetter))
                    throw new ArgumentException();
                number = Convert.ToUInt32(TextBox1.Text);
                permutations = TextBox2.Text.Trim().Split(' ').
                    Select(s => Convert.ToByte(s)).ToArray();
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

        private static uint Permute(uint value, IEnumerable<byte> colPerm)
        {
            uint result = 0;
            var permRule = colPerm.ToArray();

            for (var i = 0; i < permRule.Length; i++)
            {
                var currentIndex = permRule.Length - i - 1;
                result <<= 1;
                result |= (value >> (permRule[currentIndex] - 1)) & 1;
            }

            return result;
        }

        private void ExceptionOnInput(string message)
        {
            TextBox1.Clear();
            TextBox2.Clear();
            TextBox3.Text = message;
        }
    }
}