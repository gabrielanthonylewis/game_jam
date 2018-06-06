using UnityEngine;
using System.Collections;

public class tapeController : MonoBehaviour {


    [SerializeField]
    private AudioClip[] clips;
    public GameObject image;
    private int tapeToPlay;
    public bool playinTape = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(!this.GetComponent<AudioSource>().isPlaying && playinTape)
        {
            image.SetActive(false);
            playinTape = false;
        }
	
	}

    void playTape(int i)
    {
        this.GetComponent<AudioSource>().clip = clips[i];
        this.GetComponent<AudioSource>().Play();
    }

    void playNext()
    {
        playTape(tapeToPlay);
        tapeToPlay++;
        playinTape = true;
        image.SetActive(true);
    }
}
