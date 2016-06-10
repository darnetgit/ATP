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
using System.Windows.Shapes;

namespace ATPProject.View
{
    /// <summary>
    /// Interaction logic for Generate.xaml
    /// </summary>
    public partial class Generate : Window
    {
        private string m_Mazename, m_Rows, m_Columns, m_Floors;
        bool Cangenerate;
        public string m_mazename
        {
            get { return m_Mazename; }
            set { m_Mazename = value; }
        }
        public string m_rows
        {
            get { return m_Rows; }
            set { m_Rows = value; }
        }
        public string m_columns
        {
            get { return m_Columns; }
            set { m_Columns = value; }
        }
        public string m_floors
        {
            get { return m_Floors; }
            set { m_Floors = value; }
        }
        public bool cangenerate
        {
            get { return Cangenerate; }
            set { Cangenerate = value; }
        }
        public Generate()
        {
            InitializeComponent();
            cangenerate = false;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void generate_Click(object sender, RoutedEventArgs e)
        {
            m_rows = Trows.Text.Trim();
            m_columns = Tcolumns.Text.Trim();
            m_floors = Tfloors.Text.Trim();
            m_mazename = Tname.Text.Trim();
            int x, y, z;
            if (m_rows == "" || m_columns == "" || m_floors == "" || m_mazename == "")
            {
                MessageBox.Show("You must entet all parameters!", "Error");
            }
            else if (!Int32.TryParse(m_rows, out x) || !Int32.TryParse(m_columns, out y) || !Int32.TryParse(m_floors, out z))
                MessageBox.Show("You must enter numbers only!", "Error");
            else if (x <= 2 || y <= 2 || z <= 0)
                MessageBox.Show("Rows must be more then 2! \n Columns must be more then 2! \n Floors must be more then 0!", "Error");
            else
            {
                this.cangenerate = true;
                base.Close();
            }
        }
    }
}
