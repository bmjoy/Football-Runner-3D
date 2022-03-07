using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ShopData
{
    public StoredValue<Dictionary<string, int>> UpgradeLevel;

    public ShopData()
    {
        UpgradeLevel = new StoredValue<Dictionary<string, int>>();
    }
}