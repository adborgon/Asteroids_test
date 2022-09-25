using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, GameConfiguration.bulletLifeTime);
        //we use Coroutine instead Invoke because if the object is destroyed, the Coroutine is destroyed too.
        //StartCoroutine(DestroyBullet());
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(direction * GameConfiguration.bulletAccel);
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(GameConfiguration.bulletLifeTime);
        Destroy(gameObject);
    }
}