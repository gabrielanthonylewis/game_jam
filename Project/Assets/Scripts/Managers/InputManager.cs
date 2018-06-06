using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public static InputManager _instance = null;
	public static InputManager Instance { get { return _instance; } }

	public enum InputType
	{
		MouseKeyboard,
		Controller
	}

	[SerializeField]
	private InputType _inputType = InputType.MouseKeyboard;
	public InputType inputType { get { return _inputType; } }

	private void Awake()
	{
		if (_instance != null && _instance != this)
			Destroy (this.GetComponent<InputManager>());
		else
			_instance = this;
	}

}

