using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private Image healthBar;
    private PhotonView _photonView;
    private List<ResourceLogger> logger;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
        _photonView = PhotonView.Get(this);
        logger = new List<ResourceLogger>();
    }

    [PunRPC]
    public void TakeDamage(float damage, PhotonMessageInfo info)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        healthBar.fillAmount = currentHealth / maxHealth;

        Debug.Log($"{_photonView.Owner} took {damage} from {info.Sender} damage and currently has {currentHealth} health left.");
        logger.Add(new ResourceLogger(info.Sender.ToString(), -damage));

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    [PunRPC]
    public void TakeHealing(float heal, PhotonMessageInfo info)
    {
        currentHealth = Mathf.Min(currentHealth + heal, maxHealth);
        healthBar.fillAmount = currentHealth / maxHealth;

        Debug.Log($"{_photonView.Owner} took {heal} from {info.Sender} healing and currently has {currentHealth} health.");
        logger.Add(new ResourceLogger(info.Sender.ToString(), heal));

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (_photonView.IsMine)
        {
            Debug.Log($"{_photonView.Owner} has died");
            Debug.Log($"Combat log for {_photonView.Owner}");
            foreach (ResourceLogger log in logger)
            {
                Debug.Log($"{log.Source} : {log.Amount}");
            }

            ClassicClashingGameManager.instance.LeaveRoom();
        }
    }
}
