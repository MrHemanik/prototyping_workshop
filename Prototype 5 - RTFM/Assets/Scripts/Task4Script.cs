using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Task4Script : MonoBehaviour
{
    public bool isSolved = false;
    public GameManager gm;
    private string symbols = "◇□△◁○+-";
    private string generatedSymbols = "";
    public TextMeshProUGUI[] symbolTexts;
    public void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        gm.startGame.AddListener(InitNewTask);
    }

    private void InitNewTask()
    {
        Debug.Log("Init Task 4");

        char GenerateSymbol()
        {
            return symbols[Random.Range(0, symbols.Length)];
        }

        generatedSymbols = "";
        for (int i = 0; i < 5; i++)
        {
            generatedSymbols += GenerateSymbol();
            symbolTexts[i].text = generatedSymbols[i].ToString();
        }
        
    }
    public void CheckIfSolved(int buttonNumber) //1blue,2orange,3magenta,4green
    {
        isSolved = false;
        bool isShownSymbolOnButton = false;
        int rightButton = 0;
        for (int i = 1; i < generatedSymbols.Length; i++)
        {
            if (generatedSymbols[0].Equals(generatedSymbols[i])) isShownSymbolOnButton = true;
        }
        if(isShownSymbolOnButton)
        {
            rightButton = 3;
        }
        else if(generatedSymbols[2].Equals('○'))
        {
            rightButton = 3;
        }
        else if (generatedSymbols[3].Equals('△')||generatedSymbols[3].Equals('◁'))
        {
            rightButton = 2;
        }
        else if (generatedSymbols[3].Equals('△')||generatedSymbols[3].Equals('◁'))
        {
            rightButton = 1;
        }
        else if(generatedSymbols[0].Equals('○'))
        {
            rightButton = 4;
        }
        else if (generatedSymbols[4].Equals('◇')||generatedSymbols[4].Equals('□'))
        {
            rightButton = 2;
        }
        else
        {
            rightButton = 3;
        }

        if (rightButton.Equals(buttonNumber))
        {
            isSolved = true;
        }
        if (!isSolved) InitNewTask();
        gm.SubmitTask(4, isSolved);
    }

    
    
}
