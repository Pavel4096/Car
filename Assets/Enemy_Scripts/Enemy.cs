public class Enemy : IEnemy
{
    private string _enemyName;
    private int _playerHealth;
    private int _playerMoney;
    private int _playerPower;
    private int _playerViolence;
    private float _enemyGun;
    private float _enemyKnife;
    private IEnemyView _enemyView;

    private const float maxHealth = 10;
    private const float minPlayerHealthCoefficient = 0.4f;
    private const float basePower = 5.0f;
    private const float winPercent = 0.2f;
    private const float losePercent = 0.1f;

    public Enemy(string enemyName, IEnemyView enemyView)
    {
        _enemyName = enemyName;
        _enemyView = enemyView;
        _enemyGun = 0;
        _enemyKnife = 0;
    }

    public int Power
    {
        get
        {
            float moneyPowerCoefficient = 1;
            float playerHealthCoefficient = _playerHealth/maxHealth;

            if(playerHealthCoefficient < minPlayerHealthCoefficient)
                playerHealthCoefficient = minPlayerHealthCoefficient;

            if(_playerMoney > _playerPower && _playerPower != 0 && _playerMoney != 0)
                moneyPowerCoefficient = 1.0f*_playerMoney / _playerPower;
            else if(_playerMoney != 0 && _playerPower != 0)
                moneyPowerCoefficient = 1.0f*_playerPower / _playerMoney;

            return (int)(playerHealthCoefficient*moneyPowerCoefficient*basePower);
        }
    }

    public float Gun => _enemyGun;
    public float Knife => _enemyKnife;

    public void Update(int value, DataType type)
    {
        switch(type)
        {
            case DataType.Health:
                _playerHealth = value;
                break;
            case DataType.Money:
                _playerMoney = value;
                break;
            case DataType.Power:
                _playerPower = value;
                break;
            case DataType.Violence:
                _playerViolence = value;
                break;
            default:
                UnityEngine.Debug.Log($"Unknown type: {type}");
                break;
        }
        UpdateData();
    }

    private void UpdateData()
    {
        _enemyView.EnemyPower = Power;
        _enemyView.EnemyGun = Gun;
        _enemyView.EnemyKnife = Knife;
    }

    public void UpdateGun(float difference, bool won)
    {
        if(difference < 0 && won)
            return;

        if(won)
            _enemyGun += difference*winPercent;
        else
            _enemyGun += difference*losePercent;
        UpdateData();
    }

    public void UpdateKnife(float difference, bool won)
    {
        if(difference < 0 && won)
            return;
        
        if(won)
            _enemyKnife += difference*winPercent;
        else
            _enemyKnife += difference*losePercent;
        UpdateData();
    }
}
