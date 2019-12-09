using UnityEngine;

public class LightTorch : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Input.mousePosition);
    }
}
