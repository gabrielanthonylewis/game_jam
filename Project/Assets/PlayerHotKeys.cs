using UnityEngine;
using System.Collections;

public class PlayerHotKeys : MonoBehaviour {

    private Health _Health = null;
   

	// Use this for initialization
	void Start ()
    {
        _Health = this.GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            _Health.Invincible();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            this.transform.position = new Vector3(34.4f, 2.0f, 90.0f);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            this.transform.position = new Vector3(34.4f, 2.0f, 118.36f);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerStats.Instance.AddKill();
        }


        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerStats.Instance.AddMoney(100);
        }

    }
}
