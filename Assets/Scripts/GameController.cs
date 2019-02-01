using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // There is definitely a better pattern to accomplish this.
            foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                go.BroadcastMessage("Tick", 1, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
