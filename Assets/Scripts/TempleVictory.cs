
using UnityEngine;

public class TempleVictory : MonoBehaviour
{
    public EscapeKeyPickup escapeKey;
    public GameObject victoryPanel;

    private bool completed = false;

    void Start()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (completed)
            return;

        CharacterController player =
            other.GetComponentInParent<CharacterController>();

        if (player == null)
            return;

        if (escapeKey == null || !escapeKey.HasKey)
            return;

        completed = true;

        if (TempleGameManager.Instance != null)
        {
            TempleGameManager.Instance.AddScore(250);
        }

        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;

        Debug.Log("Temple Escaped! +250 Points");
    }
}
