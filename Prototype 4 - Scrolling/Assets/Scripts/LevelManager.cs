using Classes;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private LevelGenerator lg;
    private float _highscore;
    private float _score;
    private int _currentLevel = 0;
    private float _levelSpeed = 1f;
    private float _noteDensity = 1f;
    private bool _isInMainMenu = true;
    private Coroutine _levelGenerationCoroutine;
    
    public TextMeshProUGUI scoreText;
    public GameObject levelEndCanvas;
    public GameObject nextLevelCanvasObjects;
    public TextMeshProUGUI highscoreText;
    public GameObject newHighscoreText;
    public GameObject mainMenuCanvas;
    public Oscillator osci;
    
    private void Start()
    {
        mainMenuCanvas.SetActive(true);
    }

    public void LoadLevel(int curLev, float levSpeed, float noteDense)
    {
        _currentLevel = curLev;
        _levelSpeed = levSpeed;
        _noteDensity = noteDense;
        levelEndCanvas.SetActive(false);
        newHighscoreText.SetActive(false);
        _score = 0;
        scoreText.text = _score.ToString();
        mainMenuCanvas.SetActive(false);
        _isInMainMenu = false;

        LoadHighscore();
        lg = gameObject.GetComponent<LevelGenerator>();
        _levelGenerationCoroutine= StartCoroutine(lg.GenerateLevel(LevelContent.Levels[_currentLevel-1],_levelSpeed,_noteDensity, () =>
        {
            Debug.Log("level finished");
            if (_score > _highscore)
            {
                _highscore = _score;
                SaveHighscore();
                newHighscoreText.SetActive(true);
            }
            highscoreText.text = _highscore.ToString("0");
            nextLevelCanvasObjects.SetActive(curLev < LevelContent.Levels.Length);
            levelEndCanvas.SetActive(true);
            
        }));
    }
    public void AddScore()
    {
        _score+=_levelSpeed*_levelSpeed;
        //Update UI
        scoreText.text = _score.ToString("0");
    }

    private void LoadHighscore()
    {
        _highscore = PlayerPrefs.GetFloat("Level"+_currentLevel+"Highscore",_score);
    }

    private void SaveHighscore()
    {
        PlayerPrefs.SetFloat("Level"+_currentLevel+"Highscore",_score);
    }

    public int GetCurrentLevel()
    {
        return _currentLevel;
    }

    public void OpenMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        _isInMainMenu = true;
    }

    public void ResetLevel()
    {
        if (_levelGenerationCoroutine == null) return;
        StopCoroutine(_levelGenerationCoroutine);
        while(lg.transform.childCount>0)
        {
            DestroyImmediate(lg.transform.GetChild(0).gameObject);
        }
        osci.StopSound();
        if (!_isInMainMenu) LoadLevel(_currentLevel, _levelSpeed, _noteDensity);
    }
}
