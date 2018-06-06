using UnityEngine;
using System.Collections;

public class LookController : MonoBehaviour
{
	[SerializeField, HideInInspector]
	private bool _allowVertical = false;
	public bool allowVertical { get { return _allowVertical; } }
	[SerializeField, HideInInspector]
	private bool _allowHorizontal = false;
	public bool allowHorizontal { get { return _allowHorizontal; } }

	[SerializeField, HideInInspector]
	private float _sensitivityX = 1.5f;
	public float sensitivityX { get { return _sensitivityX; } }
	[SerializeField, HideInInspector]
	private float _sensitivityY = 1.5f;
	public float sensitivityY { get { return _sensitivityY; } }

	[SerializeField, HideInInspector]
	private float _upperLimit = 320f;
	public float upperLimit { get { return _upperLimit; } }
	[SerializeField, HideInInspector]
	private float _lowerLimit = 60f;
	public float lowerLimit { get { return _lowerLimit; } }

	[SerializeField, HideInInspector]
	private bool _useRigidbodyIfAvailable = false;
	public bool useRigidbodyIfAvailable { get { return _useRigidbodyIfAvailable; } }

	private Rigidbody _Rigidbody = null;


	private bool _isMovingStick = false;
	public bool isMovingStick { get { return _isMovingStick; } }

	private bool _isMovingStickOnce = false;
	public bool isMovingStickOnce { get { return _isMovingStickOnce; } }


	bool _useDirectionalRotation = true;
	bool _checkRou = false;

	//[SerializeField]
	//private Transform _ground = null;

	[SerializeField]
	private MoveController _MoveController = null;

	// Use this for initialization
	void Start () 
	{
		_Rigidbody = this.GetComponent<Rigidbody> ();

		//if (_ground == null) {
		//	_ground = GameObject.FindGameObjectWithTag ("Ground").transform;
		//}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.timeScale == 0.0f)
			return;
		
		if (_useRigidbodyIfAvailable && _Rigidbody)
			return;
		
		if (_allowVertical)
			VerticalLook (false);
		if (_allowHorizontal)
			HorizontalLook (false);
	}

	// For physics
	void FixedUpdate()
	{
		if (Time.timeScale == 0.0f)
			return;
		
		if (!_Rigidbody || !_useRigidbodyIfAvailable)
			return;

		if (_allowVertical)
			VerticalLook (true);
		if (_allowHorizontal)
			HorizontalLook (true);
	}


