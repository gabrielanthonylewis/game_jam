using UnityEngine;
using System.Collections;

public class BulletHit : MonoBehaviour {


	[SerializeField]
	private Collider _normalColliderToTurnOn = null;

	private bool hasHitAlready = false;

	private DestroyAfterX _DestroyAfterX = null;

    private int _damage = 1;
    public int damage { set { _damage = value; } }

	void Start()
	{
		_DestroyAfterX = this.GetComponent<DestroyAfterX> ();
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player"
			|| other.gameObject.tag == "Bullet"
            || other.gameObject.tag == "IgnoreBulletHit")
            return;

		if (hasHitAlready == true)
			return;

		hasHitAlready = true;

		if (_normalColliderToTurnOn)
			_normalColliderToTurnOn.enabled = true;

		Rigidbody rb = this.GetComponent<Rigidbody> ();
		if (rb != null) {
			rb.velocity = Vector3.zero;
			rb.useGravity = true;
		}

        if (other.gameObject.GetComponent<challenge>() != null)
        {
            other.gameObject.GetComponent<challenge>().start();
            Debug.Log("start");
        }


        if (other.gameObject.GetComponent<Health> () == null) {
			if (_DestroyAfterX != null)
				_DestroyAfterX.Die ();
			return;
		}

        other.gameObject.GetComponent<Health> ().LoseLives (_damage);

		if (!ObjectPool.Instance.Kill (this.gameObject)) {
			Destroy (this.gameObject);
		}
	}

	void OnDisable()
	{
		hasHitAlready = false;
	}
}
