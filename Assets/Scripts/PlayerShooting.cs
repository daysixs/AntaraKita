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
    public int health = 4;

    void Update()
    {
        healthBar.text = new string('-', health);

        // take input from focused window only
        if (!Application.isFocused) return;

        // movement for local player
        if (isLocalPlayer)
        {
            //// rotate
            //float horizontal = Input.GetAxis("Horizontal");
            //transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);

            //// move
            //float vertical = Input.GetAxis("Vertical");
            //Vector3 forward = transform.TransformDirection(Vector3.forward);
            //agent.velocity = forward * Mathf.Max(vertical, 0) * agent.speed;
            //animator.SetBool("Moving", agent.velocity != Vector3.zero);

            // shoot
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
