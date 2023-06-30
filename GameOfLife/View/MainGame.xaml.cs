using GameOfLife.Controller;
using GameOfLife.Properties;
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
    /// Interaction logic for MainGame.xaml
    /// </summary>
    public partial class MainGame : Page
    {
        public MainGame()
        {
            InitializeComponent();
            Loaded += MainGame_Loaded;
        }
        private void MainGame_Loaded(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("Called MainGame_Loaded");
            //Trace.WriteLine(typeof(Resources["controller"]));
            if (mainGrid.DataContext is MyController controller)
            {
                Trace.WriteLine("ENTERS AND CALLS FUNCTION");
                controller.cmdcreate.Execute(null);
            }
        }
    }
}
