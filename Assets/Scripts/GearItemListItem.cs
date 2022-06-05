using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearItemListItem : MonoBehaviour
{
    [SerializeField] TrainingItem item;
    [SerializeField] int cost;
    [SerializeField] TMPro.TMP_Text buttonLabel;

    private void Update()
    {
        cost = GlobalSettings.i.DefaultItemCosts;
        if (item.InfinitedUses)
            buttonLabel.text = "Cost: "+ GlobalSettings.i.DefaultItemCosts + " Bits";
        else
            buttonLabel.text = "Amount: " + GameController.i.FindInventoryItem(item).Amount;
    }

    public void OnClick()
    {
        GameController.i.FindInventoryItem(item)?.UseItem();
    }
}
