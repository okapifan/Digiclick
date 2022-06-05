using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gear", menuName = "Digiclick/Create new item")]
public class TrainingItem : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] bool infinitedUses = false;
    [SerializeField] DigiStats increasedStats = new DigiStats();

    public string Name => name;
    public bool InfinitedUses => infinitedUses;
    public DigiStats IncreasedStats => increasedStats;

    
}
