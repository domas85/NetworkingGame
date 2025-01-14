using Unity.Netcode;
using UnityEngine;

public class NetworkUIButtons : MonoBehaviour
{
    public void HostServer()
    {
        NetworkManager.Singleton.StartHost();
        transform.parent.gameObject.SetActive(false);
    }

    public void JoinHostAsClient()
    {
        NetworkManager.Singleton.StartClient();
        transform.parent.gameObject.SetActive(false);
    }

    public void StartAServer()
    {
        NetworkManager.Singleton.StartServer();
        transform.parent.gameObject.SetActive(false);
    }
}
