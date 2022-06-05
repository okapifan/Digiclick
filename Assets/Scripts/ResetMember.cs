using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResetMember : MonoBehaviour
{
    [SerializeField] TMP_Text nameField;
    [SerializeField] Image imageField;
    [SerializeField] DigimonSpecies digimon;

    public void Init(DigimonSpecies newDigimon)
    {
        digimon = newDigimon;
        nameField.text = newDigimon.Name;
        imageField.sprite = newDigimon.Image;
    }

    public void Onclick()
    {
        GameController.i.Partner.Reset(digimon);
        LocationController.i.EnterLocation(null);
    }
}
