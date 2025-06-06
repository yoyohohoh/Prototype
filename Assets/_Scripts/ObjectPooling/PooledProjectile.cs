// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PooledProjectile : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _maxLifeTime = 3f;
    public float _damage;
    [SerializeField] private float _speed = 10f;
    public Transform _target;

    private void OnEnable()
    {
        _lifeTime = 0;
    }

    private void Update()
    {
        _lifeTime += Time.deltaTime;
        if (_lifeTime > _maxLifeTime)
        {
            ProjectilePoolManager.Instance.ReturnToPool(this);
        }

        if (_target != null)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            transform.position += direction * _speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, _target.position) < 0.1f)
            {
                ProjectilePoolManager.Instance.ReturnToPool(this);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            other.gameObject.GetComponent<NPC>()._npcHp -= _damage;
            ProjectilePoolManager.Instance.ReturnToPool(this);
        }
    }
}