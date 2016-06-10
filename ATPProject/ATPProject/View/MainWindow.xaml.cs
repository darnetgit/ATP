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
using ATP.MazeGenerators;
using ATP.Search;
using Microsoft.Win32;

namespace ATPProject.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        //private Dictionary<string, List<Object>> m_mazesandsolutions;
        private string currentMaze;
        private string currentSolution;
        string name = "name";

        public MainWindow()
        {
            InitializeComponent();
            currentMaze = "";

        }

        public event ViewEventDelegate viewChanged;

        public void CurrentMaze(string v)
        {
            throw new NotImplementedException();
        }

        public void CurrentSolution(string v)
        {
            throw new NotImplementedException();
        }

        public void DisplayMaze(AMaze maze)
        {
            throw new NotImplementedException();
        }

        public void DisplaySolution(string v)
        {
            throw new NotImplementedException();
        }

        public void Output(string output)
        {
            //MessageBox.Show(output);
        }

        public void Start()
        {
            Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.maze)|*.maze";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, name);

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text file (*.maze)|*.maze";
            if (openFileDialog.ShowDialog() == true)
                name = File.ReadAllText(openFileDialog.FileName);
        }
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            PlayWindow playwindow = new PlayWindow();
            playwindow.Start();
            Close();
        }
    }
}
