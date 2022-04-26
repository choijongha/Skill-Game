using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public Transform player { get; private set; }
    [SerializeField] float smoothing = 0.2f;
    public Vector2 minCameraBoundary;
    public Vector2 maxCameraBoundary;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, this.transform.position.z);

        targetPos.x = Mathf.Clamp(targetPos.x, minCameraBoundary.x, maxCameraBoundary.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minCameraBoundary.y, maxCameraBoundary.y);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        if(minCameraBoundary == Vector2.zero && maxCameraBoundary == Vector2.zero)
        {
            minCameraBoundary = player.GetComponent<Movement>().saveMinPos;
            maxCameraBoundary = player.GetComponent<Movement>().saveMaxPos;
            Debug.Log("¿€µø");
        }
    }
    /*private void LateUpdate() 
    {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, this.transform.position.z);
        transform.position = targetPos;
    }*/
}
