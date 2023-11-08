using UnityEngine;

public class Task1Script : MonoBehaviour
{
    public bool isSolved = false;
    public bool[] startingSwitchStates = null;
    public SwitchScript[] switches;
    public GameManager gm;
    public void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        gm.startGame.AddListener(InitNewTask);
    }
    private void InitNewTask()
    {
        Debug.Log("Init Task 1");
        startingSwitchStates = new bool[switches.Length];
        for (var i = 0; i < switches.Length; i++)
        {
            startingSwitchStates[i] = Random.value < 0.5;
            switches[i].state = startingSwitchStates[i];
        }
    }
    public void CheckIfSolved()
    {
        bool[] targetSwitchStates = startingSwitchStates;
        //Change targetSwitchStates based on variables
        if (gm.energyLevel < 40) targetSwitchStates = new[] {false, false, false, false, true};
        else
        {
            if (gm.safetyLevel < 20)
            {
                for (var i = 0; i < targetSwitchStates.Length; i++)
                {
                    targetSwitchStates[i] = !targetSwitchStates[i];
                }
            }

            int activeSwitches = 0;
            foreach (var switchScript in switches)
            {
                activeSwitches += switchScript.state ? 1:0;
            }
            if(activeSwitches >= 2) targetSwitchStates[2] = !targetSwitchStates[2];
            if(gm.TIContainsVowel(gm.trainIdentifier)) targetSwitchStates[1] = !targetSwitchStates[1];
            targetSwitchStates[0] = true;
        }
        
        //Check for equality
        isSolved = true;
        for (int i = 0; i < targetSwitchStates.Length; i++)
        {
            if (!targetSwitchStates[i].Equals(switches[i].state))
            {
                Debug.Log("Switch"+i+"Taget:"+targetSwitchStates[i]+" State: "+switches[i].state);
                isSolved = false;
                break;
            }
        }
        if(!isSolved) InitNewTask();
        gm.SubmitTask(1, isSolved);
    }

    
}