
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject scoresPanel;
    public GameObject creditsPanel;

    [Header("Controles")]
    public TMP_InputField playerNameField;
    public TMP_Dropdown qualityDropdown;
    public Slider brightnessSlider;
    public Image brightnessOverlay;
    public Toggle fullscreenToggle;
    public TMP_Text scorePlayerNameText;
    public AudioSource musicSource;
    public Slider volumeSlider;

    [Header("Escena")]
    public string gameSceneName = "JuegoPrueba";


    void Start()
    {
        if (playerNameField != null)
            playerNameField.text = PlayerPrefs.GetString("PlayerName", "");

        if (qualityDropdown != null)
            qualityDropdown.onValueChanged.AddListener(SetQuality);

        if (brightnessSlider != null)
        {
            brightnessSlider.onValueChanged.AddListener(SetBrightness);
            SetBrightness(brightnessSlider.value);
        }

        if (fullscreenToggle != null)
        {
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
            SetFullscreen(fullscreenToggle.isOn);
        }

        if (playerNameField != null)
        {
            playerNameField.onEndEdit.AddListener(delegate {
                SavePlayerName();
                UpdateScoreName();
            });
        }

        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
            SetVolume(volumeSlider.value);
        }

        UpdateScoreName();
        ShowMain();
    }


    public void ShowMain()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
        scoresPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SavePlayerName()
    {
        if (playerNameField != null)
            PlayerPrefs.SetString("PlayerName", playerNameField.text);
    }

    public void SetQuality(int index)
    {
        int[] qualityLevels = { 1, 2, 3, 5 };

        if (index >= 0 && index < qualityLevels.Length)
        {
            QualitySettings.SetQualityLevel(qualityLevels[index], false);
        }
    }

    public void SetBrightness(float value)
    {
        if (brightnessOverlay == null)
        {
            Debug.LogError("BrightnessOverlay no está asignado");
            return;
        }

        Color color = Color.black;
        color.a = Mathf.Lerp(0.82f, 0f, value);

        brightnessOverlay.color = color;

        Debug.Log("Brillo: " + value + " | Alpha: " + color.a);
    }

    public void SetFullscreen(bool value)
    {
        Screen.fullScreen = value;
    }
    public void UpdateScoreName()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Jugador");

        if (scorePlayerNameText != null)
        {
            scorePlayerNameText.text = playerName + " : 9500 PTS";
        }
    }
    public void SetVolume(float value)
    {
        if (musicSource != null)
        {
            musicSource.volume = value;
        }
    }
}
