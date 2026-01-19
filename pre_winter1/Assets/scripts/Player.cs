using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    Rigidbody2D rig;
    Camera cam;

    [Header("이동 설정")]
    public float mamxX = 2.5f;

    [Header("점수 설정")]
    public int CurrentScore = 1;
    public TextMeshProUGUI ScoreText;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        UpdateScore();
    }

    void FixedUpdate()
    {
        FollowMouse();
    }

    public void UpdateScore()
    {
        ScoreText.text = CurrentScore.ToString();
    }

    void FollowMouse()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 mousePos = new Vector3(mousePosition.x, mousePosition.y, 10f);
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        float clampedX = Mathf.Clamp(worldPos.x, -mamxX, mamxX);
        rig.MovePosition(new Vector2(clampedX, rig.position.y));
    }
    private void OnTriggerEnter2D(Collider2D metGate)
    {
        if (metGate.CompareTag("Gate"))
        {
            Gate gate = metGate.GetComponent<Gate>();
            ApplyOperation(gate.op, gate.value);
            UpdateScore();
            Destroy(gate.partnerGate);
            Destroy(metGate.gameObject);
        }
    }

    void ApplyOperation(Operation op, int val)
    {
        switch (op)
        {
            case Operation.Add: CurrentScore += val; break;
            case Operation.Sub: CurrentScore = Mathf.Max(1, CurrentScore - val); break;
            case Operation.Mul: CurrentScore *= val; break;
            case Operation.Div: CurrentScore = Mathf.Max(1, CurrentScore / val); break;
        }
    }
}
