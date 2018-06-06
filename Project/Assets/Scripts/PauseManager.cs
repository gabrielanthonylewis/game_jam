using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour {


	[SerializeField]
	private GameObject _uiContainer = null;

	void Start()
	{
		Pause (false);
	}

	public void Pause(bool state)
	{
		_uiContainer.SetActive (state);



		Time.timeScale = System.Convert.ToInt32(!state);
		//todo: could use coroutine and make it disapear after 0.5 seconds so you can see the green button press
	}
		
}
