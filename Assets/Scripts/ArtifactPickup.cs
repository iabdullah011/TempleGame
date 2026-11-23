
using UnityEngine;

public class ArtifactPickup : MonoBehaviour
{
    public Transform player;
    public GameObject pickupPrompt;
    public GameObject victoryPanel;

    public float pickupDistance = 3f;
    public float messageDuration = 5f;

    private bool collected = false;
    public bool IsCollected => collected;
    private float messageTimer = 0f;

    void Start()
    {
        if (pickupPrompt != null)
            pickupPrompt.SetActive(false);

        if (victoryPanel != null)
            victoryPanel.SetActive(false);
    }

    void Update()
    {
        if (collected)
        {
            if (messageTimer > 0f)
            {
                messageTimer -= Time.deltaTime;

                if (messageTimer <= 0f && victoryPanel != null)
                    victoryPanel.SetActive(false);
            }

            return;
        }

        if (player == null)
            return;

        Vector3 a = transform.position;
        Vector3 b = player.position;

        a.y = 0;
        b.y = 0;

        bool nearby = Vector3.Distance(a, b) <= pickupDistance;

        if (pickupPrompt != null)
            pickupPrompt.SetActive(nearby);

        if (nearby && Input.GetKeyDown(KeyCode.E))
        {
            collected = true;
            messageTimer = messageDuration;

            if (pickupPrompt != null)
                pickupPrompt.SetActive(false);

            if (victoryPanel != null)
                victoryPanel.SetActive(true);

            foreach (Renderer r in GetComponentsInChildren<Renderer>())
                r.enabled = false;

            foreach (Collider c in GetComponentsInChildren<Collider>())
                c.enabled = false;

            if (TempleGameManager.Instance != null)
            {
                TempleGameManager.Instance.AddScore(200);
            }

            Debug.Log("Ancient Artifact Collected! +200 Points");
        }
    }
}
