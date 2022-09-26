using System.Collections;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    private bool _accelerating;
    private int _turnDirection;

    private Rigidbody2D _rigidbody;

    //prefabs
    [SerializeField] private GameObject bulletPrefab;

    public Transform bulletSpawnPoint;

    //Shield

    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject explosion;

    private bool _activeShield = false;
    private bool _isShieldReady = true;

    private bool _died = false;
    private bool _shootEnable;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(Shoot());
        StartCoroutine(Invulnerable());
    }

    private void Update()
    {
        if (_died) return;
        _accelerating = isPressUp();

        if (isPressLeft())
        {
            _turnDirection = 1;
        }
        else if (isPressRight())
        {
            _turnDirection = -1;
        }
        else
        {
            _turnDirection = 0;
        }

        if (isPressDown() && _isShieldReady)
        {
            if (PlayerManager.playerManager.player.energy >= GameConfiguration.shieldEnergyCost)
            {
                PlayerManager.playerManager.UpdateEnergy(GameConfiguration.shieldEnergyCost);
                StartCoroutine(ActiveShield());
            }
        }

        if (isShooting())
            _shootEnable = true;
        else
            _shootEnable = false;
    }

    private void FixedUpdate()
    {
        if (_accelerating)
            _rigidbody.AddForce(transform.up * GameConfiguration.playerAccelSpeed);

        if (_turnDirection != 0)
            _rigidbody.AddTorque(_turnDirection * GameConfiguration.playerTurnSpeed);
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            if (_shootEnable && !_activeShield && !_died)
            {
                GetComponent<AudioSource>().Play();
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

    private IEnumerator Invulnerable()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Animator>().SetBool("Invulnerable", true);
        yield return new WaitForSeconds(GameConfiguration.respawnInvulnerability);
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Animator>().SetBool("Invulnerable", false);
    }

    private IEnumerator ActiveShield()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        _activeShield = true;
        shield.SetActive(true);
        StartCoroutine(ShieldReady());
        yield return new WaitForSeconds(GameConfiguration.shieldLifeTime);
        _activeShield = false;
        shield.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private IEnumerator ShieldReady()
    {
        _isShieldReady = false;
        yield return new WaitForSeconds(GameConfiguration.shieldCD);
        _isShieldReady = true;
    }

    public void Beaten()
    {
        _died = true;
        PlayerManager.playerManager.PlayerBeaten();
        explosion.SetActive(true);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, 1);
    }

    #region KeyHandler

    private static bool isShooting()
    {
        return Input.GetKey(KeyCode.Space);
    }

    private static bool isPressDown()
    {
        return (Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.DownArrow));
    }

    private static bool isPressRight()
    {
        return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
    }

    private static bool isPressLeft()
    {
        return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
    }

    private static bool isPressUp()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
    }

    #endregion KeyHandler
}