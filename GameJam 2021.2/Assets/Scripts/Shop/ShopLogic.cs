using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopLogic : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int refreshPrice;
    [SerializeField] int startingMoneyAmount = 100;
    [SerializeField] int refreshDiscount = 15;

    [Header("To Attach")]
    [SerializeField] PlayerInput playerInput;
    [SerializeField] PlayerShooting playerShooting;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] GameObject playerUI;
    [Space(10)]
    [SerializeField] TMP_Text damageText;
    [SerializeField] TMP_Text fireRateText;
    [SerializeField] TMP_Text healthCurrentText;
    [SerializeField] TMP_Text healthMaxText;
    [SerializeField] Toggle trapToggle;
    [SerializeField] Toggle upgradeToggle;
    [SerializeField] TMP_Text moneyAmountText;
    [SerializeField] TMP_Text refreshPriceText;
    [Header("TRAPS")]
    [SerializeField] GameObject[] trapsTepmplates;
    [Space(10)]
    [SerializeField] GameObject[] trapRedeemedSlots;
    [Space(10)]
    [SerializeField] GameObject[] chosenTraps;
    [Space(10)]
    [SerializeField] GameObject[] trapSlots;
    [Space(10)]
    [Header("UPGRADES")]
    [SerializeField] UpgradeTemplate[] upgradeTemplates;
    [Space(10)]
    [SerializeField] GameObject[] upgradeRedeemedSlots;
    [Space(10)]
    [SerializeField] UpgradeTemplate[] chosenUpgrades;
    [Space(10)]
    [SerializeField] GameObject[] upgradeSlots;
    [Space(10)]

    public bool trapLocked = false;
    public bool upgradeLocked = false;

    int currentRefreshPrice;
    int curretMoneyAmount;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        curretMoneyAmount = startingMoneyAmount;
        currentRefreshPrice = refreshPrice;
        refreshPriceText.text = currentRefreshPrice.ToString() + "$";
        DrawRandomShop();
        RefreshStats();
    }

    #region Traps

    private void DrawRandomTrap(int i)
    {
        if (!trapLocked)
        {
            chosenTraps[i] = trapsTepmplates[Random.Range(0, trapsTepmplates.Length)];
            TrapTemplate selectedTemplate = chosenTraps[i].GetComponent<TrapTemplate>();
            trapSlots[i].GetComponent<TrapSlot>().CreateShopSlot(selectedTemplate.GetTrapName(), selectedTemplate.GetTrapPrice(), selectedTemplate.GetTrapIcon());
            trapRedeemedSlots[i].SetActive(false);
            trapSlots[i].SetActive(true);
        }
    }

    public void BuyTurret(int slot)
    {
        TrapTemplate template = chosenTraps[slot].GetComponent<TrapTemplate>();
        int price = template.GetTrapPrice();
        if (curretMoneyAmount >= price)
        {
            curretMoneyAmount -= price;

            if (template.isPlaceable)
            {
                playerInput.SelectTrap(chosenTraps[slot]);
                gameObject.SetActive(false);
            }
            else
            {
                template.SpawnTrap();
            }
            trapSlots[slot].GetComponent<TrapSlot>().HideTrap();
            trapRedeemedSlots[slot].SetActive(true);
            ApplyRefreshDiscount();
        }
        RefreshStats();
    }

    #endregion

    #region Upgrades

    private void DrawRandomUpgrade(int i)
    {
        if (!upgradeLocked)
        {
            chosenUpgrades[i] = upgradeTemplates[Random.Range(0, upgradeTemplates.Length)];
            upgradeSlots[i].GetComponent<UpgradeSlot>().CreateWeaponSlot(chosenUpgrades[i].upgradeName, chosenUpgrades[i].price, chosenUpgrades[i].upgradeIcon);
            upgradeRedeemedSlots[i].SetActive(false);
            upgradeSlots[i].SetActive(true);
        }
    }

    public void BuyWeaponUpgrade(int slot)
    {
        int price = chosenUpgrades[slot].price;
        if (curretMoneyAmount >= price)
        {
            curretMoneyAmount -= price;
            playerShooting.Upgrade(chosenUpgrades[slot].upgradeType, chosenUpgrades[slot].amount);
            upgradeSlots[slot].GetComponent<UpgradeSlot>().HideWeapon();
            upgradeRedeemedSlots[slot].SetActive(true);
            ApplyRefreshDiscount();
        }
        RefreshStats();
    }

    #endregion

    #region Shop

    public void DrawRandomShop()
    {
        for (int i = 0; i < 4; i++)
        {
            DrawRandomTrap(i);
            DrawRandomUpgrade(i);
        }
    }

    public void RefreshShop()
    {
        if (curretMoneyAmount >= currentRefreshPrice && !(trapLocked && upgradeLocked))
        {
            DrawRandomShop();
            curretMoneyAmount -= currentRefreshPrice;
            currentRefreshPrice = refreshPrice;
            refreshPriceText.text = currentRefreshPrice.ToString() + "$";
            RefreshStats();
        }
        
    }

    private void ApplyRefreshDiscount()
    {
        if (currentRefreshPrice - refreshDiscount >= 0)
        {
            currentRefreshPrice -= refreshDiscount;
            refreshPriceText.text = currentRefreshPrice.ToString() + "$";
        }
        else
        {
            currentRefreshPrice = 0;
            refreshPriceText.text = currentRefreshPrice.ToString() + "$";
        }
    }

    public void StartTheGameAlready()
    {
        gameObject.SetActive(false);
        playerUI.SetActive(true);
        gameManager.SetState(GameState.CombatPhase);
        gameManager.StartNextStage();
    }

    public void SetToggle()
    {
        if (trapToggle.isOn)
        {
            trapLocked = true;
        }
        else
        {
            trapLocked = false;
        }

        if (upgradeToggle.isOn)
        {
            upgradeLocked = true;
        }
        else
        {
            upgradeLocked = false;
        }
    }

    public void RefreshStats()
    {
        damageText.text = playerShooting.GetCombinedDamage().ToString();
        fireRateText.text = playerShooting.GetCombinedFireRate().ToString();
        healthCurrentText.text = playerHealth.GetCurrentHealth().ToString();
        healthMaxText.text = playerHealth.GetMaxHealth().ToString();
        moneyAmountText.text = curretMoneyAmount.ToString();
    }

    #endregion

}
