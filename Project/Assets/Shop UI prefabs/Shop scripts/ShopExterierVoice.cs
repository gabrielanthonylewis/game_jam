using UnityEngine;
using System.Collections;

public class ShopExterierVoice : MonoBehaviour
{

    public AudioClip shopSounds1;
    public AudioClip shopSounds2;
    public AudioClip shopSounds3;
    public AudioClip shopSounds4;
    public AudioClip shopSounds5;
    public AudioClip shopSounds6;
    public AudioClip shopSounds7;
    public AudioClip shopSounds8;
    public GameObject face;
    private int Rand2 = 0;




    private AudioSource _audioSource = null;


    // Use this for initialization
    void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
            face.SetActive(false);
    }

    void Update()
    {
        if (!_audioSource.isPlaying)
        {
            face.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (_audioSource.isPlaying)
        {
            return;
        }

        if (other.gameObject.tag != "Player")
            return;


        Rand2 = Random.Range(1, 8);

        if (Rand2 == 1)
        {
            _audioSource.clip = shopSounds1;
           // AudioSource.PlayClipAtPoint(shopSounds1, transform.position);
        }

        if (Rand2 == 2)
        {
            _audioSource.clip = shopSounds2;
            // AudioSource.PlayClipAtPoint(shopSounds2, transform.position);
        }

        if (Rand2 == 3)
        {
            _audioSource.clip = shopSounds3;
            //   AudioSource.PlayClipAtPoint(shopSounds3, transform.position);
        }

        if (Rand2 == 4)
        {
            _audioSource.clip = shopSounds4;
            //  AudioSource.PlayClipAtPoint(shopSounds4, transform.position);
        }

        if (Rand2 == 5)
        {
            _audioSource.clip = shopSounds5;
            // AudioSource.PlayClipAtPoint(shopSounds5, transform.position);
        }

        if (Rand2 == 6)
        {
            _audioSource.clip = shopSounds6;
            // AudioSource.PlayClipAtPoint(shopSounds6, transform.position);
        }

        if (Rand2 == 7)
        {
            _audioSource.clip = shopSounds7;
            // AudioSource.PlayClipAtPoint(shopSounds7, transform.position);
        }

        if (Rand2 == 8)
        {
            _audioSource.clip = shopSounds8;
            // AudioSource.PlayClipAtPoint(shopSounds8, transform.position);
        }


        _audioSource.Play();
        face.SetActive(true);


    }




}


