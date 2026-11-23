
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public Animator doorAnimator;
    public Animator leftDoorAnimator;
    public Transform player;
    public GameObject doorPrompt;

    public EscapeKeyPickup escapeKey;
    public GameObject lockedMessage;
    public GameObject unlockedMessage;

    public float openDistance = 5f;
    public float messageDuration = 4f;

    private bool firstEntryOpened = false;
    private bool entranceClosed = false;
    private bool finalGateOpened = false;
    private float messageTimer = 0f;

    void Start()
    {
        if (doorPrompt != null)
            doorPrompt.SetActive(false);

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

        if (player == null || doorAnimator == null)
            return;

        Vector3 a = transform.position;
        Vector3 b = player.position;

        a.y = 0;
        b.y = 0;

        bool nearby = Vector3.Distance(a, b) <= openDistance;

        bool canInteract = nearby &&
                           !finalGateOpened &&
                           (!firstEntryOpened || entranceClosed);

        if (doorPrompt != null)
            doorPrompt.SetActive(canInteract);

        if (!canInteract || !Input.GetKeyDown(KeyCode.E))
            return;

        if (!firstEntryOpened)
        {
            firstEntryOpened = true;
            OpenDoors();
        }
        else if (entranceClosed)
        {
            if (escapeKey == null || !escapeKey.HasKey)
            {
                ShowMessage(lockedMessage);
            }
            else
            {
                finalGateOpened = true;
                OpenDoors();
                ShowMessage(unlockedMessage);
                Debug.Log("Main Gate Unlocked!");
            }
        }
    }

    public void CloseEntrance()
    {
        if (!firstEntryOpened || entranceClosed)
            return;

        entranceClosed = true;

        doorAnimator.ResetTrigger("OpenDoor");
        doorAnimator.SetTrigger("CloseDoor");

        if (leftDoorAnimator != null)
        {
            leftDoorAnimator.ResetTrigger("OpenDoor");
            leftDoorAnimator.SetTrigger("CloseDoor");
        }

        Debug.Log("Temple entrance closed!");
    }

    void OpenDoors()
    {
        doorAnimator.ResetTrigger("CloseDoor");
        doorAnimator.SetTrigger("OpenDoor");

        if (leftDoorAnimator != null)
        {
            leftDoorAnimator.ResetTrigger("CloseDoor");
            leftDoorAnimator.SetTrigger("OpenDoor");
        }

        if (doorPrompt != null)
            doorPrompt.SetActive(false);
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
