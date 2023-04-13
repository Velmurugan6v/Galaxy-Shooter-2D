using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float move_speed;
    
    void Update()
    {
        if (transform.position.y < -5.50f)
        {
            Destroy(this.gameObject);
        }

        transform.Translate(Vector2.down * move_speed * Time.deltaTime);
    }

    
}
