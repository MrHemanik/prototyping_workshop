using System;
using UnityEngine;

public class PlayerTriggerChecker : MonoBehaviour
{

    private int _currentNote;
    public Oscillator osci;
    public LevelManager lm;
    public Material defaultMat;
    public Material touchedMat;
    

    private void OnTriggerEnter(Collider other)
    {
        _currentNote = other.transform.GetHashCode();
        osci.SetFrequencyByNoteID(other.gameObject.GetComponent<NoteMovementScript>().Note.ToNumber());
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.transform.tag.Contains("Note")) return;
        lm.AddScore();
        other.gameObject.GetComponentInChildren<MeshRenderer>().material = touchedMat;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.transform.tag.Contains("Note")) return;
        other.gameObject.GetComponentInChildren<MeshRenderer>().material = defaultMat;
        if (other.transform.GetHashCode() == _currentNote)
        {
            osci.StopSound();
        }
    }
}
