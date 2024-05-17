using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHuntBase : MonoBehaviour
{
    [Header("Hunt Configuration")]
    public GameObject target;
    public float huntRange = 1.0f;
    public float movementSpeed = 2f;
    protected Rigidbody2D rigidBody;
    protected Enemy enemy;
    protected float distanceBetweenTarget = 999999;

    public void SetTarget(GameObject _target) {
        target = _target;
    }

    public GameObject GetTarget() {
        return target;
    }

    public float GetHuntRange() {
        return huntRange;
    }

    public float GetMovementSpeed() {
        return movementSpeed;
    }

    public float GetDistanceBetweenTarget() {
        return distanceBetweenTarget;
    }
}
