using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New upgrade", menuName = "Upgrade")]
public class UpgradeTemplate : ScriptableObject
{
    public UpgradeTypes upgradeType;
    public string upgradeName;
    public int price = 50;
    public float amount = 1f;
    public float secondAmount = 1f;
    public GameObject upgradeIcon;
    public string firstLine;
    public string secondLine;

}
