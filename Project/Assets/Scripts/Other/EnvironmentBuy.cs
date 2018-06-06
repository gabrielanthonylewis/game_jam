using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnvironmentBuy : MonoBehaviour {

	[SerializeField]
	private GameObject _tempObject = null;

	[SerializeField]
	private GameObject _actualObject = null;


	[SerializeField]
	private GameObject _onPadEnter = null;

	[SerializeField]
	private int _cost = 100;

	[SerializeField]
	private Text _costText = null;



	// Use this for initialization
	void Start () 
	{
		_costText.text = "Cost: " + _cost;
	}



	bool EnoughMoney()
	{
		return (PlayerStats.Instance.money >= _cost);
	}


	void BuyObject()
	{
		PlayerStats.Instance.AddMoney (-_cost);
		_tempObject.SetActive (false);
		_onPadEnter.SetActive (false);
		_actualObject.SetActive (true);

		Destroy (this.transform.parent.gameObject);
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player")
			return;

		_onPadEnter.SetActive (true);
		_tempObject.SetActive (false);
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag != "Player")
			return;

		//	Debug.Log (Input.GetAxis ("RightTrigger" + other.GetComponent<MoveController> ().currPlayer.ToString ()));
		if ((InputManager.Instance.inputType == InputManager.InputType.MouseKeyboard
			&& Input.GetKeyDown (KeyCode.Mouse1))
			||
			(InputManager.Instance.inputType == InputManager.InputType.Controller
				&& Input.GetAxis("Triggers" + other.GetComponent<MoveController>().currPlayer.ToString()) == -1.0F)) 
			{
				if (EnoughMoney ())
					BuyObject ();
			}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag != "Player")
			return;

		_onPadEnter.SetActive (false);
		_tempObject.SetActive (true);
	}
}
