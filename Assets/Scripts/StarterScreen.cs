using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterScreen : MonoBehaviour
{
    [SerializeField] GameObject memberHolder;
    [SerializeField] GameObject memberPrefab;

    public void OnOpen()
    {
        foreach (Transform child in memberHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        var digimonArray = Resources.LoadAll<DigimonSpecies>("");
        foreach (DigimonSpecies species in digimonArray)
        {
            if(species.Stage == DigimonStage.InTraining && species.IsUnlocked)
            {
                GameObject newMember = GameObject.Instantiate(memberPrefab, memberHolder.transform);
                newMember.GetComponent<ResetMember>().Init(species);
            }
        }
    }
}
