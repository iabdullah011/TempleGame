
using UnityEngine;

public class EscapeKeyLever : MonoBehaviour
{
    public Transform player;
    public ArtifactPickup artifact;
    public Transform gate;

    public GameObject leverPrompt;
    public GameObject lockedMessage;
    public GameObject unlockedMessage;

    public float interactDistance = 3f;
    public float gateRiseHeight = 3f;
    public float gateSpeed = 1f;
    public float messageDuration = 3f;

    private bool gateUnlocked = false;
    private Vector3 openPosition;
    private float messageTimer;

    void Start()
    {
        if (gate != null)
            openPosition = gate.position + Vector3.up * gateRiseHeight;

        if (leverPrompt != null)
            leverPrompt.SetActive(false);

        if (lockedMessage != null)
            lockedMessage.SetActive(false);

        if (unlockedMessage != null)
            unlockedMessage.SetActive(false);
    }

    void Update()
    {
        if (gateUnlocked && gate != null)
        {
            gate.position = Vector3.MoveTowards(
                gate.position,
                openPosition,
                gateSpeed * Time.deltaTime
            );
        }

        if (messageTimer > 0f)
        {
            messageTimer -= Time.deltaTime;

            if (messageTimer <= 0f)
            {
                if (lockedMessage != null)
                    lockedMessage.SetActive(false);

                if (unlockedMessage != null)
                    unlockedMessage.SetActive(false);
            }
        }

        if (player == null || gateUnlocked)
        {
            if (leverPrompt != null)
                leverPrompt.SetActive(false);

            return;
        }

        Vector3 a = transform.position;
        Vector3 b = player.position;

        a.y = 0;
        b.y = 0;

        bool nearby = Vector3.Distance(a, b) <= interactDistance;

        if (leverPrompt != null)
            leverPrompt.SetActive(nearby);

        if (nearby && Input.GetKeyDown(KeyCode.E))
        {
            if (artifact == null || !artifact.IsCollected)
            {
                ShowMessage(lockedMessage);
            }
            else
            {
                gateUnlocked = true;

                if (leverPrompt != null)
                    leverPrompt.SetActive(false);

                ShowMessage(unlockedMessage);

                if (TempleGameManager.Instance != null)
                {
                    TempleGameManager.Instance.AddScore(100);
                }

                Debug.Log("Escape key room unlocked! +100 Points");
            }
        }
    }

    void ShowMessage(GameObject message)
    {
        if (lockedMessage != null)
            lockedMessage.SetActive(false);

        if (unlockedMessage != null)
            unlockedMessage.SetActive(false);

        if (message != null)
            message.SetActive(true);

        messageTimer = messageDuration;
    }
}
