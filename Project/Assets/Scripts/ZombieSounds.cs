using UnityEngine;
using System.Collections;

public class ZombieSounds : MonoBehaviour {

    public AudioClip ZombieSound1;
    public AudioClip ZombieSound2;
    public AudioClip ZombieSound3;
    private int ChooseAudioClip;
    private int ChooseTimeTillNextSound = 0;
    private AudioSource audioSource_ = null;
    private bool isBeingUsed = false;


    // Use this for initialization
    void Start ()
    {
        audioSource_ = this.GetComponent<AudioSource>();

	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isBeingUsed)
            return;

        StartCoroutine(PlaySound());
    }




    IEnumerator PlaySound() // playes a zombie sound everyfew seconds 
    {
        isBeingUsed = true;

        ChooseAudioClip = Random.Range(1,1000);
        ChooseTimeTillNextSound = Random.Range(10, 50);


        if (ChooseAudioClip <= 10 && ChooseAudioClip >= 1)
        {
            audioSource_.clip = ZombieSound1;
      
        }
        else
        if (ChooseAudioClip <= 20 && ChooseAudioClip >= 11)
        {
            audioSource_.clip = ZombieSound2;
        }
        else
        if (ChooseAudioClip <= 30 && ChooseAudioClip >= 21)
        {
            audioSource_.clip = ZombieSound3;
        }
        audioSource_.time = Random.Range(0.0f, 0.5f);
        audioSource_.PlayOneShot(audioSource_.clip);

        yield return new WaitForSeconds(ChooseTimeTillNextSound);

        isBeingUsed = false;


    }

}
