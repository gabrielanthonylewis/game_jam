using UnityEngine;
using System.Collections;

using UnityEngine.UI;

[System.Serializable]
public struct ButtonInstance
{
	public Button button;
	[HideInInspector]
	public Image image;
};

[System.Serializable]
public struct MultiDimentionalButton
{
	public ButtonInstance[] columns;
}

public class ControllerUISelection : MonoBehaviour {

	[SerializeField]
	private MultiDimentionalButton[] _buttons;

    public AudioClip btnClickSound;
    private AudioSource _audioSource = null;

    private int _currRow = -1;
	private int _currCol = -1;

	private bool _hasGoneBackVert = false;
	private bool _hasGoneBackHoriz = false;
	private bool _hasGoneBackTrigg = false;

	private bool _waitRou = false;
	bool _hasMoved = false;

	// Use this for initialization
	void Start () 
	{
        _audioSource = this.GetComponent<AudioSource>();

        if (InputManager.Instance.inputType != InputManager.InputType.Controller)
			return;

		// Assign "Image" component (optimisation reasons)
		foreach (MultiDimentionalButton row in _buttons)
		{
			for(int col = 0; col < row.columns.Length; col++)
			{
				if (row.columns[col].button == null)
					continue;

				ButtonInstance newInst = row.columns[col];
				newInst.image = row.columns[col].button.gameObject.GetComponent<Image> ();
				row.columns[col] = newInst;
			}
		}


	}
	
	// Update is called once per frame
	void Update ()
	{
		if (InputManager.Instance.inputType != InputManager.InputType.Controller)
			return;
		

		

		// move up/down
		float vert = Input.GetAxis ("Vertical"); // -1 to 1
	
		if (_hasGoneBackVert == false && vert >= -0.1f && vert <= 0.1f )
			_hasGoneBackVert = true;
		
		if (_hasGoneBackVert)
		{
			bool a = false;
			if (vert < -0.6f) {

				if(_hasMoved)
				MoveBy (+1, 0);
				a = true;
			} else if (vert > 0.6f) {

				if(_hasMoved)
				MoveBy (-1, 0);
				a = true;
			}

			if (a == true) 
			{

				if (_hasMoved == false) {

				
					if (_buttons.Length > 1) 
					{

						MoveBy (+1, +1);
						_hasMoved = true;
					}
				
				}
			}
		}


		// move left/right
		float horiz = Input.GetAxis ("Horizontal");

		if (_hasGoneBackHoriz == false && horiz >= -0.1f && horiz <= 0.1f )
			_hasGoneBackHoriz = true;

		if (_hasGoneBackHoriz)
		{
			bool a = false;
			if (horiz > 0.6f) {
				MoveBy (0, +1);
				a = true;
			} else if (horiz < -0.6f) {
				MoveBy (0, -1);
				a = true;
			}

			if (a == true) 
			{

				if (_hasMoved == false) {


					if (_buttons.Length > 0 && _buttons[0].columns.Length > 1) 
					{

						MoveBy (+1, +1);
						_hasMoved = true;
					}

				}
			}
		}


		if (_currRow < 0 || _currCol < 0)
			return;

		// press button
		float trigg = Input.GetAxis ("Triggers");

		if (_hasGoneBackTrigg == false && trigg >= -0.1f && trigg <= 0.1f )
			_hasGoneBackTrigg = true;

		if (_hasGoneBackTrigg && trigg == -1.0F)
		{
			_hasGoneBackTrigg = false;


			if (_buttons [_currRow].columns [_currCol].image != null && _buttons [_currRow].columns [_currCol].button != null)
			{
				_buttons [_currRow].columns [_currCol].image.color = _buttons [_currRow].columns [_currCol].button.colors.pressedColor;

				// Call OnClick functions if there are some
				if (_buttons [_currRow].columns [_currCol].button.onClick != null)
					_buttons [_currRow].columns [_currCol].button.onClick.Invoke ();
			}
		}
	}

	private IEnumerator WaitBeforeNextMovement()
	{
		_waitRou = true;

		yield return new WaitForSeconds (0.2f);


		_hasGoneBackHoriz = true;
		_hasGoneBackVert = true;


		_waitRou = false;
	}





	private void  MoveBy(int rowAmount, int colAmount)
	{
		if (_currRow >= 0 && _currCol >= 0) 
		{
			if(_buttons [_currRow].columns [_currCol].image != null && _buttons [_currRow].columns [_currCol].button != null)
				_buttons [_currRow].columns [_currCol].image.color = _buttons [_currRow].columns [_currCol].button.colors.normalColor;
		}	

		if ((rowAmount != 0 || colAmount != 0) && _waitRou == true) {
			StopCoroutine ("WaitBeforeNextMovement");
			_waitRou = false;
		}

		if ((rowAmount != 0 || colAmount != 0) && _waitRou == false) {
			StartCoroutine ("WaitBeforeNextMovement");
		}

		if (rowAmount != 0) 
		{
			_hasGoneBackVert = false;


			

			_currRow += rowAmount;


			if (_currRow < 0 )
				_currRow = _buttons.Length - 1;
			else if (_currRow > _buttons.Length - 1)
				_currRow = 0;

			if (_currCol > _buttons [_currRow].columns.Length - 1)
				_currCol = 0;
		}

		if (_currRow >= 0 && colAmount != 0) 
		{
			_hasGoneBackHoriz = false;

			_currCol += colAmount;

		
			if (_currCol < 0)
				_currCol = _buttons [_currRow].columns.Length - 1;
			else if (_currCol > _buttons [_currRow].columns.Length - 1)
				_currCol = 0;
		}

		if (_currCol < 0 && _currRow < 0)
			return;

        _audioSource.clip = btnClickSound;
        _audioSource.Play();

		if(_buttons [_currRow].columns [_currCol].image != null && _buttons [_currRow].columns [_currCol].button != null)
			_buttons [_currRow].columns [_currCol].image.color = _buttons [_currRow].columns [_currCol].button.colors.highlightedColor;
	}


	public void ClickShowName(string output)
	{
		Debug.Log ("Pressed: " + output);

	}

	void OnEnable()
	{
		_hasMoved = false;

		if (_currCol < 0 && _currRow < 0)
			return;

		if(_buttons [_currRow].columns [_currCol].image != null && _buttons [_currRow].columns [_currCol].button != null)
			_buttons [_currRow].columns [_currCol].image.color = _buttons [_currRow].columns [_currCol].button.colors.normalColor;

		_currCol = -1;
		_currRow = -1;


	}
}
