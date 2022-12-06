using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _tripleShotPowerUp;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject _powerUpContainer;
    private bool _stopSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(PowerUpSpawn());
    }

    IEnumerator PowerUpSpawn()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, 3);
            GameObject newTripleShotPowerUp = Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            newTripleShotPowerUp.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(1, 2));
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
            
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    //Kako vo ovaa situacija da gi stavam laserite da bidat pod-folder na SpawnManager > Weapon Container
    //Probav so transform.parent direkno na laserot ama ne mozam da zakacam GameObject - Weapon container
    //Zbesnav od probuvanje razlicni scenaria :D 

}
