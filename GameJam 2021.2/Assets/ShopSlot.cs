using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] TMP_Text turretNameText;
    [SerializeField] TMP_Text turretPriceText;

    public void CreateShopSlot(string trapName, int trapPrice)
    {
        turretNameText.text = trapName;
        turretPriceText.text = trapPrice.ToString();
    }
}
