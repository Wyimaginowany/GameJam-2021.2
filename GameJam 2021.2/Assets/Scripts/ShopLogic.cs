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
    [SerializeField] TMP_Text moneyAmountText;
    [SerializeField] TMP_Text refreshPriceText;
    [SerializeField] GameObject[] trapsTepmplates;
    [Space(10)]
    [SerializeField] GameObject[] redeemedSlots;
    [Space(10)]
    [SerializeField] GameObject[] chosenTraps;
    [Space(10)]
    [SerializeField] GameObject[] shopSlots;
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
        DrawRandomTraps();
    }

    private void Update()
    {
        moneyAmountText.text = curretMoneyAmount.ToString() + "$";
    }

    public void DrawRandomTraps()
    {
        for (int i = 0; i < 4; i++)
        {
            chosenTraps[i] = trapsTepmplates[Random.Range(0, trapsTepmplates.Length)];
            TrapTemplate selectedTemplate = chosenTraps[i].GetComponent<TrapTemplate>();
            shopSlots[i].GetComponent<ShopSlot>().CreateShopSlot(selectedTemplate.GetTrapName(), selectedTemplate.GetTrapPrice(), selectedTemplate.GetTrapIcon());
            redeemedSlots[i].SetActive(false);
            shopSlots[i].SetActive(true);
        }
    }

    public void RefreshShop()
    {
        if (curretMoneyAmount >= currentRefreshPrice)
        {
            DrawRandomTraps();
            curretMoneyAmount -= currentRefreshPrice;
            currentRefreshPrice = refreshPrice;
            refreshPriceText.text = currentRefreshPrice.ToString() + "$";
        }
    }

    public void BuyTurret(int slot)
    {
        int price = chosenTraps[slot].GetComponent<TrapTemplate>().GetTrapPrice();
        if (curretMoneyAmount >= price)
        {
            curretMoneyAmount -= price;
            playerInput.SelectTrap(chosenTraps[slot]);
            gameObject.SetActive(false);
            shopSlots[slot].GetComponent<ShopSlot>().HideTrap();
            redeemedSlots[slot].SetActive(true);
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
