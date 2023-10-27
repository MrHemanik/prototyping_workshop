using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Classes
{
    public enum Notes
    {
        A2,B2,C3,D3,E3,F3,G3,
        A3,B3,C4,D4,E4,F4,G4,
        A4,B4,C5,D5,E5,F5,G5,
        A5,B5,C6,D6,E6,F6,G6
    }

    public class Note
    {
        public Notes note;
        public float length;
        public float pause;
        public Note(Notes noteKey, float noteLength,float notePauseAfterwards)
        {
            note = noteKey;
            length = noteLength;
            pause = notePauseAfterwards;
        }

        public int ToNumber()
        {
            int number = note switch
            {
                Notes.A2 => 0,
                Notes.B2 => 1,
                Notes.C3 => 2,
                Notes.D3 => 3,
                Notes.E3 => 4,
                Notes.F3 => 5,
                Notes.G3 => 6,
                Notes.A3 => 7,
                Notes.B3 => 8,
                Notes.C4 => 9,
                Notes.D4 => 10,
                Notes.E4 => 11,
                Notes.F4 => 12,
                Notes.G4 => 13,
                Notes.A4 => 14,
                Notes.B4 => 15,
                Notes.C5 => 16,
                Notes.D5 => 17,
                Notes.E5 => 18,
                Notes.F5 => 19,
                Notes.G5 => 20,
                Notes.A5 => 21,
                Notes.B5 => 22,
                Notes.C6 => 23,
                Notes.D6 => 24,
                Notes.E6 => 25,
                Notes.F6 => 26,
                Notes.G6 => 27,
                _ => 0
            };
            return number;
        }
    }
}