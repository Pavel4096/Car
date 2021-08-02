public interface IPlayerProperty
{
    void Add(IEnemy enemy);
    void Remove(IEnemy enemy);
    void RemoveAllEnemies();
    void Change(bool isAdded);
}
