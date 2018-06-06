using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerShoot))]
public class Inventory : MonoBehaviour
{
	[System.Serializable]
	struct Gun
	{
		public string name;

		[HideInInspector] 
		public GameObject obj;

		public int initialAmmo;
	};


	List<Gun> _guns = new List<Gun>();

	[SerializeField]
	List<Gun> _gunsInventory = new List<Gun>();

	[SerializeField]
	MoveController _MoveController = null;

	int currGunIdx = -1;

	// Use this for initialization
	void Start () 
	{
		// Get all guns
		GameObject[] guns = Resources.LoadAll<GameObject>("Prefabs/Guns");

	

		foreach (GameObject gun in guns) 
		{
			Gun newGun = new Gun();
			newGun.initialAmmo = 0;
			newGun.obj = gun;
			newGun.name = gun.name;
			_guns.Add (newGun);
		}

		// Spawn in all guns
		for (int g = 0; g < _guns.Count; g++) 
		{
			GameObject newGun = Instantiate (_guns[g].obj) as GameObject;
            newGun.transform.parent = this.transform;
			newGun.transform.localPosition = Vector3.zero;
			newGun.transform.localRotation = Quaternion.Euler (Vector3.zero);



			newGun.SetActive (false);
		
			newGun.name = _guns[g].name;

			Gun temp = _guns [g];
			temp.obj = newGun;
			_guns [g] = temp;



			for (int i =0; i < _gunsInventory.Count; i++)
			{
				if (_gunsInventory[i].name == newGun.name) 
				{

					//Debug.Log (_gunsInventory[i].name + "  |   " + newGun.name + "     |     " + i.ToString()); 

					Gun newGunStruct = _gunsInventory[i];
					newGunStruct.obj = _guns[g].obj;

					_gunsInventory[i]= newGunStruct;


					//_gunsInventory[i].obj.GetComponent<GunShoot> ().initialBullets +=  _gunsInventory[i].initialAmmo;

					break;
				}
			}


		}


		// Equip initial gun
		if (_gunsInventory.Count > 0) 
		{
			//

			Equip (_gunsInventory [0].name);
		}
	}


	bool _allowTrigger = true;

	void Update()
	{
		if (Time.timeScale == 0.0f)
			return;
		
		if (InputManager.Instance.inputType != InputManager.InputType.Controller)
			return;

		if (Input.GetAxis ("Triggers" + _MoveController.currPlayer.ToString ()) < 1.0F)
			_allowTrigger = true;

		if (_allowTrigger && Input.GetAxis ("Triggers" +_MoveController.currPlayer.ToString ()) == 1.0F) 
		{
			_allowTrigger = false;

			//todo next weapon
			Equip(currGunIdx + 1);
		}


	}


	public void Equip(string name)
	{
		int idx = -1;
		for (int g = 0; g < _gunsInventory.Count; g++)
		{
			if (_gunsInventory [g].name == name) 
			{
				idx = g;
				break;
			}

		}

		if (idx < 0) 
		{
			Debug.Log (name + " not in Inventory!");
			return;
		}


		Equip (idx);
	}

	private void Equip(int index)
	{
        if (currGunIdx == index)
            return;

		if (currGunIdx > -1) 
		{
			_gunsInventory [currGunIdx].obj.SetActive (false);
		}

		if(index >= _gunsInventory.Count)
			index = 0;

		currGunIdx = index;

		_gunsInventory [currGunIdx].obj.SetActive (true);

		this.GetComponent<PlayerShoot> ().currentGun 
		= _gunsInventory [currGunIdx].obj.GetComponent<GunShoot> ();

      //  Debug.Log(_gunsInventory[currGunIdx].name + ":" + _MoveController.currPlayer);
        SpawnPlayers.Instance.cornersTexts[_MoveController.currPlayer].text = _gunsInventory[currGunIdx].name;
    }



	public void AddAmmo(string name, int val)
	{
		int idx = -1;
		for (int g = 0; g < _guns.Count; g++)
		{
			if (_guns [g].name == name) 
			{
				idx = g;
				break;
			}

		}

		if (idx < 0) 
		{
			Debug.LogError (name + " doesn't exist in _guns. Ensure Resources/Prefabs/Guns has " + name);
			return;
		}

		_guns [idx].obj.GetComponent<GunShoot> ().currentBullets += val;
	}

	public void Add(string name)
	{

		int idx = -1;
		for (int g = 0; g < _guns.Count; g++)
		{
			if (_guns [g].name == name) 
			{
				idx = g;
				break;
			}

		}

		if (idx < 0) 
		{
			Debug.LogError (name + " doesn't exist in _guns. Ensure Resources/Prefabs/Guns has " + name);
			return;
		}

		_gunsInventory.Add (_guns[idx]);

		// If holding nothing then equip this one
		if (currGunIdx < 0) 
		{
			Equip (name);
		}
	}


}
