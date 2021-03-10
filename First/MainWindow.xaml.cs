﻿using System;
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
                permutations = TextBox2.Text.Trim().Split(' ').Select(s => Convert.ToInt32(s));
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
            
            try
            {
                TextBox3.Text = Permute(number, permutations).ToString();
                TextBox1.Clear(); 
                TextBox2.Clear();
            }
            catch (ArgumentOutOfRangeException)
            {
                ExceptionOnInput("Out of range index");
            }
        }

        private static int Permute(int number, IEnumerable<int> permutations)
        {
            var charsOfNumber = 
                number.ToString().Select(c => Convert.ToInt32(char.GetNumericValue(c))).ToArray();
            var permutationsArray = permutations.ToArray();

            if (permutationsArray.Any(index => index >= charsOfNumber.Length || index < 0))
                throw new ArgumentOutOfRangeException();
            
            var resultStr = permutationsArray.
                Aggregate<int, string>(default, (current, index) => current + charsOfNumber[index]);
            
            return Convert.ToInt32(resultStr);
        }

        private void ExceptionOnInput(string message)
        {
            TextBox1.Clear();
            TextBox2.Clear();
            TextBox3.Text = message;
        }
    }
}