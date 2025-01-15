using TMPro;
using UnityEngine;

public class ChatMessage : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI textMSG;

    public void SetText(string text)
    {
        textMSG.text = text;
    }
}
