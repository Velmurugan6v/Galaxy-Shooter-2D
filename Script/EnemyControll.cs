using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    [SerializeField]
    private float move_speed;
    [SerializeField]
    private GameObject explose_vfx;
   
    
    void Update()
    {
        if (transform.position.y<-7)
        {
            //Destroy(this.gameObject);
            transform.position = new Vector3(transform.position.x, 6);
        }

        transform.Translate(Vector2.down * move_speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Laser")
        {
            UIManager.instance.AddScore(10);
            Destroy(collision.gameObject);
            DestroyOn();
        }

        if (collision.tag=="Player")
        {
            PlayerControll.instance.Damage(1);
            DestroyOn();
        }

        if (collision.tag=="Enemy")
        {
            Destroy(collision.gameObject);
        }

        if (collision.tag=="ShieldPower")
        {
            UIManager.instance.ResetOfShield();
            PlayerControll.instance.DisAbleshield();
            DestroyOn();
        }
    }

    void DestroyOn()
    {
        AudioController.instance.PlaySFX(1);
        Instantiate(explose_vfx, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
