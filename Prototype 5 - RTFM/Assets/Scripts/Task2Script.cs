using TMPro;
using UnityEngine;

public class Task2Script : MonoBehaviour
{
    public bool isSolved = false;
    private WordObj[] _words;
    private WordObj _currentWord;
    public TMP_InputField input;
    public TextMeshProUGUI wordPrompt;
    public GameManager gm;

    public void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        gm.startGame.AddListener(InitNewTask);
        _words = FillWithWords();
    }

    private void InitNewTask()
    {
        Debug.Log("Init Task 2");
        _currentWord = _words[Random.Range(0, _words.Length)];
        wordPrompt.text = _currentWord.name;
        input.text = "";
    }

    public void CheckIfSolved()
    {
        string targetWord = _currentWord.name.ToLower();
        isSolved = true;
        string submittedInput = input.text.ToLower();
        if (gm.GetActiveTasksNumber() > 1)
        {
            isSolved = false;
        }
        else
        {
            switch (_currentWord.name.ToLower())
            {
                case "plural":
                    targetWord = "no";
                    break;
                case "no":
                    targetWord = "yes";
                    break;
                case "yes":
                    targetWord = "plural";
                    break;
                default:
                {
                    if (_currentWord.isEdible)
                    {
                        targetWord = RemoveAllVowels(targetWord);
                    }

                    if (gm.safetyLevel > 40)
                    {
                        targetWord = RemoveAllN(targetWord);
                    }

                    if (gm.energyLevel > 80)
                    {
                        targetWord = "mistake" + targetWord;
                    }

                    if (_currentWord.isPlural)
                    {
                        targetWord = (targetWord.Length <= 3) ? "" : targetWord.Substring(0, targetWord.Length - 3);
                    }
                    break;
                }
            }
        }

        Debug.Log(targetWord);
        if (!targetWord.Equals(submittedInput))
        {
            isSolved = false;
        }


        if (!isSolved) InitNewTask();
        gm.SubmitTask(2, isSolved);
    }

    private string RemoveAllVowels(string targetWord)
    {
        string word = "";
        for (var i = 0; i < targetWord.Length; i++)
        {
            if (!(targetWord[i].Equals('a') || targetWord[i].Equals('e') || targetWord[i].Equals('i') ||
                  targetWord[i].Equals('o') || targetWord[i].Equals('u')))
            {
                word += targetWord[i];
            }
        }

        return word;
    }

    private string RemoveAllN(string targetWord)
    {
        string word = "";
        for (var i = 0; i < targetWord.Length; i++)
        {
            if (!targetWord[i].Equals('n'))
            {
                word += targetWord[i];
            }
        }

        return word;
    }

    private WordObj[] FillWithWords()
    {
        return new WordObj[]
        {
            new WordObj("Banana", true, false),
            new WordObj("Bananas", true, true),
            new WordObj("House", false, false),
            new WordObj("Apple", true, false),
            new WordObj("Grapefruit", true, false),
            new WordObj("Indestructable", false, false),
            new WordObj("Emergency", false, false),
            new WordObj("Emergencies", false, true),
            new WordObj("Explosion", false, false),
            new WordObj("Explosions", false, true),
            new WordObj("Failure", false, false),
            new WordObj("Failures", false, true),
            new WordObj("Circuit", false, false),
            new WordObj("Yes", false, false),
            new WordObj("No", false, false),
            new WordObj("Plural", false, false),
        };
    }
}

public class WordObj
{
    public string name;
    public bool isEdible;
    public bool isPlural;

    public WordObj(string n, bool edible, bool plural)
    {
        name = n;
        isEdible = edible;
        isPlural = plural;
    }
}