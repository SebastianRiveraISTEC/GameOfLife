using GameOfLife.Controller;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Add()
        {
            InitializeComponent();
            Loaded += Add_Loaded;
        }
        private void Add_Loaded(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("Called MainGame_Loaded");
            //Trace.WriteLine(typeof(Resources["controller"]));
            if (mainGrid.DataContext is MyController controller)
            {
                Trace.WriteLine("ENTERS AND CALLS FUNCTION");
                controller.cmdcreate.Execute(null);
            }
        }
        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            mtrxName.Text = string.Empty;
        }
    }
}

