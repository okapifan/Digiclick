using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    [Header("UI Colors")]
    [SerializeField] Color defaultColor;
    [SerializeField] Color activeColor;

    [Header("Money")]
    [SerializeField] int maxBits = 999999999;
    [SerializeField] int defaultItemCosts = 100;

    [Header("Partner")]
    [SerializeField] int maxLevel = 99;
    [SerializeField] DigiStats statsOnLevelUp = new DigiStats();

    public Color32 DefaultColor => defaultColor;
    public Color32 ActiveColor => activeColor;
    public int MaxBits => maxBits;
    public int DefaultItemCosts => defaultItemCosts;
    public int MaxLevel => maxLevel;
    public DigiStats StatsOnLevelUp => statsOnLevelUp;

    public static GlobalSettings i;

    private void Awake()
    {
        GlobalSettings.i = this;
    }
}
