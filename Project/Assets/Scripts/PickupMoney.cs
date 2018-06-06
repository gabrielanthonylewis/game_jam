using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PickupSender))]
public class PickupMoney : MonoBehaviour {


	[SerializeField]
	private int _minRange = 0;

	[SerializeField]
	private int _maxRange = 5;

	private PickupSender _PickupSender = null;

	void Start()
	{
		_PickupSender = this.GetComponent<PickupSender> ();

	}

	public void SetRange(int min, int max)
	{
		_minRange = min;
		_maxRange = max;
	}

	public void GiveMoney()
	{
		if (_PickupSender.receiver != null) 
		{
			PlayerStats.Instance.AddMoney (Random.Range (_minRange, _maxRange));
		}

		if(!ObjectPool.Instance.Kill(this.gameObject))
			Destroy (this.gameObject);
	}

}
