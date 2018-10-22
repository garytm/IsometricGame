using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{

    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void LookAtPlayer()
    {
        if (player.target != null)
        {
            /*Setting a Vec3 direction to be the normalized value between the target and players positions*/
            Vector3 direction = (player.transform.position - transform.position).normalized;
            /*Ensuring the player doesn't look up or down on the Y-Axis by setting it to 0*/
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
            /*Setting the players rotation to gradually look at the target over time*/
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * player.lookSpeed);
        }
    }

    void Update()
    {
        LookAtPlayer();
    }
}
