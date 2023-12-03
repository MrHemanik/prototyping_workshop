using UnityEngine;
using UnityEngine.UI;

public class CursorFocusHandler : MonoBehaviour
{
    private Animator _animator;
    private Transform _cameraTransform;
    private string _currentTag ="empty";
    private static readonly int Focus = Animator.StringToHash("Focus");

    void Start()
    {
        _animator = GetComponent<Animator>();
        _cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out var hit, 10f))
        {
            if (hit.collider.CompareTag(_currentTag))
            {
                return;
            }

            _currentTag = hit.collider.tag;
            switch (_currentTag)
            {
                case "Key":
                case "OpenableDoor":
                case "UnopenableDoor":
                case "Axe":
                case "AxeBreakable":
                case "ExitLadder":
                    _animator.SetBool(Focus, true);
                    break;
                default:
                    _animator.SetBool(Focus, false);
                    break;
            }
        }
    }
}