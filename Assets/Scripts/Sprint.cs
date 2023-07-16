using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Sprint : MonoBehaviourPun
{
    [SerializeField] float cooldown = 2f;
    [SerializeField] float SprintForce = 5f;

    GameObject SprintUI;
    
    Rigidbody2D rigid;
    float timer = 0f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        SprintUI = GameObject.FindWithTag("Sprint_UI");
    }
    void Update()
    {
        timer += Time.deltaTime;

        SprintUI.SetActive(timer >= cooldown);

        if (base.photonView.IsMine && timer >= cooldown && Input.GetKeyDown(KeyCode.Space))
        {
            timer = 0f;
            rigid.AddForce(transform.up * SprintForce, ForceMode2D.Impulse);
        }
    }
}
