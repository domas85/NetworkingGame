using StarterAssets;
using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChatManagerNetwork : NetworkBehaviour
{
    public static ChatManagerNetwork instance;

    [SerializeField] InputActionReference OpenTheChat;

    [SerializeField] ChatMessage chatMessagePrefab;
    [SerializeField] GameObject TextChatHUD;
    [SerializeField] CanvasGroup chatContent;
    [SerializeField] TMP_InputField chatInput;
    bool toggle = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            OpenTheChat.action.started += OnChatOpen;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendChatMessage(chatInput.text);
            chatInput.text = "";
        }
    }

    public void OnChatOpen(InputAction.CallbackContext context)
    {
        if (toggle)
        {
            Cursor.lockState = CursorLockMode.Confined;
            TextChatHUD.SetActive(true);
            toggle = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            TextChatHUD.SetActive(false);
            toggle = true;
        }
    }

    public void SendChatMessage(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return;

        string str = " > " + text;
        SendChatMessageServerRpc(str);
    }
    void AddMessage(string text)
    {
        ChatMessage msg = Instantiate(chatMessagePrefab, chatContent.transform);
        msg.SetText(text);
    }

    [ServerRpc(RequireOwnership = false)]
    void SendChatMessageServerRpc(string message, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        var tempName = "Player" + clientId;

        ReceiveChatMessageClientRpc(tempName + message);
    }

    [Rpc(SendTo.ClientsAndHost)]
    void ReceiveChatMessageClientRpc(string message)
    {
        ChatManagerNetwork.instance.AddMessage(message);
    }

}
