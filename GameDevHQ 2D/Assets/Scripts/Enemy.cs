using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    private Player _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
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
            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")

        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }
                      
            //add 10 to score 
            Destroy(this.gameObject);
        }

    }

}


 