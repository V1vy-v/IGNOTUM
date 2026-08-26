using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkDetect : MonoBehaviour
{

    private int playerLayer;

    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            PlayerObject player = collision.GetComponent<PlayerObject>();
            if (player != null)
            {
                player.Wound(100);//ФЭМо
            }
        }
    }
}
