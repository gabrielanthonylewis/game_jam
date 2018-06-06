using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunShoot : MonoBehaviour
{

    [System.Serializable]
    private enum Attachment
    {
        Silencer = 1,
        Grip = 2
    };

    [System.Serializable]
    struct AttachmentObject
    {
        public Attachment attachment;
        public GameObject obj;
    };

    [SerializeField]
    private AttachmentObject[] _possibleAttachments;

    // TODO: Allow adding an attachment
    // TODO: Attachment effects
    // TODO: Drag in GameObjects 
    // TODO: Make attachments do things
    List<Attachment> _currentAttachments = new List<Attachment>();



    [SerializeField]
    private float rateOfFire = 1;

    [SerializeField]
    private float _chamberTime = 0.05f;
    public float chamberTime { get { return _chamberTime; } }

    [SerializeField]
    private GameObject _bulletPrefab = null;

    [SerializeField]
    private Transform _firePoint = null;

    [SerializeField]
    private Light _muzzleFlashLight = null;

    [SerializeField]
    private Light _torch = null;

    [SerializeField]
    private float _bulletSpeedMag = 1000.0f;

    [SerializeField]
    private int _magazineSize = 30;

    [SerializeField]
    private float _reloadTime = 0.85f;

    [SerializeField]
    private int _damage = 1;

    private bool shootRou = false;
    private bool _reloadRou = false;

	[SerializeField]
	private AudioSource _AudioSource = null;

    [SerializeField]
    private int _currentBullets = 0;
    public int currentBullets { get { return _currentBullets; } set { _currentBullets = value; } }


    private float _reloadFillAmount = 0.0f;
    public float reloadFillAmount { get { return _reloadFillAmount; } }



    //[SerializeField]
    //private int _initialBullets = 100;
    //public int initialBullets { get { return _initialBullets; }  set { _initialBullets = value; }}

    public enum ShootType
    {
        Auto,
        OneThenReturn,
        OneThenReturnShotgunBurst
    }

    [SerializeField]
    private ShootType _shootType = ShootType.Auto;
    public ShootType shootType { get { return _shootType; } }


    void Start()
    {
		_AudioSource = this.GetComponent<AudioSource> ();

        _currentBullets = (int)(_magazineSize * GunMultipliers.Instance._magazineSizeMultiplier);

        if (_bulletPrefab == null)
        {
            _bulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
        }
    }

    void Update()
    {
        if (_reloadRou)
        {
            _reloadFillAmount -= Time.deltaTime / (_reloadTime * GunMultipliers.Instance._reloadTimeMultiplier);
        }

        if (!_muzzleFlashLight)
            return;

        if (_muzzleFlashLight.intensity <= 0)
            return;


        _muzzleFlashLight.intensity -= Time.deltaTime * 40;
    }

    public void SwitchTorchLight(bool state)
    {
        _torch.enabled = state;
    }

    public void Fire()
    {
        StartCoroutine("IShoot");
    }

    IEnumerator IShoot()
    {
        if (shootRou)
            yield break;

        shootRou = true;

        for (int i = 0; i < rateOfFire; i++)
        {
			// Reload if empty
            if (_currentBullets <= 0)
            {
                StartCoroutine("Reload");
                break;
            }

    		// Play sound
			if (_AudioSource)
				_AudioSource.PlayOneShot(_AudioSource.clip);
            

            // Fire straight from barrel
            FireProjectile(_firePoint.position, _firePoint.rotation, _firePoint.transform.forward * _bulletSpeedMag);

			// Shotgun: fire both left and right (angled)
            if (_shootType == ShootType.OneThenReturnShotgunBurst)
            {
                FireProjectile(_firePoint.position, _firePoint.rotation, (_firePoint.transform.forward + (-_firePoint.transform.right / 5.0F)) * _bulletSpeedMag);
                FireProjectile(_firePoint.position, _firePoint.rotation, (_firePoint.transform.forward + (_firePoint.transform.right / 5.0F)) * _bulletSpeedMag);
            }


			// Show muzzle flash
            if (_muzzleFlashLight)
            {

                if (_muzzleFlashLight.intensity == 0.0f)
                    _muzzleFlashLight.intensity = 2.0f;

                _muzzleFlashLight.intensity += 1.0f;

            }
        }

		// time until next shot
        yield return new WaitForSeconds(_chamberTime * GunMultipliers.Instance._rateOfFireMultiplier);

        shootRou = false;

    }

    private void FireProjectile(Vector3 pos, Quaternion rot, Vector3 acceleration)
    {
        if (_currentBullets <= 0)
        {
            //_currentBullets = 0;
            StartCoroutine("Reload");
            return;
        }

        GameObject projectile = null;

        if (ObjectPool.Instance == null)
        {
            projectile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            projectile.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
            projectile.AddComponent<Rigidbody>();
            projectile.AddComponent<DestroyAfterX>();
            projectile.AddComponent<BulletHit>();
        }
        else
            projectile = ObjectPool.Instance.GetFirstFreeObject("Bullet");

        if (projectile == null)
            return;

        projectile.transform.position = pos;
        projectile.transform.rotation = rot;

        projectile.GetComponent<BulletHit>().damage = _damage * (int)GunMultipliers.Instance._damageMultiplier;

        projectile.SetActive(true);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.AddForce(acceleration * rb.mass);
            rb.useGravity = false;
        }


        _currentBullets--;

        if (_currentBullets <= 0)
        {
            StartCoroutine("Reload");
        }

    }

    private IEnumerator Reload()
    {
        if (_reloadRou == true)
            yield break;

        _reloadRou = true;


        _reloadFillAmount = 1.0f;

        yield return new WaitForSeconds(_reloadTime * GunMultipliers.Instance._reloadTimeMultiplier);

        _currentBullets = (int)(_magazineSize * GunMultipliers.Instance._magazineSizeMultiplier);

        _reloadRou = false;

        _reloadFillAmount = 0.0f;
    }

    void OnEnable()
    {
        if (_reloadFillAmount > 0.0f)
            StartCoroutine("Reload");
    }

    void OnDisable()
    {
        //	StopAllCoroutines ();
        shootRou = false;
        _reloadRou = false;
    }
}
