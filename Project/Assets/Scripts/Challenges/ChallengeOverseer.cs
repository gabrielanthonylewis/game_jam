using UnityEngine;
using System.Collections;

public class ChallengeOverseer : MonoBehaviour
{

    int enemiesLeft = 0;
    int enemiesKilled = 0;
    int enemiesSpawned = 0;
    bool counting = false;
    public Spawning spawner;
    public int neededForChallenge;
    public GameObject[] ThingsToDeleteWhenDone;
    public int timeTill = 10000000;
    public int Timer = 0;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesKilled >= neededForChallenge)
        {
            foreach (var thing in ThingsToDeleteWhenDone)
            {
                Destroy(thing);
            }
            spawner.enableSpawning();

            Transform[] activePlayers = SpawnPlayers.Instance.GetActivePlayers();

            foreach(var persons in activePlayers)
            {
                persons.GetComponent<Health>().RegenHealth();
            }

            enemiesKilled = 0;
            MessagePopup.Instance.AddMessage("Challenge Complete!");

            startCounting();  
        }
        if (counting)
        {
            Timer++;
            if (Timer >= timeTill)
            {
                Destroy(this.gameObject);
                

            }

        }
    }

    void Spawned()
    {
        enemiesSpawned++;
    }

    public void Killed()
    {
        enemiesKilled++;
    }

   public void startCounting ()
   {
        if(!counting)
        {
            counting = true;
        }

   }
}
