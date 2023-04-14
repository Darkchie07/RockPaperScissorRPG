using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [SerializeField] State state;
    [SerializeField] private GameObject battleResult;
    [SerializeField] private TMP_Text battleResultText;
    [SerializeField] private Player player1;
    [SerializeField] private Player player2;

    enum State
    {
        Preparation,
        Player1Select,
        Player2Select,
        Attacking,
        Damaging,
        Returning,
        BattleisOver
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Preparation :
                player1.Prepare();
                player2.Prepare();

                player1.SetPlay(true);
                player2.SetPlay(false);
                state = State.Player1Select;
                break;
            case State.Player1Select :
                if(player1.SelectedCharacter != null)
                {
                    Debug.Log(player1.SelectedCharacter);
                    player1.SetPlay(false);
                    player2.SetPlay(true);
                    state = State.Player2Select;
                }
                break;
            case State.Player2Select :
                 if(player2.SelectedCharacter != null)
                 {
                     player2.SetPlay(false);
                     player1.Attack();
                     player2.Attack();
                    state = State.Attacking;
                 }
                 break;
            case State.Attacking :
                if(player1.isAttacking() == false && player2.isAttacking() == false)
                {
                    CalculateBattle(player1, player2, out Player winner, out Player loser);
                    if (loser == null)
                    {
                        player1.TakeDamage(player2.SelectedCharacter.AttackPower);
                        player2.TakeDamage(player1.SelectedCharacter.AttackPower);
                    }
                    else
                    {
                        loser.TakeDamage(winner.SelectedCharacter.AttackPower);
                    }

                    state = State.Damaging;
                }
                break;
            case State.Damaging :
                if (player1.IsDamaging() == false && player2.IsDamaging() == false)
                {
                    if (player1.SelectedCharacter.CurrentHP == 0)
                    {
                        player1.Remove(player1.SelectedCharacter);
                    }
                    
                    if (player2.SelectedCharacter.CurrentHP == 0)
                    {
                        player2.Remove(player2.SelectedCharacter);
                    }

                    state = State.Returning;
                    if (player1.SelectedCharacter != null)
                    {
                        player1.Return();
                    }

                    if (player2.SelectedCharacter != null)
                    {
                        player2.Return();
                    }
                }
                break;
            case State.Returning :
                if (player1.IsReturning() == false && player2.IsReturning() == false)
                {
                    if (player1.CharacterList.Count == 0 && player2.CharacterList.Count == 0)
                    {
                        battleResult.SetActive(true);
                        battleResultText.text = "Battle is Over!\nDraw!";
                        state = State.BattleisOver;
                    }
                    else if (player1.CharacterList.Count == 0)
                    {
                        battleResult.SetActive(true);
                        battleResultText.text = "Battle is Over!\nPlayer 2 win!";
                        state = State.BattleisOver;
                    }
                    else if (player2.CharacterList.Count == 0)
                    {
                        battleResult.SetActive(true);
                        battleResultText.text = "Battle is Over!\nPlayer 1 win!";
                        state = State.BattleisOver;
                    }
                    else
                    {
                        state = State.Preparation;
                    }
                }
                break;
            case State.BattleisOver :
                
                break;
        }
    }

    private void CalculateBattle(Player player, Player player3, out Player winner, out Player loser)
    {
        var type1 = player1.SelectedCharacter.Type;
        var type2 = player2.SelectedCharacter.Type;
        if (type1 == CharacterType.Rock && type2 == CharacterType.Paper)
        {
            winner = player2;
            loser = player1;
        }else if  (type1 == CharacterType.Rock && type2 == CharacterType.Scissor)
        {
            winner = player1;
            loser = player2;
        }else if  (type1 == CharacterType.Paper && type2 == CharacterType.Rock)
        {
            winner = player1;
            loser = player2;
        }else if  (type1 == CharacterType.Paper && type2 == CharacterType.Scissor)
        {
            winner = player2;
            loser = player1;
        }else if  (type1 == CharacterType.Scissor && type2 == CharacterType.Rock)
        {
            winner = player2;
            loser = player1;
        }else if  (type1 == CharacterType.Scissor && type2 == CharacterType.Paper)
        {
            winner = player1;
            loser = player2;
        }
        else
        {
            winner = null;
            loser = null;
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main");
    }
}
