using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using GameOfLife.Model;
using System.Windows.Controls;
using System.IO;
using System.Xml.Linq;

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
        public CmdArg cmdupd { get; set; }
        public Cmd cmdquit { get; set; }

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
            cmdupd = new CmdArg(updateMatrix, (parameter) => true);
            cmdquit = new Cmd(Quit, canexecute: null);

        }

        public void Quit()
        {
            Trace.WriteLine("Shutdown called");
            Application.Current.Shutdown();
        }

        public void updateMatrix(object parameter)
        {
            if (parameter == null) return;

            delMatrix(vm.CurrentMatrix);
            addMatrix(parameter);
            //using(matrixEntities db = new matrixEntities())
            //{


            //    MatrixInstance current = vm.CurrentMatrix;
            //    MatrixImage newImageEntry = db.MatrixImages.Find(current.name);
            //    string newName = (string)parameter;
            //    newImageEntry.Name = newName;
            //    newImageEntry.path = newName + ".png";
            //    db.SaveChanges();


            //    var toDelete = db.Matrices.Where(m => m.MatrixName == newName).ToList();
            //    foreach (var m in toDelete)
            //    {
            //        db.Matrices.Remove(m);
            //    }


            //    for (int i = 0; i < GameBoard.boardSize; i++)
            //    {
            //        for (int j = 0; j < GameBoard.boardSize; j++)
            //        {
            //            if (g.board[i, j].State == true)
            //            {
            //                Matrix newMatrixEntry = new Matrix();
            //                newMatrixEntry.CellValue = "alive";
            //                newMatrixEntry.RowNo = (short)i;
            //                newMatrixEntry.ColNo = (short)j;
            //                newMatrixEntry.MatrixName = (string)parameter;
            //                db.Matrices.Add(newMatrixEntry);
            //            }
            //        }
            //    }
            //    db.SaveChanges();
            //    vm.start(0);
            //    vm.ViewMatrices.MoveCurrentToLast();
            //    g.ResetGame();
            //}
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
                if((string)parameter != "yorkie")
                {
                    g.saveCanvasImg(path);
                }
                db.MatrixImages.Add(newImageEntry);
                db.SaveChanges();
                vm.start(0);
                vm.ViewMatrices.MoveCurrentToLast();
                g.ResetGame();
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

                if(name != "yorkie")
                {
                    string path = System.Environment.CurrentDirectory;
                    path = path.Substring(0, path.IndexOf("bin"));
                    path += @"Images\";
                    path += name + ".png";

                    File.Delete(path);
                }
            }
        }
    }
}
