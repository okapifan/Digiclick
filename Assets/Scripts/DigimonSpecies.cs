using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Digimon", menuName = "Digiclick/Create new digimon")]
public class DigimonSpecies : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] Sprite image;
    [SerializeField] DigimonStage stage;
    [SerializeField] List<Digivolution> digivolutions;
    [SerializeField] bool isUnlocked;

    public string Name => name;
    public Sprite Image => image;
    public DigimonStage Stage => stage;
    public List<Digivolution> Digivolutions => digivolutions;
    public bool IsUnlocked => isUnlocked;

    public int GetDigivolutionBoost()
    {
        return stage switch
        {
            DigimonStage.Digitama => 0,
            DigimonStage.InTraining => 0,
            DigimonStage.Rookie => 5,
            DigimonStage.Champion => 20,
            DigimonStage.Ultimate => 50,
            _ => 0,
        };
    }

    public void Unlock()
    {
        isUnlocked = true;
    }
}

public enum DigimonStage { Digitama, InTraining, Rookie, Champion, Ultimate}

[System.Serializable]
public class Digivolution
{
    [SerializeField] DigimonSpecies specie;
    [SerializeField] EvolutionRequirement requirement;

    public DigimonSpecies Specie => specie;
    public EvolutionRequirement Requirement => requirement;
}

[System.Serializable]
public class EvolutionRequirement
{
    [SerializeField] int requiredLevel = 0;
    [SerializeField] DigiStats requiredStats = new DigiStats();
    public int RequiredLevel => requiredLevel;
    public DigiStats RequiredStats => requiredStats;
}

[System.Serializable]
public class DigiStats
{
    [SerializeField] int strength = 0;
    [SerializeField] int intelligence = 0;
    [SerializeField] int speed = 0;
    [SerializeField] int stamina = 0;
    [SerializeField] int skill = 0;

    public DigiStats()
    {
        strength = 0;
        intelligence = 0;
        speed = 0;
        stamina = 0;
        skill = 0;
    }

    public DigiStats(int all)
    {
        strength = all;
        intelligence = all;
        speed = all;
        stamina = all;
        skill = all;
    }

    public int Strength => strength;
    public int Intelligence => intelligence;
    public int Speed => speed;
    public int Stamina => stamina;
    public int Skill => skill;
    public void GainStats(DigiStats stats)
    {
        strength += stats.Strength;
        intelligence += stats.Intelligence;
        speed += stats.Speed;
        stamina += stats.Stamina;
        skill += stats.Skill;
    }
}