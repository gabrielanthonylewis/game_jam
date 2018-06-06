using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shop : MonoBehaviour {

    //tset vaiables to test if the first button is working

	[SerializeField]
	private Text _currentMoneyTxt = null;

    public GameObject shopUI;
    public Text btnReloadSpeedTxt;
    public Text btnDamageTxt;
    public Text btnMagzineSizeSTxt;
    public Text btnRateOfFireTxt;
    private int curencyTestAmount = 100;
    private int AmmoTestAmount = 1;
	
	public AudioClip chaChing;
    private AudioSource _audioSource = null;




    void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
		_currentMoneyTxt.text = "Money\n" + PlayerStats._instance.money.ToString ();
    }



    private void Update()
    {

    }

	public void OpenShopUI()
	{
		shopUI.SetActive(true);
		Time.timeScale = 0;
		_currentMoneyTxt.text = "Money\n" + PlayerStats._instance.money.ToString ();
	}

    public void CloseShopUI(GameObject UI)
    {
        //closes the shop UI on exit and unpauses the game
        shopUI.SetActive(false);
        Time.timeScale = 1.0f;
    }

    // one button 1 is clicked it trigers this function and gives the player ammo for currency 
    public void BuyAmmo()
    {
        // preforms the buy ammo action
        if (AmmoTestAmount < 100)
        {
            AmmoTestAmount = AmmoTestAmount + 20;
            curencyTestAmount = curencyTestAmount - 10;
        }
    }
		

    public int moneyForMagUpgrade = 40;
    public int moneyForDamageUpgrade = 75;
    public int moneyForReloadUpgrade = 100;
    public int moneyForRateOfFireUpgrade = 100;

	public Text magText = null;
	public Text damageText = null;
	public Text reloadText = null;
	public Text rateOfFireText = null;



    private bool stopBuying4 = false;
    private int UMSCoutn = 0;
    public Button magbar1;
    public Button magbar2;
    public Button magbar3;
    public Button magbar4;
    public Button magbar5;
    public void UpgradeMagazineMultiplier(Text buttonText)
    {
        if(stopBuying4 == false)
        {
            if (PlayerStats.Instance.money - moneyForMagUpgrade >= 0)
            {
                PlayerStats.Instance.AddMoney(-moneyForMagUpgrade);
				_currentMoneyTxt.text = "Money\n" + PlayerStats._instance.money.ToString ();
                moneyForMagUpgrade *= 2;
                GunMultipliers.Instance.UpgradeMagazineMultiplier();
                buttonText.text = moneyForMagUpgrade.ToString();
                magText.text = GunMultipliers.Instance._magazineSizeMultiplier.ToString() + "x";
                UMSCoutn++;
				_audioSource.clip = chaChing;
                _audioSource.Play();
            }
        }
        if (UMSCoutn == 1)
        {
            magbar1.image.color = new Color(0f, 234f, 0f, 255f);
        }
        else if (UMSCoutn == 2)
        {
            magbar2.image.color = new Color(0f, 234f, 0f, 255f);
        }
        else if (UMSCoutn == 3)
        {
            magbar3.image.color = new Color(0f, 234f, 0f, 255f);
        }
        else if (UMSCoutn == 4)
        {
            magbar4.image.color = new Color(0f, 234f, 0f, 255f);
        }
        else if (UMSCoutn == 5)
        {
            magbar5.image.color = new Color(0f, 234f, 0f, 255f);
            stopBuying4 = true;
            btnMagzineSizeSTxt.text = "Fully Upgraded";
        }
    }



    private int UDCount = 0;
    private bool stopBuying3 = false;
    public Button Damagebar1;
    public Button Damagebar2;
    public Button Damagebar3;
    public Button Damagebar4;
    public Button Damagebar5;
    public void UpgradeDamageMultiplier(Text buttonText)
    {
        if(stopBuying3 == false)
        {
            if (PlayerStats.Instance.money - moneyForDamageUpgrade >= 0)
            {
                PlayerStats.Instance.AddMoney(-moneyForDamageUpgrade);
				_currentMoneyTxt.text = "Money\n" + PlayerStats._instance.money.ToString ();
                moneyForDamageUpgrade *= 2;
                GunMultipliers.Instance.UpgradeDamageMultiplier();
                buttonText.text = moneyForDamageUpgrade.ToString();
                damageText.text = GunMultipliers.Instance._damageMultiplier.ToString() + "x";
                UDCount++;
				_audioSource.clip = chaChing;
                _audioSource.Play();
            }
        }

        if (UDCount == 1)
        {
            Damagebar1.image.color = new Color(0f, 234f, 0f, 255f);
        }
        else if (UDCount == 2)
        {
            Damagebar2.image.color = new Color(0f, 234f, 0f, 255f);
        }
        else if (UDCount == 3)
        {
            Damagebar3.image.color = new Color(0f, 234f, 0f, 255f);
        }
        else if (UDCount == 4)
        {
            Damagebar4.image.color = new Color(0f, 234f, 0f, 255f);
        }
        else if (UDCount == 5)
        {
            Damagebar5.image.color = new Color(0f, 234f, 0f, 255f);
            stopBuying3 = true;
            btnDamageTxt.text = "Fully Upgraded";

        }
    }

    private int URCount = 0;
    public Button Reloadbar1;
    public Button Reloadbar2;
    public Button Reloadbar3;
    public Button Reloadbar4;
    public Button Reloadbar5;
    private bool stopBuying2 = false;

    public void UpgradeReloadMultiplier(Text buttonText)
    {
        if(stopBuying2 == false)
        {
            if (PlayerStats.Instance.money - moneyForReloadUpgrade >= 0)
            {
                PlayerStats.Instance.AddMoney(-moneyForReloadUpgrade);
				_currentMoneyTxt.text = "Money\n" + PlayerStats._instance.money.ToString ();
                moneyForReloadUpgrade *= 2;
                GunMultipliers.Instance.UpgradeReloadMultiplier();
                buttonText.text = moneyForReloadUpgrade.ToString();
                reloadText.text = GunMultipliers.Instance._reloadTimeMultiplier.ToString() + "x";
                URCount++;
				_audioSource.clip = chaChing;
                _audioSource.Play();
            }
        }
        if (URCount == 1)
        {
            Reloadbar1.image.color = new Color(0f, 234f, 0f, 255f);
        }
        else if (URCount == 2)
        {
            Reloadbar2.image.color = new Color(0f, 234f, 0f, 255f);
        }
        else if (URCount == 3)
        {
            Reloadbar3.image.color = new Color(0f, 234f, 0f, 255f);
        }
        else if (URCount == 4)
        {
            Reloadbar4.image.color = new Color(0f, 234f, 0f, 255f);
        }
        else if (URCount == 5)
        {
            Reloadbar5.image.color = new Color(0f, 234f, 0f, 255f);
            stopBuying2 = true;
            btnReloadSpeedTxt.text = "Fully Upgraded";
        }
    }




    private int UFRCount = 0;
    private bool stopBuying = false;
    public Button RateOfFirebar1;
    public Button RateOfFirebar2;
    public Button RateOfFirebar3;
    public Button RateOfFirebar4;
    public Button RateOfFirebar5;
 

    public void UpgradeRateOfFireMultiplier(Text buttonText)
    {

        
        if(stopBuying == false)
        {
            if (PlayerStats.Instance.money - moneyForRateOfFireUpgrade >= 0)
            {
                PlayerStats.Instance.AddMoney(-moneyForRateOfFireUpgrade);
				_currentMoneyTxt.text = "Money\n" + PlayerStats._instance.money.ToString ();
                moneyForRateOfFireUpgrade *= 2;
                GunMultipliers.Instance.UpgradeRateOfFireMultiplier();
                buttonText.text = moneyForRateOfFireUpgrade.ToString();
                rateOfFireText.text = GunMultipliers.Instance._rateOfFireMultiplier.ToString() + "x";
                UFRCount++;
				_audioSource.clip = chaChing;
                _audioSource.Play();
            }


        }

        if (UFRCount == 1)
        {
            RateOfFirebar1.image.color = new Color(0f, 234f, 0f, 255f);
        }
        else if (UFRCount == 2)
        {
            RateOfFirebar2.image.color = new Color(0f, 234f, 0f, 255f);
        }
        else if (UFRCount == 3)
        {
            RateOfFirebar3.image.color = new Color(0f, 234f, 0f, 255f);
        }
        else if (UFRCount == 4)
        {
            RateOfFirebar4.image.color = new Color(0f, 234f, 0f, 255f);
        }
        else if (UFRCount == 5)
        {
            RateOfFirebar5.image.color = new Color(0f, 234f, 0f, 255f);
            stopBuying = true;
            btnRateOfFireTxt.text = "Fully Upgraded";
        }

    }



}
