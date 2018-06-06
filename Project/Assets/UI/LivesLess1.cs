using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LivesLess1 : MonoBehaviour {

    public int lives = 1;
    private int counter = 0;
    private float timeSinceStarted;


    [SerializeField]
    private Text timeSinceStard = null;


    // Use this for initialization
    void Start () {
        lives = 1;
        timeSinceStarted = Time.realtimeSinceStartup;
        UpdateTime();

    }
	
	// Update is called once per frame
	void Update () {
        lives = 0;

        if (counter == 0)
        {
            GameObject.FindWithTag("FirstHeart").SetActive(false);
             counter = counter + 1;
        }

        if (Input.GetKey("q"))
        {
            Debug.Log(timeSinceStarted);
        }
    }

    void UpdateTime()
    {
        timeSinceStard.text = " " + timeSinceStarted;
    }

    /*
    void OnGUI()
    {
        GUIStyle Dugz = new GUIStyle();
        Dugz.alignment = TextAnchor.MiddleRight;
        Dugz.normal.textColor = Color.white;
        Dugz.fontSize = 18;

        GUI.Label(new Rect(290, 500, 100, 100), timeSinceStarted.ToString(), Dugz);
    }
    */
}
