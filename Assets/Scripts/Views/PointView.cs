using UnityEngine;
using UnityEngine.UI;

namespace Plotter.Views
{
	public class PointView : MonoBehaviour
	{
		public Text Text;
		public GameObject Sphere;
		[HideInInspector]
		public float LeftBorder;
		[HideInInspector]
		public float RightBorder;

		[HideInInspector]
		public float Y;
	}
}
