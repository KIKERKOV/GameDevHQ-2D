using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f;
    private Player _player;
    private Animator _deathAnimation;
    private AnimationClip _onEnemyDeath;
    private float _deathAnimationTime;

    // handle to animator compontent

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("The Player is NULL");
        }

        _onEnemyDeath = GetComponent<AnimationClip>();

        if (_onEnemyDeath == null)
        {
            Debug.LogError("Animation is NULL");
        }


        _deathAnimation = GetComponent<Animator>();


        if (_deathAnimation == null)
        {
            Debug.LogError("Animator is NULL");
        }


        // assign compontent to Anim
    }

    void Update()
    {
        CalculateMovement();
    }

    public void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-11.3f, 11.3f);
            transform.position = new Vector3(randomX, 8, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")

        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            _deathAnimation.SetTrigger("OnEnemyDeath");

            // trigger anim
            Destroy(this.gameObject, _deathAnimationTime);
        }

        if (other.tag == "Laser")

        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }

            _deathAnimation.SetTrigger("OnEnemyDeath");
            // trigger anim
            Destroy(this.gameObject, _deathAnimationTime);
        }

    }

}


 