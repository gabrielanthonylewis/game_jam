using UnityEngine;
using System.Collections;

public class CreateChallengeEnemy : MonoBehaviour {

    public challenge myChallenge;
    public ChallengeOverseer overseer;
    public Spawning spawn;
    public GameObject thisThing;
    int TimeTillSpawn = 0;
    int enemiesLeft;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

     

        if (TimeTillSpawn >= 100)
        {
            TimeTillSpawn = 0;

            spawn.spawnEnemy(thisThing);

           //DeathTransmuter morris =  Instantiate(attatchThing);

           // morris.myObject = phill;
           // morris.ToSendTo = overseer;

           // if(morris.myObject == null || morris.ToSendTo == null)
           // {
           //     Debug.Log("things are null");
           // }

            //// Spawn Zombie
            //GameObject newEnemy = null;

            //newEnemy = ObjectPool.Instance.GetFirstFreeObject("Enemy");

            //newEnemy.transform.position = this.gameObject.transform.position;
            //newEnemy.transform.rotation = this.gameObject.transform.rotation;

            //newEnemy.GetComponent<EStatePattern>().navMeshAgent.enabled = true;
            //// Attach component to zombie
            //trackChallenge challenge = newEnemy.AddComponent<trackChallenge>();

            //newEnemy.gameObject.tag = "zomble";
            //challenge.myChallenge = myChallenge;
            //newEnemy.GetComponent<Health>().Tracker = overseer;
            //overseer.SendMessage("Spawned");
        }
        else
        {
            TimeTillSpawn++;
        } 
	
	}
}
