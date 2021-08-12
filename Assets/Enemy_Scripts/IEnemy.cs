public interface IEnemy
{
    int Power { get; }
    float Gun { get; }
    float Knife { get; }
    void Update(int value, DataType type);
    void UpdateGun(float difference, bool won);
    void UpdateKnife(float difference, bool won);
}
