using ATP.MazeGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPProject.View
{
    public delegate void ViewEventDelegate(string Command);
    interface IView
    {
        //void SetCommands(Dictionary<string, ACommand> commands);
        event ViewEventDelegate viewChanged;
        void Start();
        void Output(string output);
        //string Input();
        void DisplayMaze(AMaze maze);
        void CurrentMaze(string v);
        void DisplaySolution(string v);
        void CurrentSolution(string v);
    }
}
