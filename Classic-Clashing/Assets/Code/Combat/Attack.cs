using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Attack : MonoBehaviour
{
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private float attackDamage = 25f;
    [SerializeField] private float attackRange = 100f;
    [SerializeField] private float attackRate = 0.1f;
    private float attackTimer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer < attackRate)
        {
            attackTimer += Time.deltaTime;
        }

        if (Input.GetButton("Fire1") && attackTimer > attackRate)
        {
            attackTimer = 0.0f;
            RaycastHit hit;
            Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (Physics.Raycast(ray, out hit, attackRange))
            {
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject.GetComponent<PhotonView>() != null &&
                    !hitObject.GetComponent<PhotonView>().IsMine)
                {
                    Debug.Log($"You attacked {hit.collider.gameObject.name}");

                    if (hitObject.CompareTag("Player"))
                    {
                        hitObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, attackDamage);
                    }
                }
            }
        }
    }
}
