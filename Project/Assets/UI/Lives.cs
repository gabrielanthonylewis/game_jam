using UnityEngine;
using System.Collections;

public class Lives : MonoBehaviour {

    int lives = 0;

	// Use this for initialization
	void Start () {
        // lives = GameObject.Find("PlayerMain0").GetComponent<Health>()._currentLives;
        lives = 4;
    }
	
	// Update is called once per frame
	void Update () {
        
        
        
        if (Input.GetKeyDown("r"))
        {
            Debug.Log(lives);
        }
        
        if(lives == 0)
        {
            gameObject.SetActive(false);
        }

    }

    
}
