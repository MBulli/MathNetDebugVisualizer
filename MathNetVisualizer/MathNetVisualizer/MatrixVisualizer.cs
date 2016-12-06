using MathNet.Numerics.LinearAlgebra;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: DebuggerVisualizer(
            typeof(MathNetVisualizer.MatrixVisualizer),
            typeof(VisualizerObjectSource),
            Target = typeof(Matrix<>),
            Description = "MathNet Matrix Visualizer")]

[assembly: DebuggerVisualizer(
            typeof(MathNetVisualizer.MatrixVisualizer),
            typeof(VisualizerObjectSource),
            Target = typeof(Vector<>),
            Description = "MathNet Vector Visualizer")]

namespace MathNetVisualizer
{
    public class MatrixVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            var obj = objectProvider.GetObject();

            MatrixWindow window = new MatrixWindow();
            SetData(window, obj);

            window.ShowDialog();
        }

        private void SetData(MatrixWindow window, object obj)
        {
            var objType = obj.GetType();

            if (TrySetDataOf<double>(window, objType, obj))
                return;
            else if (TrySetDataOf<float>(window, objType, obj))
                return;
            else if (TrySetDataOf<System.Numerics.Complex>(window, objType, obj))
                return;
            else if (TrySetDataOf<MathNet.Numerics.Complex32>(window, objType, obj))
                return;
        }

        private bool TrySetDataOf<T>(MatrixWindow window, Type type, object value)
            where T : struct, IEquatable<T>, IFormattable
        {
            if (IsMatrixOf<T>(type))
            {
                window.SetMatrix((Matrix<T>)value);
                return true;
            }
            else if(IsVectorOf<T>(type))
            {
                window.SetVector((Vector<T>)value);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsMatrixOf<T>(Type other)
            where T : struct, IEquatable<T>, IFormattable
        {
            return typeof(Matrix<T>).IsAssignableFrom(other);
        }

        private bool IsVectorOf<T>(Type other)
            where T : struct, IEquatable<T>, IFormattable
        {
            return typeof(Vector<T>).IsAssignableFrom(other);
        }
    }
}
    
