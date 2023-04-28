using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class PlayerMovement : NetworkBehaviour
{

    public bool isMoving;

    private Animator anim;

    [SyncVar]
    public float playerSpeed = 2f;

    [SerializeField]
    private float characterSize = 0.5f;

    [SerializeField]
    private float cameraSize = 2.5f;

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

    [SyncVar(hook = nameof(HandlePlayerName))]
    public string playerName;
    [SerializeField]
    private TMP_Text PlayerNameText;
    public void HandlePlayerName(string old, string newName)
    {
        PlayerNameText.text = newName;
        
    }

    public virtual void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();
        sprRend.material.SetColor("_PlayerColor", PlayerColor.GetColor(playerColor));
        anim = GetComponent<Animator>();
        if (isOwned)
        {
            Camera cam = Camera.main;
            cam.transform.SetParent(transform);
            cam.transform.localPosition = new Vector3(0f, 0f, -10f);
            cam.orthographicSize = cameraSize;
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
            if (dir.x < 0f) transform.localScale = new Vector3(-characterSize, characterSize, characterSize);
            else if (dir.x > 0f) transform.localScale = new Vector3(characterSize, characterSize, characterSize);
            transform.position += dir * playerSpeed * Time.deltaTime;
            isMove = dir.magnitude != 0f;
        }
        anim.SetBool("isMove", isMove);

        if (transform.localScale.x < 0)
        {
            PlayerNameText.transform.localScale = new Vector3(-1, 1, 1);

        }
        else if(transform.localScale.x > 0)
        {
            PlayerNameText.transform.localScale = new Vector3(1, 1, 1);

        }
    }

    
}