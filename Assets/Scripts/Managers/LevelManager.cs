using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] levels;
    [SerializeField]
    private Button replay, nextLevel;
    [SerializeField]
    private Text level;

    LevelEndControl levelEndControl;
    PlayerCombat playerCombat;

    GameObject currentLevel,finishedLevel;
    int currentLevelIndex = 0;

    void Start()
    {
        currentLevelIndex = PlayerPrefs.GetInt("LastLevel");
        currentLevel= Instantiate(levels[currentLevelIndex], Vector3.zero,Quaternion.identity);
        level.text = "LEVEL " + (currentLevelIndex + 1);
        AccessScripts();

    }

    public void NextLevel() // Called in Editor.
    {
        nextLevel.GetComponent<CanvasGroup>().alpha = 0;
        nextLevel.interactable = false;
        currentLevelIndex++;

        if (currentLevelIndex > levels.Length - 1)
        {
            currentLevelIndex = 0;
        }
        LevelCreate();
       
    }
    public void Replay() // Called in Editor.
    {
        replay.GetComponent<CanvasGroup>().alpha = 0;
        replay.interactable = false;
        LevelCreate();
    }
    void LevelCreate()
    {
        PlayerPrefs.SetInt("LastLevel", currentLevelIndex);
        finishedLevel = currentLevel;
        Destroy(finishedLevel);
        currentLevel = Instantiate(levels[currentLevelIndex], Vector3.zero, Quaternion.identity);
        level.text = "LEVEL " + (currentLevelIndex + 1);
        AccessScripts();
    }

    void AccessScripts() //Access for objects when new level created.
    {
        levelEndControl = GameObject.FindGameObjectWithTag("LevelEnd").GetComponent<LevelEndControl>();
        playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
    }
}
