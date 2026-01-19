using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [Header("속도 설정")]
    public float fallSpeed = 5f;

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
}