using UnityEngine;

public class AreaTransitionScript : MonoBehaviour
{
    private MainCameraController cam;
    public Vector2 newMinCameraBoundary;
    public Vector2 newMaxCameraBoundary;
    [SerializeField] Vector2 playerPosOffset;
    [SerializeField] Transform exitPos;
    private void Awake()
    {
        cam = Camera.main.GetComponent<MainCameraController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            cam.minCameraBoundary = newMinCameraBoundary;
            cam.maxCameraBoundary = newMaxCameraBoundary;

            cam.player.position = exitPos.position +(Vector3)playerPosOffset;
        }
    }
}
