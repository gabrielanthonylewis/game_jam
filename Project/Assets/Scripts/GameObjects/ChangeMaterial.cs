using UnityEngine;
using System.Collections;

public class ChangeMaterial : MonoBehaviour {

    public Material a;
    public Material b;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void goTransparent()
    {
        this.gameObject.GetComponent<Renderer>().material = b;
    }

    public void goSolid()
    {
        this.gameObject.GetComponent<Renderer>().material = a;
    }

}
