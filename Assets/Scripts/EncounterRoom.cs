using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterRoom : MonoBehaviour
{
    [SerializeField] DigimonEncounter enemyDigimon;

    [Header("Room Objects")]
    [SerializeField] Image encounterSprite;
    [SerializeField] GameObject healthbar;
    [SerializeField] GameObject timerbar;
    [SerializeField] TMPro.TMP_Text encounterName;

    private float restTime = 0;
    float elapsed = 0f;
    private bool oddTick = false;

    void Update()
    {
        float tickSpeed = 1f;

        timerbar.SetActive(enemyDigimon.IsBoss);
        
        elapsed += Time.deltaTime;
        if (elapsed >= tickSpeed)
        {
            elapsed = elapsed % tickSpeed;
            DoSetTimeDamage();
            if (oddTick)
            {
                DoSetTimeDamage(true);
                oddTick = false;
            }
            else
                oddTick = true;
            if (LocationController.i.CurrentState == LocationState.InBossRoom)
                DecreaseTimer();
        }
    }

    public void NewEnemy()
    {
        enemyDigimon = LocationController.i.CurrentLocation.GetRandomDigimonEncounter();

        encounterSprite.sprite = enemyDigimon.Image;
        encounterName.text = enemyDigimon.Name;

        enemyDigimon.ResetHP();
        healthbar.transform.localScale = new Vector3(((float)enemyDigimon.CurrentHp / (float)enemyDigimon.MaxHp), 1f);

        if (enemyDigimon.IsBoss)
        {
            restTime = (float)enemyDigimon.TimeLimit;
            timerbar.transform.localScale = new Vector3((restTime / (float)enemyDigimon.TimeLimit), 1f);
        }
    }

    public void DoSetTimeDamage(bool useIntInstead = false)
    {
        if(useIntInstead)
            enemyDigimon.DecreaseHp(GameController.i.GetIntDamage());
        else
            enemyDigimon.DecreaseHp(GameController.i.GetStrDamage());
        healthbar.transform.localScale = new Vector3(((float)enemyDigimon.CurrentHp / (float)enemyDigimon.MaxHp), 1f);
        if (enemyDigimon.IsFainted)
        {
            OnWon();
        }
    }

    public void RemoveHealth()
    {
        enemyDigimon.DecreaseHp(GameController.i.GetClickDamage());
        healthbar.transform.localScale = new Vector3(((float)enemyDigimon.CurrentHp / (float)enemyDigimon.MaxHp), 1f);
        if (enemyDigimon.IsFainted)
        {
            OnWon();
        }
    }

    public void DecreaseTimer()
    {
        float difference = GameController.i.Partner.Stats.Stamina / enemyDigimon.Stats.Stamina;

        float decrease;
        if (difference >= 4)
            decrease = .8f;
        else if (difference >= 2)
            decrease = .9f;
        else if (difference <= 0.5)
            decrease = 1.1f;
        else if (difference <= 0.25)
            decrease = 1.2f;
        else
            decrease = 1f;

        restTime -= decrease;
        timerbar.transform.localScale = new Vector3((restTime / (float)enemyDigimon.TimeLimit), 1f);

        if (restTime < 0f)
        {
            Debug.Log("Fight Lost");
            NewEnemy();
        }
    }

    public void OnWon()
    {
        GameController.i.Partner.GainExp(enemyDigimon.Exp_Yield);
        NewEnemy();
        LocationController.i.CurrentLocation.UnlockLocations();
        Debug.Log("Fight Won");
    }
}
