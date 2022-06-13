using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

enum EnemyStates { Counts, Looks }

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float startLookTime = 2f;
    [SerializeField] private AudioSource countAudio;
    private float curLookTime;
    private EnemyStates curStates = EnemyStates.Counts;
    public delegate void OnStartCountDelegate();
    public OnStartCountDelegate OnStartCount;
    public delegate void OnStopCountDelegate();
    public OnStopCountDelegate OnStopCount;
    private Animator anim;
    private List<CharacterMovement> characters = new List<CharacterMovement>();
    // Start is called before the first frame update
    void Start()
    {
        characters = FindObjectsOfType<CharacterMovement>().ToList();
        anim = GetComponent<Animator>();
        curLookTime = startLookTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (characters == null)
            return;

        if (characters.Count <= 0)
            return;

        StateMachines();
    }

    private void StateMachines()
    {
        switch (curStates)
        {
            case EnemyStates.Counts:
                Count();
                break;
            case EnemyStates.Looks:
                Look();
                break;
            default:
                break;
        }
    }

    private void Count()
    {
        if (!countAudio.isPlaying)
        {
            anim.SetTrigger("Look");
            curStates = EnemyStates.Looks;
            OnStopCount?.Invoke();
        }
    }
    
    private void Look()
    {
        if(curLookTime > 0)
        {
            curLookTime -= Time.deltaTime;
            var charToRemove = new List<CharacterMovement>();
            foreach(var character in characters)
            {
                if (character.IsMove() && character.IsInvulnerable == false)
                {
                    charToRemove.Add(character);
                }
            }
            foreach (var character in charToRemove)
            {
                characters.Remove(character);
                character.Die();
            }
        }
        else
        {
            curLookTime = startLookTime;
            anim.SetTrigger("Look");
            countAudio.Play();
            curStates = EnemyStates.Counts;
            OnStartCount?.Invoke();
        }
    }
}
