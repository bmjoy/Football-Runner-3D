using System;

[Serializable]
public class GameData
{
    public StoredValue<int> Level;
    public StoredValue<int> Coins;

    public GameData()
    {
        Level = new StoredValue<int>(1);
        Coins = new StoredValue<int>(0);
    }
}