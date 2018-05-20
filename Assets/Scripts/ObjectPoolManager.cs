﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour {

    private Dictionary<PoolTyp, GameObject> _dictionary;

    public enum PoolTyp
    {
        NormalBullet,
        SniperBullet,
        ant,
        OrbitingAnt
    }

    //public GameObject machineGunBulletPool;

    public GameObject normalBulletPool;

    public GameObject sniperBulletPool;

    public GameObject antPool;

    public GameObject orbitingAntPool;

    private void Awake()
    {
        _dictionary = new Dictionary<PoolTyp, GameObject>();

        _dictionary.Add(PoolTyp.ant, antPool);
        _dictionary.Add(PoolTyp.NormalBullet, normalBulletPool);
        _dictionary.Add(PoolTyp.SniperBullet, sniperBulletPool);
        _dictionary.Add(PoolTyp.OrbitingAnt, orbitingAntPool);

    }

    public GameObject GetObjFromPool(PoolTyp typ)
    {
        return _dictionary[typ];
    }


}
