using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public int YVelocity;
    public int XVelocity;
    private bool IsDamageAbility;
    public bool UnLockedOnStart;
    private int Damage;
    private int Cooldown;
    private string AbilityName;
    private string AbilityImage;



    private bool Unlocked { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override string ToString()
    {
        return AbilityName; // Or any other property you want to display
    }

    public void AddAbilityToPlayer()
    {
        // Add the Ability to the player (AFTER UNLOCKING / START OF GAME)
    }
    
    public void UnlockAbility()
    {
        // Unlock the Ability for the player (AFTER COMPLETING A TASK / ACHIEVEMENTS)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Initialize(int yVelocity, int xVelocity, bool isDamageAbility, bool unLockedOnStart, int damage, int cooldown, string abilityName, string abilityImage)
    {
        this.YVelocity = yVelocity;
        this.XVelocity = xVelocity;
        this.IsDamageAbility = isDamageAbility;
        this.UnLockedOnStart = unLockedOnStart;
        this.Damage = damage;
        this.Cooldown = cooldown;
        this.AbilityName = abilityName;
        this.AbilityImage = abilityImage;
        if (UnLockedOnStart)
        {
            this.Unlocked = true;
        } else
        {
            this.Unlocked = false;
        }
    }
}
