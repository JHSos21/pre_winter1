using System.Collections;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{
    [Header("게이트")]
    public GameObject BlueGate;
    public GameObject RedGate;

    [Header("설정")]
    public float SpawnInterval = 5f;
    public float SpawnY = 6f;
    public float xOffset = 1.2f;

    void Start()
    {
        StartCoroutine(SpawnGateRoutine());
    }

    IEnumerator SpawnGateRoutine()
    {
        while (true)
        {
            SpawnGatePair();
            yield return new WaitForSeconds(SpawnInterval);
        }
    }

    void SpawnGatePair()
    {
        bool isBlueLeft = Random.value > 0.5f;
        Vector3 leftPos = new Vector3(-xOffset, SpawnY, 0);
        Vector3 rightPos = new Vector3(xOffset, SpawnY, 0);
        GameObject leftObj = Instantiate(isBlueLeft ? BlueGate : RedGate, leftPos, Quaternion.identity);
        GameObject rightObj = Instantiate(isBlueLeft ? RedGate : BlueGate, rightPos, Quaternion.identity);
        leftObj.GetComponent<Gate>().partnerGate = rightObj;
        rightObj.GetComponent<Gate>().partnerGate = leftObj;
        if (isBlueLeft)
        {
            AssignLogic(leftObj.GetComponent<Gate>(), true);
            AssignLogic(rightObj.GetComponent<Gate>(), false);
        }
        else
        {
            AssignLogic(leftObj.GetComponent<Gate>(), false);
            AssignLogic(rightObj.GetComponent<Gate>(), true);
        }
    }
    void AssignLogic(Gate gate, bool isBlue)
    {
        if (isBlue)
        {
            Operation op = (Random.value > 0.7f) ? Operation.Mul : Operation.Add;
            int val = (op == Operation.Mul) ? Random.Range(2, 4) : Random.Range(5, 20);
            gate.SetGate(op, val);
        }
        else
        {
            Operation op = (Random.value > 0.7f) ? Operation.Div : Operation.Sub;
            int val = (op == Operation.Div) ? Random.Range(2, 4) : Random.Range(5, 20);
            gate.SetGate(op, val);
        }
    }
}