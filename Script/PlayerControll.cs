using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public bool key_controll;
    public bool mouse_controll;

    public static PlayerControll instance;

    [SerializeField]
    private float normal_speed;
    private float current_speed;
    [SerializeField]
    private float high_speed;
    private float xInput;
    private float yInput;

    //Border
    private float minX = -9f;
    private float maxX = 9f;
    private float minY = -4.1f;
    private float maxY = 1.5f;

    //Laser
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private Transform centre_firePoint;
    [SerializeField]
    private Transform left_firePoint;
    [SerializeField]
    private Transform right_firePoint;

    public float maxLaserCount;
    public float currentLaserCount;

    //Health
    public int lives = 3;

    //VFX
    public ParticleSystem shot_vfx;

    //Shoot cooler
    public float timeBTWshot;
    public float shotCountner;

    //Powers
    [SerializeField]
    private GameObject _shield_power;
    public Transform speedImg;

    //Auto
    [SerializeField] bool _autoShoot;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }

        centre_firePoint = GameObject.Find("centre").transform;
        left_firePoint = GameObject.Find("left").transform;
        right_firePoint = GameObject.Find("right").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        current_speed = normal_speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (key_controll==true)
        {
            if (Input.GetMouseButtonDown(0) && shotCountner<=0)
            {
                Shoot();
                shotCountner = timeBTWshot;
            }

            if (shotCountner>0)
            {
                shotCountner -= Time.deltaTime;
            }

            Movement();
        }

        if (_autoShoot==true)
        {
            Shoot();
        }

        
        if (mouse_controll==true)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = transform.position.z;
            transform.position = Vector2.MoveTowards(transform.position, mousePos, normal_speed * Time.deltaTime); 
        }

    }

    void Movement()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(xInput, yInput);
        transform.Translate(moveDirection * current_speed * Time.deltaTime);

        CheckBorder();
    }

    void CheckBorder()
    {
        if (transform.position.x>maxX)
        {
            transform.position = new Vector2(minX, transform.position.y);
        }
        else if (transform.position.x<minX)
        {
            transform.position = new Vector2(maxX, transform.position.y);
        }
        else if (transform.position.y>maxY)
        {
            transform.position = new Vector2(transform.position.x, maxY);
        }
        else if (transform.position.y < minY)
        {
            transform.position = new Vector2(transform.position.x, minY);
        }
    }

    void Shoot()
    {
        //Instantiate(_laser, centre_firePoint.position, Quaternion.identity);
        switch (currentLaserCount)
        {
            case 1:
                Instantiate(_laser, centre_firePoint.position, Quaternion.identity);

                Instantiate(shot_vfx, centre_firePoint.position, Quaternion.identity,transform);

                AudioController.instance.PlaySFX(0);
                break;

            case 2:
                Instantiate(_laser, right_firePoint.position, Quaternion.identity);
                Instantiate(_laser, left_firePoint.position, Quaternion.identity);

                Instantiate(shot_vfx, right_firePoint.position, Quaternion.identity,transform);
                Instantiate(shot_vfx, left_firePoint.position, Quaternion.identity, transform);

                AudioController.instance.PlaySFX(0);
                break;

            case 3:
                Instantiate(_laser, centre_firePoint.position, Quaternion.identity);
                Instantiate(_laser, right_firePoint.position, Quaternion.identity);
                Instantiate(_laser, left_firePoint.position, Quaternion.identity);

                Instantiate(shot_vfx, centre_firePoint.position, Quaternion.identity, transform);
                Instantiate(shot_vfx, right_firePoint.position, Quaternion.identity, transform);
                Instantiate(shot_vfx, left_firePoint.position, Quaternion.identity, transform);

                AudioController.instance.PlaySFX(0);
                break;

            default:
                break;
        }
    }
    
    public void Damage(int amount)
    {
        lives -= amount;
        UIManager.instance.UpdateHealth(lives);

        if (lives<1)
        {
            FindObjectOfType<SpownEnemy>().StopSpownEnemy();
            UIManager.instance.GameOverPanel();
            AudioController.instance.PlaySFX(1);
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }

    #region Powers and Damage

    IEnumerator IncreaseSpeed()
    {
        current_speed = high_speed;
        yield return new WaitForSeconds(5f);
        current_speed = normal_speed;
    }

    IEnumerator ShieldPower()
    {
        _shield_power.SetActive(true);
        yield return new WaitForSeconds(10f);
        _shield_power.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Shield")
        {
            StartCoroutine(ShieldPower());
            StartCoroutine(UIManager.instance.ShieldTimer());
            AudioController.instance.PlaySFX(2);
            Destroy(collision.gameObject);
        }

        if (collision.tag=="Speed")
        {
            StartCoroutine(IncreaseSpeed());
            StartCoroutine(UIManager.instance.SpeedTimer());
            AudioController.instance.PlaySFX(2);
            Destroy(collision.gameObject);
        }

        if (collision.tag=="Bonus")
        {
            AudioController.instance.PlaySFX(2);
            Destroy(collision.gameObject);
            if (currentLaserCount<maxLaserCount)
            {
                currentLaserCount++;
            }
        }
    }


    public void DisAbleshield()
    {
        _shield_power.SetActive(false);
    }

    #endregion
}
