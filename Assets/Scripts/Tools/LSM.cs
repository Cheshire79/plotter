using System;
using System.Linq;

namespace Plotter.Tools
{

	public class LSM
	{

		public double[] X { get; set; }
		public double[] Y { get; set; }


		private double[] coeff;
		public double[] Coeff { get { return coeff; } }


		public double? Delta { get { return getDelta(); } }


		public LSM(double[] x, double[] y)
		{
			if (x==null || y==null || x.Length ==0|| y.Length==0)
				throw new ArgumentException("There is no input data");
			if (x.Length != y.Length) throw new ArgumentException("Dimensions of X and Y must be equal");
			X = new double[x.Length];
			Y = new double[y.Length];

			for (int i = 0; i < x.Length; i++)
			{
				X[i] = x[i];
				Y[i] = y[i];
			}
		}

		public void Polynomial(int m)
		{
			if (m <= 0) throw new ArgumentException("The polinom power need to be more then 0");
			if (m >= X.Length) throw new ArgumentException("The polinom power need to be more then number of points !");

		
			double[,] basic = new double[X.Length, m + 1];

			for (int i = 0; i < basic.GetLength(0); i++)
				for (int j = 0; j < basic.GetLength(1); j++)
					basic[i, j] = Math.Pow(X[i], j);

			Matrix basicFuncMatr = new Matrix(basic);

			Matrix transBasicFuncMatr = basicFuncMatr.Transposition();
			
			Matrix lambda = transBasicFuncMatr * basicFuncMatr;
			 
			Matrix beta = transBasicFuncMatr * new Matrix(Y);

			Matrix a = lambda.InverseMatrix() * beta;
	
			coeff = new double[a.Row];
			for (int i = 0; i < coeff.Length; i++)
			{
				coeff[i] = a.Args[i, 0];
			}
		}

	
		private double? getDelta()
		{
			if (coeff == null) return null;
			double[] dif = new double[Y.Length];
			double[] f = new double[X.Length];
			for (int i = 0; i < X.Length; i++)
			{
				for (int j = 0; j < coeff.Length; j++)
				{
					f[i] += coeff[j] * Math.Pow(X[i], j);
				}
				dif[i] = Math.Pow((f[i] - Y[i]), 2);
			}
			return Math.Sqrt(dif.Sum() / X.Length);
		}
	}
}
