using GameOfLife.Controller;
using GameOfLife.view;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace GameOfLife.Model
{
    public class MatrixInstance : UIElement
    {
        public MatrixInstance() { }
        //public string imgLocation = "";



        public Cell[,] matrix
        {
            get { return (Cell[,])GetValue(matrixProperty); }
            set { SetValue(matrixProperty, value); }
        }

        // Using a DependencyProperty as the backing store for matrix.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty matrixProperty =
            DependencyProperty.Register("matrix", typeof(Cell[,]), typeof(MatrixInstance), new PropertyMetadata(new Cell[GameBoard.boardSize, GameBoard.boardSize]));



        public string name
        {
            get { return (string)GetValue(nameProperty); }
            set { SetValue(nameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for name.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty nameProperty =
            DependencyProperty.Register("name", typeof(string), typeof(MatrixInstance), new PropertyMetadata(""));



        public string imgLocation
        {
            get { return (string)GetValue(imgLocationProperty); }
            set { SetValue(imgLocationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for imgLocation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty imgLocationProperty =
            DependencyProperty.Register("imgLocation", typeof(string), typeof(MatrixInstance), new PropertyMetadata(""));



    }

    public class ViewModel : UIElement
    {
        public MyController ctrl { get; set; }

        #region Propriedades

        public ObservableCollection<MatrixInstance> Matrices
        {
            get { return (ObservableCollection<MatrixInstance>)GetValue(MatricesProperty); }
            set { SetValue(MatricesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Matrices.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MatricesProperty =
            DependencyProperty.Register("Matrices", typeof(ObservableCollection<MatrixInstance>), typeof(ViewModel), new PropertyMetadata(null));


        public ICollectionView ViewMatrices
        {
            get { return (ICollectionView)GetValue(ViewMatricesProperty); }
            set { SetValue(ViewMatricesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewMatrices.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewMatricesProperty =
            DependencyProperty.Register("ViewMatrices", typeof(ICollectionView), typeof(ViewModel), new PropertyMetadata(null));


        public MatrixInstance CurrentMatrix
        {
            get { return (MatrixInstance)GetValue(CurrentMatrixProperty); }
            set { SetValue(CurrentMatrixProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentMatrix.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentMatrixProperty =
            DependencyProperty.Register("CurrentMatrix", typeof(MatrixInstance), typeof(ViewModel), new PropertyMetadata(null));




        public PageHandler Ph
        {
            get { return (PageHandler)GetValue(PhProperty); }
            set { SetValue(PhProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ph.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PhProperty =
            DependencyProperty.Register("Ph", typeof(PageHandler), typeof(ViewModel), new PropertyMetadata(null));


        #endregion

        public ViewModel()
        {
            start(0);
            Ph = new PageHandler();
            Frame frame = Application.Current.MainWindow.FindName("frm") as Frame;
            if (frame != null && frame.Content is Page mainPage) 
            { 
                Trace.WriteLine("frame was not null");
                ctrl = mainPage.Resources["ctrl"] as MyController;
            }
        }

        public void start(int pos)
        {
            using (matrixEntities db = new matrixEntities())
            {

                List<Matrix> _matrices = db.Matrices.ToList();
                List<MatrixImage> _images = db.MatrixImages.ToList();

                List<MatrixInstance> mtrxs = DBtoBoard.Convert(_matrices, _images, 102);
                //Trace.WriteLine(mtrxs);
                Matrices = new ObservableCollection<MatrixInstance>(mtrxs);
                ViewMatrices = CollectionViewSource.GetDefaultView(Matrices);
                ViewMatrices.MoveCurrentToPosition(pos);
                ViewMatrices.CurrentChanged += ViewMatrices_CurrentChanged;
                ViewMatrices.CurrentChanging += ViewMatrices_CurrentChanging;
                CurrentMatrix = ViewMatrices.CurrentItem as MatrixInstance;
            }
        }

        private void ViewMatrices_CurrentChanging(object sender, CurrentChangingEventArgs e)
        {
            //ctrl.updateMatrix(CurrentMatrix);
        }

        private void ViewMatrices_CurrentChanged(object sender, EventArgs e)
        {
            CurrentMatrix = ViewMatrices.CurrentItem as MatrixInstance;
        }
    }
}
