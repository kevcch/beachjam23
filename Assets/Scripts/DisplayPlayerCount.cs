using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPlayerCount : MonoBehaviour
{
    [SerializeField] TMP_Text PlayerText;

    private void Update()
    {
        int playerCount = GameObject.FindGameObjectsWithTag("Player").Length;

        if(playerCount == 1)
        {
            if(Camera.main.gameObject.GetComponent<FollowPlayer>().PlayerObject != null)
            {
                PlayerText.text = "YOU WON!";
                PlayerText.color = Color.green;
            }
            else
            {
                PlayerText.text = "GAME OVER";
                PlayerText.color = Color.red;
            }
        }
        else if(playerCount <= 3)
        {
            PlayerText.text = playerCount.ToString() + " Players Remaining!";
            PlayerText.color = Color.red;
        }
        else
        {
            PlayerText.text = playerCount.ToString() + " Players Remaining!";
            PlayerText.color = Color.white;
        }
    }
}
