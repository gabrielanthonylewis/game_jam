using UnityEngine;
using System.Collections;

public class there : MonoBehaviour {

    public GameObject be = null;

    private bool intriguing = false;

    private void OnTriggerEnter(Collider other)
    {
        intriguing = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;
        
        if (intriguing == true && InputManager.Instance.inputType == InputManager.InputType.Controller
                  && Input.GetAxis("Triggers" + other.GetComponent<MoveController>().currPlayer.ToString()) == -1.0F)
        {
            intriguing = false;
            be.SetActive(true);
            Time.timeScale = 0;
        }

    }

    public void whales()
    {
        be.SetActive(false);
        Time.timeScale = 1;
    }
}
