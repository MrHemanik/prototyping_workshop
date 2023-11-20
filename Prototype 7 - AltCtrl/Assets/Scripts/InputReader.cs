using System.Collections;
using System.IO.Ports;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputReader : MonoBehaviour
{
    // This project ignores performace so hard, its not even funny anymore

    private SerialPort stream;
    public Transform playerObject;
    private Vector3 acceleration;
    private int _curUltrasoundDistance;
    private int _inGame = 0;
    public Transform targetObject;
    public AudioSource backgroundMusic;
    private bool _inSpeedZone = false;
    public WorldGeneratorScript worldGeneratorScript;
    private float backgroundMusicVolume = 0.4f;
    private float backgroundMusicPitch = 0.85f;
    public GameObject startCanvas;
    public GameObject endCanvas;
    private int restartInt = 0;

    // Game functions
    private float baseSpeed = 6f;
    private float speed;
    private int finishedSegments = 0;


    // Start is called before the first frame update
    void Start()
    {
        stream = new SerialPort(SerialPort.GetPortNames()[0], 9600);
        stream.Open();
        speed = baseSpeed;
        ChangeGameState(0);
    }

    // Update is called once per frame
    void Update()
    {
        string value = stream.ReadLine();
        //Debug.Log(value);
        if (!value.Contains(',')) return;
        int[] values = value.Split(',').Select(int.Parse).ToArray();
        _curUltrasoundDistance = values[2];
        DistanceInputCheck();
        if (_inGame != 1) return;
        //Debug.Log(acceleration);
        if (values.Length < 3) return;
        acceleration = new Vector3(values[1], values[0], 0);
        acceleration = acceleration / 16384f * 90f;
        //Set the rotation with smoothness, like lerp
        playerObject.rotation = Quaternion.Slerp(playerObject.rotation, Quaternion.Euler(acceleration), 0.5f);
        // transform in direction of target
        transform.position =
            Vector3.MoveTowards(transform.position, targetObject.position,
                0.05f * speed); //0.01f = 0.2m/s ; 0.05f = 1m/s
    }

    private void DistanceInputCheck()
    {
        if (_inGame == 1)
        {
            _curUltrasoundDistance = _curUltrasoundDistance > 30 ? 30 :
                _curUltrasoundDistance < 5 ? 5 : _curUltrasoundDistance;
            CalculateSpeed();
        }
        else if (_inGame == 0)
        {
            if (_curUltrasoundDistance < 14)
            {
                ChangeGameState(1);
            }
        }
        else
        {
            if (_curUltrasoundDistance < 14)
            {
                restartInt++;
                if (restartInt > 8)
                {
                    stream.Close();
                    SceneManager.LoadScene(0);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("AreaStartTrigger"))
        {
            worldGeneratorScript.OnNextSegment();
            finishedSegments++;
            _inSpeedZone = false;
        }

        if (other.tag.Equals("SpeedZone")) _inSpeedZone = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.transform.tag);
        Debug.Log(other.transform.name);
        if (other.transform.tag.Equals("Damage"))
        {
            ChangeGameState(2);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("SpeedZone")) _inSpeedZone = false;
    }

    private void CalculateSpeed()
    {
        speed = (baseSpeed + finishedSegments * 0.5f) * _curUltrasoundDistance / 10f;
        if (_inSpeedZone) speed *= 1.5f;
    }

    private void ChangeGameState(int newState)
    {
        _inGame = newState;
        if (_inGame == 0)
        {
            startCanvas.SetActive(true);
            endCanvas.SetActive(false);
        }
        else if (_inGame == 1) startCanvas.SetActive(false);
        else
        {
            endCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You finished " + finishedSegments + " segments!";
            endCanvas.SetActive(true);
        }
        
        backgroundMusicVolume = _inGame == 1 ? 0.6f : 0.4f;
        backgroundMusicPitch = _inGame == 1 ? 1f : 0.85f;
        StartCoroutine(LerpBackgroundMusic());
    }

    private IEnumerator LerpBackgroundMusic()
    {
        while (Mathf.Abs(backgroundMusic.volume - backgroundMusicVolume) > 0.0001f ||
               Mathf.Abs(backgroundMusic.pitch - backgroundMusicPitch) > 0.0001f)
        {
            backgroundMusic.volume = Mathf.Lerp(backgroundMusic.volume, backgroundMusicVolume, 0.2f);
            backgroundMusic.pitch = Mathf.Lerp(backgroundMusic.pitch, backgroundMusicPitch, 0.2f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
/* UP Down Left Right on normal position not handpostion
 *      acceleration.z = 0;
        acceleration.y *= -1;
        acceleration = acceleration/16384f*90f;
        playerObject.rotation = Quaternion.Euler(acceleration);
        // transform in direction of target
        transform.position = Vector3.MoveTowards(transform.position, targetObject.position, 0.05f*speed); //0.01f = 0.2m/s ; 0.05f = 1m/s
 */