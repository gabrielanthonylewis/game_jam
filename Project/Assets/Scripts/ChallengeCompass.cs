using UnityEngine;
using System.Collections;

public class ChallengeCompass : MonoBehaviour {


    public static ChallengeCompass _instance = null;
    public static ChallengeCompass Instance { get { return _instance; } }

    [SerializeField]
    private RectTransform _challengeCompassPointers;

    [SerializeField]
    private GameObject _compassContainer = null;

    Quaternion _challengeDirection;

 

    Vector3 _target = new Vector3(-15.01689f, 0, -67.85826f);



    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.GetComponent<ChallengeCompass>());
        else
            _instance = this;
    }

    // Update is called once per frame
    void Update ()
    {
        UpdatePointer();


        if(Input.GetKeyDown(KeyCode.U))
        {
            foreach(Transform player in SpawnPlayers.Instance.GetActivePlayers())
                player.transform.position = _target;
        }


    }

    public void SetTarget(Vector3 newTarget)
    {
        _target = newTarget;
    }


    public void TurnOn(bool state)
    {
        _compassContainer.SetActive(state);
    }

    void UpdatePointer()
    {
        if (Camera.main == null)
            return;

        Transform[] players = SpawnPlayers.Instance.GetActivePlayers();

        Vector3 middle = Vector3.zero;
        foreach (Transform player in players)
            middle += player.transform.position;
        middle /= players.Length;

        Vector3 direction = _target - middle;// should be closest challenge position

        _challengeDirection = Quaternion.LookRotation(direction);// * Quaternion.Euler(new Vector3(0.0f, 45.0f, 0.0f));

        _challengeDirection.z = -_challengeDirection.y;
        _challengeDirection.x = 0;
        _challengeDirection.y = 0;



   

        Vector3 NorthDirection = Vector3.zero;
        NorthDirection.z = Camera.main.transform.eulerAngles.y;
        _challengeCompassPointers.localRotation = _challengeDirection * Quaternion.Euler(NorthDirection);
    }
}
