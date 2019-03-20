using System;
using System.Collections.Generic;
using System.Linq;

namespace Plotter.Tools
{

	public class Lagrange
	{
		private static bool DoublesAreNearlyEquals(double d1, double d2, double epsilon = 0.01D)
		{
			return System.Math.Abs(d1 - d2) < epsilon;
		}

		private double[] _xValues;
		private double[] _yValues;

		public Lagrange(double[] x, double[] y)
		{
			if (x.Length != y.Length) throw new ArgumentException("Dimensions of X and Y must be equal");
			var hash = new HashSet<double>();
			var duplicates = x.Where(i => !hash.Add(Math.Round(i, 4)));

			if (duplicates.Count() > 0)
				throw new ArgumentException("Elements in X must be different");
			_xValues = new double[x.Length];
			_yValues = new double[y.Length];

			for (int i = 0; i < x.Length; i++)
			{
				_xValues[i] = x[i];
				_yValues[i] = y[i];
			}
		}

		public double GetValue(double x)
		{

			double lagrangePol = 0;

			for (int i = 0; i < _xValues.Length; i++)
			{
				double basicsPol = 1;
				for (int j = 0; j < _xValues.Length; j++)
				{
					if (j != i)
					{
						basicsPol *= (x - _xValues[j]) / (_xValues[i] - _xValues[j]);
					}
				}
				lagrangePol += basicsPol * _yValues[i];
			}
			return lagrangePol;

		}
	}
}
