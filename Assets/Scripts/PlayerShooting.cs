using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerShooting : NetworkBehaviour
{
    public Animator animator;
    public TextMesh healthBar;
    public Transform aimPoint;

    public float rotationSpeed = 100;

    public KeyCode shootKey = KeyCode.Mouse0;
    public GameObject bulletPrefab;
    public Transform bulletMount;

    [SyncVar] 
    public int health = 5;

    void Update()
    {
        healthBar.text = new string('-', health);

        // take input from focused window only
        if (!Application.isFocused) return;

        if (isLocalPlayer)
        {

            if (Input.GetKeyDown(shootKey))
            {
                CmdFire();
            }

            Aiming();
        }
    }

    // this is called on the server
    [Command]
    void CmdFire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletMount.position, bulletMount.rotation);
        NetworkServer.Spawn(bullet);
        RpcOnFire();
    }

    
    [ClientRpc]
    void RpcOnFire()
    {
        animator.SetTrigger("Shoot");
    }

    [ServerCallback]
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>() != null)
        {
            Debug.Log("Hit");
            --health;
            if (health == 0)
                NetworkServer.Destroy(gameObject);
        }
    }

    void Aiming()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            Debug.DrawLine(ray.origin, hit.point);
            Vector3 lookRotation = new Vector3(hit.point.x, aimPoint.transform.position.y, hit.point.z);
            aimPoint.transform.LookAt(lookRotation);
        }
    }
}
