using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public int safetyLevel;
    public string trainIdentifier = "AX74B";
    public int energyLevel;
    public int currentWindow = 0;
    private float _timeTilSafetyPercentLost = 2f;
    private float _curTimeForSafetyLoss;
    private float _timeTilEnergyPercentLost = 0.5f;
    private float _curTimeForEnergyLoss;
    public GameObject backgroundExitButton;
    public GameObject[] windows;
    public GameObject[] buttons;
    private bool[] activeTasks = {true,true,true,true};
    public UnityEvent startGame;
    public UIManager uim;
    public bool hasScrewdriver = false;
    
    private AudioSource _as;
    private AudioManager _am;
    private float _audioSpeed = 2f;
    private float _curAudioCooldown = 0f;
    private void Start()
    {
        _as= GetComponent<AudioSource>();
        _am = FindObjectOfType<AudioManager>();
        for (var i = 1; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }
        GenerateTrainIdentifier();
        SetSafetyLevel(100);
        SetEnergyLevel(100);
        backgroundExitButton.SetActive(false);
        startGame.Invoke();
    }
    private void Update()
    {
        _curTimeForSafetyLoss += Time.deltaTime;
        if (_curTimeForSafetyLoss > _timeTilSafetyPercentLost)
        {
            SetSafetyLevel(safetyLevel-1);
            _curTimeForSafetyLoss -= _timeTilSafetyPercentLost;
        }

        _curAudioCooldown += Time.deltaTime;
        if (_curAudioCooldown > 0.1f+_audioSpeed*safetyLevel/100)
        {
            _as.pitch = (100f - safetyLevel) / 200 + 0.75f;
            _as.Play();
            
            _curAudioCooldown -= 0.1f+_audioSpeed*safetyLevel/100;
        }
        _curTimeForEnergyLoss += Time.deltaTime;
        if (_curTimeForEnergyLoss > _timeTilEnergyPercentLost)
        {
            SetEnergyLevel(energyLevel-1);
            _curTimeForEnergyLoss -= _timeTilEnergyPercentLost;
        }
        
    }

    private void SetActiveTasks(int slot, bool state)
    {
        activeTasks[slot] = state;
        int activeIssues= GetActiveTasksNumber();
        if (activeIssues.Equals(0))
        {
            WinGame();
        }
        uim.UpdateIssues(activeIssues);
    }

    public int GetActiveTasksNumber()
    {
        int a = 0;
        foreach (var activeTask in activeTasks)
        {
            a += activeTask ? 1 : 0;
        }
        return a;
    }
    private void SetSafetyLevel(int lvl)
    {
        if (lvl <= 0) EndGame();
        safetyLevel = lvl;
        uim.UpdateSafetyLevel(safetyLevel);
    }
    private void SetEnergyLevel(int lvl)
    {
        energyLevel = (lvl <= 0) ? 0 : (lvl >= 100) ? 100 : lvl;
        uim.UpdateEnergyLevel(energyLevel);
    }

    public void AddOneToEnergyLevel()
    {
        SetEnergyLevel(energyLevel+2);
    }

    private void GenerateTrainIdentifier()
    {
        string letter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string number = "0123456789";
        
        char RandomLetter()
        {
            return letter[Random.Range(0, letter.Length)];
        }
        char RandomNumber()
        {
            return number[Random.Range(0, number.Length)];
        }
        string generatedTID = ""+RandomLetter()+RandomLetter()+RandomLetter()+RandomNumber()+RandomNumber();
        trainIdentifier = generatedTID;
        uim.SetTrainIdentifier(trainIdentifier);
    }

    public void ChangeWindow(int num)
    {
        //if its to the same window or from not default to another, do nothing
        if (currentWindow.Equals(num) || (!currentWindow.Equals(0) && !num.Equals(0))) return;
        if (currentWindow != 0)
        {
            windows[currentWindow].SetActive(false);
            backgroundExitButton.SetActive(false);
        }
        else
        {
            windows[num].SetActive(true);
            backgroundExitButton.SetActive(true);
        }
        currentWindow = num;
    }

    public void SubmitTask(int taskNum, bool success)
    {
        
        ChangeWindow(0);
        ChangeTaskActivity(taskNum, success);
        if (!success)
        {
            _am.Play("failSound");
            SetSafetyLevel(safetyLevel-5);
        }
        else
        {
            _am.Play("success");
        }
        
    }

    private void ChangeTaskActivity(int taskNum, bool success)
    {
        SetActiveTasks(taskNum-1,!success);
        buttons[taskNum - 1].SetActive(!success);
    }
    public bool TIContainsVowel(string input)
    {
        string vowels = "aeiou"; // List of vowels
        input = input.ToLower();
        foreach (char character in input)
        {
            if (vowels.Contains(character))
            {
                return true; // The string contains a vowel
            }
        }
        return false; // No vowel found in the string
    }

    public void OnReset(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(1);
    }

    private void EndGame()
    {
        SceneManager.LoadScene(2);
    }
    private void WinGame()
    {
        SceneManager.LoadScene(3);
    }
}
