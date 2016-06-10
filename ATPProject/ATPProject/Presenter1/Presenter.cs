using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATP.MazeGenerators;
using ATPProject.Model1;
using ATPProject.View;
using ATPProject.Presenter1;

namespace ATPProject.Presenter1
{
    class Presenter
    {
        private IModel m_model;
        private IView m_view;
        private Dictionary<string, ACommand> m_commands;
        public Presenter(IModel model, IView view)
        {
            m_model = model;
            m_view = view;
            GetEvent();
            GetCommands();
            //SetEvents();
            int nuofthreads = Settings.Default.Threads;
            string solvingalgo = Settings.Default.SolvingAlgorithm;
            string generatealgo = Settings.Default.Generate;
        }

        private Dictionary<string, ACommand> GetCommands()
        {
            m_commands = new Dictionary<string, ACommand>();
            ACommand Generate = new CommandGen(m_model, m_view);
            ACommand DisplayMaze = new CommandDisplayMaze(m_model, m_view);
            ACommand DisplaySolution = new CommandDisplaySolution(m_model, m_view);
            ACommand Solve = new CommandSolve(m_model, m_view);
            ACommand Save = new CommandSave(m_model, m_view);
            ACommand Load = new CommandLoad(m_model, m_view);
            m_commands.Add(Generate.GetName(), Generate);
            m_commands.Add(DisplayMaze.GetName(), DisplayMaze);
            m_commands.Add(Solve.GetName(), Solve);
            m_commands.Add(DisplaySolution.GetName(), DisplaySolution);
            m_commands.Add(Save.GetName(), Save);
            m_commands.Add(Load.GetName(), Load);
            return m_commands;
        }

        private void SetEvents()
        {
            m_model.Event += RetriveSolution;
        }
        private void RetriveSolution(string mazename)
        {
            string s = m_model.printsolution(mazename);
            m_view.Output("solution for maze '" + mazename.ToLower() + "' \n" + s);
        }
        private void RetriveSolution(string mazename, int x, int y, int z)
        {
            AMaze maze = m_model.GetMaze(mazename);
            if (maze != null)
                m_view.Output("maze generated!");
        }
        private void GetEvent()
        {
            m_view.viewChanged += new ViewEventDelegate((string viewEvent) =>
            {
                string s = viewEvent;
                string[] splitted = s.Split(' ');
                m_commands[splitted[0]].DoCommand(splitted);
            });
            m_model.Event += new ModelEventDelegate((string modelEvent) =>
              {
                  string s = modelEvent;
                  string[] splitted = s.Split(' ');
                  if (s != null)
                  {
                      switch (splitted[0])
                      {
                          case "SolutionExist":
                              m_view.Output("Solution for " + splitted[1].ToLower() + " already exist!");
                              break;

                          case "MazeNotExist":
                              m_view.Output("Maze '" + splitted[1].ToLower() + "' does not exist!");
                              break;

                          case "MazeExist":
                              m_view.Output("Maze '" + splitted[1].ToLower() + "' already exist!");
                              break;

                          case "MazeSaved":
                              m_view.Output("Maze '" + splitted[1].ToLower() + "' was saved\n do we want to show this???");
                              break;

                          case "SolutionReady":
                              m_view.Output("Solution for '" + splitted[1].ToLower() + "' is ready!");
                              break;

                          case "MazeReady":
                              m_view.Output("Maze '" + splitted[1].ToLower() + "' is ready!");
                              break;

                          case "UnzipError":
                              m_view.Output("Error while unziping");
                              break;

                          case "ZipError":
                              m_view.Output("Error while ziping");
                              break;

                          case "DisplayMaze":
                             // m_view.DisplayMaze(splitted[1].ToLower());
                              break;

                          case "DisplayError":
                              m_view.Output("Error accurred while trying to display '" + splitted[1].ToLower() + "'");
                              break;
                      }
                  }
              });
        }
    }
}
