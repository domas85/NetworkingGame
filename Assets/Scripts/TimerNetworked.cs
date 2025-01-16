using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimerNetworked : NetworkBehaviour
{
    public NetworkVariable<float> Time = new NetworkVariable<float>();
    public InputActionReference Interact;

    enum TimerType { CountDown, StopWatch}
    [SerializeField] private TimerType timerType;

    private bool isRunning;

    private void Awake()
    {
        //Interact.action.started +=
    }

    private void OnEnable()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        NetworkObject networkObject = other.GetComponent<NetworkObject>();
        if (IsClient && networkObject != null && networkObject.IsOwner)
        {
            
        }
    }

    public void tmp()
    {
        
    }


}
