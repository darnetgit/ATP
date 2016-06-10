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
    /// Interaction logic for MazeCell.xaml
    /// </summary>
    public partial class MazeCell : UserControl
    {
        public MazeCell(int mazeCellSize, bool visible)
        {
            InitializeComponent();

            userControl.Width = mazeCellSize;

            cell.Visibility = (visible) ? Visibility.Visible : Visibility.Hidden;
            
        }
    }
}
