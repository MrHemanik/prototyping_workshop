using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNoteScript : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag.Contains("Note"))
        {
            Destroy(other.gameObject);
        }
    }
}
