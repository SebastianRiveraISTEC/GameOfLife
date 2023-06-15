using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;

namespace GameOfLife.Controller
{
    public class MyController
    {
        public Model.GameBoard g { get; set; }
        public Cmd cmdplay { get; set; }
        public Cmd cmdcreate { get; set; }
        public Cmd cmdpause { get; set; }
        public Cmd cmdreset { get; set; }
        public Cmd cmdstep { get; set; }
        public MyController(int lowerLimit, int upperLimit, int reproduction, int maintain1, int maintain2)
        {
            Trace.WriteLine("MyController constructor called");
            g = new Model.GameBoard(lowerLimit, upperLimit, reproduction, maintain1, maintain2);
            cmdplay = new Cmd(g.btnStartOnClick, canexecute: null);
            cmdcreate = new Cmd(g.CreateBoard, canexecute: null);
            cmdpause = new Cmd(g.btnPauseOnClick, canexecute: null);
            cmdreset = new Cmd(g.btnResetOnClick, canexecute: null);
            cmdstep = new Cmd(g.btnSingleStep, canexecute: null);
        }
    }
}
