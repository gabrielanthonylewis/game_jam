using UnityEngine;
using System.Collections;

public class DestroyAfterX : MonoBehaviour {

	[SerializeField]
	private float _idleTimeBeforeDeath = 10.0f;

	[SerializeField]
	private float _time = 5.0f;

	[SerializeField]
	private bool _inactiveOnDeathNoPool = false;

	public void Die()
	{
		if (this.gameObject.activeSelf == false)
			return;
		
		StartCoroutine(DieAfter(_time));
	}
	IEnumerator DieAfter(float time)
	{
		yield return new WaitForSeconds (time);

		if (ObjectPool.Instance == null)
			Destroy (this.gameObject);
		else if (_inactiveOnDeathNoPool)
			this.gameObject.SetActive (false);
		else
			ObjectPool.Instance.Kill(this.gameObject);

		StopAllCoroutines ();
	}

	void OnEnable()
	{
		StartCoroutine(DieAfter(_idleTimeBeforeDeath));
	}
}
