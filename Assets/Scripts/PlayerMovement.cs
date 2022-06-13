using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    private void Update()
    {
        verDirect = Input.GetAxis("Vertical");
        verDirect = Mathf.Clamp(verDirect, 0, 1);
        sprintValue = Input.GetAxis("Sprint");
        anim.SetFloat("Speed", verDirect + sprintValue);
    }
    public override void Die()
    {
        base.Die();
        UIManager.Instance.TriggerLoseMenu();
    }
    public override void Win()
    {
        base.Win();
        UIManager.Instance.TriggerWinMenu();
    }
}
