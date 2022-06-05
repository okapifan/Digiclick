using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartnerMember : MonoBehaviour
{
    [SerializeField] TMP_Text nameField;
    [SerializeField] Image imageField;
    [SerializeField] TMP_Text statsPart1;
    [SerializeField] TMP_Text statsPart2;

    void Update()
    {
        nameField.text = GameController.i.Partner.species.Name;
        imageField.sprite = GameController.i.Partner.species.Image;
        statsPart1.text = $"Level: {GameController.i.Partner.level}\nStrength: {GameController.i.Partner.Stats.Strength}\nIntelligence: {GameController.i.Partner.Stats.Intelligence}";
        statsPart2.text = $"Speed: {GameController.i.Partner.Stats.Speed}\nStamina: {GameController.i.Partner.Stats.Stamina}\nSkill: {GameController.i.Partner.Stats.Skill}";
    }
}
