using System.Collections;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    private bool _accelerating;
    private int _turnDirection;

    private Rigidbody2D _rigidbody;

    //prefabs
    public GameObject bulletPrefab;

    //shield

    public GameObject shield;
    private bool _activeShield = false;
    private bool isShieldReady = true;

    public Transform bulletSpawnPoint;

    //player statistics
    public float accelSpeed, turnSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(Shoot());
    }

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

        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && isShieldReady)
        {
            if (PlayerManager.playerManager.energy >= GameConfiguration.shieldEnergyCost)
            {
                PlayerManager.playerManager.UpdateEnergy(GameConfiguration.shieldEnergyCost);
                StartCoroutine(ActiveShield());
            }
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
            if (shootEnable && !_activeShield)
            {
                Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation).GetComponent<Bullet>();
                bullet.direction = transform.up;
                yield return new WaitForSeconds(GameConfiguration.machineGunCD);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private IEnumerator ActiveShield()
    {
        _activeShield = true;
        shield.SetActive(true);
        StartCoroutine(ShieldReady());
        yield return new WaitForSeconds(GameConfiguration.shieldLifeTime);
        _activeShield = false;
        shield.SetActive(false);
    }

    private IEnumerator ShieldReady()
    {
        isShieldReady = false;
        yield return new WaitForSeconds(GameConfiguration.shieldCD);
        isShieldReady = true;
    }

    public void Beaten()
    {
        PlayerManager.playerManager.PlayerBeaten();
        Destroy(gameObject);
    }
}