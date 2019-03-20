
using UnityEngine;
namespace Plotter
{
	public class DrawCurve : MonoBehaviour
	{
		private LineRenderer _lineRenderer;
		private Color _color;

		public void Init(Color color)
		{
			_color = color;
			if (_lineRenderer == null)
				CreateDefaultLineRenderer();		
		}
		public void Draw(Vector2[] points)
		{
			_lineRenderer.positionCount = points.Length;
			for (int i = 0; i < points.Length; i++)
			{
				_lineRenderer.SetPosition(i, new Vector3(points[i].x , points[i].y , 0));
			}
		}


		private void CreateDefaultLineRenderer()
		{
			_lineRenderer = gameObject.GetComponent<LineRenderer>();
			_lineRenderer.useWorldSpace = false;
			_lineRenderer.sortingOrder = 1;
			_lineRenderer.positionCount = 0;
			_lineRenderer.material = new Material(Shader.Find("Particles/Alpha Blended"));
			_lineRenderer.startColor = _color;
			_lineRenderer.endColor = _color;
			_lineRenderer.startWidth = 0.1f;
			_lineRenderer.endWidth = 0.1f;
		}

	}
}
