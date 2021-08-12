using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game2 : MonoBehaviour, IPlayerView, IEnemyView
{
    [SerializeField]
    private TMP_Text _playerHealth;
    [SerializeField]
    private TMP_Text _playerMoney;
    [SerializeField]
    private TMP_Text _playerPower;
    [SerializeField]
    private TMP_Text _playerViolence;
    [SerializeField]
    private TMP_Text _playerGun;
    [SerializeField]
    private TMP_Text _playerKnife;


    [SerializeField]
    private TMP_Text _enemyPower;
    [SerializeField]
    private TMP_Text _enemyGun;
    [SerializeField]
    private TMP_Text _enemyKnife;

    [SerializeField]
    private Button _addHealth;
    [SerializeField]
    private Button _subtractHealth;
    [SerializeField]
    private Button _addMoney;
    [SerializeField]
    private Button _subtractMoney;
    [SerializeField]
    private Button _addPower;
    [SerializeField]
    private Button _subtractPower;
    [SerializeField]
    private Button _addViolence;
    [SerializeField]
    private Button _subtractViolence;
    [SerializeField]
    private Button _addGun;
    [SerializeField]
    private Button _subtractGun;
    [SerializeField]
    private Button _addKnife;
    [SerializeField]
    private Button _subtractKnife;

    [SerializeField]
    private Button _gunAttack;
    [SerializeField]
    private Button _knifeAttack;
    [SerializeField]
    private Button _passBy;

    private IPlayer _player;
    private IEnemy _enemy;

    public event Action<bool> HealthChanged;
    public event Action<bool> MoneyChanged;
    public event Action<bool> PowerChanged;
    public event Action<bool> ViolenceChanged;
    public event Action<bool> GunChanged;
    public event Action<bool> KnifeChanged;
    public event Action<IEnemy> GunAttack;
    public event Action<IEnemy> KnifeAttack;
    public event Action<IEnemy> PassBy;

    public int PlayerHealth
    {
        set => _playerHealth.text = $"Player Health: {value}";
    }

    public int PlayerMoney
    {
        set => _playerMoney.text = $"Player Money: {value}";
    }

    public int PlayerPower
    {
        set => _playerPower.text = $"Player Power: {value}";
    }

    public int PlayerViolence
    {
        set => _playerViolence.text = $"Player Violence: {value}";
    }

    public int PlayerGun
    {
        set => _playerGun.text = $"Player Gun: {value}";
    }

    public int PlayerKnife
    {
        set => _playerKnife.text = $"PlayerKnife: {value}";
    }

    public int EnemyPower
    {
        set => _enemyPower.text = $"Enemy Power: {value}";
    }

    public float EnemyGun
    {
        set => _enemyGun.text = $"Enemy Gun: {value}";
    }

    public float EnemyKnife
    {
        set => _enemyKnife.text = $"Enemy Knife: {value}";
    }

    public void ShowPassByButton(bool state)
    {
        _passBy.gameObject.SetActive(state);
    }

    private void DataChanged(bool isAdded, DataType type)
    {
        switch(type)
        {
            case DataType.Health:
                HealthChanged?.Invoke(isAdded);
                break;
            case DataType.Money:
                MoneyChanged?.Invoke(isAdded);
                break;
            case DataType.Power:
                PowerChanged?.Invoke(isAdded);
                break;
            case DataType.Violence:
                ViolenceChanged?.Invoke(isAdded);
                break;
            case DataType.Gun:
                GunChanged?.Invoke(isAdded);
                break;
            case DataType.Knife:
                KnifeChanged?.Invoke(isAdded);
                break;
        }
        _player.Update();
    }

    private void Start()
    {
        _addHealth.onClick.AddListener(() => DataChanged(true, DataType.Health));
        _subtractHealth.onClick.AddListener(() => DataChanged(false, DataType.Health));
        _addMoney.onClick.AddListener(() => DataChanged(true, DataType.Money));
        _subtractMoney.onClick.AddListener(() => DataChanged(false, DataType.Money));
        _addPower.onClick.AddListener(() => DataChanged(true, DataType.Power));
        _subtractPower.onClick.AddListener(() => DataChanged(false, DataType.Power));
        _addViolence.onClick.AddListener(() => DataChanged(true, DataType.Violence));
        _subtractViolence.onClick.AddListener(() => DataChanged(false, DataType.Violence));
        _addGun.onClick.AddListener(() => DataChanged(true, DataType.Gun));
        _subtractGun.onClick.AddListener(() => DataChanged(false, DataType.Gun));
        _addKnife.onClick.AddListener(() => DataChanged(true, DataType.Knife));
        _subtractKnife.onClick.AddListener(() => DataChanged(false, DataType.Knife));
        _gunAttack.onClick.AddListener(() => GunAttack?.Invoke(_enemy));
        _knifeAttack.onClick.AddListener(() => KnifeAttack?.Invoke(_enemy));
        _passBy.onClick.AddListener(() => PassBy?.Invoke(_enemy));

        _player = new Player(this);
        _enemy = new Enemy("Enemy", this);
        _player.AddEnemy(_enemy);
        _player.Update();
    }

    private void OnDestroy()
    {
        _addHealth.onClick.RemoveAllListeners();
        _subtractHealth.onClick.RemoveAllListeners();
        _addMoney.onClick.RemoveAllListeners();
        _subtractMoney.onClick.RemoveAllListeners();
        _addPower.onClick.RemoveAllListeners();
        _subtractPower.onClick.RemoveAllListeners();
        _addViolence.onClick.RemoveAllListeners();
        _subtractViolence.onClick.RemoveAllListeners();
        _addGun.onClick.RemoveAllListeners();
        _subtractGun.onClick.RemoveAllListeners();
        _addKnife.onClick.RemoveAllListeners();
        _subtractKnife.onClick.RemoveAllListeners();
        _gunAttack.onClick.RemoveAllListeners();
        _knifeAttack.onClick.RemoveAllListeners();
        _passBy.onClick.RemoveAllListeners();
        _player.Clear();
    }
}
