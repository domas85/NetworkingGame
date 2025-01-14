using Unity.Netcode;
using UnityEngine;

public class MovingPlatform : NetworkBehaviour
{
    public float speed = 1f;
    public Transform startPoint;
    public Transform targetPoint;
    bool toggle = false;
    float lerpValue = 0;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (!IsOwner)
        {
            enabled = false;
        }
    }

    void FixedUpdate()
    {
        MovePlatformServerRpc();
    }

    [Rpc(SendTo.Server)]
    public void MovePlatformServerRpc()
    {
        if (toggle)
        {
            lerpValue += Time.fixedDeltaTime * speed;

            if (lerpValue >= 1f)
            {
                lerpValue = 1f;
                toggle = false;
            }
        }
        else
        {
            lerpValue -= Time.fixedDeltaTime * speed;
            if (lerpValue <= 0f)
            {
                lerpValue = 0f;
                toggle = true;
            }
        }
        if (speed > 0)
        {
            transform.position = Vector3.Lerp(startPoint.position, targetPoint.position, lerpValue);
        }
    }
}
