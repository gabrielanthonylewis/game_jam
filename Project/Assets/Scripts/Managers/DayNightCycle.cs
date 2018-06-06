using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class DayNightCycle : MonoBehaviour
{

    enum TimeState
    {
        TransitionToDawn,
        Dawn,
        TransitionToDay,
        Day,
        TransitionToDusk,
        Dusk,
        TransitionToNight,
        Night       
    }

    [SerializeField]
    TimeState _currentState = TimeState.Day;

    [SerializeField]
    float _MAX_INTENSITY = 1.2f;
    [SerializeField]
    float _MIN_INTENSITY = 0.0f;

    private float _dawnIntensity = 0.0f;
    private float _dayIntensity = 0.0f;
    private float _duskIntensity = 0.0f;
    private float _nightIntensity = 0.0f;

    private Light _Light = null;


    private NightLight[] _nightLights;

    private float _generalReflectionIntensity = 1.0f;

    [SerializeField]
    private Color _dawnSkyColour;
    [SerializeField]
    private Color _daySkyColour;
    [SerializeField]
    private Color _duskSkyColour;
    [SerializeField]
    private Color _nightSkyColour;

    [SerializeField]
    private float _dawnLength = 3.0f;
    [SerializeField]
    private float _dayLength = 3.0f;
    [SerializeField]
    private float _duskLength = 3.0f;
    [SerializeField]
    private float _nightLength = 3.0f;

    void Start()
    {
        _nightLights = GameObject.FindObjectsOfType<NightLight>();
        _Light = this.GetComponent<Light>();

        _generalReflectionIntensity = RenderSettings.reflectionIntensity;
        //_generalAmbientSkyColour = RenderSettings.ambientSkyColor;
        
        _dawnIntensity = _MAX_INTENSITY * 0.5f;
        _dayIntensity = _MAX_INTENSITY;
        _duskIntensity = _MAX_INTENSITY * 0.25f;
        _nightIntensity = _MIN_INTENSITY;

        switch (_currentState)
        {
            case TimeState.Dawn:
                TransitionToDawn();
                break;

            case TimeState.Day:
                TransitionToDay();
                break;

            case TimeState.Dusk:
                TransitionToDusk();
                break;

            case TimeState.Night:
                TransitionToNight();
                break;
        }
    }

    void Update()
    {
       

        switch (_currentState)
        {
            case TimeState.Dawn:


                if (_Light.intensity >= _dawnIntensity)
                {
                    TransitionToDay();
                    return;
                }

                float offset = _dawnIntensity / _dawnLength;
                _Light.intensity += offset * Time.deltaTime;


                float num0 = (_dawnIntensity / 2.0f);
                if (_Light.intensity >= num0)
                {
                    RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, _daySkyColour, Time.deltaTime / _dawnLength * 4.0f);
                    RenderSettings.reflectionIntensity = Mathf.Lerp(RenderSettings.reflectionIntensity, 1.0f, Time.deltaTime / _dawnLength * 4.0f);
                }
                break;

            case TimeState.Day:

                //if (_Light.intensity < _dawnIntensity)
                //    TransitionToDay();


                if (_Light.intensity >= _dayIntensity)
                {
                    TransitionToDusk();
                    return;
                }

                float offset2 = (_dayIntensity - _dawnIntensity) / _dayLength;
                _Light.intensity += offset2 * Time.deltaTime;

                float num = _dawnIntensity + ((_dayIntensity - _dawnIntensity) / 2.0f);
                if (_Light.intensity >= num)
                    RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, _duskSkyColour, Time.deltaTime / _dayLength * 4.0f);
        
                break;

            case TimeState.Dusk:

                //if (_Light.intensity > _dayIntensity)
                //   TransitionToDusk();


                if (_Light.intensity <= _duskIntensity)
                {
                    TransitionToNight();
                    return;
                }

                float offset3 = (_duskIntensity - _dayIntensity) / _duskLength;
                _Light.intensity += offset3 * Time.deltaTime;


                float num2 = _dayIntensity + ((_duskIntensity - _dayIntensity) / 2.0f);
                if (_Light.intensity <= num2)
                    RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, _nightSkyColour, Time.deltaTime / _duskLength * 4.0f);
                break;

            case TimeState.Night:



                if (_Light.intensity <= _nightIntensity)
                {
                    TransitionToDawn();
                    return;
                }
                float offset4 = (_nightIntensity - _dawnIntensity) / _nightLength;
                _Light.intensity += Time.deltaTime * offset4;

             
                float num3 = _nightIntensity + ((_nightIntensity - _dawnIntensity) / 2.0f);
                if (_Light.intensity >= num3)
                {
                    RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, _dawnSkyColour, Time.deltaTime / _nightLength * 4.0f);
                    RenderSettings.reflectionIntensity = Mathf.Lerp(RenderSettings.reflectionIntensity, 0.75f, Time.deltaTime / _nightLength * 4.0f);
                }
                break;


        }

    }


    void TransitionToDawn()
    {
       // Debug.Log("Transition to Dawn");


        _Light.intensity = _nightIntensity;

        RenderSettings.reflectionIntensity = 0.75f;
        RenderSettings.ambientSkyColor = _dawnSkyColour; //0.6102941, 0.29153, 0.1480861; 9B4A25FF

        GunShoot[] guns = GameObject.FindObjectsOfType<GunShoot>();
        foreach (GunShoot gun in guns)
            gun.SwitchTorchLight(false);


        foreach (NightLight light in _nightLights)
            light.SwitchLight(false);

        _currentState = TimeState.Dawn;
    }

    void TransitionToDay()
    {
       // Debug.Log("Transition to Day");

        _Light.intensity = _dawnIntensity;

        RenderSettings.reflectionIntensity = _generalReflectionIntensity;
        RenderSettings.ambientSkyColor = _daySkyColour; // E2F5FFFF

        GunShoot[] guns = GameObject.FindObjectsOfType<GunShoot>();
        foreach (GunShoot gun in guns)
            gun.SwitchTorchLight(false);


        foreach (NightLight light in _nightLights)
            light.SwitchLight(false);

        _currentState = TimeState.Day;
    }

    void TransitionToDusk()
    {
       // Debug.Log("Transition to Dusk");

        _Light.intensity = _dayIntensity;

        RenderSettings.reflectionIntensity = 0.5f;
        RenderSettings.ambientSkyColor = _duskSkyColour; //0.1697769, 0.0569853, 0.4558824      2B1074FF

        GunShoot[] guns = GameObject.FindObjectsOfType<GunShoot>();
        foreach (GunShoot gun in guns)
            gun.SwitchTorchLight(false);


        foreach (NightLight light in _nightLights)
            light.SwitchLight(false);

        _currentState = TimeState.Dusk;
    }

    void TransitionToNight()
    {
      //  Debug.Log("Transition to Night");

        _Light.intensity = _duskIntensity;

        RenderSettings.reflectionIntensity = 0.5f;
        RenderSettings.ambientSkyColor = _nightSkyColour; //0.0627451, 0.04705882, 0.04705882  100B0BFF

        foreach (NightLight light in _nightLights)
            light.SwitchLight(true);

        GunShoot[] guns = GameObject.FindObjectsOfType<GunShoot>();
        foreach (GunShoot gun in guns)
            gun.SwitchTorchLight(true);

        _currentState = TimeState.Night;
    }




}


