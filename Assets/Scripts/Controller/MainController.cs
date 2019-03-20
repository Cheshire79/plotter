using Plotter.Tools;
using Plotter.Tools.GUI;
using Plotter.Views;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Plotter.Controller
{
	public class MainController : MonoBehaviour
	{
		double[] _yValues;
		public InputData InputData;
		public OutPutGraphics GraphicView;
		public Text Text;
		public ButtonAdv CalculateButton;
		public Transform LeftDownCorner;
		public Transform RightUpperCorner;


		public void Init(Camera camera)
		{
			Text.text = "";
			CalculateButton.interactable = false;
			CalculateButton.onClick.AddListener(OnCalculateButtonClicked);
			InputData.OnAllPointsSetted += CalculateButtonEnable;
			GraphicView.Init(LeftDownCorner, RightUpperCorner);
			InputData.Init(camera);
		}


		private void CalculateButtonEnable(double[] values)
		{
			CalculateButton.interactable = true;
			_yValues = values;
		}

		private void OnCalculateButtonClicked()
		{
			string failedMessage = "";
			bool isFailed = false;

			if (_yValues != null)
			{
				try
				{
					double[] xValues = new double[_yValues.Length];
					for (int i = 0; i < _yValues.Length; i++)
						xValues[i] = i + 1;
					LSM lsm = new LSM(xValues, _yValues);
					lsm.Polynomial(3);
					StringBuilder polinom = new StringBuilder();
					for (int i = 0; i < lsm.Coeff.Length; i++)
					{
						polinom.Append("x " + i + "*" + lsm.Coeff[i].ToString("0.00") + " + ");
					}					
					GraphicView.DrawLSM(CreateSetOfPointsFromPolinom(lsm.Coeff));
				}
				catch
				{
					failedMessage += "МНК модели";
					isFailed = true;
				}

				try
				{
					double[] xs = new double[_yValues.Length];
					for (int i = 0; i < _yValues.Length; i++)
						xs[i] = i + 1;
					Lagrange lagrange = new Lagrange(xs, _yValues);

					Vector2[] setOfPoints = new Vector2[Constants.PointsNumber];
					for (int i = 0; i < setOfPoints.Length; i++)
					{

						double x = (i + 10) / 10.0f;
						double height = lagrange.GetValue(x);

						setOfPoints[i] = new Vector3((float)x, (float)height, 0);
					}
					GraphicView.DrawLagrange(setOfPoints);

				}
				catch
				{
					if (failedMessage.Length > 0)
						failedMessage += ", ";
					failedMessage += "Лагранж модели";
					isFailed = true;
				}
				if (isFailed)
				{
					Text.text = "Ошибка при расчете " + failedMessage;
				}
			}
		}

		private Vector2[] CreateSetOfPointsFromPolinom(double[] coeff)
		{
			Vector2[] setOfPoints = new Vector2[Constants.PointsNumber];
			for (int i = 0; i < setOfPoints.Length; i++)
			{

				double x = (i + 10) / 10.0f;
				double height = 0;
				for (int j = 0; j < coeff.Length; j++)
					height += coeff[j] * Math.Pow(x, j);
				setOfPoints[i] = new Vector3((float)x, (float)height, 0);
			}
			return setOfPoints;
		}
	}
}
