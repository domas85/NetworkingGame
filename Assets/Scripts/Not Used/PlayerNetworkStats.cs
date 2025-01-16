using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetworkStats : NetworkBehaviour
{
    public NetworkVariable<int> XP = new NetworkVariable<int>(0);

    public NetworkVariable<int> Health = new NetworkVariable<int>(100);
    
    public NetworkVariable<int> Strength = new NetworkVariable<int>(1); 

    public NetworkVariable<int> Dexterity = new NetworkVariable<int>(1);
    
    public NetworkVariable<float> Charisma = new NetworkVariable<float>(0f);

    //public NetworkVariable<FixedString32Bytes> Username = new("Anonymous");

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if(Charisma.Value < 15)
        {
            Debug.Log("Charisma too low");
        }
     
        //Charisma.OnValueChanged
    }
}
