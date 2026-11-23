
using UnityEngine;

public class TempleEntryTrigger : MonoBehaviour
{
    public DoorInteraction mainDoor;

    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if (!other.CompareTag("Player"))
            return;

        triggered = true;

        if (mainDoor != null)
            mainDoor.CloseEntrance();
    }
}
