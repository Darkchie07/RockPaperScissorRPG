using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using JetBrains.Annotations;
using Random = System.Random;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Character selectedCharacter;
    [SerializeField] private List<Character> characterList;
    [SerializeField] private Transform atkRef;
    [SerializeField] private UnityEvent onTakeDamage;
    [SerializeField] private bool isBot;

    public Character SelectedCharacter { get => selectedCharacter; }
    public List<Character> CharacterList { get => characterList; }

    private void Start()
    {
        if (isBot)
        {
            foreach (var character in characterList)
            {
                character.Button.interactable = false;
            }
        }
    }

    public void Prepare()
    {
        selectedCharacter = null;
    }

    public void SelectCharacter(Character character)
    {
        selectedCharacter = character;
    }

    public void SetPlay(bool value)
    {
        if (isBot)
        {
            List<Character> lotteryList = new List<Character>();
            foreach (var character in characterList)
            {
                int ticket = Mathf.CeilToInt(((float)character.CurrentHP / (float)character.MaxHP) * 10);
                for (int i = 0; i < ticket; i++)
                {
                    lotteryList.Add(character);
                }
            }

            int index = UnityEngine.Random.Range(0, lotteryList.Count);
            selectedCharacter = lotteryList[index];
        }
        else
        {
            foreach (var character in characterList)
            {
                character.Button.interactable = value;
            }
        }
    }

    public void Attack()
    {
        selectedCharacter.transform.DOMove(atkRef.position, 0.5f);
    }

    public bool isAttacking()
    {
        if (selectedCharacter == null)
            return false;
        return DOTween.IsTweening(selectedCharacter.transform);
    }

    public void TakeDamage(int damageValue)
    {
        selectedCharacter.ChangeHP(-damageValue);
        var spirteRend = selectedCharacter.GetComponent<SpriteRenderer>();
        spirteRend.DOColor(Color.red, 0.1f).SetLoops(6, LoopType.Yoyo);
        onTakeDamage.Invoke();
    }

    public bool IsDamaging()
    {
        if (selectedCharacter == null)
            return false;
        var spirteRend = selectedCharacter.GetComponent<SpriteRenderer>();
        return DOTween.IsTweening(spirteRend);
    }

    public void Remove(Character character)
    {
        if(characterList.Contains(character) == false)
            return;
        if (selectedCharacter == character)
        {
            selectedCharacter = null;
        }
        character.Button.interactable = false;
        character.gameObject.SetActive(false);
        characterList.Remove(character);
    }

    public void Return()
    {
        selectedCharacter.transform.DOMove(selectedCharacter.InitialPosition, 0.5f);
    }

    public bool IsReturning()
    {
        if (selectedCharacter == null)
            return false;
        
        return DOTween.IsTweening(selectedCharacter.transform);
    }
}
