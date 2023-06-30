using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace GameOfLife.Controller
{
    internal class PathToImg : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = System.Environment.CurrentDirectory;
            path = path.Substring(0, path.IndexOf("bin")) + @"Images\";
            
            if (value != null && !String.IsNullOrEmpty(value.ToString())){
                path = path + value;
            }
            else
            {
                path = path + "noimg.jpg";
            }

            BitmapImage bm = new BitmapImage();
            bm.BeginInit();
            bm.CacheOption = BitmapCacheOption.OnLoad;
            bm.CreateOptions = BitmapCreateOptions.DelayCreation;
            bm.UriSource = new Uri(path, UriKind.Absolute);
            bm.EndInit();
            return bm;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
