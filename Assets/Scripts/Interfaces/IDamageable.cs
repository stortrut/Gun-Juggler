using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
   bool hasProtection { get; set; }   
    void ApplyDamage(int amount);
    
    
}
