using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Model
{
    public static class DBtoBoard
    {
        static public Cell[,] Convert(Object[] listFromDB, int boardSize)
        {
            Cell[,] ans = new Cell[boardSize, boardSize];

            for (int i=0 ; i< boardSize; i++ )
            {
                for(int j=0 ; j< boardSize;  j++ )
                {
                    ans[i, j] = new Cell();
                }
            }

            foreach (var coord in listFromDB)
            {
                int i = ((Matrix)coord).RowNo;
                int j = ((Matrix)coord).ColNo;
                if(i > boardSize || j > boardSize)
                {
                    throw new Exception("Matrix doesn't fit board");
                }
                if (((Matrix)coord).CellValue == "alive") 
                {
                    ans[i, j].State = true;
                }
            }

            return ans;
        }  
    }
}
