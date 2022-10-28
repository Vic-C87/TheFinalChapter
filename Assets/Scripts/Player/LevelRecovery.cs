using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRecovery : MonoBehaviour
{
    Player myPlayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void GetPlayerComponents()
    {
        myPlayer.GetComponent<Player>();

        myPlayer.GetPlayerCollider();
        myPlayer.GetCurrentHp();
        myPlayer.GetSpeed();
        myPlayer.GetDamage();
        myPlayer.GetMeleeDistance();
        myPlayer.GetProjectileDamage();
        myPlayer.GetKnifeSprite();
        myPlayer.GetBowSprite();
        myPlayer.GetProjectile();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
