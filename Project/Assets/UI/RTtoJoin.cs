using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RTtoJoin : MonoBehaviour {

    int maxPlayers = 0;


    void Update()
    {

        maxPlayers = GameObject.Find("SpawnPlayer").GetComponent<SpawnPlayers>().currActivatedPlayers;

        if (Input.GetKeyDown("e"))
        {
            Debug.Log(maxPlayers);
        }

        if(maxPlayers >= 4)
        {
            gameObject.SetActive(false);
        }


    }

    

	
}
