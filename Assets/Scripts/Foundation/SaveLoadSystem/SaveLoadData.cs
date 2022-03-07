using System;

[Serializable]
public class SaveLoadData
{
    public GameData Game;
    public SettingsData Settings;
    public ShopData Shop;

    public SaveLoadData()
    {
        Game = new GameData();
        Settings = new SettingsData();
        Shop = new ShopData();
    }
}