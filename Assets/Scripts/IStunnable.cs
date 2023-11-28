using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStunnable 
{
    bool isStunnable { get; set; }   
    float timeStunned { get; set; }
    bool timeStop { get; set; }
}
