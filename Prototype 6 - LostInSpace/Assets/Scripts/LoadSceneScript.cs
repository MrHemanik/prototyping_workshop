using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static void LoadScene(int id)
    {
        SceneManager.LoadScene(id);
    }
}
