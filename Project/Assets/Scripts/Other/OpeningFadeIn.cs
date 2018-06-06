using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OpeningFadeIn : MonoBehaviour {


    public Image startingScreen;
    public float timeForFade = 0;



    // Use this for initialization
    void Start () {
        StartCoroutine(FadeOut());
    }
	
	// Update is called once per frame

        




    IEnumerator FadeOut() // fades out the elements from their alpha of 255 to 0
    {

        while (startingScreen.color.a > 0.0f)
        {
            startingScreen.color -= new Color(0f, 0f, 0f, 0.1f);

            yield return new WaitForSeconds(timeForFade);
        }
    }

}
