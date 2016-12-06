using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var m = 200;
            var n = m;
            
            var md = CreateMatrix.Random<double>(m, n);
            var mf = CreateMatrix.Random<float>(m, n);
            var mc = CreateMatrix.Random<Complex>(m, n);
            var mx = CreateMatrix.Random<Complex32>(m, n);

            var vd = CreateVector.Random<double>(m);
            var vf = CreateVector.Random<float>(m);
            var vc = CreateVector.Random<Complex>(m);
            var vx = CreateVector.Random<Complex32>(m);

            object value = mc;

            MathNetVisualizer.Test.ShowVisualizer(value);
        }
    }
}
