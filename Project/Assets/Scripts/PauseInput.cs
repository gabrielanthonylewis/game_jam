using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MoveController))]
public class PauseInput : MonoBehaviour {


	bool _once = false;
	float _downTime = 0.0f;
	bool _allowPause = true;
    bool _isMovingStickOnce = false;


    PauseManager _PauseManager = null;
    MoveController _MoveController = null;

    // Use this for initialization
    void Start () 
	{
        _MoveController = this.GetComponent<MoveController>();
		_PauseManager = GameObject.FindObjectOfType<PauseManager> ();	
	}

	// Update is called once per frame
	void Update () 
	{
		if (InputManager.Instance.inputType == InputManager.InputType.Controller
		   && Input.GetAxis ("Triggers" + _MoveController.currPlayer.ToString ()) == -1.0F) 
		{
			if (_once == false) {
	
				_downTime = Time.time;
				//Debug.Log (_downTime);
				_once = true;
			}
		} else {
			_once = false;
			_allowPause = true;
		}


        float horizInput = Input.GetAxis("RightStickHoriz" + _MoveController.currPlayer.ToString());
        float vertInput = Input.GetAxis("RightStickVert" + _MoveController.currPlayer.ToString());
        if (vertInput < 0)
        {
            vertInput = 180f;
            horizInput = -horizInput;
        }

        if (vertInput >= 0.7F || vertInput <= -0.7F
            || horizInput >= 0.7F || horizInput <= -0.7F)
        {
            _isMovingStickOnce = true;
        }

        float input = Input.GetAxis("RightStickHoriz" + _MoveController.currPlayer.ToString())
              + Input.GetAxis("RightStickVert" + _MoveController.currPlayer.ToString());

        if(input != 0.0f)
        {
            _allowPause = false;
        }


        if (_allowPause == true 
            && Time.time >= (_downTime + 3.0F) 
            && InputManager.Instance.inputType == InputManager.InputType.Controller
            && Input.GetAxis("Triggers" + _MoveController.currPlayer.ToString()) == -1.0F
          /* && _isMovingStickOnce == false*/
          && input == 0.0f)
		{
            //pause
            //Debug.Log("PAUSE");
			_PauseManager.Pause(true);

			_allowPause = false;
		}
	
	}
}
