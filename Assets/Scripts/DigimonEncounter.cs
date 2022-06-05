using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DigimonBoss", menuName = "Digiclick/Create new digimon boss")]
public class DigimonEncounter : DigimonSpecies
{
    [SerializeField] int maxHp = 100;
    private int currentHp;
    [SerializeField] int timeLimit = -1;
    [SerializeField] DigiStats stats = new DigiStats();
    [SerializeField] DefeatReward rewards = new DefeatReward();
    [SerializeField] int exp_yield;
    private bool isBoss = false;
    private bool isFainted = false;


    public int MaxHp => maxHp;
    public int CurrentHp => currentHp;
    public int TimeLimit => timeLimit;
    public DigiStats Stats => stats;
    public int Exp_Yield => exp_yield;
    public bool IsBoss => isBoss;
    public bool IsFainted => isFainted;

    public void OnEnable()
    {
        if (timeLimit >= 0)
            isBoss = true;
    }

    public void ResetHP()
    {
        currentHp = MaxHp;
        isFainted = false;
    }

    public void DecreaseHp(int decreasingHP)
    {
        currentHp -= decreasingHP;
        Debug.Log(currentHp);
        if (currentHp <= 0)
        {
            isFainted = true;
            GameController.i.GainMoney(rewards.money);
            GameController.i.GainTrainingItems(rewards.trainingItem, rewards.trainingItemChance);
            if (rewards.unlockableSpecies != null)
                rewards.unlockableSpecies.Unlock();
        }
    }
}

[System.Serializable]
public class DefeatReward
{
    public int money = 0;
    public TrainingItem trainingItem;
    public int trainingItemChance = 0;
    public DigimonSpecies unlockableSpecies;
}
