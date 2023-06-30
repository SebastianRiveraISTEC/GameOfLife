using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameOfLife.Model
{
    public static class DBtoBoard
    {
        public static List<MatrixInstance> Convert(List<Matrix> listFromDB, List<MatrixImage> imgsFromDB, int boardSize)
        {
            var names = listFromDB.Select(x => x.MatrixName).Distinct().ToList();

            List<MatrixInstance> ans = new List<MatrixInstance>();

            foreach (var name in names)
            {
                MatrixInstance tmp = new MatrixInstance();
                tmp.name = name;
                
                var coordinates = listFromDB.Where(x => x.MatrixName == name);
                var img = imgsFromDB.Where(x => x.Name == name).FirstOrDefault();
                tmp.imgLocation = img.path;

                Cell[,] mtrx = new Cell[boardSize, boardSize];
                for (int i = 0; i < boardSize; i++)
                {
                    for (int j = 0; j < boardSize; j++)
                    {
                        mtrx[i, j] = new Cell();
                    }
                }

                foreach (var coord in coordinates)
                {
                    int i = coord.RowNo;
                    int j = coord.ColNo;

                    if (i >= boardSize-1 || j >= boardSize-1 || i==0 || j==0)
                    {
                        throw new Exception("Matrix doesn't fit board");
                    }
                    if (coord.CellValue == "alive")
                    {
                        mtrx[i, j].State = true;
                    }
                }
                tmp.matrix = mtrx;
                ans.Add(tmp);
            }
            return ans;
        }
    }
}