	private void HorizontalLook(bool usePhysics)
	{
		/*float horizontal = Input.GetAxis ("Mouse X") * _sensitivityX;

		if (_Rigidbody != null && usePhysics) 
		{
			Quaternion deltaRotation = Quaternion.AngleAxis(horizontal, Vector3.up) ;
			_Rigidbody.MoveRotation (this.transform.localRotation * deltaRotation);
		}
		else
		{
			this.transform.Rotate(Vector3.up, horizontal);
		}*/

		if (Camera.main == null)
			return;

		if (InputManager.Instance.inputType == InputManager.InputType.MouseKeyboard) {
			//Get the Screen positions of the object
			Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);

			//Get the Screen position of the mouse
			Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint (Input.mousePosition);

			//Get the angle between the points
			float angle = AngleBetweenTwoPoints (positionOnScreen, mouseOnScreen);

			transform.rotation = Quaternion.Euler (new Vector3 (0f, -angle - 55.0f, 0.0f));
		}
		// Controller
		else if(InputManager.Instance.inputType == InputManager.InputType.Controller)
		{
			float horizInput = Input.GetAxis ("RightStickHoriz" + _MoveController.currPlayer.ToString ());
			float horiz = horizInput;// * -90.0F;
			horiz = horiz * -90.0f;

			//(Input.GetAxis ("RightStickHoriz0") + 1) * 90;
			//horiz -= 35; // cos angle

			float vertInput = Input.GetAxis ("RightStickVert" + _MoveController.currPlayer.ToString ());
			float vert = vertInput;//* 90.0F;
			//vert = vert * -180.0f;
			//if (vert < 0)
			//	vert = 180.0F;

			if (vert < 0) {
				vert = 180f;
				horiz = -horiz;
			}

			//Debug.Log (Input.GetAxis ("RightStickVert0"));

			if (vertInput >= 0.7F || vertInput <= -0.7F
				|| horizInput >= 0.7F || horizInput <= -0.7F) 
			{
				_isMovingStickOnce = true;
			}
			else if (vertInput <= 0.15F && vertInput >= -0.15F
				&& horizInput <= 0.15F && horizInput >= -0.15F) 
			{
				_isMovingStickOnce = false;
			}


			if (vertInput >= 0.2F || vertInput <= -0.2F
			    ||
			    horizInput >= 0.2F || horizInput <= -0.2F) {
				_isMovingStick = true;

				if(_checkRou)
					StopCoroutine ("CheckIfHaventShotForOneSecond");

				if (vertInput >= 0.7F || vertInput <= -0.7F
					|| horizInput >= 0.7F || horizInput <= -0.7F) 
						transform.rotation = Quaternion.Euler (new Vector3 (0, 45 + horiz + vert, 0));
			}
			else {


				if(_isMovingStick)
					StartCoroutine ("CheckIfHaventShotForOneSecond");

				_isMovingStick = false;
	


				float horiz2Input = Input.GetAxis ("Vertical" + _MoveController.currPlayer.ToString ());
				float horiz2 = horiz2Input;// * -90.0F;
				horiz2 = horiz2 * -90.0f;

				float vert2Input = Input.GetAxis ("Horizontal" + _MoveController.currPlayer.ToString ());
				float vert2 =  vert2Input;
				if (vert2 < 0) {
					vert2 = 180f;
					horiz2 = -horiz2;
				}



				if (_useDirectionalRotation) {
					if (Mathf.Abs (vert2Input) > 0.3F || Mathf.Abs (horiz2Input) > 0.3F)
						this.transform.localEulerAngles = new Vector3 (0, 90 + vert2 + horiz2, 0);
				}
				//Debug.Log ("Horizontal: " + horiz2Input.ToString() + "  ,   Vertical: " + vert2Input.ToString ()); 
			}
			
		
			//Debug.Log ("RightStickHoriz0: " + horizInput.ToString() + "  ,   RightStickVert0: " + vertInput.ToString ()); 
		}
	}



	IEnumerator CheckIfHaventShotForOneSecond()
	{
		_checkRou = true;

		_useDirectionalRotation = false;
		yield return new WaitForSeconds (0.25F);

		if (_isMovingStick == false) {
			_useDirectionalRotation = true;
		}

		_checkRou = false;
	}

	float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
		return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}

	private void VerticalLook(bool usePhysics)
	{
		float vertical = Input.GetAxis ("Mouse Y") * _sensitivityY;

		Quaternion preRot = this.transform.localRotation;

		if (_Rigidbody != null && usePhysics) 
		{
			Quaternion deltaRotation = Quaternion.AngleAxis(vertical, new Vector3(-1f, 0f, 0f)) ;
			_Rigidbody.MoveRotation (this.transform.localRotation * deltaRotation);
		}
		else
		{
			this.transform.Rotate(new Vector3(-1f, 0f, 0f), vertical);
		}

		// Limits
		if ((this.transform.localRotation.eulerAngles.x < _upperLimit && this.transform.localRotation.eulerAngles.x > _lowerLimit) ||
			(this.transform.localRotation.eulerAngles.x > _lowerLimit && this.transform.localRotation.eulerAngles.x < _upperLimit))
				this.transform.localRotation = preRot;

		// Force z rotation axis to be 0
		Vector3 temp = this.transform.localRotation.eulerAngles;
		temp.z = 0f;
		this.transform.localRotation = Quaternion.Euler (temp);
	}

	public void SetAllowHorizontal(bool state)
	{
		_allowHorizontal = state;
	}

	public void SetSensitivityX(float val)
	{
		_sensitivityX = val;
	}

	public void SetAllowVertical(bool state)
	{
		_allowVertical = state;
	}

	public void SetSensitivityY(float val)
	{
		_sensitivityY = val;
	}

	public void SetUpperLimit(float val)
	{
		_upperLimit = val;
	}

	public void SetLowerLimit(float val)
	{
		_lowerLimit = val;
	}

	public void SetUseRigidbodyIfAvailable(bool state)
	{
		_useRigidbodyIfAvailable = state;
	}
}
