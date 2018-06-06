using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpawnPlayers : MonoBehaviour {

	public static SpawnPlayers _instance = null;
	public static SpawnPlayers Instance { get { return _instance; } }

	[SerializeField]
	private GameObject _playerPrefab;

	[SerializeField]
	private Transform[] _spawnPoints;


	[SerializeField]
	private int _maxPlayers = 4;

	[SerializeField]
	private int _playersToSpawn = 2;

	private int _playerCount = 0;

	[SerializeField]
	private GameObject[] _players;

	int num = 0;
	public int currActivatedPlayers = 0;

	bool[] _controllersInUse;

	[SerializeField]
	private bool _allowHotPluggable = true;

    [SerializeField]
    private GameObject[] _corners = null;
    public GameObject[] corners { get { return _corners; } }

    [SerializeField]
    private Text[] _cornersTexts = null;
    public Text[] cornersTexts { get { return _cornersTexts; } }


    // Use this for initialization
    void Awake () 
	{
		if (_instance != null && _instance != this)
			Destroy (this.GetComponent<SpawnPlayers>());
		else
			_instance = this;



		if (_playerPrefab == null) 
			_playerPrefab = Resources.Load ("Prefabs/PlayerMain") as GameObject;


		if (_playerPrefab == null) {

			Debug.LogError ("PlayerPrefab not found or assigned");

			return;
		}

		_players = new GameObject[_maxPlayers];

		_controllersInUse = new bool[_maxPlayers];

		for (int i = 0; i < _maxPlayers; i++) 
			SpawnPlayer ();


		for (int i = 0; i < _playersToSpawn; i++)
		{

			ActivatePlayerSpawnPoint ();

		}
	}


	void Update()
	{
		if (!_allowHotPluggable)
			return;


		string[] controllers = Input.GetJoystickNames ();

		int count = 0;
		for (int i = 0; i < controllers.Length; i++) {

			if (controllers [i] == "Controller (XBOX 360 For Windows)")
				count++;

		}
		if (count > currActivatedPlayers) {

			int firstAlivePlayerIdx = -1;
			for (int i = 0; i < _players.Length; i++) {

				if (_players [i].activeSelf == true) {
					firstAlivePlayerIdx = i;
					break;
				}
			}

			if (firstAlivePlayerIdx == -1) {
				ActivatePlayer ();
			} else {

             
				// Dont allow 0
				int rand1 = Random.Range (-3, 3);
				if (rand1 < 0)
					rand1 -= 1;
				else
					rand1 += 1;

				int rand2 = Random.Range (-3, 3);
				if (rand2 < 0)
					rand2 -= 1;
				else
					rand2 += 1;

				_players [currActivatedPlayers].transform.position 
				= _players [firstAlivePlayerIdx].transform.position + new Vector3 (rand1, 0, rand2);

				//_players [currActivatedPlayers].SetActive (true);


				ActivatePlayer ();
			}
		}
	}

	public Transform[] GetAllPlayers()
	{
		Transform[] tempPlayers = new Transform[_players.Length];




		for (int i = 0; i < tempPlayers.Length; i++) {
			tempPlayers [i] = _players [i].transform;

		}


		return tempPlayers;
	}

	//TODO optomize this so I can just get list instead of this calculation every time
	public Transform[] GetActivePlayers()
	{
		List<Transform> tempPlayers = new List<Transform> ();;

		int count = 0;
		for(int i = 0; i < _players.Length; i++)
		{
			if (_players [i] != null &&	
				_players [i].activeSelf == true)
			{
				tempPlayers.Add(_players [i].transform);
			}
		}




		return tempPlayers.ToArray();
	}

	void ActivatePlayerSpawnPoint()
	{		
		if (num >= _spawnPoints.Length)
			num = 0;

		if (_spawnPoints.Length == 0 || _spawnPoints [num] == null) {

			_players [currActivatedPlayers].transform.position = Vector3.zero + Vector3.up * 2 + new Vector3(Random.Range(-5,5), 0, Random.Range(-5,5));

            //_players [currActivatedPlayers].SetActive (true);
            _players[currActivatedPlayers].GetComponentInChildren<PlayerIndicator>().SetColour(_corners[currActivatedPlayers].GetComponent<Image>().color);

            ActivatePlayer ();

			return;
		}


		_players [currActivatedPlayers].transform.position = _spawnPoints [num].position;
		_players [currActivatedPlayers].transform.rotation = Quaternion.Euler( _players[currActivatedPlayers].transform.rotation.eulerAngles);

		//_players [currActivatedPlayers].SetActive (true);

		num++;


		ActivatePlayer ();
	}




	void ActivatePlayer()
	{
		string[] controllers = Input.GetJoystickNames ();

      
		for (int i = 0; i < controllers.Length; i++) {

			if (i >= _controllersInUse.Length)
				break;

			if (controllers [i] == "Controller (XBOX 360 For Windows)") 
			{
				if (_controllersInUse [i] == false) {

					//_players[currActivatedPlayers].GetComponent<MoveController> ().currPlayer = i; //??
					_controllersInUse [i] = true;
					continue;
				}
			}

		}

		_players [currActivatedPlayers].name = _playerPrefab.name + currActivatedPlayers.ToString ();

       

        
        _players[currActivatedPlayers].SetActive(true);
        _corners[currActivatedPlayers].SetActive(true);
        _players[currActivatedPlayers].GetComponent<MoveController>().currPlayer = currActivatedPlayers;

        _players[currActivatedPlayers].GetComponentInChildren<PlayerIndicator>().SetColour(_corners[currActivatedPlayers].GetComponent<Image>().color);

        currActivatedPlayers++;
	}

	void SpawnPlayer()
	{
		_players[_playerCount] = Instantiate (_playerPrefab) as GameObject;

		//_players[_playerCount].GetComponent<MoveController> ().currPlayer = _playerCount;

		_players[_playerCount].SetActive (false);

		_playerCount++;
	}
}
