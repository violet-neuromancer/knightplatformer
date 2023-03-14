using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private static readonly int TriggerOpen = Animator.StringToHash("Open");

    [SerializeField] private Text scoreLabel;
    [SerializeField] private Slider healthBar;
    [SerializeField] private GameObject inventoryWindow;
    [SerializeField] private GameObject levelWonWindow;
    [SerializeField] private GameObject levelLoseWindow;
    [SerializeField] private GameObject inGameMenuWindow;
    [SerializeField] private GameObject optionsWindow;
    [SerializeField] private Transform inventoryContainer;
    [SerializeField] private InventoryUIButton inventoryUIButtonPrefab;

    [SerializeField] public Text damageValue;
    [SerializeField] public Text speedValue;
    [SerializeField] public Text healthValue;

    public static HUD Instance { get; private set; }

    public Slider HealthBar
    {
        get => healthBar;
        set => healthBar = value;
    }

    public Text DamageValue
    {
        get => damageValue;
        set => damageValue = value;
    }

    public Text SpeedValue
    {
        get => speedValue;
        set => speedValue = value;
    }

    public Text HealthValue
    {
        get => healthValue;
        set => healthValue = value;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void SetScore(string scoreValue)
    {
        scoreLabel.text = scoreValue;
    }

    public void ShowWindow(GameObject window)
    {
        window.GetComponent<Animator>().SetBool(TriggerOpen, true);
        GameController.Instance.State = GameState.Pause;
    }

    public void HideWindow(GameObject window)
    {
        window.GetComponent<Animator>().SetBool(TriggerOpen, false);
        GameController.Instance.State = GameState.Play;
    }

    public InventoryUIButton AddNewInventoryItem(InventoryItem itemData)
    {
        var newItem = Instantiate(inventoryUIButtonPrefab);

        newItem.transform.SetParent(inventoryContainer);
        newItem.ItemData = itemData;
        return newItem;
    }

    public void UpdateCharacterValues(float newHealth, float newSpeed, float newDamage)
    {
        healthValue.text = newHealth.ToString();
        speedValue.text = newSpeed.ToString();
        damageValue.text = newDamage.ToString();
    }

    public void ButtonNext()
    {
        GameController.Instance.AudioManager.PlaySound("DM-CGS-01");
        GameController.Instance.LoadNextLevel();
    }

    public void ButtonRestart()
    {
        GameController.Instance.AudioManager.PlaySound("DM-CGS-01");
        GameController.Instance.RestartLevel();
    }

    public void ButtonMainMenu()
    {
        GameController.Instance.AudioManager.PlaySound("DM-CGS-01");
        GameController.Instance.LoadMainMenu();
    }
    
    public void ShowLevelWonWindow()
    {
        ShowWindow(levelWonWindow);
    }
    
    public void ShowLevelLoseWindow() 
    {
        ShowWindow(levelLoseWindow);
    }
    
    public void LoadInventory()
    {
        InventoryUsedCallback callback = new InventoryUsedCallback(GameController.Instance.InventoryItemUsed);

        for (int i = 0; i < GameController.Instance.Inventory.Count; i++)
        {
            InventoryUIButton newItem =
                AddNewInventoryItem(GameController.Instance.Inventory[i]);
             	
            newItem.Callback = callback;
        }
	
    }

    public void Start()
    {
        LoadInventory();
        GameController.Instance.OnUpdateHeroParameters += HandleOnUpdateHeroParameters;
        GameController.Instance.StartNewLevel();
    }
    
    private void HandleOnUpdateHeroParameters(HeroParameters parameters)
    {
        HealthBar.maxValue = parameters.MaxHealth;
        HealthBar.value = parameters.MaxHealth;
        UpdateCharacterValues(parameters.MaxHealth, parameters.Speed, parameters.Damage);
    }
    
    private void OnDestroy()
    {
        GameController.Instance.OnUpdateHeroParameters -= HandleOnUpdateHeroParameters;
    }
    
    public void SetSoundVolume(Slider slider)
    {
        GameController.Instance.AudioManager.SfxVolume = slider.value;
    }
    public void SetMusicVolume(Slider slider)
    {
        GameController.Instance.AudioManager.MusicVolume = slider.value;
    }
}