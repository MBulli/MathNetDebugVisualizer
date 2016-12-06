using FastWpfGrid;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Storage;
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

namespace MathNetVisualizer
{
    /// <summary>
    /// Interaction logic for MatrixWindow.xaml
    /// </summary>
    public partial class MatrixWindow : Window
    {
        private GridModelUntyped gridModel;
        public MatrixWindow()
        {
            InitializeComponent();
        }

        public void SetMatrix<T>(Matrix<T> matrix)
            where T : struct, IEquatable<T>, IFormattable
        {
            textBlock.Text = matrix.ToTypeString();

            this.gridModel = new GridModel<T>(matrix);
            fastGrid.Model = gridModel;

            fastGrid.AutoSizeColumns();
        }

        public void SetVector<T>(Vector<T> vector)
            where T : struct, IEquatable<T>, IFormattable
        {
            textBlock.Text = vector.ToTypeString();

            this.gridModel = new GridModel<T>(vector.ToColumnMatrix());
            fastGrid.Model = gridModel;
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            this.DisableMinimizeButton();
        }

        private void checkBoxNumberFormat_Checked(object sender, RoutedEventArgs e)
        {
            gridModel.NumberFormat = NumberFormat.LongFormat;
            fastGrid.AutoSizeColumns();
        }

        private void checkBoxNumberFormat_Unchecked(object sender, RoutedEventArgs e)
        {
            gridModel.NumberFormat = NumberFormat.ShortFormat;
            fastGrid.AutoSizeColumns();
        }
    }

    public abstract class GridModelUntyped : FastGridModelBase
    {
        NumberFormat numberFormat = NumberFormat.ShortFormat;
        public NumberFormat NumberFormat
        {
            get { return numberFormat; }
            set { numberFormat = value; InvalidateAll(); }
        }

        public override string GetEditText()
        {
            return null; // disable editing
        }
    }

    public class GridModel<T> : GridModelUntyped
        where T : struct, IEquatable<T>, IFormattable
    {
        private Matrix<T> matrix;


        public GridModel(Matrix<T> matrix)
        {
            this.matrix = matrix;
        }

        public override int ColumnCount
        {
            get { return matrix.ColumnCount; }
        }

        public override int RowCount
        {
            get { return matrix.RowCount; }
        }

        public override string GetColumnHeaderText(int column)
        {
            return column.ToString(); ;
        }

        public override string GetRowHeaderText(int row)
        {
            return row.ToString();
        }

        public override string GetCellText(int row, int column)
        {
            if (NumberFormat == NumberFormat.ShortFormat)
            {
                return matrix[row, column].ToString("0.0000", System.Globalization.CultureInfo.CurrentUICulture);
            }
            else
            {
                return matrix[row, column].ToString();
            }           
        }

        public override int RightAlignBlockCount
        {
            get
            {
                return 1;
            }
        }
    }

    public enum NumberFormat
    {
        LongFormat,
        ShortFormat,
    }
}
