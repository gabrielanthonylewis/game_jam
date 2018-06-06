using UnityEngine;
using System.Collections;

public class FollowBus : MonoBehaviour {

    [SerializeField]
    private Transform _bus = null;

    [SerializeField]
    private Transform[] bus;

    [SerializeField]
    private Vector3 _initialPos;

    void Start()
    {
        this.transform.position = _initialPos;

        this.transform.rotation = Quaternion.Euler(new Vector3(35, 45, 0));

        this.GetComponent<Camera>().orthographic = true;
        this.GetComponent<Camera>().orthographicSize = 11;

        if (_bus == null)
        {

            GameObject temp = GameObject.FindGameObjectWithTag("Bus");

            if (temp != null)
                _bus = temp.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
		Vector3 newPos = this.transform.position;
		newPos.x = _player.position.x;
		newPos.z = _initialPos.z + _player.position.z;

		this.transform.position = newPos;
		*/

        Vector3 newPos = this.transform.position;

        float xAverage = 0.0F;
        float zAverage = 0.0F;


        newPos.x = xAverage - 31.42F;
        newPos.y = _initialPos.y;
        newPos.z = _initialPos.z + zAverage;


        // if someone is magically off screen so be it (e.g. physics mess up that shouldnt happen)
        /*for (int i = 0; i < alivePlayers.Length; i++) 
		{
			Vector3 viewPos = Camera.main.WorldToViewportPoint (alivePlayers [i].position);

			Debug.Log (alivePlayers[i].name + ": " + viewPos.x);
		//	if (viewPos.x > 1 || 
		//		viewPos.x < 0 || 
		//		viewPos.y > 1 ||
		//		viewPos.y < 0)
		//		return;
		}*/

        this.transform.position = newPos;
    }
}
