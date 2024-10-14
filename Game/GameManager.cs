using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentWave = 0;
    public int baseWaveAmount = 5;
    public bool waveInProgress = false;
    public bool store = false;

    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;

    public GameObject TextObject;
    public TextMeshProUGUI RoundText;
    public GameObject TextObject2;
    public TextMeshProUGUI StartText;

    GameObject[] enemiesSpawned;

    public ShopInteraction shopScript;

    [SerializeField] public AudioClip nextRoundSFX;

    
    int[] waveArr = new int[] { 0, 0, 0, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };


    private void Start()
    {
        TextObject.SetActive(false);
        TextObject2.SetActive(false);
        waveInProgress = true;
        StartCoroutine(SpawnWave());
    }

    private void Update()
    {
        if (!waveInProgress && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            nextRoundToggle(true);
            store = true;
            if (Input.GetKeyDown(KeyCode.Q) && shopScript.storeIsOpen == false)
            {
                nextRoundToggle(false);
                store = false;
                SFXManager.instance.PlaySoundFXClip(nextRoundSFX, transform, 1f);
                StartCoroutine(SpawnWave());
            }
        }
    }

    

    IEnumerator SpawnWave()
    {
        waveInProgress = true;
        nextRoundToggle(false);
        StartCoroutine(displayRound());
        yield return new WaitForSeconds(3);
        int enemiesToSpawn = baseWaveAmount + (currentWave * 2);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy(currentWave);
            yield return new WaitForSeconds(Random.Range(0.5f, 2));
        }
        currentWave++;
        waveInProgress = false;
    }
    void SpawnEnemy(int waveIndex)
    {
        int minIndex = 0;
        int maxIndex = waveIndex + 1;
        int enemy = Random.Range(minIndex, maxIndex);
        int spawn = Random.Range(0, 2);

        Instantiate(enemyPrefabs[waveArr[enemy]], spawnPoints[spawn].position, Quaternion.identity);
    }

    int[] shuffle(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            int rand = Random.Range(0, arr.Length - 1);
            int x = arr[i];
            arr[i] = arr[rand];
            arr[rand] = x;
        }
        return arr;
    }

    IEnumerator displayRound()
    {
        TextObject.SetActive(true);
        RoundText.text = $"Round {currentWave + 1}";
        yield return new WaitForSeconds(2);
        TextObject.SetActive(false);
    }

    public void nextRoundToggle(bool on)
    {
        if (on)
        {
            TextObject2.SetActive(true);
        }
        if (!on)
        {
            TextObject2.SetActive(false);
        }
    }
}
