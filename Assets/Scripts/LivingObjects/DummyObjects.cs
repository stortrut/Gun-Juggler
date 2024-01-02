using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyObjects : MonoBehaviour
{
    public bool air;
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
        }
    }
}