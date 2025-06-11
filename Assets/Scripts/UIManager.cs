using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private PlayerAttack _playerAttack;
    [SerializeField]
    private HealthComponent _playerHealth;

    [SerializeField]
    private Slider _healthBar;

    [SerializeField]
    private Slider _specialBar;

    [SerializeField]
    private TextMeshProUGUI _timerText;

    [SerializeField]
    private TextMeshProUGUI _killCountText;

    [SerializeField]
    private Slider _spellDecayBar;

    [SerializeField]
    private GameObject _pauseUI;

    public void TogglePauseUI(bool toggle)
    {
        _pauseUI.gameObject.SetActive(toggle);
    }


    public static UIManager instance { get; private set; }
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void SetKillCountText()
    {
        _killCountText.text = _gameManager.TotalKillCount.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        if(!_playerHealth)
        {
            Debug.LogWarning("UIManager: Player health component null!");
        }
        if(!_healthBar)
        {
            Debug.LogWarning("UIManager: Healthbar is null!");
        }
        if(!_specialBar)
        {
            Debug.LogWarning("UIManager: Specialbar is null!");
        }
        _healthBar.maxValue = _playerHealth.MaxHealth;
        _specialBar.maxValue = _gameManager.SpecialKillAmount;

    }

    // Update is called once per frame
    void Update()
    {
        if(_playerAttack.SpecialActive)
        {
            if(!_spellDecayBar.gameObject.activeSelf)
            {
                _spellDecayBar.gameObject.SetActive(true);
                _spellDecayBar.maxValue = _playerAttack.SpecialTimeLeft;
                _spellDecayBar.value = _playerAttack.SpecialTimeLeft;
            }
            else
            {
                _spellDecayBar.value = _playerAttack.SpecialTimeLeft;
            }
        }
        else
        {
            if(_spellDecayBar.gameObject.activeSelf)
            {
                _spellDecayBar.gameObject.SetActive(false);
            }
        }

        int minutes = Mathf.FloorToInt(GameManager.instance.TimeElapsed / 60);
        int seconds = Mathf.FloorToInt(GameManager.instance.TimeElapsed % 60);

        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        _healthBar.value = _playerHealth.Health;
        _specialBar.value = _gameManager.KillCount;
    }
}
