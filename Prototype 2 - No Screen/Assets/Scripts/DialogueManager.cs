using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class DialogueManager: MonoBehaviour
{
        public AudioManager am;
        public int currentDialogueState = 0;
        private void Start()
        {
                am.Play("Dialogue0");
        }
        public void OnOne(InputAction.CallbackContext context)
        {
                if (!context.started) return;
                if (currentDialogueState.Equals(0))
                {
                        am.Play("Dialogue1");
                        currentDialogueState = 1;
                }
                else if (currentDialogueState.Equals(1))
                {
                        am.Play("Dialogue1.1");
                        currentDialogueState = 11;
                }
                else if (currentDialogueState.Equals(11))
                {
                        SceneManager.LoadScene("MazeScene");
                }
                else if (currentDialogueState.Equals(12))
                {
                        am.Play("Dialogue1.1");
                        currentDialogueState = 11;
                }
        }
        public void OnTwo(InputAction.CallbackContext context)
        {
                if (!context.started) return;
                if (currentDialogueState.Equals(0))
                {
                        am.Play("Dialogue2");
                        currentDialogueState = 2;
                        StartCoroutine(WaitForDialogueToFinish(am.GetSoundLength("Dialogue2")));
                }
                else if (currentDialogueState.Equals(1))
                {
                        am.Play("Dialogue1.2");
                        currentDialogueState = 12;
                }
                else if (currentDialogueState.Equals(12))
                {
                        am.Play("Dialogue1.2");
                        currentDialogueState = 12;
                }
        }
        private IEnumerator WaitForDialogueToFinish(float length)
        {
                yield return new WaitForSeconds(length);
                Application.Quit();
        }
}