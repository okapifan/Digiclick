using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Location", menuName = "Digiclick/Create new location")]
public class Location : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] bool isTown = false;
    [SerializeField] List<DigimonEncounter> encounters;
    [SerializeField] bool isUnlockedByDefault = false;
    [SerializeField] List<Location> unlocks;
    private bool isUnlocked;

    public string Name => name;
    public bool IsTown => isTown;
    public List<DigimonEncounter> Encounters => encounters;
    public bool IsUnlocked => isUnlocked;

    private void OnEnable()
    {
        isUnlocked = false;
        if (isUnlockedByDefault)
            isUnlocked = true;
    }

    public DigimonEncounter GetRandomDigimonEncounter()
    {
        if (encounters.Count <= 0)
            return null;
        return encounters[Random.Range(0, encounters.Count)];
    }

    public void SetUnlocked(bool getsUnlocked)
    {
        isUnlocked = getsUnlocked;
    }

    public void UnlockLocations()
    {
        foreach(Location loc in unlocks)
        {
            loc.SetUnlocked(true);
        }
    }
}
