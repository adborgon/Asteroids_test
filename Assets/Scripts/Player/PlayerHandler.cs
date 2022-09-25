using System.Collections;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    private bool _accelerating;
    private int _turnDirection;

    private Rigidbody2D _rigidbody;

    //prefabs
    public GameObject bulletPrefab;

    public Transform bulletSpawnPoint;

    //player statistics
    public float accelSpeed, turnSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    private void Update()
    {
        _accelerating = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _turnDirection = 1;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _turnDirection = -1;
        }
        else
        {
            _turnDirection = 0;
        }

        if (Input.GetKey(KeyCode.Space))

            shootEnable = true;
        else
            shootEnable = false;
    }

    private void FixedUpdate()
    {
        if (_accelerating)
            _rigidbody.AddForce(transform.up * accelSpeed);

        if (_turnDirection != 0)
            _rigidbody.AddTorque(_turnDirection * turnSpeed);
    }

    private bool shootEnable;

    private IEnumerator Shoot()
    {
        while (true)
        {
            if (shootEnable)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
                bullet.GetComponent<Bullet>().direction = transform.up;
                yield return new WaitForSeconds(GameConfiguration.bulletCD);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void Beaten()
    {
        PlayerManager.player.PlayerBeaten();
        Destroy(gameObject);
    }
}