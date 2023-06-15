using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace GameOfLife.Model
{
    public class Cell
    {
        public Rectangle Shape { get; set; }
        public bool State { get; set; }
        public bool _NextState;
        public bool NextState
        {
            get { return _NextState; }
            set
            {
                _NextState = value;
            }
        }
        public Cell()
        {
            Shape = new Rectangle();
            State = false;
            //            Shape.MouseLeftButtonDown += 
        }
        public bool isBorder { get; set; }

        public void Update()
        {
            State = NextState;
        }
    }
}
