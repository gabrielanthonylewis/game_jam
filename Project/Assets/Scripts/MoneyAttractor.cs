using UnityEngine;
using System.Collections;

public class MoneyAttractor : MonoBehaviour {

    [SerializeField]
    float _speedMulti = 1.0f;


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Money")
            return;

        //Debug.Log("ATTRACT");

        Vector3 direction = (other.transform.position - this.transform.position).normalized;

        other.GetComponent<Rigidbody>().AddForce(-direction * _speedMulti * Time.deltaTime, ForceMode.Acceleration);
    }
}
