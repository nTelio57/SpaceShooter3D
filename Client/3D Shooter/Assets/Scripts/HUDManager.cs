using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public Transform player;
    public PlaneControl planeControl;
    public PlaneCombat planeCombat;
    [Header("Coordinates")]
    public TMP_Text x;
    public TMP_Text y;
    public TMP_Text z;
    [Header("Target")]
    public Target target;
    public Text targetText;
    public int infFontSize = 35;
    public int digitFontSize = 27;
    [Header("Health")]
    public Slider healthSlider;
    public Text healthText;
    [Header("Boost")]
    public Slider boostSlider;
    [Header("Weapons")]
    public Slider laserSlider;
    public Laser laser;

    void Start()
    {
        
        
            

    }

    // Update is called once per frame
    void Update()
    {
        x.text = "X: " + (int)player.position.x;
        y.text = "Y: " + (int)player.position.y;
        z.text = "Z: " + (int)player.position.z;

        healthText.text = (int)planeCombat.currentHealth + " / " + (int)planeCombat.maxHealth;
        healthSlider.value = planeCombat.currentHealth / planeCombat.maxHealth;

        boostSlider.value = planeControl.currentEnergy / planeControl.maxEnergy;

        laserSlider.value = laser.currentEnergy / laser.maxEnergy;
        if (laser.isTurnedOn)
            laserSlider.gameObject.SetActive(true);
        else
            laserSlider.gameObject.SetActive(false);

        if (target.averageDistance == -1)
        {
            targetText.fontSize = infFontSize;
            targetText.text = "\u221E";
        }
        else
        {
            targetText.fontSize = digitFontSize;
            targetText.text = (int)target.averageDistance + "";
        }
    }
}
