using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberVisualizer : MonoBehaviour
{
    public GameObject heartPrefab;
    private Color32[] digitColors= new Color32[]
    {
        new Color32(255, 0, 0, 200),   // Red
        new Color32(255, 125, 0, 200), // Orange
        new Color32(255, 255, 0, 200), // Yellow
        new Color32(0, 255, 0, 200),   // Green
        new Color32(0, 255, 255, 200), // Cyan
        new Color32(0, 0, 255, 200),   // Blue
        new Color32(255, 0, 255, 200),   // Magenta
    };

    private Vector3 baseDistanceVector = new Vector3(5, -5, 0);
    private Vector3 multiplierVector = new Vector3(23, 0, 0);

    // Update is called once per frame
    public void VisualizeHearts(int num)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        int number = num;
        int numDigits = (int) Mathf.Floor(Mathf.Log10(number)) + 1;
        int digitMultiplier = numDigits-1;
        int renderedHearts = 0;
        
        for (int i = numDigits - 1; i >= 0; i--)
        {
            int divisor = (int)Mathf.Pow(10, i);
            int digit = (number / divisor) % 10;
            for (int j = 0; j < digit; j++)
            {
                GameObject hp = Instantiate(heartPrefab,transform);
                hp.transform.localPosition = baseDistanceVector+multiplierVector*renderedHearts;
                hp.GetComponent<Image>().color = digitColors[digitMultiplier];
                renderedHearts++;
            }
            digitMultiplier--;
        }
    }
    
    
    
}
