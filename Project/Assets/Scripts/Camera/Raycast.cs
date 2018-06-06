using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Raycast : MonoBehaviour {

    //Arrays to store gameobjects
    GameObject[] allPlayers;
    GameObject[] allBuildings;
    RaycastHit[] currentHits;
    //List to store all the hits
    List<RaycastHit> allHits = new List<RaycastHit>();
    void Start ()
    {
        //Gets all buildings in scene on start up
        allBuildings  = GameObject.FindGameObjectsWithTag("Building");
    }

    void Update ()
    {
        //Empty the list each update
        allHits.Clear();

        //Incase players die or join. Keep refreshing
        allPlayers = GameObject.FindGameObjectsWithTag("Player");
        //Goes through all players
        for (int i = 0; i < allPlayers.Length; i++)
        {
            //Raycast all returns an array of all hit objects. Draws a ray to all players currently playing
            currentHits = Physics.RaycastAll(transform.position, allPlayers[i].transform.position - transform.position);
            //Adds these current hits to the list. Basically a pushback
            allHits.AddRange(currentHits);

            for (int j = 0; j < currentHits.Length; j++)
            {
                //gets one hit and if that hits a building then make it transparent
                RaycastHit currentHit = currentHits[j];
                if (currentHit.transform.tag == "Building")
                {
                    currentHit.transform.gameObject.GetComponent<ChangeMaterial>().goTransparent();
                }
            }
        }

        for (int i = 0; i < allBuildings.Length; i++)
        {
            //Bool to check if the building should be transparent. If its in the list then it should be.
            bool inList = false;
            //Go through the list of hits
            for (int j = 0; j < allHits.Count; j++)
            {
                //If the id of the current building and the hit match then it should be transparent so it stays the same
                if (allBuildings[i].GetInstanceID() == allHits[j].transform.gameObject.GetInstanceID())
                {
                    inList = true;
                    break;
                }
            }
            //If the building isnt in the list then make it solid again. 
            if (!inList)
            {
                if (allBuildings[i].GetComponent<ChangeMaterial>())
                    allBuildings[i].GetComponent<ChangeMaterial>().goSolid();
            }
        }


        //Need to test new code above with multiple players. Keep below code for now

        //4 if statements to check how many players are playing. Draws a ray to each one that exists
        //if (players.Length == 1 || players.Length == 2 || players.Length == 3 || players.Length == 4)
        //{
        //    RaycastHit[] test;
        //    //Raycast all returns an array of hits
        //    test = Physics.RaycastAll(transform.position, players[0].transform.position - transform.position);
        //    //Add hits onto the list. Basically a pushback.
        //    allTests.AddRange(test);
        //    //Go through all the hits
        //    for (int i = 0; i < test.Length; i++)
        //    {
        //        RaycastHit tes = test[i];
        //        //If the current hit hits a building then make it transparent
        //        if (tes.transform.tag == "Building")
        //        {
        //            tes.transform.gameObject.GetComponent<ChangeMaterial>().goTransparent();
        //        }
        //    }


        //}
        //if (players.Length == 2 || players.Length == 3 || players.Length == 4)
        //{
        //    RaycastHit[] test;
        //    test = Physics.RaycastAll(transform.position, players[1].transform.position - transform.position);
        //    allTests.AddRange(test);
        //    for (int i = 0; i < test.Length; i++)
        //    {
        //        RaycastHit tes = test[i];
        //        if (tes.transform.tag == "Building")
        //        {
        //            tes.transform.gameObject.GetComponent<ChangeMaterial>().goTransparent();
        //        }
        //    }        
        //}
        //if (players.Length == 3 || players.Length == 4)
        //{
        //    RaycastHit[] test;
        //    test = Physics.RaycastAll(transform.position, players[2].transform.position - transform.position);
        //    allTests.AddRange(test);

        //    for (int i = 0; i < test.Length; i++)
        //    {
        //        RaycastHit tes = test[i];
        //        if (tes.transform.tag == "Building")
        //        {
        //            tes.transform.gameObject.GetComponent<ChangeMaterial>().goTransparent();
        //        }
        //    }
        //}
        //if (players.Length == 4)
        //{
        //    RaycastHit[] test;
        //    test = Physics.RaycastAll(transform.position, players[3].transform.position - transform.position);
        //    allTests.AddRange(test);

        //    for (int i = 0; i < test.Length; i++)
        //    {
        //        RaycastHit tes = test[i];
        //        if (tes.transform.tag == "Building")
        //        {
        //            tes.transform.gameObject.GetComponent<ChangeMaterial>().goTransparent();
        //        }
        //    }
        //}
    }
}
