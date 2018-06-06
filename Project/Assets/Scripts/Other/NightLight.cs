using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Light))]
public class NightLight : MonoBehaviour {


	private Light _light = null;

	// Use this for initialization
	void Start ()
    {
		_light = this.GetComponent<Light> ();

        SwitchLight(false);
	}
	
	public void SwitchLight(bool state)
	{
       if(_light == null)
        {
            _light = this.GetComponent<Light>();
        }
		_light.enabled = state;
	}
}
