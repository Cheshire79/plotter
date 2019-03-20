
using NUnit.Framework;
using Plotter.Tools;
using System;

namespace AlgorithmsTest
{
	[TestFixture]
	public class AlgorithmsTest
	{
		[Test]
		public void LSMLinierEquation()
		{
			double[] xValue = new double[5] { 1D, 2D, 3D, 4D, 5D };
			double[] yValue = new double[5] { 1D, 2D, 3D, 4D, 5D };
			LSM lsm = new LSM(xValue, yValue);
			lsm.Polynomial(3);
			var actual = lsm.Coeff;
			double[] expected = new double[4] { 0D, 1D, 0D, 0D };
			Assert.That(actual, Is.EqualTo(expected).Within(0.0001));
		}

		[Test]
		public void LSMEQuadraticEquation()
		{
			double[] xValue = new double[5] { -1D, 0D, 1D, 2D, 3D };
			double[] yValue = new double[5] { 3D, 2D, 3D, 6D, 11D };
			LSM lsm = new LSM(xValue, yValue);
			lsm.Polynomial(3);
			var actual = lsm.Coeff;
			double[] expected = new double[4] { 2D, 0D, 1D, 0D };
			Assert.That(actual, Is.EqualTo(expected).Within(0.0001));
		}

		[Test]
		public void LSMInverseMatrixMoesNotExistExceptio()
		{
			double[] xValue = new double[5] { -1D, -1D, -1D, -1D, 3D };
			double[] yValue = new double[5] { 3D, 23, 3D, 3D, 3D };
			LSM lsm = new LSM(xValue, yValue);
			Assert.Throws<ArgumentException>(() =>
			{
				lsm.Polynomial(3);
			});
		}

		[Test]
		public void LSMPolinomPowLessThenOneException()
		{
			double[] xValue = new double[5] { -1D, 0D, 1D, 2D, 3D };
			double[] yValue = new double[5] { 3D, 2D, 3D, 6D, 11D };
			LSM lsm = new LSM(xValue, yValue);

			Assert.Throws<ArgumentException>(() =>
			{
				lsm.Polynomial(0);
			});
		}

		[Test]
		public void LSMPolinomPowEqualNumberOfPointsException()
		{
			double[] xValue = new double[5] { -1D, 0D, 1D, 2D, 3D };
			double[] yValue = new double[5] { 3D, 2D, 3D, 6D, 11D };
			LSM lsm = new LSM(xValue, yValue);

			Assert.Throws<ArgumentException>(() =>
			{
				lsm.Polynomial(5);
			});
		}

		[Test]
		public void LSMPolinomPowLargerThenNumberOfPointsException()
		{
			double[] xValue = new double[5] { -1D, 0D, 1D, 2D, 3D };
			double[] yValue = new double[5] { 3D, 2D, 3D, 6D, 11D };
			LSM lsm = new LSM(xValue, yValue);

			Assert.Throws<ArgumentException>(() =>
			{
				lsm.Polynomial(6);
			});
		}

		[Test]
		public void LSMEqualDimensionOfXandYException()
		{
			double[] xValue = new double[5] { -1D, 0D, 1D, 2D, 3D };
			double[] yValue = new double[4] { 3D, 2D, 3D, 6D };


			Assert.Throws<ArgumentException>(() =>
			{
				LSM lsm = new LSM(xValue, yValue);
			});
		}

		[Test]
		public void LSMAbsenceOfDataException()
		{
			double[] xValue = new double[0] { };
			double[] yValue = new double[0] { };


			Assert.Throws<ArgumentException>(() =>
			{
				LSM lsm = new LSM(xValue, yValue);
			});
		}


		[Test]
		public void LagrangeEqualDimensionOfXandYException()
		{
			double[] xValue = new double[5] { -1D, 0D, 1D, 2D, 3D };
			double[] yValue = new double[4] { 3D, 2D, 3D, 6D };

			Assert.Throws<ArgumentException>(() =>
			{
				Lagrange lsm = new Lagrange(xValue, yValue);
			});
		}

		[Test]
		public void LagrangeEqualValueOfXElementsException()
		{
			double[] xValue = new double[3] { 1D, 1.0000000000001D, 2D};
			double[] yValue = new double[3] { 3D, 2D, 3D, };

			Assert.Throws<ArgumentException>(() =>
			{
				Lagrange lsm = new Lagrange(xValue, yValue);
			});
		}

		[Test]
		public void LagrangeEqualValueOfXElements()
		{
			double[] xValue = new double[3] { 1D, 1.001D, 2D };
			double[] yValue = new double[3] { 3D, 2D, 3D, };

			Assert.DoesNotThrow(() =>
			{
				Lagrange lsm = new Lagrange(xValue, yValue);
			});
		}

	}
}
