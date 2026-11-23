
using UnityEngine;

public class EscapeKeyPickup : MonoBehaviour
{
    public Transform player;
    public GameObject pickupPrompt;
    public GameObject collectedMessage;

    public float pickupDistance = 2.5f;
    public float messageDuration = 4f;

    public bool HasKey { get; private set; }

    private float messageTimer;

    void Start()
    {
        HasKey = false;

        if (pickupPrompt != null)
            pickupPrompt.SetActive(false);

        if (collectedMessage != null)
            collectedMessage.SetActive(false);
    }

    void Update()
    {
        if (messageTimer > 0f)
        {
            messageTimer -= Time.deltaTime;

            if (messageTimer <= 0f && collectedMessage != null)
                collectedMessage.SetActive(false);
        }

        if (HasKey || player == null)
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
            HasKey = true;
            if (TempleGameManager.Instance != null)
            {
                TempleGameManager.Instance.AddScore(150);
            }
            messageTimer = messageDuration;

            if (pickupPrompt != null)
                pickupPrompt.SetActive(false);

            if (collectedMessage != null)
                collectedMessage.SetActive(true);

            foreach (Renderer r in GetComponentsInChildren<Renderer>())
                r.enabled = false;

            foreach (Collider c in GetComponentsInChildren<Collider>())
                c.enabled = false;

            Debug.Log("Escape Key Collected!");
        }
    }
}
