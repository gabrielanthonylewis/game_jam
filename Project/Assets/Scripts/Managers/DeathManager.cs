using UnityEngine;
using System.Collections;

public class DeathManager : MonoBehaviour {

	public static DeathManager _instance = null;
	public static DeathManager Instance { get { return _instance; } }


	[SerializeField]
	private DeathCamera _DeathCamera = null;

	[SerializeField]
	private GameObject _deathCanvas = null;

	private bool _sequencePlayer = false;

	private void Awake()
	{
		if (_instance != null && _instance != this)
			Destroy (this.GetComponent<DeathManager>());
		else
			_instance = this;
	}



	public void CheckGameOver()
	{
		
		StartCoroutine ("CheckRou");
	
	}

	private void GameOver()
	{
		if (_DeathCamera != null && _sequencePlayer == false) 
		{
			_DeathCamera.transform.parent.position = Camera.main.transform.position;
			_DeathCamera.StartSequence ();
			_deathCanvas.SetActive (true);
			_sequencePlayer = true;
		}
	}

	IEnumerator CheckRou()
	{
		yield return new WaitForSeconds (0.25f);

		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");

		if (players.Length <= 0) 
		{
			GameOver ();
		}
	}
}
