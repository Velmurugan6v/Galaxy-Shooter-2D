using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpownEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    private bool stopEnemySpowning;

    [SerializeField]
    private GameObject[] _powers;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpowningEnemies());
    }

    IEnumerator SpowningEnemies()
    {
        while (stopEnemySpowning==false)
        {
            int randomNo = Random.Range(0, 10);

            if (randomNo==5)
            {
                int randomPower = Random.Range(0, _powers.Length);
                Vector2 randomPos = new Vector2(Random.Range(-8, 8), transform.position.y);
                Instantiate(_powers[randomPower], randomPos, Quaternion.identity);
            }
            else
            {
                Vector2 randomPos = new Vector2(Random.Range(-8, 8), transform.position.y);
                Instantiate(_enemy, randomPos, Quaternion.identity);
            }
            

            yield return new WaitForSeconds(2f);
        }
    }

    public void StopSpownEnemy()
    {
        stopEnemySpowning = true;
    }
}
