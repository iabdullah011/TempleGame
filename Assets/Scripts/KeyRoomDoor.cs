
using UnityEngine;

public class KeyRoomDoor : MonoBehaviour
{
    public Transform player;
    public GameObject doorPrompt;

    public float interactDistance = 3f;
    public float openAngle = 90f;
    public float rotationSpeed = 90f;

    private bool opened = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.localRotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);

        if (doorPrompt != null)
            doorPrompt.SetActive(false);
    }

    void Update()
    {
        if (opened)
        {
            transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation,
                openRotation,
                rotationSpeed * Time.deltaTime
            );

            return;
        }

        if (player == null)
            return;

        Vector3 a = transform.position;
        Vector3 b = player.position;

        a.y = 0;
        b.y = 0;

        bool nearby = Vector3.Distance(a, b) <= interactDistance;

        if (doorPrompt != null)
            doorPrompt.SetActive(nearby);

        if (nearby && Input.GetKeyDown(KeyCode.E))
        {
            opened = true;

            if (doorPrompt != null)
                doorPrompt.SetActive(false);
        }
    }
}
