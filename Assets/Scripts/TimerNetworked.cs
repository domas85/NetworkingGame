using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimerNetworked : NetworkBehaviour
{
    [HideInInspector] public NetworkVariable<float> TimeToDisplayNetworked = new NetworkVariable<float>();
    public NetworkVariable<bool> isRaceRunning = new NetworkVariable<bool>(false);
    public InputActionReference Interact;
    public GameObject Barrier;
    enum TimerType { CountDown, StopWatch }
    [SerializeField] private TimerType timerType;

    private bool isRunning;

    private void OnEnable()
    {
        EventManager.TimerStart += EventManagerOnTimerStart;
        EventManager.TimerStop += EventManagerOnTimerStop;
        EventManager.TimerUpdate += EventManagerOnTimerUpdateServerRpc;
    }

    private void OnDisable()
    {
        EventManager.TimerStart -= EventManagerOnTimerStart;
        EventManager.TimerStop -= EventManagerOnTimerStop;
        EventManager.TimerUpdate -= EventManagerOnTimerUpdateServerRpc;
    }

    private void EventManagerOnTimerStart() => isRunning = true;

    private void EventManagerOnTimerStop() => isRunning = false;

    [Rpc(SendTo.Server)]
    private void EventManagerOnTimerUpdateServerRpc(float value) => TimeToDisplayNetworked.Value += value;

    private void Awake()
    {
        isRaceRunning.Value = false;
    }

    private void Update()
    {
        if (isRaceRunning.Value == true)  // not good but works, so thats good enought for me
        {
            var test = Barrier.GetComponent<MeshRenderer>();
            var ccol = Barrier.GetComponent<BoxCollider>();
            test.enabled = false;
            ccol.enabled = false;
        }
        if (!isRunning) return;


        CalculateTimerServerRpc();
    }

    private void OnTriggerStay(Collider other)
    {
        NetworkObject networkObject = other.GetComponent<NetworkObject>();
        if (Interact.action.IsPressed())
        {
            if (IsClient && networkObject != null && networkObject.IsOwner && isRaceRunning.Value == false)
            {
                EventManager.OnTimerStart();
                StartRaceServerRpc();
            }
        }
    }

    [Rpc(SendTo.Server)]
    public void StartRaceServerRpc()
    {
        isRaceRunning.Value = true;
    }

    [Rpc(SendTo.Server)]
    public void CalculateTimerServerRpc()
    {
        if (timerType == TimerType.CountDown && TimeToDisplayNetworked.Value < 0.0f) return;
        TimeToDisplayNetworked.Value += timerType == TimerType.CountDown ? -Time.deltaTime : Time.deltaTime;
    }
}
