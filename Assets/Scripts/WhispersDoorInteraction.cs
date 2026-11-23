
using UnityEngine;

public class WhispersDoorInteraction : MonoBehaviour
{
    public Animator doorAnimator;
    public Transform player;
    public GameObject doorPrompt;
    public float openDistance = 3f;

    private bool opened = false;

    void Update()
    {
        if (player == null || doorAnimator == null)
            return;

        Vector3 a = transform.position;
        Vector3 b = player.position;
        a.y = 0;
        b.y = 0;

        bool nearby = Vector3.Distance(a, b) <= openDistance && !opened;

        if (doorPrompt != null)
            doorPrompt.SetActive(nearby);

        if (nearby && Input.GetKeyDown(KeyCode.E))
        {
            doorAnimator.SetTrigger("OpenDoor");
            opened = true;

            if (doorPrompt != null)
                doorPrompt.SetActive(false);
        }
    }
}
