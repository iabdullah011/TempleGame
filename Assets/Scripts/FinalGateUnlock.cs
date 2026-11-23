
using UnityEngine;

public class FinalGateUnlock : MonoBehaviour
{
    public Transform player;
    public EscapeKeyPickup escapeKey;

    public Animator rightDoorAnimator;
    public Animator leftDoorAnimator;

    public GameObject gatePrompt;
    public GameObject lockedMessage;
    public GameObject unlockedMessage;

    public float interactDistance = 4f;
    public float messageDuration = 4f;

    private bool unlocked = false;
    private float messageTimer = 0f;

    void Start()
    {
        if (gatePrompt != null)
            gatePrompt.SetActive(false);

        if (lockedMessage != null)
            lockedMessage.SetActive(false);

        if (unlockedMessage != null)
            unlockedMessage.SetActive(false);
    }

    void Update()
    {
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

        if (player == null || unlocked)
        {
            if (gatePrompt != null)
                gatePrompt.SetActive(false);

            return;
        }

        Vector3 a = transform.position;
        Vector3 b = player.position;
        a.y = 0;
        b.y = 0;

        bool nearby = Vector3.Distance(a, b) <= interactDistance;

        if (gatePrompt != null)
            gatePrompt.SetActive(nearby);

        if (nearby && Input.GetKeyDown(KeyCode.E))
        {
            if (escapeKey == null || !escapeKey.HasKey)
            {
                ShowMessage(lockedMessage);
            }
            else
            {
                unlocked = true;

                if (gatePrompt != null)
                    gatePrompt.SetActive(false);

                if (rightDoorAnimator != null)
                    rightDoorAnimator.SetTrigger("OpenDoor");

                if (leftDoorAnimator != null)
                    leftDoorAnimator.SetTrigger("OpenDoor");

                ShowMessage(unlockedMessage);

                Debug.Log("Main Gate Unlocked!");
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
