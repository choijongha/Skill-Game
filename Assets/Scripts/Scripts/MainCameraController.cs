using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public Transform player { get; private set; }
    [SerializeField] float smoothing = 0.2f;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, this.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);

    }
    /*private void LateUpdate() 
    {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, this.transform.position.z);
        transform.position = targetPos;
    }*/
}
