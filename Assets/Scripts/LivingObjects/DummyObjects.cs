using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyObjects : MonoBehaviour
{
    public bool air;
    public EnemyType enemyType;
    [ReadOnly] public int spot;
    private void OnDestroy()
    {
        if (Respawn.Instance != null)
        {
            if (air)
            {
                Respawn.Instance.respawnAir.Invoke();
            }
            else
                Respawn.Instance.respawnGround.Invoke();
            StartCoroutine(SpotNumber());
            Respawn.Instance.boo.Invoke(enemyType); 
        }
    }
    private IEnumerator SpotNumber()
    {
        yield return new WaitForSeconds(0.4f);
        Respawn.Instance.spotNumber = spot;
    }
}
