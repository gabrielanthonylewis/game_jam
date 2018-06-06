using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	[SerializeField]
	private int _intialLives = 1;

	[SerializeField]
	private int _currentLives = 0;

	[SerializeField]
	private GameObject[] _setActiveOnDeath;

	[SerializeField]
	private bool _destroyOnDeath = true;

	[SerializeField]
	private bool _inactiveOnDeath = false;

    public AudioClip deathSound;
    public AudioClip jabSound;
    private AudioSource _audioSource = null;

    public GameObject hitEffect = null;

    public Image HealthImage;

    public GameObject HealthBar;

    ParticleSystem hitPS;

    private bool _invincible = false;

    [SerializeField]
    private GameObject _shield = null;


    public ChallengeOverseer Tracker;

    // Use this for initialization
    void Start () {

        _audioSource = this.GetComponent<AudioSource>();
        Initialise ();

        if(hitEffect != null)
        {
            hitPS = hitEffect.GetComponent<ParticleSystem>();

        }
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Return)) 
		{
			LoseLives (_currentLives);
		}

        UpdateHealthUI();
	}

	public void Initialise()
	{
		_currentLives = _intialLives;

        if(HealthBar != null)
        {
            HealthImage = HealthBar.GetComponent<Image>();
        }
	}

    public void Invincible()
    {
        _invincible = !_invincible;
        _shield.SetActive(_invincible);
    }

    public void LoseLivesAtChallenge(int x)
    {
       

        _currentLives -= x;
      //  _audioSource.clip = jabSound; // plays the jab sound when the player is hit
       // _audioSource.Play();

        if (_currentLives <= 0)
        {
            _currentLives = 0;

        //    _audioSource.clip = deathSound; // plays the deathsound once the player is out of health.
         //   _audioSource.Play();
        }

        if (this.GetComponent<Tracker>() != null)
        {
            //Only zombies spawned by the challenge have a tracker on
            this.GetComponent<Tracker>().KillYourselfIRL();
        }

        if (this.GetComponent<EStatePattern>())
        {

            bool fromDeath = false;
            this.GetComponent<EStatePattern>().toDead(fromDeath);
        }
        else
        {

            if (_destroyOnDeath || _inactiveOnDeath)
            {
                if (!ObjectPool.Instance.Kill(this.gameObject))
                {
                    if (_inactiveOnDeath)
                        this.gameObject.SetActive(false);

                    if (_destroyOnDeath)
                        Destroy(this.gameObject);
                }
            }
        }

        if (_setActiveOnDeath.Length > 0)
            this.gameObject.SetActive(false);
        for (int i = 0; i < _setActiveOnDeath.Length; i++)
        {
            _setActiveOnDeath[i].SetActive(true);
        }
    }


    public void LoseLives(int x)
	{
        if (_invincible)
            return;

		if (_currentLives <= 0)
			return;
		
		_currentLives -= x;
        if (this.gameObject.tag == "Player")
        {
            _audioSource.clip = jabSound; // plays the jab sound when the player is hit
            _audioSource.Play();
        }
        if (hitPS != null)
        {
            hitPS.Play();
        }

		if (_currentLives <= 0)
		{
			_currentLives = 0;
           // Debug.Log("aaa");
            if (this.gameObject.tag == "Player")
            {
                   _audioSource.clip = deathSound; // plays the deathsound once the player is out of health.
                 _audioSource.Play();

                // Debug.Log("bbb: " + PlayerStats.Instance.respawns);
                PlayerStats.Instance.AddRespawns(-1);
              //  Debug.Log("ccc: " + PlayerStats.Instance.respawns);
                if (PlayerStats.Instance.respawns > 0)
                {
                    Debug.Log("respawnn");
                    _currentLives = _intialLives;

                    _invincible = true;
                    if (_shield)
                    _shield.SetActive(true);
                    StartCoroutine("InvincibleTimer");
                    return;
                }
            }

            if(hitEffect != null)
            {
                GameObject deathEffect = Instantiate(hitEffect, transform.position + Vector3.up, transform.rotation) as GameObject;
                deathEffect.GetComponent<ParticleSystem>().Play();
                Destroy(deathEffect, 1);

                //hitEffect.transform.parent = null;
                //hitEffect = null;
            }

            if (this.GetComponent<Tracker>() != null)
            {
                //Only zombies spawned by the challenge have a tracker on
                this.GetComponent<Tracker>().KillYourselfIRL();
            }

            if (this.GetComponent<EStatePattern> ()) {

                bool fromDeath = true;
				this.GetComponent<EStatePattern> ().toDead (fromDeath);

                
            }
			else
            { 
				if (_destroyOnDeath || _inactiveOnDeath) {
					if (!ObjectPool.Instance.Kill (this.gameObject)) 
					{
						if (_inactiveOnDeath)
							this.gameObject.SetActive (false);

						if(_destroyOnDeath)
							Destroy (this.gameObject);
					}
				}
			}

			//if player?
			if(DeathManager.Instance != null)
				DeathManager.Instance.CheckGameOver ();

	
			if (_setActiveOnDeath.Length > 0)
				this.gameObject.SetActive (false);
			for (int i = 0; i < _setActiveOnDeath.Length; i++) {
				_setActiveOnDeath [i].SetActive (true);
			}

		
		}
	}

	public void HealthIncrease(int x)
    {
        if(this.GetComponent<EStatePattern>())
        {
            _intialLives = x;
            Initialise();
        }
    }
	
	void OnEnable()
	{
		Initialise ();
	}

    void UpdateHealthUI()
    {
        if(HealthImage != null)
        {
            HealthImage.fillAmount = _currentLives * (float)0.20;
        }
    }

    public void RegenHealth()
    {
        _currentLives = _intialLives;
    }
    bool invicbleRou = false;
    IEnumerator InvincibleTimer()
    {
        if (invicbleRou)
            yield return null;

        invicbleRou = true;

        yield return new WaitForSeconds(3.0f);

        if(_shield)
        _shield.SetActive(false);
        _invincible = false;
        invicbleRou = false;
    }
}
