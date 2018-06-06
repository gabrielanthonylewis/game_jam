using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour {

	[SerializeField]
	private GunShoot _currentGun = null;
	public GunShoot currentGun { set { _currentGun = value; } }

	[SerializeField]
	private Text _ammoUI = null;

    [SerializeField]
    private GameObject _ammoUIContainer = null;

    [SerializeField]
    private Image _reloadUI = null;

	[SerializeField]
	private LookController _LookController = null;
    [SerializeField]
    private MoveController _MoveController = null;

    private bool _fadeRou = false;
    private bool _waitTilNextShotRou = false;

    private bool _shootOnceTracker = false;

    private bool _rightTriggerDown = true;

    bool _allowShooting = true;
    void Start()
    {
        _reloadUI.enabled = false;
    }

	// Update is called once per frame
	void Update () 
	{

		if (_currentGun == null) {
			_ammoUI.text = "";
            _ammoUIContainer.SetActive(false);
            _reloadUI.fillAmount = 0.0f;
            _reloadUI.enabled = false;

            return;
		}

		//todo, will probs want to do inbetween, not 0 and 1? (right left right left all the way to middle)

		if (!_shootOnceTracker && !_LookController.isMovingStickOnce) {

			_shootOnceTracker = true;
		}


        if ((Input.GetAxis("Triggers" + _MoveController.currPlayer.ToString()) >= 0.0F))
        {
            _rightTriggerDown = false;

        }

        float input = Input.GetAxis("RightStickHoriz" + _MoveController.currPlayer.ToString())
            + Input.GetAxis("RightStickVert" + _MoveController.currPlayer.ToString());
  


        if (
			(_currentGun.shootType == GunShoot.ShootType.Auto
				&& Input.GetKey (KeyCode.Mouse0) && InputManager.Instance.inputType == InputManager.InputType.MouseKeyboard)
			||
			((_currentGun.shootType == GunShoot.ShootType.OneThenReturn || _currentGun.shootType == GunShoot.ShootType.OneThenReturnShotgunBurst)
				&& Input.GetKeyDown (KeyCode.Mouse0) && InputManager.Instance.inputType == InputManager.InputType.MouseKeyboard)
			|| 
			((_currentGun.shootType == GunShoot.ShootType.Auto
				&& _LookController.isMovingStick) && InputManager.Instance.inputType == InputManager.InputType.Controller
                && Input.GetAxis("Triggers" + _MoveController.currPlayer.ToString()) == -1.0F
                && input != 0.0f)
			||
			((_currentGun.shootType == GunShoot.ShootType.OneThenReturn || _currentGun.shootType == GunShoot.ShootType.OneThenReturnShotgunBurst) &&
				/*_shootOnceTracker && */ (!_rightTriggerDown || _allowShooting) && 
                /*_LookController.isMovingStickOnce&&*/  InputManager.Instance.inputType == InputManager.InputType.Controller
                  && Input.GetAxis("Triggers" + _MoveController.currPlayer.ToString()) == -1.0F
                  && input != 0.0f)
		)
		{

            _rightTriggerDown = true;
            _shootOnceTracker = false;


			//_LookController.isMovingStickOnce = false;
			if(_fadeRou)
				StopAllCoroutines ();

			_fadeRou = false;




			_currentGun.Fire ();
            _allowShooting = false;
            StartCoroutine(WaitTilNextShot(_currentGun.chamberTime));
            if (_ammoUI != null)
            {
                if (_currentGun.reloadFillAmount > 0.0f)
                {
                    _ammoUI.enabled = false;

                    _ammoUIContainer.SetActive(false);

                }
                else
                {
                    _ammoUI.enabled = true;
                    _ammoUIContainer.SetActive(true);
                    _ammoUI.text = _currentGun.currentBullets.ToString();
                }
            }
         

		} else {
			if(_fadeRou == false)
				StartCoroutine ("FadeOut");
		}

        if(_currentGun != null)
        {
            if (_currentGun.reloadFillAmount > 0.0f)
            {
                _reloadUI.enabled = true;
                _reloadUI.fillAmount = _currentGun.reloadFillAmount;
            }
            else
            {
                _reloadUI.enabled = false;
                _reloadUI.fillAmount = 0.0f;
            }
        }

	}

	IEnumerator FadeOut()
	{
		_fadeRou = true;
		yield return new WaitForSeconds (0.5f);
		_ammoUI.text = "";
        _ammoUIContainer.SetActive(false);

    }

    IEnumerator WaitTilNextShot(float time)
    {
        if (_waitTilNextShotRou)
            yield return null;

        _waitTilNextShotRou = true;
        
        yield return new WaitForSeconds(time);
        _waitTilNextShotRou = false;
        _allowShooting = true;
    }
}
