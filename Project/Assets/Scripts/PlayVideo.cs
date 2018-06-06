using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayVideo : MonoBehaviour {

    public MovieTexture movie;
    public MovieTexture meme2;


    public AudioSource audioSource = null;

	// Use this for initialization
	void Start () {

        GetComponent<RawImage>().enabled = false;

      
	}
	
	public void PlayMeme1()
    {
        GetComponent<RawImage>().enabled = true;
        GetComponent<RawImage>().texture = movie as MovieTexture;
        movie.Play();

        if (movie == null)
            Debug.Log("MOVIE NULL");

        if (audioSource == null)
            Debug.Log("AUDIO NULL");

        audioSource.enabled = true;
   
        audioSource.clip = movie.audioClip;
        audioSource.Play();
    }

    public void PlayMeme2()
    {
        GetComponent<RawImage>().enabled = true;
        GetComponent<RawImage>().texture = meme2 as MovieTexture;
        meme2.Play();
        audioSource.clip = meme2.audioClip;
        audioSource.Play();
    }

    public void Stop()
    {
        GetComponent<RawImage>().enabled = false;
        movie.Stop();
        meme2.Stop();
        audioSource.Stop();
    }
}
