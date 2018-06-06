using UnityEngine;
using System.Collections;

public class SpawnChallenge : MonoBehaviour {

    //Array of challenge game objects
    public GameObject[] challenges_;

	void Start ()
    {

	}
	
    void Update()
    {
        if (SpawnPlayers.Instance.GetActivePlayers().Length <= 0)
            return;

        // Find closet challenge
        float smallestDistance = 99999.0f;
        int iOffset = -1;
    
        Vector3 myPos = SpawnPlayers.Instance.GetActivePlayers()[0].transform.position;
        for (int i = 0; i < challenges_.Length; i++)
        {
            if (challenges_[i].activeSelf == false)
                continue;

            if (challenges_[i].GetComponentInChildren<challenge>() == null)
                continue;

            float testDistance = Vector3.Distance(myPos, challenges_[i].transform.position);
            if (testDistance <= smallestDistance)
            {
                smallestDistance = testDistance;
                iOffset = i;
            }
        }

        if (iOffset > -1)
        {
            ChallengeCompass.Instance.TurnOn(true);
            ChallengeCompass.Instance.SetTarget(challenges_[iOffset].GetComponentInChildren<challenge>().transform.position);
        }
        else
        {
            ChallengeCompass.Instance.TurnOn(false);
        }
    }
    public void EnableRandomChallenge()
    {
        //Number between 0 and length to get element number
        int random_number_ = Random.Range(0, challenges_.Length);
        //Make it visable and active
        MessagePopup.Instance.AddMessage("Challenge Spawned!");
        challenges_[random_number_].SetActive(true);
    }
}
