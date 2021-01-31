using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music background_music;


    // Start is called before the first frame update
    void Start()
    {
        if (background_music != null)
        {
            Destroy(gameObject);
            return;
        }
        background_music = this;

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
