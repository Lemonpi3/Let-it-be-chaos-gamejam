using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGun : Weapon
{
    public override void SpawnBullet(Vector2 shootDir, Transform spawnPos)
    {
        base.SpawnBullet(shootDir, spawnPos);
    }
}
