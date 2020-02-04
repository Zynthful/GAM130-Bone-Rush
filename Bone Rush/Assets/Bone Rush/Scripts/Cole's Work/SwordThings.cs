using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordThings : MonoBehaviour
{

	float attackHoldTime;
	bool countAttackTime;
	bool startedCounting;
	float timePassedSinceAttacking;
	float attackDelay;
    bool canAttack;


    public bool isBlocking;

    [SerializeField]
    float heavyAttackReqTime = 1.5f;

    [SerializeField]
    float shieldBlockModifier = .1f;

    [Header("Components")]
    [Space(30)]
    public Animator swordAnimation;
    [SerializeField]
    Animator shieldAnimation;
    [SerializeField]
    public PlayerStaminaBar stam;

	CameraShake cameraShake;

	// Start is called before the first frame update
	void Start()
	{
		cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        shieldAnimation = GameObject.Find("PlayerShield").GetComponent<Animator>();
		swordAnimation = GameObject.Find("PlayerSword").GetComponent<Animator>();
		swordAnimation.SetBool("Left?", true);
        stam = GetComponent<PlayerStaminaBar>();
	}

	// Update is called once per frame
	void Update()
	{

        if(attackDelay <= 0 && !isBlocking)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

		if (canAttack)
		{
			if (Input.GetMouseButtonDown(0))
			{
				countAttackTime = true;
				startedCounting = true;
				timePassedSinceAttacking = 0f;
			}
			else if (Input.GetMouseButton(0) && countAttackTime == true)
			{
				if (!cameraShake.shaking)
				{
					StartCoroutine(cameraShake.Shake(.2f));
				}
				timePassedSinceAttacking = 0f;
			}
            if (Input.GetMouseButtonUp(0))
			{
				countAttackTime = false;
			}
		}
		else
		{
			attackDelay -= Time.deltaTime;
		}

		if (countAttackTime)
		{
			attackHoldTime += Time.deltaTime;
			if(attackHoldTime >= 3f)
			{
				countAttackTime = false;
			}
		}
		else if (startedCounting == true)
		{
			startedCounting = false;
			Attack(attackHoldTime);
			attackHoldTime = 0;
		}


        // Blocking checks
        CheckBlocking();

        /// <summary>
        /// Following code is related to animation (in region "anim")
        /// Will need to be changed when animations are implemented
        /// </summary>
        #region anim
        timePassedSinceAttacking += Time.deltaTime;

		if ((timePassedSinceAttacking >= 1.5f && !swordAnimation.GetBool("Left?")) || (isBlocking && !swordAnimation.GetBool("Left?")))
		{
			swordAnimation.SetBool("Swing", false);
			swordAnimation.SetBool("ResetPos", true);
			swordAnimation.SetBool("Left?", !swordAnimation.GetBool("Left?"));
		}
		else if (swordAnimation.GetBool("ResetPos") && !swordAnimation.GetCurrentAnimatorStateInfo(0).IsName("SwordSwing"))
		{
			swordAnimation.SetBool("ResetPos", false);
		}

		if (swordAnimation.GetBool("Left?") && swordAnimation.GetCurrentAnimatorStateInfo(0).IsName("SwordSwing 0"))
		{
			swordAnimation.SetBool("Swing", false);
		}
		else if (!swordAnimation.GetBool("Left?") && swordAnimation.GetCurrentAnimatorStateInfo(0).IsName("SwordSwing"))
		{
			swordAnimation.SetBool("Swing", false);
		}
        #endregion
    }

    void CheckBlocking()
    {
        if (Input.GetMouseButtonDown(1) && !startedCounting && stam.staminaBar.value > stam.maxStamina*0.1)
        {
            isBlocking = true;
            Block();
        }
        else if ((Input.GetMouseButtonUp(1) && isBlocking) || stam.staminaBar.value < stam.maxStamina * 0.1)
        {
            isBlocking = false;
            Block();
        }
    }

    void Attack(float time)
	{
		attackDelay = .3f;
        if(time > heavyAttackReqTime) // heavyAttackReqTime is how long the attack needs to be charged to be counted as a heavy attack
        {
            // Heavy Attack
        }
        else
        {
            // Light Attack
        }
		swordAnimation.SetBool("Left?", !swordAnimation.GetBool("Left?"));
		swordAnimation.SetBool("Swing", true);
        // Triggers SwordSwings event in FMOD
        FMODUnity.RuntimeManager.PlayOneShot("event:/SwordSwings");
    }

    public void Block(float damage = 0)
    {
        if (isBlocking)
        {
            Debug.Log("Blocking");
            shieldAnimation.SetBool("isBlocking", true);
            Debug.Log(damage);
            stam.staminaBar.value -= Mathf.Clamp(damage*shieldBlockModifier, stam.minStamina, stam.maxStamina);
        }
        else
        {
            shieldAnimation.SetBool("isBlocking", false);
        }
    }
}
