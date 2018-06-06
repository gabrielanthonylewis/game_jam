using UnityEngine;
using System.Collections;

public class Tape : MonoBehaviour {

    
    public tapeController cont;

    // Use this for initialization
    void Start()
    {

    }

	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!cont.playinTape)
            {
                cont.SendMessage("playNext");
                Destroy(this.gameObject);
            }
        }
    }
}