/*[SerializeField]
	private Transform _sun;

	//[SerializeField]
	//private float _speedMulti = 1.0f;

	enum State
	{
		Day,
		TransitionToNight,
		Night,
		TransitionToDawn,
		TransitionToDay
	};

	[SerializeField]
	State _currState = State.Day;


	private NightLight[] _nightLights;

	//private Color _initialColour = Color.white;

	void Start()
	{
		//_initialColour = this.GetComponent<Light> ().color;
		_nightLights = GameObject.FindObjectsOfType<NightLight> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (_currState) {

		case State.Day:

			_sun.GetComponent<Light> ().intensity -= Time.deltaTime / 250.0f;

			RenderSettings.reflectionIntensity = _sun.GetComponent<Light> ().intensity;

			if (_sun.GetComponent<Light> ().intensity <= 0.3F) 
			{
				//todo, when night hits sendmessage that can be captured by guns which turns on light automatically
				GunShoot[] guns = GameObject.FindObjectsOfType<GunShoot> ();
				foreach (GunShoot gun in guns) {
					gun.SwitchTorchLight (true);
				}


				//RenderSettings.reflectionIntensity = 0.5f;
				RenderSettings.ambientSkyColor = new Color(0.1F, 0.1F, 0.1F);
				_currState = State.TransitionToNight;
			}
			break;

		case State.TransitionToNight:

			_sun.GetComponent<Light> ().intensity -= Time.deltaTime / 50.0f;
			RenderSettings.reflectionIntensity = _sun.GetComponent<Light> ().intensity;

			if (_sun.GetComponent<Light> ().intensity <= 0.1F) {
				this.GetComponent<Light> ().color =  new Color(0.05F, 0.05F, 0.05F);
			}

			if (_sun.GetComponent<Light> ().intensity <= 0.02F) 
			{
				foreach (NightLight light in _nightLights)
					light.SwitchLight (true);


				this.GetComponent<Light> ().color =  new Color(0.02F, 0.02F, 0.02F);
			
				RenderSettings.ambientSkyColor = Color.black;
				//RenderSettings.reflectionIntensity = 0.01f;

				_currState = State.Night;
			}
			break;

		case State.Night:

			_sun.GetComponent<Light> ().intensity += Time.deltaTime / 1000.0f;
			//RenderSettings.reflectionIntensity = _sun.GetComponent<Light> ().intensity;

			if (_sun.GetComponent<Light> ().intensity >= 0.2F) 
			{

				
				_currState = State.TransitionToDawn;
			}

			break;

		case State.TransitionToDawn:
			_sun.GetComponent<Light> ().intensity += Time.deltaTime / 10.0f;
			RenderSettings.reflectionIntensity = _sun.GetComponent<Light> ().intensity;

			if (_sun.GetComponent<Light> ().intensity >= 0.4F) {
				//todo sendmessage for lights out
				GunShoot[] guns = GameObject.FindObjectsOfType<GunShoot>();
				foreach (GunShoot gun in guns) {
					gun.SwitchTorchLight (false);
				}

				foreach (NightLight light in _nightLights)
					light.SwitchLight (false);

		

				_currState = State.TransitionToDay;
			}

			break;
		
		case State.TransitionToDay:

			_sun.GetComponent<Light> ().intensity += Time.deltaTime / 50.0f;
			RenderSettings.reflectionIntensity = _sun.GetComponent<Light> ().intensity;

			if (_sun.GetComponent<Light> ().intensity >= 1.0F) 
			{
				//this.GetComponent<Light> ().color = _initialColour;


				_currState = State.Day;
			}
			break;
		}
	}*/
