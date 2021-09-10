using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopLogic : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int refreshPrice;
    [SerializeField] int startingMoneyAmount = 100;
    [SerializeField] int refreshDiscount = 15;

    [Header("To Attach")]
    [SerializeField] PlayerInput playerInput;
    [SerializeField] PlayerShooting playerShooting;
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
    }

    private void Update()
    {
        moneyAmountText.text = curretMoneyAmount.ToString() + "$";
    }

    public void DrawRandomShop()
    {
        for (int i = 0; i < 4; i++)
        {
            DrawRandomTrap(i);
            DrawRandomUpgrade(i);
        }
    }

    private void DrawRandomTrap(int i)
    {
        chosenTraps[i] = trapsTepmplates[Random.Range(0, trapsTepmplates.Length)];
        TrapTemplate selectedTemplate = chosenTraps[i].GetComponent<TrapTemplate>();
        trapSlots[i].GetComponent<TrapSlot>().CreateShopSlot(selectedTemplate.GetTrapName(), selectedTemplate.GetTrapPrice(), selectedTemplate.GetTrapIcon());
        trapRedeemedSlots[i].SetActive(false);
        trapSlots[i].SetActive(true);
    }

    private void DrawRandomUpgrade(int i)
    {
        chosenUpgrades[i] = upgradeTemplates[Random.Range(0, upgradeTemplates.Length)];
        upgradeSlots[i].GetComponent<UpgradeSlot>().CreateWeaponSlot(chosenUpgrades[i].upgradeName, chosenUpgrades[i].price, chosenUpgrades[i].upgradeIcon);
        upgradeRedeemedSlots[i].SetActive(false);
        upgradeSlots[i].SetActive(true);
    }

    public void RefreshShop()
    {
        if (curretMoneyAmount >= currentRefreshPrice)
        {
            DrawRandomShop();
            curretMoneyAmount -= currentRefreshPrice;
            currentRefreshPrice = refreshPrice;
            refreshPriceText.text = currentRefreshPrice.ToString() + "$";
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
        gameManager.SetState(GameState.CombatPhase);
        gameManager.StartNextStage();
    }
}
