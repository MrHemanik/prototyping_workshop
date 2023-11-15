using System.Collections;
using System.Collections.Generic;
using Classes;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject NoteObject;
    public readonly float _lowestZ = 0.5f;
    public readonly float _highestZ = 7.5f;
    public IEnumerator GenerateLevel(Note[] levelNotes, float speed, float noteDensity, System.Action callback)
    {
        int lowestNote = 100;
        int highestNote = 0;
        foreach (var note in levelNotes)
        {
            highestNote= Mathf.Max(highestNote, note.ToNumber());
            lowestNote= Mathf.Min(lowestNote, note.ToNumber());
        }
        float noteSpan = highestNote - lowestNote + 1; //from 2 to 5 = 5-2+1 = 4
        float noteWidth = (_highestZ-_lowestZ) / noteSpan;
        float GetNoteZPosition(Note n)
        {
            float pos = ((n.ToNumber() - lowestNote) * noteWidth) + _lowestZ+noteWidth/2;
            return pos;
        }
        
        yield return new WaitForSeconds(0.1f); //Wait til game has fully loaded
        foreach (var note in levelNotes)
        {
            float newNotePhysicalLength = note.length * 5*noteDensity;
            GameObject newNote = Instantiate(NoteObject,new Vector3(16.5f+newNotePhysicalLength/2, 1f, GetNoteZPosition(note)),Quaternion.identity,transform);
            newNote.transform.localScale = new Vector3(newNotePhysicalLength, 1, noteWidth);
            var script = newNote.GetComponent<NoteMovementScript>();
            script.Note = note;
            script.Speed = speed*noteDensity;
            yield return new WaitForSeconds((note.length+note.pause)/speed);
        }

        yield return new WaitForSeconds(4f / speed+1f);
        callback.Invoke();
    }

    public (float, float) GetRange()
    {
        return (_lowestZ, _highestZ);
    }

 
}
