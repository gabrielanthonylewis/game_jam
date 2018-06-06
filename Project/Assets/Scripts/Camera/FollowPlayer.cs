using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	[SerializeField]
	private Transform _player = null;

	[SerializeField]
	private Transform[] _players;

	[SerializeField]
	private Vector3 _initialPos;

	void Start()
	{
		this.transform.position = _initialPos;

		this.transform.rotation = Quaternion.Euler (new Vector3 (35, 45, 0));

		this.GetComponent<Camera> ().orthographic = true;
		this.GetComponent<Camera> ().orthographicSize = 11;

		if (_player == null) {

			GameObject temp = GameObject.FindGameObjectWithTag ("Player");

			if(temp != null)
				_player = temp.transform;
		}


		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		_players = new Transform[players.Length];

		for (int i = 0; i < players.Length; i++)
			_players [i] = players [i].transform;

	}

	// Update is called once per frame
	void Update () 
	{
		/*
		Vector3 newPos = this.transform.position;
		newPos.x = _player.position.x;
		newPos.z = _initialPos.z + _player.position.z;

		this.transform.position = newPos;
		*/

		Vector3 newPos = this.transform.position;

		float xAverage = 0.0F;
		float zAverage = 0.0F;


		Transform[] alivePlayers = SpawnPlayers.Instance.GetActivePlayers ();
		for (int i = 0; i < alivePlayers.Length; i++) 
		{


			xAverage += alivePlayers [i].position.x;
			zAverage += alivePlayers [i].position.z;
		}


		xAverage /= alivePlayers.Length ;
		zAverage /= alivePlayers.Length ;

		// Dead state, so stay still, no players to follow
		if (alivePlayers.Length == 0) {

			return;
		}


		newPos.x = xAverage - 31.42F;
		newPos.y = _initialPos.y ;
		newPos.z = _initialPos.z + zAverage  ;


		// if someone is magically off screen so be it (e.g. physics mess up that shouldnt happen)
		/*for (int i = 0; i < alivePlayers.Length; i++) 
		{
			Vector3 viewPos = Camera.main.WorldToViewportPoint (alivePlayers [i].position);

			Debug.Log (alivePlayers[i].name + ": " + viewPos.x);
		//	if (viewPos.x > 1 || 
		//		viewPos.x < 0 || 
		//		viewPos.y > 1 ||
		//		viewPos.y < 0)
		//		return;
		}*/

		this.transform.position = newPos;
	}
}
