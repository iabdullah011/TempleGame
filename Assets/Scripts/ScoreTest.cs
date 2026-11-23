
using UnityEngine;

public class ScoreTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (TempleGameManager.Instance != null)
            {
                TempleGameManager.Instance.AddScore(50);
                Debug.Log("50 Points Added!");
            }
        }
    }
}
