using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Movement : MonoBehaviourPun
{
    [SerializeField] float Movespeed = 200f;
    [SerializeField] float Duration = 1f;
    [SerializeField] float SprintCooldown = 5f;
    [SerializeField] float SprintFactor = 2f;

    Rigidbody2D rigid;
    GameObject SprintUI;
    float factor;
    float durationTimer = 0;
    float cooldownTimer;
    bool sprintOn = false;

    private void Start()
    {
        factor = 1;
        durationTimer = 0;
        cooldownTimer = SprintCooldown;
        sprintOn = false;

        SprintUI = GameObject.FindWithTag("Sprint_UI");
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
        if (sprintOn)
        {
            durationTimer += Time.deltaTime;
        }
        else
        {
            cooldownTimer += Time.deltaTime;
        }

        

        if (base.photonView.IsMine)
        {
            SprintUI.SetActive(cooldownTimer >= SprintCooldown);

            rigid.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Movespeed, Input.GetAxisRaw("Vertical") * Movespeed) * factor;

            if (cooldownTimer >= SprintCooldown && Input.GetKeyDown(KeyCode.Space))
            {
                cooldownTimer = 0;
                durationTimer = 0;
                factor = SprintFactor;
                sprintOn = true;
            }

            if (durationTimer >= Duration)
            {
                factor = 1;
                sprintOn = false;
            }
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }
        
    }
}
