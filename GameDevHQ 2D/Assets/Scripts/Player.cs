using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    private bool _shieldActive = false;
    private bool _speedBoostActive = false;
    [SerializeField]
    private float _speedBoost = 2f;
    [SerializeField]
    private GameObject shieldVisualizer;
    [SerializeField]
    private int _score;



    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn manager is NULL");
        }
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);   
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.6f, 0), 0);
        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(-0.1861651f, 1.05f, 0), Quaternion.identity);
        }
        else 
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
    }

    public void Damage ()
    {
        if (_shieldActive == true)
        {
            _shieldActive = false;
            shieldVisualizer.SetActive(false);
            return;
        }
        
        _lives--;
        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotACtive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotCooldown());
    }

    IEnumerator TripleShotCooldown()
    {
        while (_isTripleShotActive == true)
        {
            yield return new WaitForSeconds(2.0f);
            _isTripleShotActive = false;
        }
    }

    public void ShieldActive()
    {
        _shieldActive = true;
        shieldVisualizer.SetActive(true);
    } 

    public void SpeedBoostActive()
    {
        _speedBoostActive = true;
        _speed *= _speedBoost;
        StartCoroutine(SpeedBoostCooldown());

    }
    IEnumerator SpeedBoostCooldown()
    {
        while (_speedBoostActive == true)
        {
            yield return new WaitForSeconds(2.0f);
            _speedBoostActive = false;
            _speed /= _speedBoost;
        }

    }


    public void AddScore()
    {
        _score += 10;
    }


    //method to add 10 to score 
    //Communicate to UI to update the score
}
