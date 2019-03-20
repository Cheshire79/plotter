using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Plotter.Views
{
	public class InputData : MonoBehaviour
	{
		private Camera _camera;
		private GraphicRaycaster _raycaster;

		private PointView[] _points = new PointView[5];
		public Transform LeftDownCorner;
		public Transform RightUpperCorner;
		public PointView PointPrefab;
		public Transform GraphicElementsHolder;
		public Action<double[]> OnAllPointsSetted;

		public void Init(Camera camera)
		{
			_camera = camera;
			float distanceX = (RightUpperCorner.localPosition.x - LeftDownCorner.localPosition.y) / _points.Length;
			_raycaster = GetComponent<GraphicRaycaster>();

			for (int i = 0; i < _points.Length; i++)
			{
				_points[i] = Instantiate(PointPrefab);
				_points[i].transform.parent = GraphicElementsHolder;
				_points[i].transform.localScale = Vector3.one;
				_points[i].transform.localPosition = new Vector3(LeftDownCorner.localPosition.x + (i + 1) * distanceX, 0, 0);//todo const
				_points[i].transform.name = "Point " + (1 + i);
				_points[i].LeftBorder = 1 + i - 0.5f;
			}
		}

		void Update()
		{
			Vector3 mousePosition;
			if (Input.GetMouseButtonUp(0))
			{
				if (IsPLotterClicked())
				{
					mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
					float x = mousePosition.x + Constants.CoordiateOffsetFromPlotterCenter.x;
					float y = mousePosition.y + Constants.CoordiateOffsetFromPlotterCenter.y;
					Debug.LogWarning(" mouse x= " + x + "  y= " + y);

					TryToSetPoint(x, y);
					if (IsAllPointSetted())
					{
						if (OnAllPointsSetted != null)
						{
							double[] values = new double[_points.Length];
							for (int i = 0; i < _points.Length; i++)
								values[i] = _points[i].Y;
							OnAllPointsSetted(values);
						}
					}
				}
			}
		}

		private bool IsPLotterClicked()
		{
			PointerEventData pointerData = new PointerEventData(EventSystem.current);
			List<RaycastResult> results = new List<RaycastResult>();
			pointerData.position = Input.mousePosition;
			_raycaster.Raycast(pointerData, results);
			return (results.FindAll(x => x.gameObject.name.Equals("Plot")).Count > 0);
		}

		private void TryToSetPoint(float x, float y)
		{
			for (int i = 0; i < _points.Length; i++)
				if (_points[i].LeftBorder <= x && (_points[i].LeftBorder + 1) > x)
				{
					float height = LeftDownCorner.localPosition.y + ((RightUpperCorner.localPosition.x - LeftDownCorner.localPosition.y) / _points.Length) * y;
					_points[i].Sphere.SetActive(true);
					_points[i].Text.text = "y=" + y.ToString("0.00");
					_points[i].Sphere.transform.localPosition = new Vector3(_points[0].Sphere.transform.localPosition.x, height, _points[0].Sphere.transform.localPosition.z);
					_points[i].Y = y;
					break;
				}
		}

		private bool IsAllPointSetted()
		{
			for (int i = 0; i < _points.Length; i++)
				if (!_points[i].Sphere.activeSelf)
					return false;
			return true;
		}

	}
}
