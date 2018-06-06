using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class AllowGhostThrough : MonoBehaviour {

	[SerializeField]
	private Collider _collider = null;

	private Rigidbody _Rigidbody = null;

	[SerializeField]
	private List<GameObject> _currentlyOn = new List<GameObject>();


	void Start()
	{
		_Rigidbody = this.GetComponent<Rigidbody> ();
	}


	void OnTriggerEnter(Collider other)
	{
		if ((other.tag != "Player" && other.tag != "Bullet") && other.GetComponent<EStatePattern> () == false)
			return;

		if (!_currentlyOn.Contains (other.gameObject))
			_currentlyOn.Add (other.gameObject);

		if (_collider) 
			_collider.enabled = false;

		if(_Rigidbody)
			_Rigidbody.isKinematic = true;
	}

	// do every x seconds instead?
	void Update()
	{
		foreach (GameObject obj in _currentlyOn) {
			if (obj == null || obj.activeSelf == false) {
				_currentlyOn.Remove (obj);

				if (_currentlyOn.Count == 0)
				{
					if (_collider) 
						_collider.enabled = true;

					if(_Rigidbody)
						_Rigidbody.isKinematic = false;
				}

				return;
			}
		}

	}

	void OnTriggerExit(Collider other)
	{
		if (_currentlyOn.Count <= 0)
			return;

		if ((other.tag != "Player" && other.tag != "Bullet") && other.GetComponent<EStatePattern> () == false)
			return;

		if (_currentlyOn.Contains (other.gameObject))
			_currentlyOn.Remove (other.gameObject);

		if (_currentlyOn.Count == 0)
		{
			if (_collider) 
				_collider.enabled = true;

			if(_Rigidbody)
				_Rigidbody.isKinematic = false;
		}
	}
}
