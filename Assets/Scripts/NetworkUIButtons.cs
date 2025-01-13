using Unity.Netcode;
using UnityEngine;

public class NetworkUIButtons : MonoBehaviour
{
    public void HostServer()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void JoinHostAsClient()
    {
        NetworkManager.Singleton.StartClient();
    }

    public void StartAServer()
    {
        NetworkManager.Singleton.StartServer();
    }
}
