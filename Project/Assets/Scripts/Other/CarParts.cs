using UnityEngine;
using System.Collections;

public class CarParts : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats.Instance.AddCarPart();

            Destroy(this.gameObject);
        }
    }
}
