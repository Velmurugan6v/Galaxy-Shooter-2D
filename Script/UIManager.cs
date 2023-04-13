using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public int _score;
    [SerializeField]
    private Text score_text;

    //Health var
    [SerializeField]
    private Image[] _lives_icon;
    public Color liveColor;
    public Color unLiveColor;

    //Super Power Var
    [SerializeField] Image _shieldImage;
    [SerializeField] bool _shieldActive;
    [SerializeField] Image _speedImg;
    [SerializeField] bool _speedActive;
    public static UIManager instance;

    //Game Over
    [SerializeField] GameObject _gameOverPanel;

    //Animation
    [SerializeField] Animator anim;
    [SerializeField] Animator second_anim;
    [SerializeField] TextMeshProUGUI timer_text;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //_gameOverPanel.SetActive(false);
        score_text.text = "Score  : "  + _score.ToString();
        _shieldImage.enabled = false;
        _speedImg.enabled = false;
        //Game start timer
        StartCoroutine(GameStartTimer());
    }

    private void Update()
    {

        //Shield Power
        if (_shieldActive==true)
        {
            _shieldImage.transform.position = PlayerControll.instance.transform.position;
        }
        else
        {
            _shieldImage.transform.position = Vector2.zero;
        }

        //Speed Power
        if (_speedActive==true)
        {
            _speedImg.transform.position = PlayerControll.instance.speedImg.position;
        }
        else
        {
            _speedImg.transform.position = Vector2.zero;
        }
            
    }

    public void AddScore(int amount)
    {
        _score = _score + amount;
        score_text.text = "Score  : " + _score.ToString();
    }

    public void UpdateHealth(int current_health)
    {
        for (int i = 0; i < _lives_icon.Length; i++)
        {
            if (i <current_health)
            {
                _lives_icon[i].color = liveColor;
            }
            else
            {
                _lives_icon[i].color = unLiveColor;
            }
        }
    }

    public IEnumerator ShieldTimer()
    {
        _shieldActive = true;
        _shieldImage.enabled = true;
        int fullShieldAmount = (int)_shieldImage.fillAmount * 10;

        for (int i = 0; i < fullShieldAmount; i++)
        {
            yield return new WaitForSeconds(1);
            _shieldImage.fillAmount -= 0.1f;
        }

        ResetOfShield();
    }

    public IEnumerator SpeedTimer()
    {
        _speedActive = true;
        _speedImg.enabled = true;
        int fullSpeedAmount = (int)_speedImg.fillAmount * 5;

        for (int i = 0; i < fullSpeedAmount; i++)
        {
            yield return new WaitForSeconds(1);
            _speedImg.fillAmount -= 0.2f;

            /*if (_speedImg.fillAmount<=0.3)
            {
                break;
            }*/
        }

        ResetOfSpeed();
    }

    public void ResetOfShield()
    {
        _shieldImage.enabled = false;
        _shieldImage.fillAmount = 1f;
        _shieldActive = false;
    }

    public void ResetOfSpeed()
    {
        _speedImg.enabled = false;
        _speedImg.fillAmount = 1f;
        _speedActive = false;
    }

    public void GameOverPanel()
    {
        //PlayerControll.instance.gameObject.SetActive(false);
        //yield return new WaitForSeconds(0f);
        anim.Play("UI-menu_anim");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public IEnumerator GameStartTimer()
    {
        
        timer_text.text = 3.ToString();
        yield return new WaitForSeconds(1f);
        timer_text.text = 2.ToString();
        yield return new WaitForSeconds(1f);
        timer_text.text = 1.ToString();
        yield return new WaitForSeconds(1f);
        second_anim.Play("StartTimer_anim");
        EveryOneStart();

    }

    void EveryOneStart()
    {
        PlayerControll.instance.enabled = true;
        FindObjectOfType<SpownEnemy>().enabled = true;
    }
}
