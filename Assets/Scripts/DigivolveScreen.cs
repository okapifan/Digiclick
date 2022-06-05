using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigivolveScreen : MonoBehaviour
{
    [SerializeField] GameObject memberHolder;
    [SerializeField] GameObject memberPrefab;

    public void OnOpen()
    {
        foreach (Transform child in memberHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach(Digivolution digivolveOption in GameController.i.Partner.species.Digivolutions)
        {
            GameObject newMember = GameObject.Instantiate(memberPrefab, memberHolder.transform);
            newMember.GetComponent<DigivolutionMember>().Init(digivolveOption);
        }
    }
}
