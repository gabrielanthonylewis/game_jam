using UnityEngine;
using System.Collections;

public class challenge : MonoBehaviour {


    //[SerializeField]
    //private GameObject Object1 = null;
    //[SerializeField]
    //private GameObject Object2 = null;
    //[SerializeField]
    //private GameObject Object3 = null;
    //[SerializeField]
    //private GameObject Object4 = null;

    public GameObject[] walls;
    public GameObject kibble;
    public GameObject activator;
    public GameObject[] mySpawners;
    public GameObject activeZone;
    public int playerLevel = 1;
    public GameObject reward;
    private bool kibbled = false;
    private bool myState = false;
    int pointsTillStop = 0;
    int currentPoints = 0;
    int enemiesLeft = 0;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        
    }

   public void start()
    {
        if (myState != true)
            return;

        if (!kibbled)
        {
            Kibbler(kibble);
            kibbled = true;
        }

        activeZone.SendMessage("setKillAllZombies");

        foreach (Transform child in kibble.transform)
        {
            foreach (Transform childChild in child.transform)
            {
                GameObject clone;
                clone = Instantiate(childChild.gameObject,childChild.transform.position,childChild.transform.rotation) as GameObject;

                Destroy(clone, 4);
                Rigidbody body = clone.AddComponent<Rigidbody>();
                DestroyAfterX desx = clone.AddComponent<DestroyAfterX>();
                
            }
        }

        GameObject gun = Instantiate(reward, transform.position, transform.rotation) as GameObject;


        foreach (GameObject spawner in mySpawners)
        {
            spawner.SetActive(true);
        }


        activator.SetActive(false);


        

        foreach (GameObject wall in walls)
        {
            wall.SetActive(true);
        }
    }

    void ActivateYourself(int activationCode)
    {
        if(activationCode == 1)
        {
            myState = false;
           // Debug.Log("deactivated");
        }
        if(activationCode == 2)
        {
            myState = true;
            
           // Debug.Log("activated");
        }
    }

    void Kibbler(GameObject obj)
    {

        Debug.Log(obj.name);
        foreach(Transform child in obj.transform)
        {
            if (obj.name != kibble.name)
            {
                GameObject clone;
                clone = Instantiate(obj) as GameObject;
                Rigidbody body = clone.AddComponent<Rigidbody>();
            }
            
        }

        
    }

    void calculatePoints()
    {
        if (currentPoints >= pointsTillStop)
        {
            Debug.Log("CHALLENGE DONE");
        }
    }
}
