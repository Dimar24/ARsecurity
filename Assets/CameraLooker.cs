using UnityEngine;

public class CameraLooker : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    private void FixedUpdate()
    {
        transform.LookAt(_camera);
    }
}
