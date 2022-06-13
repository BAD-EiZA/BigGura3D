using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
public class Timer : MonoBehaviour
{
    [SerializeField] private float initTime = 60f;
    [SerializeField] private TextMeshProUGUI timerText;
    private List<CharacterMovement> characters = new List<CharacterMovement>();
    private float curTime;
    // Start is called before the first frame update
    void Start()
    {
        characters = FindObjectsOfType<CharacterMovement>().ToList();
        curTime = initTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (curTime > 0)
        {
            curTime -= Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(curTime);
            timerText.text = span.ToString(@"mm\:ss");
            return;
        }
        else
        {
            foreach (var character in characters)
            {
                character.Die();
            }
        }
    }
}
