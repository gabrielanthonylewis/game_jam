using UnityEngine;
using System.Collections;

public class GunMultipliers : MonoBehaviour {

    public static GunMultipliers _instance = null;
    public static GunMultipliers Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.GetComponent<GunMultipliers>());
        else
            _instance = this;
    }

    public  float _magazineSizeMultiplier = 1.0f;
    public  float _damageMultiplier = 0.0f;
    public  float _reloadTimeMultiplier = 1.0f;
    public float _rateOfFireMultiplier = 1.0f;

    public void UpgradeMagazineMultiplier()
    {
        _magazineSizeMultiplier += 0.1f;
    }

    public void UpgradeDamageMultiplier()
    {
        _damageMultiplier += 1.0f;
    }

    public void UpgradeReloadMultiplier()
    {
        _reloadTimeMultiplier *= 0.9f;
    }
    public void UpgradeRateOfFireMultiplier()
    {
        _rateOfFireMultiplier *= 0.9f;
    }

}
