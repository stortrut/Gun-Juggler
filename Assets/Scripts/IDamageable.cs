using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{

    int Protection { get; set; }
    void ApplyDamage(int amount);
    void RemoveProtection(int protection);
}
