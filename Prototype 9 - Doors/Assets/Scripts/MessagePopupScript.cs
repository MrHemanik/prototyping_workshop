using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessagePopupScript : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private bool _hasActivePopup = false;
    public void ShowMessage(string message)
    {
        if (_hasActivePopup)
        {
            StopAllCoroutines();
        }
        _hasActivePopup = true;
        StartCoroutine(DisplayMessage(message));
    }
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = "";
    }
    private IEnumerator DisplayMessage(string message)
    {
        _text.text = message;
        _hasActivePopup = true;
        var color = new Color(1f, 1f, 1f, 0f);
        while (color.a < 1f){
            //Wait for a frame
            color.a += 2*Time.deltaTime;
            _text.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        while (color.a > 0f){
            
            color.a -= 2*Time.deltaTime;
            _text.color = color;
            yield return null;
        }
        _hasActivePopup = false;
        _text.text = "";
    }
}
