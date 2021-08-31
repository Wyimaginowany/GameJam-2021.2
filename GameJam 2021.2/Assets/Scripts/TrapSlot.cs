using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrapSlot : MonoBehaviour
{
    [SerializeField] TMP_Text turretNameText;
    [SerializeField] TMP_Text turretPriceText;
    [SerializeField] Transform trapIconSpawn;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject[] objectsToHide;

    GameObject trapIconPrefab;

    public void CreateShopSlot(string trapName, int trapPrice, GameObject trapIcon)
    {
        if (trapIconPrefab != null)
        {
            Destroy(trapIconPrefab);
        }

        turretNameText.text = trapName;
        trapIconPrefab = Instantiate(trapIcon, trapIconSpawn.position, Quaternion.identity, canvas.transform);
        trapIconPrefab.transform.position = trapIconSpawn.position;
        turretPriceText.text = trapPrice.ToString() + "$";
        ShowTrap();
    }

    public void HideTrap()
    {
        foreach (GameObject element in objectsToHide)
        {
            element.SetActive(false);
        }
        trapIconPrefab.SetActive(false);
    }

    public void ShowTrap()
    {
        foreach (GameObject element in objectsToHide)
        {
            element.SetActive(true);
        }
    }
}
