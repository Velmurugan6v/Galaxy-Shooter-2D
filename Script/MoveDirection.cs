using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDirection : MonoBehaviour
{
    [SerializeField] float move_speed;
    void Update()
    {
        transform.Translate(Vector2.down * move_speed * Time.deltaTime);
    }
}
