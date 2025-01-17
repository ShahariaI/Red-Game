using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager.PlaySFX(audioManager.fire);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
