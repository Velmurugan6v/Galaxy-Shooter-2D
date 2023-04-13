using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Animator menu_anim;
    [SerializeField] Animator _player_anim;
    // Start is called before the first frame update
   

    public void GameStartFun()
    {
        menu_anim.Play("menu-start");
        StartCoroutine(Startfun());
    }

    IEnumerator Startfun()
    {
        _player_anim.Play("Player_Go");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        print("Quit");
        Application.Quit();
    }
}
