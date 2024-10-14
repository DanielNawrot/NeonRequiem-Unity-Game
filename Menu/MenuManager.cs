using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public LevelLoader levelLoader;
    public AudioClip select;
    public void PlayGame()
    {
        SFXManager.instance.PlaySoundFXClip(select, transform, 1f);
        levelLoader.LoadNextLevel();
    }
    public void QuitGame()
    {
        SFXManager.instance.PlaySoundFXClip(select, transform, 1f);
        Application.Quit();
    }

    public void Options()
    {
        SFXManager.instance.PlaySoundFXClip(select, transform, 1f);
    }

    public void clickSound()
    {
        SFXManager.instance.PlaySoundFXClip(select, transform, 1f);
    }
}
