using ATP.MazeGenerators;
using ATPProject.Presenter1;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ATPProject.View
{
    /// <summary>
    /// Interaction logic for PlayWindow.xaml
    /// </summary>
    public partial class PlayWindow : Window, IView
    {
        private ACommand currentcommand;
        private string mazename;

        public event ViewEventDelegate viewChanged;

        public PlayWindow()
        {
            InitializeComponent();
            mazename = "dar";
        }
        private void New_Click(object sender, RoutedEventArgs e)
        {
            generate_Click(sender, e);
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.maze)|*.maze";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, mazename);
                viewChanged("save " + mazename + " " + saveFileDialog.FileName);
            }
        }
        private void Save1_Click(object sender, RoutedEventArgs e)
        {
            Save_Click(sender, e);
        }
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text file (*.maze)|*.maze";
            string ans="";
            if (openFileDialog.ShowDialog() == true)
            {
                string s = openFileDialog.FileName;
                int index=s.LastIndexOf("\\");
                ans = s.Substring(index+1);
                ans = ans.Substring(0, ans.Length - 5);
                mazename = ans;
                viewChanged("load " + mazename + " " + openFileDialog.FileName);
            }
                
        }
        private void Load1_Click(object sender, RoutedEventArgs e)
        {
            Load_Click(sender, e);
        }
        private void generate_Click(object sender, RoutedEventArgs e)
        {
            Generate gen = new Generate();
            gen.ShowDialog();
            if (gen.cangenerate)
            {
                string s = "generate " + gen.m_mazename + " " + gen.m_rows + " " + gen.m_columns + " " + gen.m_floors;
                this.viewChanged(s);
            }
        }

        public void Start()
        {
            Show();
        }

        public void CurrentMaze(string v)
        {
            mazename=v;
        }

        public void DisplaySolution(string v)
        {
            throw new NotImplementedException();
        }

        public void CurrentSolution(string v)
        {
            throw new NotImplementedException();
        }

        public void Output(string output)
        {
            MessageBox.Show(output);
        }

        private void displaymaze_Click(object sender, RoutedEventArgs e)
        {
            //JUST FOR NOW!!!!!!
            viewChanged("displaymaze " + mazename);
        }
        public void DisplayMaze(AMaze maze)
        {
            MazeBoard mb = new MazeBoard(maze, 9);
            for (int i = 0; i < (int)maze.sizes[0]; i++)
            {
                this.maze_cnvs.Children.Clear();
                this.maze_cnvs.Children.Add(mb);
            }

        }
    }
}
