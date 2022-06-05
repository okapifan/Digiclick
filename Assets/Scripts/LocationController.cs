using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LocationState { None, InDigivolveScene, InRoute, InBossRoom, InTown, InStarterScreen}

public class LocationController : MonoBehaviour
{
    [SerializeField] GameObject encounterScene;
    [SerializeField] GameObject townScene;
    [SerializeField] GameObject digivolveScene;
    [SerializeField] GameObject starterScreen;
    [SerializeField] GameObject bossTimer;

    private LocationState currentState;
    private Location currentLocation;
    public static LocationController i;

    public LocationState CurrentState => currentState;
    public Location CurrentLocation => currentLocation;

    public void EnterLocation(Location location, bool enterDigivolveScene = false, bool enterStarterScreen = false)
    {
        if (enterStarterScreen)
        {
            EnterStarterScreenScene();
            return;
        }

        if(enterDigivolveScene)
        {
            EnterDigivolveScene();
            return;
        }

        if (location == null)
        {
            EnterNone();
            return;
        }

        currentLocation = location;
        if (location.IsTown)
            EnterTown();
        else if (location.Encounters[0].IsBoss)
            EnterBossRoom();
        else if (location.Encounters.Count > 0)
            EnterRoute();
        else
            EnterNone();
    }

    public void EnterNone()
    {
        encounterScene.SetActive(false);
        townScene.SetActive(false);
        digivolveScene.SetActive(false);
        starterScreen.SetActive(false);
        currentState = LocationState.None;
    }

    public void EnterDigivolveScene()
    {
        encounterScene.SetActive(false);
        townScene.SetActive(false);
        digivolveScene.SetActive(true);
        starterScreen.SetActive(false);
        digivolveScene.GetComponent<DigivolveScreen>().OnOpen();
        currentState = LocationState.InDigivolveScene;
    }

    public void EnterStarterScreenScene()
    {
        encounterScene.SetActive(false);
        townScene.SetActive(false);
        digivolveScene.SetActive(false);
        starterScreen.SetActive(true);
        starterScreen.GetComponent<StarterScreen>().OnOpen();
        currentState = LocationState.InDigivolveScene;
    }

    public void EnterTown()
    {
        encounterScene.SetActive(false);
        townScene.SetActive(true);
        digivolveScene.SetActive(false);
        starterScreen.SetActive(false);
        currentState = LocationState.InTown;
    }

    public void EnterBossRoom()
    {
        encounterScene.SetActive(true);
        townScene.SetActive(false);
        digivolveScene.SetActive(false);
        bossTimer.SetActive(true);
        starterScreen.SetActive(false);
        currentState = LocationState.InBossRoom;
        encounterScene.GetComponent<EncounterRoom>().NewEnemy();
    }

    public void EnterRoute()
    {
        encounterScene.SetActive(true);
        townScene.SetActive(false);
        digivolveScene.SetActive(false);
        bossTimer.SetActive(false);
        starterScreen.SetActive(false);
        currentState = LocationState.InRoute;
        encounterScene.GetComponent<EncounterRoom>().NewEnemy();
    }

    // Start is called before the first frame update
    void Start()
    {
        EnterLocation(null);
    }

    private void Awake()
    {
        LocationController.i = this;
    }
}
