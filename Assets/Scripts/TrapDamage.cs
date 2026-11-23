
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public int damage = 20;
    public float damageCooldown = 1.5f;

    private float nextDamageTime = 0f;

    void OnTriggerEnter(Collider other)
    {
        TryDamage(other);
    }

    void OnTriggerStay(Collider other)
    {
        TryDamage(other);
    }

    void TryDamage(Collider other)
    {
        CharacterController player =
            other.GetComponentInParent<CharacterController>();

        if (player == null)
            return;

        if (Time.time < nextDamageTime)
            return;

        if (TempleGameManager.Instance == null)
        {
            Debug.LogWarning("TempleGameManager not found!");
            return;
        }

        TempleGameManager.Instance.TakeDamage(damage);

        nextDamageTime = Time.time + damageCooldown;

        Debug.Log("Player hit trap! -" + damage + " HP");
    }
}
