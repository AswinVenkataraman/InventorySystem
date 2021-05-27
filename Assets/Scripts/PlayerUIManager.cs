using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class PlayerUIManager : MonoBehaviour
{
    [Inject]
    public BarProgress healthBar;
    [Inject]
    public BarProgress attackBar;
    [Inject]
    public BarProgress defenceBar;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenceText;

    private void Awake()
    {
        Debug.Log("healthBar = " + healthBar.currBarValue);

        healthText.text = "Health:" + healthBar.currBarValue + "%";
        attackText.text = "Attack:" + attackBar.currBarValue + "%";
        defenceText.text = "Defence:" + defenceBar.currBarValue + "%";
    }

    private void Update()
    {
        healthText.text = "Health:" + healthBar.currBarValue + "%";
        attackText.text = "Attack:" + attackBar.currBarValue + "%";
        defenceText.text = "Defence:" + defenceBar.currBarValue + "%";
    }
}
