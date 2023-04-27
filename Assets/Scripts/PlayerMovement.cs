using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    public bool isMoving;

    private Animator anim;

    [SyncVar]
    public float playerSpeed = 2f;

    private SpriteRenderer sprRend;

    [SyncVar(hook = nameof(SetPlayerColor_Hook))]
    public EPlayerColor playerColor;

    public void SetPlayerColor_Hook(EPlayerColor oldColor, EPlayerColor newColor)
    {
        if (sprRend == null)
        {
            sprRend = GetComponent<SpriteRenderer>();
        }
        sprRend.material.SetColor("_PlayerColor", PlayerColor.GetColor(newColor));
    }

    private void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();
        sprRend.material.SetColor("_PlayerColor", PlayerColor.GetColor(playerColor));
        anim = GetComponent<Animator>();
        if (isOwned)
        {
            Camera cam = Camera.main;
            cam.transform.SetParent(transform);
            cam.transform.localPosition = new Vector3(0f, 0f, -10f);
            cam.orthographicSize = 2.5f;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        bool isMove = false;
        if (isOwned && isMoving)
        {
            Vector3 dir = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f), 1f);
            if (dir.x < 0f) transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
            else if (dir.x > 0f) transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            transform.position += dir * playerSpeed * Time.deltaTime;
            isMove = dir.magnitude != 0f;
        }
        anim.SetBool("isMove", isMove);
    }
}