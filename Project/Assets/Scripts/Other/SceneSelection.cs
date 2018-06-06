using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class SceneSelection : MonoBehaviour {

	public void ChangeTo(int x)
	{
		SceneManager.LoadScene (x, LoadSceneMode.Single);
	}

	public void RestartLevel()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
