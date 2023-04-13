using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScrolling : MonoBehaviour
{
    //[SerializeField] Vector2 offestPos;
    //[SerializeField] float move_speed;

    [SerializeField] float _verticalSize;
    void Update()
    {
        //transform.Translate(Vector2.down * move_speed * Time.deltaTime);

        if (transform.position.y < -_verticalSize)
        {
            RepositionBackground();
        }
    }

    void RepositionBackground()
    {
        Vector3 groundOffSet = new Vector3(0, _verticalSize * 2f, 0);
        transform.position += groundOffSet;
    }
}
