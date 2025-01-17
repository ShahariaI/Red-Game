using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Staminabar : MonoBehaviour
{
    [SerializeField]private Slider staminaBar;
    [SerializeField] private PlayerMovement playerMovement;
    void Start()
    {
        staminaBar = GameObject.Find("Stamina bar").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.currentStamina = staminaBar.value;
    }
}
