using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public int money = 0;
    public GunController GunManager;
    public int ChangedWeaponIndex;

    public TextMeshProUGUI moneyText;

    public TextMeshProUGUI storePromptText;

    bool[] weaponBought  = new bool[10] { true, false, false, false, false, false, false, false, false, false };

    void Start()
    {
        money = 0;
        storePromptText.enabled = false;
        if (moneyText != null)
            moneyText.text = $"${money}";
    }

    // Update is called once per frame
    void Update()
    {
        if (moneyText != null)
            moneyText.text = $"${money}";
    }

    public void PurchaseWeapon(int weaponIndex)
    {
        Debug.Log("Button Pressed");
        if (money > GunManager.weapons[weaponIndex].weaponPrice && weaponBought[weaponIndex] == false)
        {
            StartCoroutine(storePrompt(true));
            ChangedWeaponIndex = weaponIndex;
            GunManager.EquipWeapon(weaponIndex);
            money -= GunManager.weapons[weaponIndex].weaponPrice;
            weaponBought[weaponIndex] = true;
        }
        else if (money < GunManager.weapons[weaponIndex].weaponPrice && weaponBought[weaponIndex] == false)
        {
            StartCoroutine(storePrompt(false));
        }
        else if (weaponBought[weaponIndex] == true)
        {
            StartCoroutine(storePrompt(true));
            ChangedWeaponIndex = weaponIndex;
            GunManager.EquipWeapon(weaponIndex);
            weaponBought[weaponIndex] = true;
        }
        
    }

    IEnumerator storePrompt(bool bought)
    {
        if (bought)
        {
            storePromptText.enabled = true;
            storePromptText.text = "EQUIPED";
            yield return new WaitForSeconds(1);
            storePromptText.enabled = false;
        }
        else if (!bought)
        {
            storePromptText.enabled = true;
            storePromptText.text = "NOT ENOUGH MONEY";
            yield return new WaitForSeconds(1);
            storePromptText.enabled = false;
        }
    }
}
