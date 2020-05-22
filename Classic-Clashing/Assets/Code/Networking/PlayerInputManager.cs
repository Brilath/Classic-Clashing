using UnityEngine;
using Photon.Pun;

public class PlayerInputManager : MonoBehaviour
{
    public void SetPlayerName(string playerName)
    {
        if (!string.IsNullOrEmpty(playerName))
        {
            PhotonNetwork.NickName = playerName;
        }
        else
        {
            Debug.Log("Player Name is empty");
        }
    }
}
