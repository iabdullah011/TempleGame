
using UnityEngine;

public class AncientPaperInteraction : MonoBehaviour
{
    public Transform player;
    public GameObject paperPrompt;
    public GameObject ancientNotePanel;
    public float readDistance = 3f;

    public int hintScore = 50;

    private bool isReading = false;
    private bool scoreAwarded = false;

    void Start()
    {
        if (paperPrompt != null)
            paperPrompt.SetActive(false);

        if (ancientNotePanel != null)
            ancientNotePanel.SetActive(false);
    }

    void Update()
    {
        if (player == null || paperPrompt == null || ancientNotePanel == null)
            return;

        Vector3 a = transform.position;
        Vector3 b = player.position;

        a.y = 0;
        b.y = 0;

        bool nearby = Vector3.Distance(a, b) <= readDistance;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isReading)
            {
                isReading = false;
            }
            else if (nearby)
            {
                isReading = true;

                if (!scoreAwarded)
                {
                    scoreAwarded = true;

                    if (TempleGameManager.Instance != null)
                    {
                        TempleGameManager.Instance.AddScore(hintScore);
                    }

                    Debug.Log("Hint Read! +" + hintScore + " Points");
                }
            }
        }

        if (paperPrompt.activeSelf != (nearby && !isReading))
            paperPrompt.SetActive(nearby && !isReading);

        if (ancientNotePanel.activeSelf != isReading)
            ancientNotePanel.SetActive(isReading);
    }
}
