using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {

    public bool rotateX;
    public bool rotateY;
    public bool rotateZ;

    public float xRotation;
    public float yRotation;
    public float zRotation;

	
	void Update () {

        if (!rotateX)
        {
            xRotation = 0;
        }
        if (!rotateY)
        {
            yRotation = 0;
        }
        if (!rotateZ)
        {
            zRotation = 0;
        }

        gameObject.transform.Rotate(xRotation * Time.deltaTime, yRotation * Time.deltaTime, zRotation * Time.deltaTime);
	
	}
}
