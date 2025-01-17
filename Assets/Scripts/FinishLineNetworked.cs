using TMPro;
using Unity.Netcode;
using UnityEngine;

public class FinishLineNetworked : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI winnerText;
    NetworkVariable<bool> raceIsRunning = new NetworkVariable<bool>(true);

    private void OnTriggerEnter(Collider other)
    {
        NetworkObject networkObject = other.GetComponent<NetworkObject>();

        if (IsClient && networkObject != null && networkObject.IsOwner && raceIsRunning.Value)
        {
            EventManager.OnTimerStop();
            SetWinnerServerRpc(networkObject.OwnerClientId, false);
        }
    }

    [Rpc(SendTo.Everyone)]
    public void SetWinnerServerRpc(ulong playerId, bool isRunning)
    {
        if (IsServer)
        {
            raceIsRunning.Value = isRunning;
        }
        winnerText.text = "Player" + playerId + " is the Winner";
    }

}
