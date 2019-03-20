using UnityEngine;

namespace Plotter.Views
{
	public class OutPutGraphics : MonoBehaviour
	{	
		public DrawCurve DrawCurvePrefab;
		public Transform GraphicElementsHolder;
		public LineRenderer SLMLegedLine;
		public LineRenderer LagranzeLegedLine;
		private Transform _leftDownCorner;
		private Transform _rightUpperCorner;

		public DrawCurve LSMCurve
		{
			get;
			set;
		}

		public DrawCurve LagrangeCurve
		{
			get;
			set;
		}

		public void Init( Transform leftDownCorner,Transform rightUpperCorner)
		{
			Color LagrangeColor = Color.green;
			Color LSMColor = Color.red;
			SLMLegedLine.material = new Material(Shader.Find("Particles/Alpha Blended"));
			SLMLegedLine.startColor = LSMColor;
			SLMLegedLine.endColor = LSMColor;
			SLMLegedLine.startWidth = 0.1f;
			SLMLegedLine.endWidth = 0.1f;

			LagranzeLegedLine.material = new Material(Shader.Find("Particles/Alpha Blended"));
			LagranzeLegedLine.startColor = LagrangeColor; 
			LagranzeLegedLine.endColor = LagrangeColor;
			LagranzeLegedLine.startWidth = 0.1f;
			LagranzeLegedLine.endWidth = 0.1f;

			_leftDownCorner = leftDownCorner;
			 _rightUpperCorner = rightUpperCorner;

			LSMCurve = Instantiate(DrawCurvePrefab);
			LSMCurve.transform.parent = GraphicElementsHolder;
			LSMCurve.transform.localScale = Vector3.one;
			LSMCurve.transform.localPosition = Vector3.one;
			LSMCurve.transform.localScale = new Vector3(1/transform.lossyScale.x, 1/transform.lossyScale.y, 1/transform.lossyScale.z);
			LSMCurve.transform.localPosition = new Vector3(_leftDownCorner.localPosition.x, _leftDownCorner.localPosition.y,10);			

			LSMCurve.transform.name = "LSM";
			LSMCurve.Init(LSMColor);

			LagrangeCurve = Instantiate(DrawCurvePrefab);
			LagrangeCurve.transform.parent = GraphicElementsHolder;
			LagrangeCurve.transform.localScale = Vector3.one;
			LagrangeCurve.transform.localPosition = Vector3.one;
			LagrangeCurve.transform.localScale = new Vector3(1 / transform.lossyScale.x, 1 / transform.lossyScale.y, 1 / transform.lossyScale.z);
			LagrangeCurve.transform.localPosition = new Vector3(_leftDownCorner.localPosition.x, _leftDownCorner.localPosition.y, 10);

			LagrangeCurve.transform.name = "Lagrange";
			LagrangeCurve.Init(LagrangeColor);

		}
		public void DrawLSM(Vector2[] coeffFromMnk)
		{
			LSMCurve.Draw(coeffFromMnk);
		}
		public void DrawLagrange(Vector2[] coeffFromMnk)
		{
			LagrangeCurve.Draw(coeffFromMnk);
		}
	}
}
