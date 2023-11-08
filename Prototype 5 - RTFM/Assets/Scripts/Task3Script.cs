using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Task3Script : MonoBehaviour
{
    public bool isSolved = false;
    public GameManager gm;
    public Image button;
    private Color[] _possibleColors = new[] {Color.green, Color.red, Color.yellow, Color.blue};
    private Color _buttonColor;
    public CoverupScript cos;
    public void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        gm.startGame.AddListener(InitNewTask);
    }

    private void InitNewTask()
    {
        Debug.Log("Init Task 3");
        cos.ResetAll();
        _buttonColor = _possibleColors[Random.Range(0, _possibleColors.Length)];
        button.color = _buttonColor;
    }

    public void HiddenButton()
    {
        if (_buttonColor.Equals(_possibleColors[0]))
        {
            isSolved = true;
        }
        if (!isSolved) InitNewTask();
        gm.SubmitTask(3, isSolved);
    }
    public void CheckIfSolved()
    {
        isSolved = false;
        if (_buttonColor.Equals(_possibleColors[3]) && gm.safetyLevel.ToString().Contains('9')||
            _buttonColor.Equals(_possibleColors[1]) && gm.GetActiveTasksNumber().Equals(2)||
            _buttonColor.Equals(_possibleColors[2]) && gm.energyLevel>80 && (gm.safetyLevel%2).Equals(1))
        {
            isSolved = true;
        }
        if (!isSolved) InitNewTask();
        gm.SubmitTask(3, isSolved);
    }

    
    
}
