using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopInteraction : MonoBehaviour
{
    public GameObject storeUI;
    public GameObject interactButton;
    public GameObject player;
    public GameObject EInteractIcon;

    public PlayerPhysics PlayerMovement;
    public GunController GunController;

    private bool isPlayerNearStore = false;
    public bool storeIsOpen = false;

    public GameManager GameManager;

    public GameObject GameUI;

    void Start()
    {
        storeUI.SetActive(false);
        EInteractIcon.SetActive(false);
        interactButton.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.store == true)
        {
            EInteractIcon.SetActive(true);
            if (!isPlayerNearStore)
            {
            interactButton.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (isPlayerNearStore && Input.GetKeyDown(KeyCode.E) && !GameManager.waveInProgress)
            {
                ToggleStore();
            }
            else if (storeIsOpen && Input.GetKeyDown(KeyCode.E))
            {
                ToggleStore();
            }
        }
        else
        {
            EInteractIcon.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.store == true)
        {
            isPlayerNearStore = true;
            interactButton.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearStore = false;
            interactButton.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    [SerializeField] public AudioClip openStore;
    
    void ToggleStore()
    {
        if (!storeIsOpen)
        {
            /* player.GetComponent<SpriteRenderer>().enabled = false;
            PlayerMovement.enabled = false;
            GunController.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            GunController.activated = false;
            */
            SFXManager.instance.PlaySoundFXClip(openStore, transform, 1f);
            player.SetActive(false);
            GameUI.SetActive(false);


            storeIsOpen = true;
            storeUI.SetActive(true);
        }
        else if (storeIsOpen)
        {
            /* player.GetComponent<SpriteRenderer>().enabled = true;
            PlayerMovement.enabled = true;
            GunController.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            GunController.activated = true;
            */
            player.SetActive(true);
            GameUI.SetActive(true);
            storeIsOpen = false;
            storeUI.SetActive(false);
        }
    }
}
