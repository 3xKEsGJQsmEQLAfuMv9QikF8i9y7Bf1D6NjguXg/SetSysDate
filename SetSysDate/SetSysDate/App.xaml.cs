using System;
using System.Globalization;
using System.Windows;

namespace SetSysDate
{
    public partial class App : Application
    {
        public App()
        {
            const string DefaultLang = "en-US";

            InitializeComponent();

            var rd = new ResourceDictionary();
            string lang = CultureInfo.CurrentUICulture.Name == "ja-JP" ? "ja-JP" : DefaultLang;
            rd.Source = new Uri($"/SetSysDate;component/Resources/{lang}.xaml", UriKind.Relative);
            Resources.MergedDictionaries[0] = rd;
        }

        public static string GetRes(string key)
        {
            return Current.Resources[key].ToString();
        }
    }
}
