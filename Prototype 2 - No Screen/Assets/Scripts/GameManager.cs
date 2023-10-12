using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public MapVisualizer mapV;
    public AudioManager am;
    public GameObject camera;
    private int[,] map = new int[10, 10] //0 = Walkable, 1 = Wall, 2 = Player, 3 = Goal
    {
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 0, 1, 0, 1, 0, 1, 3, 1, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 1, 0, 1, 1, 0, 1, 0, 1},
        {1, 0, 1, 0, 1, 0, 0, 1, 0, 1},
        {1, 1, 1, 0, 1, 0, 1, 1, 0, 1},
        {1, 0, 0, 0, 1, 0, 0, 1, 0, 1},
        {1, 0, 1, 0, 1, 1, 0, 1, 0, 1},
        {1, 2, 1, 0, 0, 0, 0, 1, 0, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
    };

    private int _rows;
    private int _columns;
    public enum Direction
    {
        North,
        South,
        East,
        West
    }

    private readonly List<Direction> _directionList = new List<Direction>()
        {Direction.North, Direction.East, Direction.South, Direction.West};
    

    private Direction _currentDirection = Direction.North;
    private Vector2 _playerPosition;

    private Coroutine _mazeAudioCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        
        _rows = map.GetLength(0);
        _columns = map.GetLength(1);
        mapV.DrawMap(map);
        //Find Player
        FindPlayer();
        DetectNextObject();
    }
    private void FindPlayer()
    {
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _columns; col++)
            {
                if (map[row, col] == 2)
                {
                    _playerPosition = new Vector2(row, col);
                    return;
                }
            }
        }
    }

    private Vector2 DirectionToDigit(Direction d)
    {
        Vector2 a;
        switch (d)
        {
            case Direction.North:
                a = new Vector2(-1, 0);
                break;
            case Direction.South:
                a = new Vector2(+1, 0);
                break;
            case Direction.East:
                a = new Vector2(0, 1);
                break;
            case Direction.West:
                a = new Vector2(0, -1);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(d), d, null);
        }
        return a;
    }

    [UsedImplicitly]
    public void OnForward(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Vector2 targetPosition = _playerPosition+DirectionToDigit(_currentDirection);
        //Debug.Log(targetPosition+"MapValue: "+map[(int) targetPosition.x, (int) targetPosition.y]);
        //If goal, end game.
        if (map[(int) targetPosition.x, (int) targetPosition.y].Equals(3))
        {
            //Goalstuff
            Debug.Log("Goal Reached!");
            StartCoroutine(Goal());
            return;
        }
        //else if empty, move to space
        if (map[(int) targetPosition.x, (int) targetPosition.y].Equals(0))
        {
            map[(int) _playerPosition.x, (int) _playerPosition.y] = 0;
            map[(int) targetPosition.x, (int) targetPosition.y] = 2;
            _playerPosition = targetPosition;
            mapV.UpdatePlayer(_playerPosition);
        }
        DetectNextObject();
        
    }

    [UsedImplicitly]
    //Rotates current direction depending on input
    public void OnRotate(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        //current number + or - 1, guaranteed to be between 0-3 with modulo, then turned back into direction
        _currentDirection = _directionList[(int) ((4+_directionList.IndexOf(_currentDirection) + context.ReadValue<float>())%4)];
        Debug.Log("New Direction: "+_currentDirection);
        DetectNextObject();
    }
    private IEnumerator Goal()
    {
        transform.GetComponent<PlayerInput>().enabled = false; // Deactivate controls
        am.Play("Dialogue3");
        yield return new WaitForSeconds(am.GetSoundLength("Dialogue3"));
        am.Play("Dialogue3.1");
        yield return new WaitForSeconds(am.GetSoundLength("Dialogue3.1"));
        SceneManager.LoadScene("StartScene");
    }

    private void DetectNextObject()
    {
        if (_mazeAudioCoroutine != null) StopCoroutine(_mazeAudioCoroutine);
        int distance = 1;
        while (true)
        {
            Vector2 observedBlock = _playerPosition + distance * DirectionToDigit(_currentDirection);
            int observedBlockState = map[(int) observedBlock.x, (int) observedBlock.y];
            if (observedBlockState == 0)
            {
                distance++;
            }
            else{
                _mazeAudioCoroutine = StartCoroutine(PlayDistanceAudio(distance-1,observedBlockState));
                return;
            }
        }
    }
    private IEnumerator PlayDistanceAudio(int distance, int objectType)
    {
        
        if (objectType == 1)
        {
            am.Play(distance.ToString());
            yield return new WaitForSeconds(am.GetSoundLength(distance.ToString())-0.8f);
            am.Play("Wall");
        }
        if (objectType == 3)
        {
            am.Play((1+distance).ToString());
            yield return new WaitForSeconds(am.GetSoundLength((1+distance).ToString())-0.8f);
            am.Play("Goal");
        }
    }
    [UsedImplicitly]
    public void OnVision(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        camera.SetActive(!camera.activeSelf);
    }
}
