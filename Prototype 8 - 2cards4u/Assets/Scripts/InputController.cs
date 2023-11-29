using UnityEngine;

public class InputController : MonoBehaviour
{
    private AnimationController _animationController;
    private AudioController _audioController;
    public KeyCode[] keys;
    private int _currentKey = 0;
    public int animalId = 0;
    private GameManager _gameManager;
    void Start()
    {
        _animationController = GetComponent<AnimationController>();
        _audioController = GetComponent<AudioController>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Wanted to see if just using Input.GetKeyDown() would work better for small prototyping than the new input system
    void Update()
    {
        if (Input.GetKeyDown(keys[0]))
        {
            _animationController.SetPitch(3,true);
            _audioController.StartAudio(2);
            _currentKey = 1;
            _gameManager.curSoundStates[animalId] = 1;

        }
        else if (Input.GetKeyDown(keys[1]))
        {
            _animationController.SetPitch(2,true);
            _audioController.StartAudio(1);
            _currentKey = 2;
            _gameManager.curSoundStates[animalId] = 2;
        }
        else if (Input.GetKeyDown(keys[2]))
        {
            _animationController.SetPitch(1,true);
            _audioController.StartAudio(0);
            _currentKey = 3;
            _gameManager.curSoundStates[animalId] = 3;
        }

        if (Input.GetKeyUp(keys[0]))
        {
            if (_currentKey != 1) return;
            StopAudio();
        }
        if (Input.GetKeyUp(keys[1]))
        {
            if (_currentKey != 2) return;
            StopAudio();
        }
        else if (Input.GetKeyUp(keys[2]))
        {
            if (_currentKey != 3) return;
            StopAudio();
        }

    
    }
    private void StopAudio()
    {
        _animationController.SetPitch(2,false);
        _audioController.StopAudio();
        _gameManager.curSoundStates[animalId] = 0;
    }
}
