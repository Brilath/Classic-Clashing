using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private TextMeshProUGUI playerNameText;

    private void Start()
    {
        if (photonView.IsMine)
        {
            transform.GetComponent<CharacterMovement>().enabled = true;
            transform.GetComponent<AimController>().enabled = true;
            transform.GetComponent<CameraController>().enabled = true;
            playerCamera.GetComponent<Camera>().enabled = true;
            playerCamera.GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            transform.GetComponent<CharacterMovement>().enabled = false;
            transform.GetComponent<AimController>().enabled = false;
            transform.GetComponent<CameraController>().enabled = false;
            playerCamera.GetComponent<Camera>().enabled = false;
            playerCamera.GetComponent<AudioListener>().enabled = false;
        }

        SetupUI();
    }

    private void SetupUI()
    {
        playerNameText.text = photonView.Owner.NickName;
    }
}
