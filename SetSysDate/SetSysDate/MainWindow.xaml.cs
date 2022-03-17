using Microsoft.VisualBasic;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SetSysDate
{
    public partial class MainWindow : Window
    {
        private static readonly Regex _numPattern = new Regex("^.(?<num>[0-9]+).$");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(sender is Button btn))
                {
                    return;
                }

                SetSystemDate(btn.Tag.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetSystemDate(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                throw new Exception(App.GetRes("MsgTagErr1"));
            }

            int sign;
            if (tag.Substring(0, 1) == "-")
            {
                sign = -1;
            }
            else if (tag.Substring(0, 1) == "+")
            {
                sign = 1;
            }
            else
            {
                throw new Exception(App.GetRes("MsgTagErr2"));
            }

            var m = _numPattern.Match(tag);
            if (!m.Success)
            {
                throw new Exception(App.GetRes("MsgTagErr3"));
            }
            int num = int.Parse(m.Groups["num"].Value);

            DateTime d;
            switch (tag.Substring(tag.Length - 1, 1))
            {
                case "H":
                    d = DateTime.Now.AddHours(sign * num);
                    break;
                case "D":
                    d = DateTime.Now.AddDays(sign * num);
                    break;
                case "M":
                    d = DateTime.Now.AddMonths(sign * num);
                    break;
                case "Y":
                    d = DateTime.Now.AddYears(sign * num);
                    break;
                default:
                    throw new Exception(App.GetRes("MsgTagErr4"));
            }

            DateAndTime.TimeOfDay = d;
            DateAndTime.Today = d;

            SetResultDateText(d);
        }

        private void SetResultDateText(DateTime d)
        {
            ResultYear.Text = $"{d.Year}";
            ResultMonth.Text = $"{d.Month}";
            ResultDay.Text = $"{d.Day}";
            ResultHours.Text = $"{d.Hour}";
            ResultMinutes.Text = $"{d.Minute:D2}";
            try
            {
                var sb = FindResource("ResultTextAnimation") as Storyboard;
                sb?.Begin();
            }
            catch
            {
            }
        }
    }
}
