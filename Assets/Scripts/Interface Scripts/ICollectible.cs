using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface ICollectible
{
    float Delay { get; }
    float Time { get; }
    float Damage { get; }

    ShotType BulletType { get; }


}
