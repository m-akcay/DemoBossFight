using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitbox : DamageSource
{
    private Rigidbody _rb;
    private ConstantForce _gravity;
    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody>();
        _gravity = GetComponent<ConstantForce>();
    }
    public override void Init(in Transform inTransform, in AmmoSO ammoSO)
    {
        base.Init(inTransform, ammoSO);
        var bullet = ammoSO as BulletSO;

        var defaultPos = inTransform.position;
        var defaultFwd = inTransform.forward;
        var fwd = Quaternion.AngleAxis(bullet.FiringAngle, inTransform.right) * defaultFwd;

        var pos = defaultPos + fwd * bullet.Size;
        _transform.position = pos;
        _transform.LookAt(pos + fwd);

        _rb.isKinematic = false;
        _gravity.enabled = true;
        _rb.AddForce(fwd * bullet.Speed, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        _transform.LookAt(_transform.position + _rb.velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var collidedObj = collision.gameObject;
        if (collision.gameObject.layer == MOB_LAYER)
        {
            collidedObj.GetComponent<Damageable>().ApplyDamage(this);
        }

        ReturnToPool();
    }

    private void ReturnToPool()
    {
        _gravity.enabled = false;
        _rb.isKinematic = true;
        BulletHitboxPool.Instance.Return(this);
    }

    protected override void OnTriggerEnter(Collider other)
    {
    }
}
