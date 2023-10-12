using UnityEngine;

public class MapVisualizer : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject playerPrefab;
    public GameObject goalPrefab;
    private GameObject _playerInstance;

    public void DrawMap(int[,] map)
    {
        
        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int col = 0; col < map.GetLength(1); col++)
            {
                switch (map[row, col])
                {
                    case 1: //Wall
                        Instantiate(wallPrefab, new Vector3(col, -row), Quaternion.identity);
                        break;
                    case 2: //Player
                        _playerInstance = Instantiate(playerPrefab, new Vector3(col, -row), Quaternion.identity);
                        break;
                    case 3: //Goal
                        Instantiate(goalPrefab, new Vector3(col, -row), Quaternion.identity);
                        break;
                }
            }
        }
    }

    public void UpdatePlayer(Vector2 newPos)
    {
        _playerInstance.transform.position = new Vector3(newPos.y,-newPos.x);
    }
}
