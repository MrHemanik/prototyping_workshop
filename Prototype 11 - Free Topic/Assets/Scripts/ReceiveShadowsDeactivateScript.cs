using UnityEngine;

public class ReceiveShadowsDeactivateScript : MonoBehaviour
{
    void Start()
    {
        GetComponent<MeshRenderer>().receiveShadows = false;
    }

}
