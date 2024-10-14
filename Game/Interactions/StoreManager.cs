using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public int money = 10000;
    public Button[] StoreButtons;
    public GunController GunManager;
    public int ChangedWeaponIndex;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (StoreButtons[0].onClick != null)
        {

        }
    }

    public void PurchaseWeapon(int weaponIndex)
    {
        Debug.Log("Button Pressed");
        if (money > GunManager.weapons[weaponIndex].weaponPrice)
        {
            ChangedWeaponIndex = weaponIndex;
            GunManager.EquipWeapon(weaponIndex);
        }
    }

}
