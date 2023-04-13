using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGmusicSingleTon : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("BGMusic");
        if (musicObj.Length>1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
