using ATP.MazeGenerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPProject.View
{
    class View : IView
    {
        //public event viewEventDelegate ViewChanged;
        private Stream m_input = Console.OpenStandardInput();
        private Stream m_output = Console.OpenStandardOutput();
        //private Dictionary<string, ACommand> m_commands;
        private string m_cursor = ">>";

        public event ViewEventDelegate viewChanged;

        public View()
        {
            //To output to files
            //m_output = new FileStream(@"d:\output.txt",FileMode.Create);
        }
        public void SetInput(Stream input)
        {
            m_input = input;
        }

        public void SetOutput(Stream output)
        {
            m_output = output;
        }

        /*public void SetCommands(Dictionary<string, ACommand> commands)
        {
            m_commands = commands;
        }*/

        public void Start()
        {
            PrintInstructions();

            string userCommand;
            string[] splitedCommand;
            string @operator;
            while (true)
            {
                Output("");
                try
                {
                    userCommand = Input().Trim();
                    if (userCommand == "exit") { break; }

                    splitedCommand = userCommand.Split(' ');
                    @operator = splitedCommand[0].Trim();

                    /*if (!m_commands.ContainsKey(@operator.ToLower()) /*|| splitedCommand.Length != 3)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        m_commands[@operator].DoCommand(splitedCommand);
                    }*/
                }
                catch (Exception)
                {
                    Output("Unrecognized command!");
                }
            }
        }

        private static void PrintInstructions()
        {
            Console.WriteLine("Command Line Interface (CLI) started!");
            Console.WriteLine("");
            Console.WriteLine("Enter calculation in '[operator] X Y' format, for example 'sum 3 5' or 'mult 6 8'.");
            Console.WriteLine(String.Format("Available operators:{0}sum (summation){0}sub (substraction){0}div (division){0}mult (multiplication){0}pow (power){0}save <path>{0}load <path>", "\n"));
            Console.WriteLine("");
            Console.WriteLine("Press 'exit' to finish.");
        }

        public void Output(string output)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            StreamWriter streamWriter = new StreamWriter(m_output);
            streamWriter.AutoFlush = true;
            Console.SetCursorPosition(0, Console.CursorTop);
            streamWriter.WriteLine(output);
            streamWriter.WriteLine("");
            streamWriter.Write(m_cursor);
            Console.ResetColor();
        }

        public string Input()
        {
            StreamReader streamReader = new StreamReader(m_input);
            return streamReader.ReadLine();
        }

        public void DisplayMaze(AMaze maze)
        {
            throw new NotImplementedException();
        }

        public void CurrentMaze(string v)
        {
            throw new NotImplementedException();
        }

        public void DisplaySolution(string v)
        {
            throw new NotImplementedException();
        }

        public void CurrentSolution(string v)
        {
            throw new NotImplementedException();
        }
    }
}
