using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnumerators : MonoBehaviour {
    PlayerManager player;

    private void Start()
    {
        player = GetComponent<PlayerManager>();
        if (player == null)
        {
            Debug.Log("No Anim component found.");
        }
    }
    //ritardo salto da fermo
    public IEnumerator DelayIdleJump()
    {
        yield return new WaitForSeconds(1.2F);
        player.InAir = false;
        player.speed = 15.0F;
    }
    //ritardo salto in corsa
     public IEnumerator DelayRunJump()
    {
        yield return new WaitForSeconds(0.15F);
        player.Jump();
    }
    public IEnumerator Wait()
    {
        if (Wait() == null)
        {
            Debug.Log("Wait is null");
        }
        yield return new WaitForSeconds(1F);
        player.speed = 15.0F;

    }
}
