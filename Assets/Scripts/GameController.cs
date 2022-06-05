using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] int digiCoin = 0;
    [SerializeField] int baseClickDamage = 1;
    [SerializeField] int baseAutoDamage = 0;
    [SerializeField] TMPro.TMP_Text moneyLabel;
    [SerializeField] TMPro.TMP_Text dummyLabel;
    [SerializeField] DigimonPartner partner;
    [SerializeField] int maxUseItems = 25;
    [SerializeField] int currentUsedItems = 0;

    [Header("ResetValues")]
    [SerializeField] int totalRuns = 1;
    [SerializeField] int rookieResets = 0;
    [SerializeField] int championResets = 0;
    [SerializeField] int ultimateResets = 0;
    public int TotalRuns => totalRuns;
    public int RookieResets => rookieResets;
    public int ChampionResets => championResets;
    public int UltimateResets => ultimateResets;

    [Header("Items")]
    [SerializeField] List<InventoryItem> inventory;

    public int Digicoin => digiCoin;
    public DigimonPartner Partner => partner;
    public int MaxUseItems => maxUseItems;
    public int CurrentUsedItems => currentUsedItems;
    public List<InventoryItem> Inventory => inventory;

    public static GameController i;

    private void Awake()
    {
        GameController.i = this;
    }

    private void Update()
    {
        UpdateDummyTotal();
    }

    public int GetClickDamage()
    {
        return Mathf.FloorToInt(baseClickDamage + (partner.Stats.Skill / 10));
    }

    public int GetStrDamage()
    {
        return Mathf.FloorToInt(baseAutoDamage + (partner.Stats.Strength / 5));
    }

    public int GetIntDamage()
    {
        return Mathf.FloorToInt(baseAutoDamage + (partner.Stats.Intelligence / 2f));
    }

    public void GainMoney(int addedMoney)
    {
        if ((digiCoin + addedMoney) < GlobalSettings.i.MaxBits)
            digiCoin += addedMoney;
        else
            digiCoin = GlobalSettings.i.MaxBits;
        moneyLabel.text = digiCoin.ToString();
    }

    public void UpdateDummyTotal()
    {
        dummyLabel.text = currentUsedItems.ToString() + "/" + maxUseItems;
    }

    public void GainTrainingItems(TrainingItem i, int chance)
    {
        if (i == null || chance == 0)
            return;

        if (Random.value < ((float)chance / 100))
            GameController.i.FindInventoryItem(i).GetItem();
    }

    public void AddItemUsed()
    {
        currentUsedItems++;
    }

    public void AddMaxItemUse(int amount)
    {
        maxUseItems = +amount;
    }

    public InventoryItem FindInventoryItem(TrainingItem item)
    {
        foreach(InventoryItem i in inventory)
        {
            if (i.Item.Name == item.Name)
                return i;
        }
        return null;
    }

    public void SoftReset()
    {
        totalRuns++;

        UpdateResetStages();

        currentUsedItems = 0;
        maxUseItems = 25 + (rookieResets * 5) + (championResets * 15) + (ultimateResets * 25);

        Debug.Log("Resetted to run: " + totalRuns);
        OpenStarterSelectionScreen();
    }

    public void OpenStarterSelectionScreen()
    {
        LocationController.i.EnterLocation(null, enterStarterScreen: true);
    }

    public void UpdateResetStages()
    {
        switch (partner.species.Stage)
        {
            case DigimonStage.Rookie:
                rookieResets++;
                break;
            case DigimonStage.Champion:
                championResets++;
                break;
            case DigimonStage.Ultimate:
                ultimateResets++;
                break;
            default:
                break;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

[System.Serializable]
public class DigimonPartner
{
    public DigimonSpecies species;
    public int level = 1;
    public int currentExp = 0;
    public DigiStats stats;
    public DigiStats Stats => stats;

    

    public void Reset(DigimonSpecies newSpecies)
    {
        level = 1;
        currentExp = 0;
        stats = new DigiStats(5 + (GameController.i.RookieResets * 1) + (GameController.i.ChampionResets * 5) + (GameController.i.UltimateResets * 15));
        species = newSpecies;
    }

    public void AttemptDigivolve(Digivolution digivolution)
    {
        if (level >= digivolution.Requirement.RequiredLevel &&
            stats.Strength >= digivolution.Requirement.RequiredStats.Strength &&
            stats.Intelligence >= digivolution.Requirement.RequiredStats.Intelligence &&
            stats.Speed >= digivolution.Requirement.RequiredStats.Speed &&
            stats.Stamina >= digivolution.Requirement.RequiredStats.Stamina &&
            stats.Skill >= digivolution.Requirement.RequiredStats.Skill)
        {
            species = digivolution.Specie;
            GameController.i.AddMaxItemUse(digivolution.Specie.GetDigivolutionBoost());
            stats.GainStats(new DigiStats(digivolution.Specie.GetDigivolutionBoost()));
            Debug.Log("Digivolution succesfull");
        }
        else
            Debug.Log("Digivolution failed");
    }

    public void GainExp(int exp)
    {
        if (level < GlobalSettings.i.MaxLevel)
        {
            currentExp += exp;
            while (currentExp >= GetExpForLevel(level + 1))
            {
                //LevelUp
                level++;
                Debug.Log("Leveled up");
                stats.GainStats(GlobalSettings.i.StatsOnLevelUp);
            }
        }
    }

    public int GetExpForLevel(int level)
    {
        //Formula from Pokémon fast level monsters.
        return Mathf.FloorToInt(4 * (Mathf.Pow(level, 3)) / 5);
    }
    
}

[System.Serializable]
public class InventoryItem
{
    [SerializeField] TrainingItem item;
    [SerializeField] int amount;

    public TrainingItem Item => item;
    public int Amount => amount;

    public void GetItem()
    {
        amount++;
    }

    public void UseItem()
    {
        if (item.InfinitedUses || amount >= 1)
        {
            if (GameController.i.CurrentUsedItems < GameController.i.MaxUseItems)
            {
                if (!item.InfinitedUses)
                    amount--;
                if (GameController.i.Digicoin >= GlobalSettings.i.DefaultItemCosts)
                {
                    GameController.i.GainMoney(-GlobalSettings.i.DefaultItemCosts);
                    GameController.i.AddItemUsed();
                    GameController.i.Partner.Stats.GainStats(item.IncreasedStats);
                }
            }
        }
    }
}