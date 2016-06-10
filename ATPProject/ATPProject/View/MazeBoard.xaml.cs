using ATP.MazeGenerators;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ATPProject.View
{
    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class MazeBoard : UserControl
    {
        MazeCell[,,] m_cells;
        public MazeBoard(AMaze mazename, int mazeCellSize)
        {
            InitializeComponent();
            
            CreateMaze(mazename, mazeCellSize);
        }

        public MazeCell this[int i, int j, int k]
        {
            get
            {
                return m_cells[i, j,k];
            }
        }
        private void CreateMaze(AMaze mazename, int mazeCellSize)
        {
            m_cells = new MazeCell[(mazename as Maze3d).maze3d.Length,(int)mazename.sizes[0], (int)mazename.sizes[1]];
            for (int i = 0; i < (mazename as Maze3d).maze3d.Length; i++)
            {
                for (int j = 0; j < (int)mazename.sizes[0]; j++)
                {
                    for (int k = 0; k < (int)mazename.sizes[1]; k++)
                    {
                        if ((mazename as Maze3d).maze3d[i].maze2d[j, k] == 1 || (mazename as Maze3d).maze3d[i].maze2d[j, k] == 2)
                            m_cells[i,j,k] = new MazeCell(mazeCellSize, false);
                        else
                            m_cells[i,j, k] = new MazeCell(mazeCellSize, true);
                        mazeBoard.Children.Add(m_cells[i,j, k]);
                        Canvas.SetLeft(m_cells[i,j, k], mazeCellSize * j);
                        Canvas.SetTop(m_cells[i,j, k], mazeCellSize * k);
                    }
                }

            }
        }
    }
}
