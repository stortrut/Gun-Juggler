using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStunnable 
{
    bool isStunnable { get; set; }
  
    bool timeStopped { get; set; }
    // public float timeStunned { get; set; }
}
