using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeSlot : MonoBehaviour
{
    [SerializeField] TMP_Text weaponNameText;
    [SerializeField] TMP_Text weaponPriceText;
    [SerializeField] Transform weaponIconSpawn;
    [SerializeField] GameObject[] objectsToHide;

    GameObject weaponIconPrefab;

    public void CreateWeaponSlot(string weaponName, float weaponPrice, GameObject weaponIcon)
    {
        if (weaponIconPrefab != null)
        {
            Destroy(weaponIconPrefab);
        }

        weaponNameText.text = weaponName;
        weaponIconPrefab = Instantiate(weaponIcon, weaponIconSpawn.position, Quaternion.identity, weaponIconSpawn);
        weaponIconPrefab.transform.position = weaponIconSpawn.position;
        weaponPriceText.text = weaponPrice.ToString() + "$";
        ShowWeapon();
    }

    public void HideWeapon()
    {
        foreach (GameObject element in objectsToHide)
        {
            element.SetActive(false);
        }
    }

    public void ShowWeapon()
    {
        foreach (GameObject element in objectsToHide)
        {
            element.SetActive(true);
        }
    }
}