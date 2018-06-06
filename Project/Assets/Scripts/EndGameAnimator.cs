using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class EndGameAnimator : MonoBehaviour {

    Animator animator;
    public Camera actionCam;
    public GameObject redPad;
    public GameObject fuel;

    public Spawning spawn;

    private bool playAnim = false;
    void Start ()
    {
        animator = GetComponent<Animator>();
	}
	
	void Update ()
    {
      
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && PlayerStats.Instance.GetCarLevel() == 4)
        {
            other.transform.SetParent(transform);
            other.transform.localPosition = transform.position;

            spawn.endGameSpawn();


            actionCam.enabled = true;

            Camera.main.enabled = false;

            
            fuel.SetActive(false);
            redPad.SetActive(false);
            animator.SetBool("end", true);
            animator.SetBool("done", true);
        }
    }

    void OnColliderEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Health>().LoseLives(10000);
            
        }
    }
    void NextScene()
    {
        SceneManager.LoadScene(2);
    }
}
