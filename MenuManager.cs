using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private MenuObject[] menus;
    [SerializeField] private Button quitButton, muteButton, startButton;
    [SerializeField] private TMP_Text scoreText;

    private bool isPaused = true, forcedPause = false;
    private MenuObject currentSelectedMenu;
    private static MenuManager instance;
    public static MenuManager Instance
    {
        get
        {
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += DisableForcedPause;
            //quitButton.onClick.AddListener(() => { Quit(); });
            //muteButton.onClick.AddListener(() => { AudioManager.Instance.SetMuted(AudioManager.Instance.GetIsMuted()); });
            //startButton.onClick.AddListener(() => { StartLevel(); });
            currentSelectedMenu = menus.Where(menu => menu.menu.activeSelf == true).FirstOrDefault();
            //SetMenuPausedState(true);
            DontDestroyOnLoad(this);
            //SetScoreText();
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = isPaused ? 0 : 1;
    }

    #region ExtremelyGeneralMenuMethods

    public MenuType GetCurrentMenu()
    {
        return currentSelectedMenu.type;
    }

    public bool GetCurrentMenuActiveState()
    {
        return currentSelectedMenu.menu.activeSelf;
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }

    public void SwitchMenu(MenuType desiredMenu)
    {
        if (currentSelectedMenu.type == desiredMenu) return;
        currentSelectedMenu.menu.SetActive(false);
        currentSelectedMenu = menus.Where(menu => menu.type == desiredMenu).FirstOrDefault();
        currentSelectedMenu.menu.SetActive(true);
    }

    public void SetMenuPausedState(bool state)
    {
        if (forcedPause) return;
        isPaused = state;
        currentSelectedMenu.menu.SetActive(state);
    }

    public void ForcePausedState()
    {
        SetMenuPausedState(true);
        forcedPause = true;
    }

    void DisableForcedPause(Scene scene, LoadSceneMode mode)
    {
        forcedPause = false;
    }

    #endregion

    #region FairlyGeneralMenuMethods

    public void StartLevel()
    {
        if (forcedPause)
        {
            forcedPause = false;
            if (PinSpawner.Instance.GetScore().points > DataSerializer.GetScore().points)
            {
                DataSerializer.SaveScore(PinSpawner.Instance.GetScore());
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SetMenuPausedState(false);
        }
        else
        {
            SetMenuPausedState(false);
        }
    }

    public void Restart()
    {
        SetMenuPausedState(true);
        if (PinSpawner.Instance.GetScore().points > DataSerializer.GetScore().points)
        {
            DataSerializer.SaveScore(PinSpawner.Instance.GetScore());
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Resume()
    {
        SetMenuPausedState(false);
    }

    public void Quit()
    {
        if (SceneManager.GetActiveScene().name.Equals("Main"))
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void SetScoreText()
    {
        // if (PinSpawner.Instance.GetScore().points <= 0)
        // {
        //     scoreText.text = "";
        // }
        if (PinSpawner.Instance.GetScore().points > DataSerializer.GetScore().points)
        {
            DataSerializer.SaveScore(PinSpawner.Instance.GetScore());
        }
        scoreText.text = string.Format("Highscore: {0} Points", DataSerializer.GetScore().points);
    }

    public void EndGame()
    {
        SetScoreText();
        ForcePausedState();
    }

    //For saving & loading settings -> setup with the GameSettingsManager in the editor

    #endregion
}

[System.Serializable]
public struct MenuObject
{
    public GameObject menu;
    public MenuType type;
}

public enum MenuType
{
    settings,
    main,
    levels,
    inGameScore,
    inGameSettings
}