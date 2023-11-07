using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatrixWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            double[,] matrix1 = RandomMatrix(10, 0);
            double[,] matrix2 = RandomMatrix(10, 0);
            initializeGrid(Grid1, matrix1);
            initializeGrid(Grid2, matrix2);
            initializeGrid(Grid3, MultiplyMatrix(matrix1, matrix2));
        }

        private double[,] RandomMatrix(int max, int min) => RandomMatrix(3, 3, max, min);

        private double[,] RandomMatrix(int rows, int cols, int max, int min)
        {
            double[,] matrix = new double[cols, rows];
            for (int x = 0; x < matrix.GetLength(0); x++)
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    Random rand = new Random((int)DateTime.UtcNow.Ticks);
                    matrix[x, y] = rand.Next(max - min) + min;
                }
            return matrix;
        }

        public double[,] MultiplyMatrix(double[,] A, double[,] B)
        {
            var r = new double[A.GetLength(0), B.GetLength(1)];

            if (A.GetLength(0) != B.GetLength(1))
            {
                MessageBox.Show("matrices have incompatible dimensions");
                return r;
            }
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < B.GetLength(1); j++)
                    for (int k = 0; k < B.GetLength(0); k++)
                        r[i, j] += A[i, k] * B[k, j];
            return r;
        }



        private void initializeGrid(Grid grid, double[,] matrix)
        {
            if (grid == null) return;
            // Reset the grid before doing anything
            grid.Children.Clear();
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();
            // Get the dimensions
            int columns = matrix.GetLength(0);
            int rows = matrix.GetLength(1);
            // Add the correct number of coumns to the grid
            for (int x = 0; x < columns; x++)
                grid.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(1, GridUnitType.Star),
                });

            for (int y = 0; y < rows; y++)
                // GridUnitType.Star - The value is expressed as a weighted proportion of available space
                grid.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new
                GridLength(1, GridUnitType.Star),
                });
            // Fill each cell of the grid with an editable TextBox containing the value from the matrix
            for (int x = 0; x < columns; x++)
                for (int y = 0; y < rows; y++)
                {
                    double cell = (double)matrix[x, y];
                    TextBox t = new TextBox();
                    t.Text = cell.ToString();
                    t.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    t.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    t.SetValue(Grid.RowProperty, y);
                    t.SetValue(Grid.ColumnProperty, x);
                    grid.Children.Add(t);
                }
        }
        
        private double[,] getValuesFromGrid(Grid grid)
        {
            int columns = grid.ColumnDefinitions.Count;
            int rows = grid.RowDefinitions.Count;
            double[,] matrix = new double[columns, rows];
            // Iterate over cells in Grid, copying to matrix array
            for (int c = 0; c < grid.Children.Count; c++)
            {
                TextBox t = (TextBox)grid.Children[c];
                int row = Grid.GetRow(t);
                int column = Grid.GetColumn(t);
                matrix[column, row] = double.Parse(t.Text);
            }
            return matrix;
        }

        private void MultiplyBtn_Click(object sender, RoutedEventArgs e)
        {
            var matrix1 = getValuesFromGrid(Grid1);
            var matrix2 = getValuesFromGrid(Grid2);
            initializeGrid(Grid3, MultiplyMatrix(matrix1, matrix2));
        }

        private void CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var mat1Cols = (int)(Mat1Cols?.SelectedItem ?? 3);
            var mat1Rows = (int)(Mat1Rows?.SelectedItem ?? 3);
            var mat2Cols = (int)(Mat2Cols?.SelectedItem ?? 3);

            var matrix1 = RandomMatrix(mat1Rows, mat1Cols, 10, 0);
            var matrix2 = RandomMatrix(mat1Rows, mat2Cols, 10, 0);

            initializeGrid(Grid1, matrix1);
            initializeGrid(Grid2, matrix2);
            initializeGrid(Grid3, MultiplyMatrix(matrix1, matrix2));
        }
    }
}
