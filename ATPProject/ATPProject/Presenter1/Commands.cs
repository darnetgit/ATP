using ATP.MazeGenerators;
using ATP.Search;
using ATPProject.Model1;
using ATPProject.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPProject.Presenter1
{
    class CommandLoad : ACommand
    {
        public CommandLoad(IModel model, IView view) : base(model, view)
        {
        }

        public override void DoCommand(params string[] parameters)
        {
            m_model.LoadMaze(parameters[1], parameters[2]);
            }

        public override string GetName()
        {
            return "load";
        }
    }

    class CommandSave : ACommand
    {
        public CommandSave(IModel model, IView view) : base(model, view)
        {
        }

        public override void DoCommand(params string[] parameters)
        {
            m_model.SaveMaze(parameters[1], parameters[2]);
            }

        public override string GetName()
        {
            return "save";
        }
    }


    class CommandGen : ACommand
    {
        public CommandGen(IModel model, IView view) : base(model, view)
        {
        }

        public override void DoCommand(params string[] parameters)
        {
            int x = Convert.ToInt32(parameters[2]);
            int y = Convert.ToInt32(parameters[3]);
            int z = Convert.ToInt32(parameters[4]);
            m_model.GenerateMaze(parameters[1], x, y, z);
        }

        public override string GetName()
        {
            return "generate";
        }
    }

    class CommandSolve : ACommand
    {
        public CommandSolve(IModel model, IView view) : base(model, view)
        {
        }

        public override void DoCommand(params string[] parameters)
        {
            m_model.SolveMaze(parameters[1]);
        }

        public override string GetName()
        {
            return "solve";
        }
    }
    class CommandDisplayMaze : ACommand
    {
        public CommandDisplayMaze(IModel model, IView view) : base(model, view)
        {
        }

        public override void DoCommand(params string[] parameters)
        {
            AMaze maze = m_model.GetMaze(parameters[1]);
            m_view.DisplayMaze(maze);
            m_view.CurrentMaze(parameters[1]);
        }

        public override string GetName()
        {
            return "displaymaze";
        }
    }
    class CommandDisplaySolution : ACommand
    {
        public CommandDisplaySolution(IModel model, IView view) : base(model, view)
        {
        }

        public override void DoCommand(params string[] parameters)
        {
            AMaze maze = m_model.GetMaze(parameters[1]);
            Solution solution = m_model.GetSolution(parameters[1]);
            m_view.DisplaySolution(parameters[1]);
            m_view.CurrentSolution(parameters[1]);
        }

        public override string GetName()
        {
            return "displaysolution";
        }
    }
}
