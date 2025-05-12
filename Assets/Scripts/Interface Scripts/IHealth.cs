using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth 
{
    float Health { get; }
    void AddHealth(float amount);

    void RemoveHealth(float amount);

    void ResetHealth();

}
