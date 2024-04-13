using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillList : MonoBehaviour
{
    public SkillList()
    {
        // Check if the player has the ability unlocked, if not, Don't add it to the players current abilites.
    }

    public List<Ability> abilityList = new List<Ability>();
    public List<Ability> playerAbilities = new List<Ability>();
    private bool doubleJumpAvailable;
    private float lastDoubleJumpTime;
    private float doubleJumpCooldown = 1f;
    private Rigidbody2D rb;
    private float dirX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Skill List Started");
        CreateAbilities();

        rb = GetComponent<Rigidbody2D>();
    }

    public void CreateAbilities()
    {
        
        // List all the Abilities in the game With stats, through to another function. (START OF GAME)
        AddAbilityToAbilityList(24, 0, false, true, 0, 5, "Double Jump", "Double-Jump.png");

        AddAbilityToAbilityList(0, 30, false, true, 0, 5, "Dash", "Dash.png");

        
    }

    public void AddAbilityToAbilityList(int yVelocity, int xVelocity, bool isDamageAbility, bool unLockedOnStart, int damage, int cooldown, string abilityName, string abilityImage)
    {
        // Add the Ability to the Ability List (START OF GAME)
        Ability ability = gameObject.AddComponent<Ability>();
        ability.Initialize(yVelocity, xVelocity, isDamageAbility, unLockedOnStart, damage, cooldown, abilityName, abilityImage);

        abilityList.Add(ability);
        if (ability.UnLockedOnStart)
        {
            Debug.Log("Ability Unlocked");
            playerAbilities.Add(ability);
        }
    }

    // Use ability
    public void UseAbility(Ability ability)
    {
        Debug.Log("Ability:" + ability);


        if (ability.YVelocity > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, ability.YVelocity);
        }
        if (ability.XVelocity > 0)
        {
            Debug.Log(rb.velocity.x);
            Debug.Log(rb.velocity.y);
            Debug.Log(GetComponent<PlayerMovement>().GetIsFacingRight());
            if (GetComponent<PlayerMovement>().GetIsFacingRight() == true)
            {
                rb.velocity = new Vector2(rb.velocity.x + ability.XVelocity, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x - ability.XVelocity, rb.velocity.y);
            }
            Debug.Log(rb.velocity.x);
            Debug.Log(rb.velocity.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Player input check for ability usage
        CheckAbilityUsage();

    }

    public void CheckAbilityUsage()
    {
        if (GetComponent<PlayerMovement>().IsGrounded == true)
        {
            doubleJumpAvailable = true;
        }
        // if space is pressed twice, use the double jump ability
        if (Input.GetKeyDown(KeyCode.Space) && doubleJumpAvailable && playerAbilities.Count > 0 && GetComponent<PlayerMovement>().IsGrounded == false)
        {
            UseAbility(playerAbilities[0]);
            Debug.Log("Double Jump Used");
            
            // Set double jump on cooldown and record the time
            doubleJumpAvailable = false;
            lastDoubleJumpTime = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            UseAbility(playerAbilities[1]);
        }
    }
}
