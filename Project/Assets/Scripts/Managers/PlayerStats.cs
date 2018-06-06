using UnityEngine;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour {

	public static PlayerStats _instance = null;
	public static PlayerStats Instance { get { return _instance; } }

	[SerializeField]
	private int _money = 100;
	public int money { get { return _money; } }

	[SerializeField]
    private int _kills = 0;
    public int kills { get { return _kills; } }

    [SerializeField]
    private int _level = 1;
    public int level { get { return _level; } }

    [SerializeField]
    private int _respawns = 5;
    public int respawns { get { return _respawns; } }

    public int Kills_Needed_For_Level_Up;

    public int Level_Needed_For_Challenge;

    [SerializeField]
    private int killNeededInChallenge = 50;

    [SerializeField]
    private int carPartsGot = 0;

    [SerializeField]
    private int carLevel = 0;

	[SerializeField]
	private Text _moneyUI = null;

    [SerializeField]
    private Text playerKillsUI = null;

    public Image[] carPartsUI = null;

    [SerializeField]
    private Text _respawnUI = null;

    

    private GameObject[] players;

    private void Awake()
	{
		if (_instance != null && _instance != this)
			Destroy (this.GetComponent<PlayerStats>());
		else
			_instance = this;
	}

	private void Start()
	{
        players = GameObject.FindGameObjectsWithTag("Player");
		UpdateMoneyUI ();
        UpdateRespawnUI();
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AddCarPart();
        }
    }

	public void AddMoney(int x)
	{
		_money += x;

		if (_money < 0)
			_money = 0;
	
		//AddKill();
		UpdateMoneyUI ();
	}

    public void AddCarPart()
    {
        if(carPartsGot < 4)
        {
            carPartsGot += 1;
            MessagePopup.Instance.AddMessage("Car Part Acquired");
            UpdatePartsUI(carPartsGot - 1);
        }
    }

    public int GetCarParts()
    {
        return carPartsGot;
    }

    public int GetCarLevel()
    {
        return carLevel;
    }

    public void SetCarLevel()
    {
        carLevel += 1;

        if(carLevel == 4)
        {
            MessagePopup.Instance.AddMessage("Bus built!");
        }
    }
    public void AddKill()
    {
        _kills += 1;

        UpdateKills();

        if (_kills % Kills_Needed_For_Level_Up == 0)
        {
            IncreaseLevel();
            return;
        }
    }

    private void IncreaseLevel()
    {
        _level += 1;

        if (_level == 2)
        {
            players = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject currentPlayer in players)
            {
                currentPlayer.GetComponentInChildren<Inventory>().Add("Shotgun");
            }
            MessagePopup.Instance.AddMessage("Shotgun Unlocked!");
        }
        if (_level == 4)
        {
            players = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject currentPlayer in players)
            {
                currentPlayer.GetComponentInChildren<Inventory>().Add("UZI");
            }
            MessagePopup.Instance.AddMessage("Uzi Unlocked!");
        }
        if (_level == 6)
        {
            players = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject currentPlayer in players)
            {
                currentPlayer.GetComponentInChildren<Inventory>().Add("AK");
            }
            MessagePopup.Instance.AddMessage("AK47 Unlocked!");
        }
        if (_level == 8)
        {
            players = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject currentPlayer in players)
            {
                currentPlayer.GetComponentInChildren<Inventory>().Add("MachineGun");
            }
            MessagePopup.Instance.AddMessage("M4 Unlocked!");
        }

        if (_level % Level_Needed_For_Challenge == 0)
        {
            gameObject.GetComponent<SpawnChallenge>().EnableRandomChallenge();
            //tell challenge how many kills

            killNeededInChallenge += 50;
        }
    }

    private void UpdateKills()
    {
        playerKillsUI.text =_kills.ToString();
    }

    private void UpdateMoneyUI()
	{
		if (_moneyUI == null)
			return;
		
		_moneyUI.text = _money.ToString();
	}

    public void AddRespawns(int i)
    {
        _respawns += i;

        if(_respawns < 0)
        {
            _respawns = 0;
        }

        UpdateRespawnUI();
    }

    private void UpdateRespawnUI()
    {
        if (_respawnUI == null)
            return;

        _respawnUI.text = _respawns.ToString();
    }

    private void UpdatePartsUI(int currentPart)
    {
        carPartsUI[currentPart].enabled = true; 

    }
}
