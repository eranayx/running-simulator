using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    private const int MAIN_GAME_SCENE = 1;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _instructionsButton;
    [SerializeField] private Button _disclaimerButton;
    [SerializeField] private GameObject _titleScreen;
    [SerializeField] private GameObject _instructionsScreen;
    [SerializeField] private GameObject _disclaimerScreen;

    // Start is called before the first frame update
    void Start()
    {
        _playButton.onClick.AddListener(() => SceneManager.LoadScene(MAIN_GAME_SCENE));
        _backButton.onClick.AddListener(() => DisplayTitleScreen());
        _instructionsButton.onClick.AddListener(() => DisplayInstructionsScreen());
        _disclaimerButton.onClick.AddListener(() => DisplayDisclaimerScreen());
    }
    
    private void DisplayTitleScreen()
    {
        _titleScreen.SetActive(true);
        _instructionsScreen.SetActive(false);
        _disclaimerScreen.SetActive(false);
        _backButton.gameObject.SetActive(false);
    }

    private void DisplayInstructionsScreen()
    {
        _titleScreen.SetActive(false);
        _instructionsScreen.SetActive(true);
        _disclaimerScreen.SetActive(false);
        _backButton.gameObject.SetActive(true);
    }

    private void DisplayDisclaimerScreen()
    {
        _titleScreen.SetActive(false);
        _instructionsScreen.SetActive(false);
        _disclaimerScreen.SetActive(true);
        _backButton.gameObject.SetActive(true);
    }
}
