using UnityEngine;
using TMPro;

public enum Operation { Add, Sub, Mul, Div }

public class Gate : MonoBehaviour
{
    public Operation op;
    public int value;
    public TextMeshProUGUI gateText;
    public GameObject partnerGate;

    public void SetGate(Operation oper, int val)
    {
        op = oper;
        value = val;
        UpdateText();
    }

    void UpdateText()
    {
        string opSymbol = "";
        switch (op)
        {
            case Operation.Add: opSymbol = "+"; break;
            case Operation.Sub: opSymbol = "-"; break;
            case Operation.Mul: opSymbol = "¡¿"; break;
            case Operation.Div: opSymbol = "¡À"; break;
        }
        gateText.text = opSymbol + value;
    }
}