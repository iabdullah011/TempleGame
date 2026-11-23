
using UnityEngine;

public class ThreeButtonPuzzle : MonoBehaviour
{
    public Transform player;

    public Transform moonButton;
    public Transform sunButton;
    public Transform starButton;

    public Transform treasureGate;
    public GameObject puzzleSuccessPanel;

    public float interactDistance = 3f;
    public float gateRiseHeight = 2.125f;
    public float gateSpeed = 1f;
    public float messageDuration = 4f;

    private int sequenceStep = 0;
    private bool puzzleSolved = false;

    private Vector3 gateOpenPosition;
    private float messageTimer = 0f;

    void Start()
    {
        if (treasureGate != null)
        {
            gateOpenPosition = treasureGate.position +
                               Vector3.up * gateRiseHeight;
        }

        if (puzzleSuccessPanel != null)
            puzzleSuccessPanel.SetActive(false);
    }

    void Update()
    {
        if (messageTimer > 0f)
        {
            messageTimer -= Time.deltaTime;

            if (messageTimer <= 0f &&
                puzzleSuccessPanel != null)
            {
                puzzleSuccessPanel.SetActive(false);
            }
        }

        if (puzzleSolved)
        {
            if (treasureGate != null)
            {
                treasureGate.position = Vector3.MoveTowards(
                    treasureGate.position,
                    gateOpenPosition,
                    gateSpeed * Time.deltaTime
                );
            }

            return;
        }

        if (player == null)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            int closestButton = GetClosestButton();

            if (closestButton != -1)
                PressButton(closestButton);
        }
    }

    int GetClosestButton()
    {
        Transform[] buttons =
        {
            moonButton,
            sunButton,
            starButton
        };

        int closest = -1;
        float closestDistance = interactDistance;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] == null)
                continue;

            Vector3 a = player.position;
            Vector3 b = buttons[i].position;

            a.y = 0;
            b.y = 0;

            float distance = Vector3.Distance(a, b);

            if (distance <= closestDistance)
            {
                closestDistance = distance;
                closest = i;
            }
        }

        return closest;
    }

    void PressButton(int buttonNumber)
    {
        int[] correctSequence = { 0, 1, 2 };

        string[] names = { "Moon", "Sun", "Star" };

        Debug.Log("Pressed: " + names[buttonNumber]);

        if (buttonNumber == correctSequence[sequenceStep])
        {
            sequenceStep++;
            Debug.Log("Correct Button!");
        }
        else
        {
            sequenceStep = 0;
            Debug.Log("Wrong Button! Puzzle Reset.");
        }

        if (sequenceStep == correctSequence.Length)
        {
            CompletePuzzle();
        }
    }

    void CompletePuzzle()
    {
        if (puzzleSolved)
            return;

        puzzleSolved = true;

        if (puzzleSuccessPanel != null)
        {
            puzzleSuccessPanel.SetActive(true);
            messageTimer = messageDuration;
        }

        if (TempleGameManager.Instance != null)
        {
            TempleGameManager.Instance.AddScore(200);
        }

        Debug.Log("Puzzle Solved! +200 Points");
    }
}
