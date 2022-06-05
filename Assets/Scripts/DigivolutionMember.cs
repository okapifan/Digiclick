using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DigivolutionMember : MonoBehaviour
{
    [SerializeField] TMP_Text nameField;
    [SerializeField] Image imageField;
    [SerializeField] TMP_Text requirementsPart1;
    [SerializeField] TMP_Text requirementsPart2;
    [SerializeField] Digivolution digimon;

    // Start is called before the first frame update
    public void Init(Digivolution newDigimon)
    {
        digimon = newDigimon;
        nameField.text = newDigimon.Specie.Name;
        imageField.sprite = newDigimon.Specie.Image;
        requirementsPart1.text = $"Level: {newDigimon.Requirement.RequiredLevel}\nStrength: {newDigimon.Requirement.RequiredStats.Strength}\nIntelligence: {newDigimon.Requirement.RequiredStats.Intelligence}";
        requirementsPart2.text = $"Speed: {newDigimon.Requirement.RequiredStats.Speed}\nStamina: {newDigimon.Requirement.RequiredStats.Stamina}\nSkill: {newDigimon.Requirement.RequiredStats.Skill}";
    }

    public void Onclick()
    {
        GameController.i.Partner.AttemptDigivolve(digimon);
        GameObject.FindObjectOfType<DigivolveScreen>().OnOpen();
    }
}
