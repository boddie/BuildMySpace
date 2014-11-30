using UnityEngine;
using System.Collections;

public class Item
{
    public Texture2D Logo { get; private set; }
    public Texture2D EfficiencyImage { get; private set; }
    public string Prefab { get; private set; }
    public string PurchaseURL { get; private set; }
    public bool Favorite { get; set; }

    public Item(Texture2D logo, Texture2D effImage, string prefab, string purchaseURL)
    {
        this.Logo = logo;
        this.EfficiencyImage = effImage;
        this.Prefab = prefab;
        this.PurchaseURL = purchaseURL;
        Favorite = false;
    }
}
