using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ActivateChallenge : MonoBehaviour {

    public GameObject myChallenge;

    //List<GameObject> contained;

    bool killAllZombies = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit(Collider other)
    {

        if (other.tag != "Player" && other.tag != "enemy")
        {
            return;
        }

       // if (other.tag == "enemy")
        //{
        //    contained.Remove(other.GetComponent<GameObject>());
        //    return;
      //  }

        if (other.gameObject.tag == "Player")
        {
            if (myChallenge.activeInHierarchy == true)
                myChallenge.SendMessage("ActivateYourself", 1);
        }
    }

    void OnTriggerEnter(Collider other)
    {
       

        if (other.gameObject.tag != "Player" && other.gameObject.tag != "enemy")
        {
            return;
            
        }


        if (other.gameObject.tag == "Player")
        {
            if (myChallenge.activeInHierarchy == true)
                myChallenge.SendMessage("ActivateYourself", 2);
        }

    
     

       
    }


    

    public void setKillAllZombies()
    {
       
        EStatePattern[] allZombies = GameObject.FindObjectsOfType<EStatePattern>();
        for(int i = 0; i < allZombies.Length; i++)
        {
            allZombies[i].gameObject.GetComponent<Health>().LoseLivesAtChallenge(9999999);
        }

       
    }
}
