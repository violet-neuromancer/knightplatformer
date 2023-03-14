using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Play,
    Pause
}

public delegate void InventoryUsedCallback(InventoryUIButton uiButton);
public delegate void UpdateHeroParametersHandler(HeroParameters parameters);

public class GameController : MonoBehaviour
{
    public event UpdateHeroParametersHandler OnUpdateHeroParameters;
    private static GameController _instance;
    [SerializeField] private int dragonHitScore = 10;
    [SerializeField] private int dragonKillScore = 50;
    [SerializeField] private Audio audioManager;
    [SerializeField]private List<InventoryItem> _inventory;
    [SerializeField] private HeroParameters hero;
    [SerializeField] private int dragonKillExperience;

    private int _score;
    private GameState _state;

    public List<InventoryItem> Inventory
    {
        get => _inventory;
        set => _inventory = value;
    }
    
    public HeroParameters Hero
    {
        get => hero;
        set => hero = value;
    }

    public GameState State
    {
        get => _state;
        set
        {
            if (value == GameState.Play)
                Time.timeScale = 1.0f;
            else
                Time.timeScale = 0.0f;

            _state = value;
        }
    }

    private int Score
    {
        get => _score;
        set
        {
            if (value != _score)
            {
                _score = value;
                HUD.Instance.SetScore(_score.ToString());
            }
        }
    }

    public Audio AudioManager
    {
        get => audioManager;
        set => audioManager = value;
    }

    public Knight Knight { get; set; }

    public static GameController Instance
    {
        get
        {
            if (_instance == null)
            {
                var gameController = Instantiate(Resources.Load("Prefabs/GameController")) as GameObject;
                _instance = gameController.GetComponent<GameController>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
        State = GameState.Play;
        _inventory = new List<InventoryItem>();
        InitializeAudioManager();
    }

    public void StartNewLevel()
    {
        HUD.Instance.SetScore(Score.ToString());
     	
        if (OnUpdateHeroParameters != null)
        {
            OnUpdateHeroParameters(hero);
        }
 
        State = GameState.Play;
    }

    public void Hit(IDestructable victim)
    {
        AudioManager.PlaySound("DM-CGS-03");
        
        if (victim.GetType() == typeof(Dragon))
        {
            //дракон получил урон
            Score += dragonHitScore;
        }

        if (victim.GetType() == typeof(Knight))
        {
            HUD.Instance.HealthBar.value = victim.Health;
        }
    }
    
    public void Killed(IDestructable victim)
    {
        if (victim.GetType() == typeof(Dragon))
        {
            Score += dragonKillScore;

            hero.Experience += dragonKillExperience;

            Destroy((victim as MonoBehaviour).gameObject);
        }

        if (victim.GetType() == typeof(Knight))
        {
	
            GameOver();
        }
    }

    public void AddNewInventoryItem(InventoryItem itemData)
    {
        InventoryUIButton newUiButton = HUD.Instance.AddNewInventoryItem(itemData);

        InventoryUsedCallback callback = InventoryItemUsed;
        newUiButton.Callback = callback;
        _inventory.Add(itemData);
    }

    public void InventoryItemUsed(InventoryUIButton item)
    {
        switch (item.ItemData.CrystalType)
        {
            case CrystalType.Blue:
                hero.Speed += item.ItemData.Quantity / 10f;
                break;
            case CrystalType.Red:
                hero.Damage += item.ItemData.Quantity / 10f;
                break;
            case CrystalType.Green:
                hero.MaxHealth += item.ItemData.Quantity / 10f;
                break;
            default:
                Debug.LogError("Wrong crystall type!");
                break;
        }

        Inventory.Remove(item.ItemData);
        Destroy(item.gameObject);
        if (OnUpdateHeroParameters != null)
        {
            OnUpdateHeroParameters(hero);
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void PrincessFound()
    {
        HUD.Instance.ShowLevelWonWindow();
    }

    public void GameOver()
    {
        HUD.Instance.ShowLevelLoseWindow();
    }
    
    private void InitializeAudioManager()
    {
        audioManager.SourceSFX = gameObject.AddComponent<AudioSource>();
        audioManager.SourceMusic = gameObject.AddComponent<AudioSource>();
        audioManager.SourceRandomPitchSFX = gameObject.AddComponent<AudioSource>();
        gameObject.AddComponent<AudioListener>();
    }
    
    public void LevelUp()
    {
        if (OnUpdateHeroParameters != null)
        {
            OnUpdateHeroParameters(hero);
        }
    }
}