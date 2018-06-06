using UnityEngine;
using System.Collections;
public class Build_Bus : MonoBehaviour {

    public GameObject[] carParts;
    public GameObject buildingSymbol;

    private float currentTime;
    private float endTime;

    [SerializeField]
    private float buildDuration = 0.1f;

    void OnTriggerEnter(Collider other)
    {

        if (PlayerStats.Instance.GetCarParts() > PlayerStats.Instance.GetCarLevel())
        {
            if (other.gameObject.CompareTag("Player"))
            {
                buildingSymbol.SetActive(true);

                StartCoroutine(Build(buildDuration));
             //   Debug.Log("player found, starting building of car part");
            }      
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            buildingSymbol.SetActive(false);
           // Debug.Log("Player off pad");
        }
    }

    IEnumerator Build(float duration)
    {
        float t = 0;

        t += Time.deltaTime / duration;
       

        while (t <= 1)
        {
            t += Time.deltaTime / duration;
           // Debug.Log(t);
            yield return null;
        }

        if(t >= 1)
        {
            carParts[PlayerStats.Instance.GetCarLevel()].SetActive(true);
          //  Debug.Log("Build part" + PlayerStats.Instance.GetCarLevel());
            PlayerStats.Instance.SetCarLevel();
            buildingSymbol.SetActive(false);

        }


    }
}
