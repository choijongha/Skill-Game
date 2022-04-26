using UnityEngine;
using UnityEngine.SceneManagement;
using AllUnits;
public class GameManager : MonoBehaviour
{
    private Movement playerScript;
    [SerializeField] float waitToLoad = 2f;
    public Transform damageText;
    private void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        ReLoadScene();
    }
    private void ReLoadScene()
    {
        if (playerScript.isDead)
        {
            waitToLoad -= Time.deltaTime;
            if (waitToLoad <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

}
