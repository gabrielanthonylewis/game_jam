using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MessagePopup : MonoBehaviour {

    public static MessagePopup _instance = null;
    public static MessagePopup Instance { get { return _instance; } }

    public GameObject popupUI;
    public Button popupElement;
    public Button popupElement2;
    public Text popupElement3;
    public string message;

    public Queue<string> messageQueue = new Queue<string>();

    private int counter = 0;

    bool isBeingUsed = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.GetComponent<MessagePopup>());
        else
            _instance = this;
    }

    // Use this for initialization
    void Start()
    {
        popupElement.image.color -= new Color(0f, 0f, 0f, 1f);
        popupElement2.image.color -= new Color(0f, 0f, 0f, 1f);
        popupElement3.color -= new Color(0f, 0f, 0f, 1f);
        popupElement3.text = "";

        AddMessage("Complete Challenges to get Car Parts");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (isBeingUsed)
            return;

        if (other.gameObject.tag == "Player")
            StartCoroutine(FadeIn());
            

    }

    void Update()
    {
        if (isBeingUsed)
            return;

        if (messageQueue.Count <= 0)
            return;

        // Something in queue.
        //todo: Print message

        StopAllCoroutines();

        message = messageQueue.Peek();
        popupElement3.text = message;

        messageQueue.Dequeue();

        StartCoroutine(FadeIn());
    
    }

    public void AddMessage(string newMessage)
    {

        messageQueue.Enqueue(newMessage);
       
    
    }

    IEnumerator FadeIn() // fades in the elements from their alpha 0 to 255
    {
        isBeingUsed = true;
        while (popupElement.image.color.a < 1.0f)
        {
            popupElement.image.color += new Color(0f, 0f, 0f, 0.1f);
            popupElement2.image.color += new Color(0f, 0f, 0f, 0.1f);
            popupElement3.color += new Color(0f, 0f, 0f, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(3.0f);

        isBeingUsed = false;
        StartCoroutine(FadeOut());
        
    }

    IEnumerator FadeOut() // fades out the elements from their alpha of 255 to 0
    {
        isBeingUsed = true;
        while (popupElement.image.color.a > 0.0f)
        {
            popupElement.image.color -= new Color(0f, 0f, 0f, 0.1f);
            popupElement2.image.color -= new Color(0f, 0f, 0f, 0.1f);
            popupElement3.color -= new Color(0f, 0f, 0f, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        isBeingUsed = false;
    }

    void popupMessage(int popupBoxWidth, int PopupBoxHeight, string displayText)
    {




    }
  
}


