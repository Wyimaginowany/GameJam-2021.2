using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopLogic : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] GameObject[] trapsTepmplates;
    [SerializeField] int refreshPrice;
    [SerializeField] TMP_Text moneyAmountText;
    [SerializeField] TMP_Text refreshPriceText;

    //remopve serializefield
    [SerializeField] int startingMoneyAmount = 100;
    [SerializeField] int curretMoneyAmount;
    [SerializeField] GameObject[] chosenTraps;
    [SerializeField] ShopSlot[] shopSlots;


    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        curretMoneyAmount = startingMoneyAmount;
        refreshPriceText.text = refreshPrice.ToString();
    }

    private void Update()
    {
        moneyAmountText.text = curretMoneyAmount.ToString();
    }

    public void DrawRandomTraps()
    {
        for (int i = 0; i < 4; i++)
        {
            chosenTraps[i] = trapsTepmplates[Random.Range(0, trapsTepmplates.Length)];
            TrapTemplate selectedTemplate = chosenTraps[i].GetComponent<TrapTemplate>();
            shopSlots[i].CreateShopSlot(selectedTemplate.GetTrapName(), selectedTemplate.GetTrapPrice());
        }
    }

    public void RefreshShop()
    {
        if (curretMoneyAmount >= refreshPrice)
        {
            DrawRandomTraps();
            curretMoneyAmount -= refreshPrice;
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
        }
    }

    public void StartTheGameAlready()
    {
        gameObject.SetActive(false);
        gameManager.SetState(GameState.CombatPhase);
    }
}
