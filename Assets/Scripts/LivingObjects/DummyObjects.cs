using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyObjects : MonoBehaviour
{
    public bool air;
    public EnemyType enemyType;
    [ReadOnly] public int spot;

    public bool shouldNotRespawn;


    private void OnDestroy()
    {
        if (shouldNotRespawn) { return; }


        if (Respawn.Instance != null)
        {
            if (air)
            {
                //Respawn.Instance.respawnAir.Invoke();
                Respawn.Instance.air = true;
            }
            else
            {
                //Respawn.Instance.respawnGround.Invoke();
                Respawn.Instance.air = false;
            }
            Respawn.Instance.Spot(spot);
            Respawn.Instance.boo.Invoke(enemyType); 
        }
    }
 
}
