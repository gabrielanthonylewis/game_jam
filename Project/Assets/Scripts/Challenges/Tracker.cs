using UnityEngine;
using System.Collections;

public class Tracker : MonoBehaviour {

    public ChallengeOverseer Overseer = null;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        if(GameObject.FindGameObjectWithTag("Overseer") != null){
            Overseer = GameObject.FindGameObjectWithTag("Overseer").GetComponent<ChallengeOverseer>();
        }
        
    }

    public void KillYourselfIRL()
    {
        if (Overseer != null)
        {
            Overseer.Killed();
            Destroy(this.GetComponent<Tracker>());
        }
        else
        {
            Debug.Log("OverSeer is null");
            Destroy(this.GetComponent<Tracker>());
        }
    }
}
