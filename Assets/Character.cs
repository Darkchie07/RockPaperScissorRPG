using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.GlobalIllumination;

public class Character : MonoBehaviour
{
    [SerializeField] private new string name;
    [SerializeField] private CharacterType _type;
    [SerializeField] private int currentHP;
    [SerializeField] private int maxHP;
    [SerializeField] private int attackPower;
    [SerializeField] private TMP_Text overHeadText;
    [SerializeField] private Image avatar;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text typeText;
    [SerializeField] private Image healthBar;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Button button;
    private Vector3 initialPosition;
    public Button Button { get => button; }
    public CharacterType Type { get => _type; }
    public int AttackPower { get => attackPower; }
    public int CurrentHP { get => currentHP; }
    public Vector3 InitialPosition { get => initialPosition; }
    public int MaxHP { get => maxHP; }
    private void Start()
    {
        initialPosition = this.transform.position;
        overHeadText.text = name;
        nameText.text = name;
        typeText.text = _type.ToString();
        button.interactable = false;
        UpdateHpUI();
    }

    public void ChangeHP(int amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHpUI();
    }

    private void UpdateHpUI()
    {
        healthBar.fillAmount = (float)currentHP / (float)maxHP;
        hpText.text = currentHP + "/" + maxHP;
    }
}
