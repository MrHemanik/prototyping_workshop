using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// The most hardcoded prototype I've made so far, not proud of that one, but time is running out
public class GameManager : MonoBehaviour
{
    private int _currentGameState = 0;
    
    // Zoom out transition
    public RectTransform mainCanvas;
    private Vector3 _targetScale = new Vector3(1, 1, 1);
    private Vector3 _targetPosition = new Vector3(0, 0, 0);
    private bool _isTransitioning = false;

    // Timer
    public GameObject introductionText;
    public GameObject timer;
    
    public TextMeshProUGUI timerInfoText;
    public TextMeshProUGUI timerText;
    public float timerTime = 0f;
    private bool _activeTimer = false;
    
    // MainGame
    public JudgeAnimator[] judges;
    public float[] judgeScores = {0, 0, 0};
    public int[] curSoundStates = {0,0,0};
    
    //End
    public GameObject results;
    public TextMeshProUGUI resultsText;
    public RectTransform[] judgeHands;
    void Start()
    {
        _currentGameState = 0;
        HandleGameState(_currentGameState);
    }
    
    public void HandleGameState(int gameState)
    {
        if (gameState == 0)
        {
            mainCanvas.localScale = new Vector3(3, 3, 3);
            mainCanvas.localPosition = new Vector3(0, -200, 0);
            introductionText.SetActive(true);
            timer.SetActive(false);
            results.SetActive(false);
        }
        else if (gameState == 1)
        {
            //Warmup Timer, Zoomout
            _isTransitioning = true;
            introductionText.SetActive(false);
            timer.SetActive(true);
            StartTimer(20f);
            
        }
        else if(gameState == 2)
        {
            timerInfoText.text = "Impress the judges!";
            StartTimer(30f);
        }
        else if(gameState == 3)
        {
            _targetPosition = new Vector3(0, -200, 0);
            _targetScale = new Vector3(3, 3, 3);
            _isTransitioning = true;
            timer.SetActive(false);
            results.SetActive(true);
            EndGame();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Space) && _currentGameState == 3)
        {
            SceneManager.LoadScene(0);

        }
        if (Input.GetKeyDown(KeyCode.Space)&& _currentGameState == 1)
        {
            timerTime = 0;
        }
        //Start Stuff
        if (Input.GetKeyDown(KeyCode.Space)&& _currentGameState == 0)
        {
            _currentGameState = 1;
            HandleGameState(_currentGameState);
        }
        
        if (_isTransitioning)
        {
            mainCanvas.localScale = Vector3.Lerp(mainCanvas.localScale, _targetScale, Time.deltaTime * 2);
            mainCanvas.localPosition = Vector3.Lerp(mainCanvas.localPosition, _targetPosition, Time.deltaTime * 2);
            if (mainCanvas.localScale.x < 1.001f)
            {
                mainCanvas.localScale = _targetScale;
                mainCanvas.localPosition = _targetPosition;
                _isTransitioning = false;
            }
        }
        //Timer
        if(_activeTimer)
        {
            timerTime -= Time.deltaTime;
            if(timerTime <= 0)
            {
                timerTime = 0;
                _activeTimer = false;
                if(_currentGameState == 1)
                {
                    _currentGameState = 2;
                    HandleGameState(_currentGameState);
                }
                else if(_currentGameState == 2)
                {
                    _currentGameState = 3;
                    HandleGameState(_currentGameState);
                }
            }
            timerText.text = timerTime.ToString("F2");
        }
        //Judge
        if(_currentGameState == 2) ChangeJudgeScore();
    }

    private void StartTimer(float time)
    {
        timerTime = time;
        _activeTimer = true;
    }

    private void ChangeJudgeScore()
    {
        //make prevJudgeScores a copy of judgeScores in short
        var prevJudgeScores = new float[judges.Length];
        for (var i = 0; i < judges.Length; i++)
        {
            prevJudgeScores[i] = judgeScores[i];
        }
        //Each judge loves the respective sound
        if(curSoundStates[0] != 0) judgeScores[0] += 1*Time.deltaTime;
        if(curSoundStates[1] != 0) judgeScores[1] += 1*Time.deltaTime;
        if(curSoundStates[2] != 0) judgeScores[2] += 1*Time.deltaTime;
        if (curSoundStates[0] != 0 && curSoundStates[1] != 0)
        {
            judgeScores[0] -= 0.5f*Time.deltaTime;
            judgeScores[1] -= 0.5f*Time.deltaTime;
            judgeScores[2] += 2*Time.deltaTime;
        }
        if(curSoundStates[2] != 0 && curSoundStates[0] != 0)
        {
            judgeScores[0] += 1*Time.deltaTime;
            judgeScores[2] += 1*Time.deltaTime;
        }
        if(curSoundStates[1] != 0 && curSoundStates[2] != 0)
        {
            judgeScores[1] += 1*Time.deltaTime;
            judgeScores[2] += 1*Time.deltaTime;
            judgeScores[0] -= 1*Time.deltaTime;
        }
        if(curSoundStates[0] == 0 && curSoundStates[1] == 0 && curSoundStates[2] != 0)
        {
            judgeScores[0] -= 0.5f*Time.deltaTime;
            judgeScores[1] -= 0.5f*Time.deltaTime;
            judgeScores[2] -= 1*Time.deltaTime;
        }
        if(curSoundStates[0] == 0 && curSoundStates[1] == 0 && curSoundStates[2] == 0)
        {
            judgeScores[0] -= 0.2f*Time.deltaTime;
            judgeScores[1] -= 0.2f*Time.deltaTime;
            judgeScores[2] -= 0.2f*Time.deltaTime;
        }
        //Cap judge scores at max 1500, min -1000
        for (var i = 0; i < judges.Length; i++)
        {
            if (judgeScores[i] > 10) judgeScores[i] = 10;
            else if (judgeScores[i] < -4) judgeScores[i] = -4;
        }
        for (var i = 0; i < judges.Length; i++)
        {
            if(judgeScores[i] > 2 && prevJudgeScores[i] <= 2) judges[i].SetAnimation(2);
            else if(judgeScores[i] < -2 && prevJudgeScores[i] >= -2) judges[i].SetAnimation(0);
            else if(judgeScores[i] > -2 && judgeScores[i] < 2 && (prevJudgeScores[i] >= 2 || prevJudgeScores[i] <= -2 )) judges[i].SetAnimation(1);
        }
    }

    private void EndGame()
    {
        var judgeApproval = new int[judges.Length];
        for (var i = 0; i < judgeApproval.Length; i++)
        {
            judgeApproval[i] = judgeScores[i] switch
            {
                > 2 => 2,
                < -2 => 0,
                _ => 1
            };
            judgeHands[i].rotation = Quaternion.Euler(0,0, judgeApproval[i] switch
            {
                1 => 90,
                _ => 0
            });
            judgeHands[i].localScale= new Vector3(1,judgeApproval[i] switch
            {
                0 => -1,
                1 => -1,
                _ => 1
            },1);
        }
        if(judgeApproval[0] == 2 && judgeApproval[1] == 2 && judgeApproval[2] == 2)
        {
            resultsText.text = "You got all three votes! Congratulations!";
        }
        else
        {
            resultsText.text = "You didn't get all three votes";
        }
        
        
    }
}
