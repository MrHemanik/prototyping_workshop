using TMPro;
using UnityEngine;

namespace Project.Scripts
{
    public class CounterScript : MonoBehaviour
    {
        private float _counter = 0;
        private TextMeshProUGUI _counterUI;

        void Start()
        {
            _counterUI = gameObject.GetComponent<TextMeshProUGUI>();
        }
    
        void FixedUpdate()
        {
            _counter += Time.deltaTime;
            _counterUI.text = _counter.ToString("F2");
        }
    }
}
