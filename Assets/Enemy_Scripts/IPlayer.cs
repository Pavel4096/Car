public interface IPlayer
{
    void Update();
    void AddEnemy(IEnemy enemy);
    void RemoveEnemy(IEnemy enemy);
    void Clear();
}
