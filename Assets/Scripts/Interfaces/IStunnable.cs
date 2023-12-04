using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStunnable 
{
    bool isStunnable { get; set; }
    public float timeStunned { get; set; }
    bool timeStopped { get; set; }
}
