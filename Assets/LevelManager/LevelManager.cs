using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour
{
    #region Singleton
    private static LevelManager _instance=null;
    public static LevelManager instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }
    #endregion

    [SerializeField]
    private GameObject[] levels;
    [SerializeField]
    private Button replay, nextLevel;
    [SerializeField]
    private Text level;

    InputManager inputManager;
    GameObject currentLevel,finishedLevel;

    private bool _levelFail = false, _levelWin = false;
    #region Get Set

    public bool levelFail
    {
        get
        {
            return _levelFail;
        }
        set
        {
            _levelFail = value;
        }
    }
    public bool levelWin
    {
        get
        {
            return _levelWin;
        }
        set
        {
            _levelWin = value;
        }
    }
    #endregion
    private int currentLevelIndex = 0;

    void Start()
    {
        inputManager = InputManager.Instance;
        currentLevelIndex = PlayerPrefs.GetInt("LastLevel");
        currentLevel= Instantiate(levels[currentLevelIndex], Vector3.zero,Quaternion.identity);
        level.text = "LEVEL " + (currentLevelIndex + 1);

    }
    void Update()
    {
        if (_levelFail)
        {
            PlayAgainButtonVisibility();
        }
        if (_levelWin)
        {
            NextLevelButtonVisibility();
        }
    }
    #region Level Controller

    public void NextLevel() // Called in Editor.
    {
        _levelWin = false;
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
        _levelFail = false;
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
    }
    #endregion

    #region Button Visibility Controls

    void PlayAgainButtonVisibility()
    {
        replay.GetComponent<CanvasGroup>().alpha += Time.deltaTime;

        if (replay.GetComponent<CanvasGroup>().alpha >= 0.5f)
        {
            replay.GetComponent<Button>().interactable = true;
        }
    }
   void NextLevelButtonVisibility()
    {
            nextLevel.GetComponent<CanvasGroup>().alpha += Time.deltaTime;

            if (nextLevel.GetComponent<CanvasGroup>().alpha >= 0.5f)
            {
                nextLevel.GetComponent<Button>().interactable = true;
            }
    }
    #endregion
}
