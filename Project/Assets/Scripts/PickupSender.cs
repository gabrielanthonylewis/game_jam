using UnityEngine;
using System.Collections;
using UnityEngine.Events;


public class PickupSender : MonoBehaviour {

	[SerializeField]
	private UnityEvent _onPickup;


	private GameObject _pickupReceiver = null;
	public GameObject receiver { get { return _pickupReceiver; } }


	private void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player")
			return;

		_pickupReceiver = other.gameObject;

		CallPickupEvents ();
	}

	// set to private if never use PickupReceiver
	public void CallPickupEvents()
	{
		if (_onPickup == null)
			return;

		_onPickup.Invoke ();
	}
}
