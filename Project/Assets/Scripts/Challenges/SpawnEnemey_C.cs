using UnityEngine;
using System.Collections;

public class SpawnEnemey_C : MonoBehaviour {

    // Use this for initialization

    public challenge myChallenge;
    public int zombiesToSpawn = 25;
    public ChallengeOverseer overseer;
    public Spawning spawn;
    GameObject phill;
    int TimeTillSpawn = 0;
    int enemiesLeft;
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {

        spawn.stopSpawning();

        if (TimeTillSpawn >= 300)
        {
            if (zombiesToSpawn != 0)
            {
                TimeTillSpawn = 0;
                zombiesToSpawn--;

                phill = spawn.spawnEnemyWithName(this.gameObject.transform);
                phill.AddComponent<Tracker>();
                phill.SetActive(true);
                if (phill.GetComponent<Tracker>() == null)
                {
                    Debug.Log("Tracker didn't stick");
                }

                if(zombiesToSpawn == 0)
                {
                    Debug.Log("NO MORE TOO SPAWN");
                }
            }
        }
        else
        {
            TimeTillSpawn++;
        }
    }
}
