using Plotter.Controller;
using UnityEngine;

namespace Plotter
{
	public class Start : MonoBehaviour
	{
		public Camera Camera;
		public MainController MainPrefab;

		private void Awake()
		{
			MainController main = Instantiate(MainPrefab);
			main.transform.name = "Main" ;
			main.GetComponent<Canvas>().worldCamera = Camera;
			main.Init(Camera);
			
		}
		private void Quit()
		{
#if UNITY_EDITOR
			// Application.Quit() does not work in the editor so
			// UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
			Debug.LogWarning(" Quit from EDITOR");
			UnityEditor.EditorApplication.isPlaying = false;
#else
         Debug.LogWarning(" Quit " );			
			Application.Quit();
#endif
		}

		void OnApplicationQuit()
		{
			Quit();

		}
		void Update()
		{
			if (Input.GetKeyUp(KeyCode.Escape))
				Quit();
		}
	}
}

