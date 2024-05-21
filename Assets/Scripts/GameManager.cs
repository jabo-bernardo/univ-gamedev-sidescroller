using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Player logic
    [Header("Player Stats")]
    private float playerHealth = 100f;
    private float playerMaxHealth = 100f;
    private float playerCritChance = 0.0f;
    private float playerBaseDamage = 15f;

    // Player Items
    private int beerCount = 0;

    private bool isUserActionsDisabled;

    // Cave Logic
    private bool hasFirstKey, hasSecondKey, hasThirdKey;
    private bool shouldShowKeys;
    private bool hasCastleKey;
    private int castleKeyLocation;

    // Tavern Logic
    private bool shouldActorsRunaway;
    private bool hasTalkedWithBartender;
    private bool shouldCameraFocusOnActors;
    private bool tavernActorsDefeated;

    [Header("Dictionaries")]

    public Dictionary<string, Vector3> playerSavedLocationsBeforeTeleport = new Dictionary<string, Vector3>();

    public Dictionary<string, int> killTracker = new Dictionary<string, int>();

    public Dictionary<string, bool> yoloTracker = new Dictionary<string, bool>();

    private bool isGamePaused = false;
    private bool isFirstBeer = true;
    private bool isFirstGoblin = true;
    private bool isFirstCavePlatform = true;
    private bool isFirstGoblinKill = true;
    private bool isFirstCaveEntrance = true;
    private bool isFirstCastleEntrance = true;

    void Update() {
        GameObject inGameUi = GameObject.Find("InGameUi");
        GameObject pauseMenu = null;

        if (inGameUi) {
            pauseMenu = inGameUi.transform.GetChild(1).gameObject;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            isGamePaused = !isGamePaused;
        }

        if (isGamePaused) {
            Time.timeScale = 0;
            if (pauseMenu) pauseMenu.SetActive(true);
        } else {
            Time.timeScale = 1;
             if (pauseMenu)  pauseMenu.SetActive(false);
        }
    }

    public void DisableUserActions()
    {
        isUserActionsDisabled = true;
    }

    public void EnableUserActions()
    {
        isUserActionsDisabled = false;
    }

    public bool IsUserActionsDisabled()
    {
        return isUserActionsDisabled;
    }

    public GameManager SetHasFirstKey(bool _key) {
        hasFirstKey = true;
        return GameManager.Instance;
    }

    public GameManager SetHasSecondKey(bool _key) {
        hasSecondKey = true;
        return GameManager.Instance;
    }

    public GameManager SetHasThirdKey(bool _key) {
        hasThirdKey = true;
        return GameManager.Instance;
    }

    public GameManager SetShouldShowKeys(bool _shouldShowKeys) {
        shouldShowKeys = _shouldShowKeys;
        return GameManager.Instance;
    }

    public GameManager SetShouldActorsRunaway(bool _should) {
        shouldActorsRunaway = _should;
        return GameManager.Instance;
    }

    public GameManager SetHasTalkedWithBartender(bool _has) {
        hasTalkedWithBartender = _has;
        return GameManager.Instance;
    }

    public GameManager SetShouldCameraFocusOnActors(bool _should) {
        shouldCameraFocusOnActors = _should;
        return GameManager.Instance;
    }

    public GameManager SetPlayerHealth(float health) {
        playerHealth = Mathf.Min(health, playerMaxHealth);
        return GameManager.Instance;
    }

    public GameManager SetPlayerMaxHealth(float maxHealth) {
        playerMaxHealth = maxHealth;
        return GameManager.Instance;
    }

    public GameManager SetPlayerCritChance(float critChance) {
        playerCritChance = critChance;
        return GameManager.Instance;
    }

    public GameManager SetPlayerBaseDamage(float baseDamage) {
        playerBaseDamage = baseDamage;
        return GameManager.Instance;
    }

    public GameManager SetTavernActorsDefeated() {
        tavernActorsDefeated = true;
        return GameManager.Instance;
    }

    public GameManager SetHasCastleKey(bool _hasCastleKey) {
        hasCastleKey = _hasCastleKey;
        return GameManager.Instance;
    }

    public GameManager SetCastleKeyLocation(int _castleKeyLocation) {
        castleKeyLocation = _castleKeyLocation;
        return GameManager.Instance;
    }

    public GameManager IncreaseBeerCount(int _beerCount = 1) {
        beerCount += _beerCount;
        return GameManager.Instance;
    }

    public GameManager ConsumeBeer(int _beerToConsume = 1) {
        beerCount -= _beerToConsume;
        return GameManager.Instance;
    }

    public GameManager SetIsGamePaused(bool _gamePaused) {
        isGamePaused = _gamePaused;
        return GameManager.Instance;
    }

    public GameManager IHaveSeenBeer() {
        isFirstBeer = false;
        return GameManager.Instance;
    } 

    public GameManager IHaveSeenGoblin() {
        isFirstGoblin = false;
        return GameManager.Instance;
    }

    public GameManager IHaveSeenCavePlatform() {
        isFirstCavePlatform = false;
        return GameManager.Instance;
    }

    public GameManager IHaveKilledAGoblin() {
        isFirstGoblinKill = false;
        return GameManager.Instance;
    }

    public GameManager IHaveSeenCave() {
        isFirstCaveEntrance = false;
        return GameManager.Instance;
    }

    public GameManager IHaveSeenCastle() {
        isFirstCastleEntrance = false;
        return GameManager.Instance;
    }

    public bool GetHasFirstKey() {
        return hasFirstKey;
    }

    public bool GetHasSecondKey() {
        return hasSecondKey;
    }

    public bool GetHasThirdKey() {
        return hasThirdKey;
    }

    public bool GetShouldShowKeys() {
        return shouldShowKeys;
    }

    public bool GetShouldActorsRunaway() {
        return shouldActorsRunaway;
    }

    public bool GetShouldCameraFocusOnActors() {
        return shouldCameraFocusOnActors;
    }

    public bool GetHasTalkedWithBartender() {
        return hasTalkedWithBartender;
    }

    public bool AreTavernsActorsDefeated() {
        return tavernActorsDefeated;
    }

    public float GetPlayerHealth() {
        return playerHealth;
    }

    public float GetPlayerMaxHealth() {
        return playerMaxHealth;
    }

    public float GetPlayerCritChance() {
        return playerCritChance;
    }

    public float GetPlayerBaseDamage() {
        return playerBaseDamage;
    }

    public bool GetHasCastleKey() {
        return hasCastleKey;
    }

    public int GetCastleKeyLocation() {
        return castleKeyLocation;
    }

    public int GetBeerCount() {
        return beerCount;
    }

    public bool GetIsGamePaused() {
        return isGamePaused;
    }

    public bool GetIsFirstBeer() {
        return isFirstBeer;
    }

    public bool GetIsFirstGoblin() {
        return isFirstGoblin;
    }

    public bool GetIsFirstCavePlatform() {
        return isFirstCavePlatform;
    }

    public bool GetIsFirstGoblinKill() {
        return isFirstGoblinKill;
    }

    public bool GetIsFirstCaveEntrance() {
        return isFirstCaveEntrance;
    }

    public bool GetIsFirstCastleEntrance() {
        return isFirstCastleEntrance;
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            int randomNumber = Random.Range(1, 3);
            SetCastleKeyLocation(randomNumber); 
            if (randomNumber == 2) {
                int decider = Random.Range(1, 10);
                if (decider < 2) 
                    randomNumber = Random.Range(1, 3);
            }
            SetCastleKeyLocation(randomNumber); 

            DontDestroyOnLoad(gameObject);
        }
    }

    
}
