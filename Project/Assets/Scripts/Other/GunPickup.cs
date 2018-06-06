using UnityEngine;
using System.Collections;

public class GunPickup : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats.Instance.AddCarPart();

            Destroy(this.gameObject);
        }
    }
}
