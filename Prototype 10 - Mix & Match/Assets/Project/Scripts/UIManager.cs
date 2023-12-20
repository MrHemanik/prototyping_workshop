
using UnityEngine;

namespace Project.Scripts
{
        public class UIManager : MonoBehaviour
        {

                public void QuitGame()
                {
                        Application.Quit();
                        UnityEditor.EditorApplication.isPlaying = false;
                }

        }
}