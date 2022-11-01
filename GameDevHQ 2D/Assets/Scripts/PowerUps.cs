using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField] //0 = Triple Shot 1 = Speed 2 = Shields
    private int _powerUpID;
    void Update()
    {
        calculateMovement();
        powerUpCleanUp();
    }
    void calculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch(_powerUpID)
                {
                    case 0:
                        player.TripleShotACtive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    default:
                        Debug.Log("Default Case for _powerUpID");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
    private void powerUpCleanUp()
    {
        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

}
