using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using GameOfLife.Model;

namespace GameOfLife.Controller
{
    public class MyController
    {
        ViewModel vm { get; set; }
        public Model.GameBoard g { get; set; }
        public Cmd cmdplay { get; set; }
        public Cmd cmdcreate { get; set; }
        public Cmd cmdpause { get; set; }
        public Cmd cmdreset { get; set; }
        public Cmd cmdstep { get; set; }
        public CmdArg cmdload { get; set; }
        public CmdArg cmdadd { get; set; }
        public CmdArg cmddel { get; set; }

        public MyController(int lowerLimit, int upperLimit, int reproduction, int maintain1, int maintain2)
        {
            Trace.WriteLine("MyController constructor called");
            vm = Application.Current.Resources["vm"] as ViewModel;
            g = new Model.GameBoard(lowerLimit, upperLimit, reproduction, maintain1, maintain2);
            cmdplay = new Cmd(g.btnStartOnClick, canexecute: null);
            cmdcreate = new Cmd(g.CreateBoard, canexecute: null);
            cmdpause = new Cmd(g.btnPauseOnClick, canexecute: null);
            cmdreset = new Cmd(g.btnResetOnClick, canexecute: null);
            cmdstep = new Cmd(g.btnSingleStep, canexecute: null);
            cmdload = new CmdArg(g.btnLoadBoard, (parameter) => true);
            cmdadd = new CmdArg(addMatrix, (parameter) => true);
            cmddel = new CmdArg(delMatrix, (parameter) => true);
        }

        public void updateMatrix(object parameter)
        {
            if (parameter == null) return;
            using(matrixEntities db = new matrixEntities())
            {
                MatrixInstance current = parameter as MatrixInstance;
                Cell[,] currMatrix = current.matrix;
                string name = current.name;
                string path = current.imgLocation;



            }
        }

        public void addMatrix(object parameter)
        {
            using (matrixEntities db = new matrixEntities())
            {
                string path = (string)parameter + ".png";
                MatrixImage newImageEntry = new MatrixImage();
                Matrix newMatrixEntry;

                for(int i =0;i< GameBoard.boardSize;i++)
                {
                    for(int j =0;j< GameBoard.boardSize; j++)
                    {
                        if (g.board[i,j].State == true)
                        {
                            newMatrixEntry = new Matrix();
                            newMatrixEntry.CellValue = "alive";
                            newMatrixEntry.RowNo = (short)i;
                            newMatrixEntry.ColNo = (short)j;
                            newMatrixEntry.MatrixName = (string)parameter;
                            db.Matrices.Add(newMatrixEntry);
                        }
                    }
                }
                newImageEntry.Name = (string)parameter;
                newImageEntry.path = path;
                db.MatrixImages.Add(newImageEntry);
                db.SaveChanges();
                vm.start(0);
                vm.ViewMatrices.MoveCurrentToLast();
            }
        }

        public void delMatrix(object parameter)
        {
            if (parameter == null) return;
            using (matrixEntities db = new matrixEntities())
            {
                MatrixInstance current = parameter as MatrixInstance;
                string name = current.name;

                MatrixImage imgToDelete = db.MatrixImages.Find(current.name);
                db.MatrixImages.Remove(imgToDelete);

                var toDelete = db.Matrices.Where(m => m.MatrixName == name).ToList();
                //Matrix toDelete = db.Matrices.Find(name);

                foreach( var m in toDelete )
                {
                    db.Matrices.Remove(m);
                }
                db.SaveChanges();
                vm.start(0);
            }
        }
    }
}
