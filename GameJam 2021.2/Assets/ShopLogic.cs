using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopLogic : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int refreshPrice;
    [SerializeField] int startingMoneyAmount = 100;

    [Header("To Attach")]
    [SerializeField] PlayerInput playerInput;
    [SerializeField] TMP_Text moneyAmountText;
    [SerializeField] TMP_Text refreshPriceText;
    [SerializeField] GameObject[] trapsTepmplates;
    [Space(10)]
    [SerializeField] GameObject[] redeemedSlots;

    int curretMoneyAmount;
    GameObject[] chosenTraps;
    GameObject[] shopSlots;
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
            shopSlots[i].GetComponent<ShopSlot>().CreateShopSlot(selectedTemplate.GetTrapName(), selectedTemplate.GetTrapPrice());
            redeemedSlots[i].SetActive(false);
            shopSlots[i].SetActive(true);
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
            shopSlots[slot].SetActive(false);
            redeemedSlots[slot].SetActive(true);
        }
    }

    public void StartTheGameAlready()
    {
        gameObject.SetActive(false);
        gameManager.SetState(GameState.CombatPhase);
        gameManager.StartNextStage();
    }
}
