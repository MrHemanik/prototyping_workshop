using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class RotateElementScript : MonoBehaviour
{
    public enum Axis
    {
        x,
        y,
        z
    };
    // Start is called before the first frame update
    public Axis axis = Axis.z;
    public float speedModifier;

    // Update is called once per frame
    void FixedUpdate()
    {
        //Rotate in the axis that is selected in axis with time.fixeddeltatime
        switch (axis)
        {
            case Axis.x:
                transform.Rotate(Time.fixedDeltaTime * speedModifier, 0, 0);
                break;
            case Axis.y:
                transform.Rotate(0, Time.fixedDeltaTime * speedModifier, 0);
                break;
            case Axis.z:
                transform.Rotate(0, 0, Time.fixedDeltaTime * speedModifier);
                break;
        }
        
    }
}
