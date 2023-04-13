using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float move_speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y>10)
        {
            Destroy(this.gameObject);
        }

        transform.Translate(Vector2.up * move_speed * Time.deltaTime);
    }
}
