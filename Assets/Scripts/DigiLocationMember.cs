using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DigiLocationMember : MonoBehaviour
{
    [SerializeField] TMP_Text nameLabel;
    [SerializeField] Location location;

    public Location ConnectedLocation => location;

    public void Awake()
    {
        nameLabel.text = location.Name;
        this.gameObject.GetComponent<Button>().interactable = location.IsUnlocked;
    }

    private void Update()
    {
        this.gameObject.GetComponent<Button>().interactable = location.IsUnlocked;
    }

    public void SetLocation()
    {
        LocationController.i.EnterLocation(location);
        //SetActiveMember();
    }

    public void SetActiveMember()
    {
        DigiLocationMember[] members = GameObject.FindObjectsOfType<DigiLocationMember>();
        foreach(DigiLocationMember member in members)
        {
            member.GetComponentInChildren<TMP_Text>().color = GlobalSettings.i.DefaultColor;
        }
        this.GetComponentInChildren<TMP_Text>().color = GlobalSettings.i.ActiveColor;
    }
}
