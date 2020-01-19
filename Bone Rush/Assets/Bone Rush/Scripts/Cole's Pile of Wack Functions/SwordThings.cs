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

	Animator swordAnimation;

	CameraShake cameraShake;

    // Start is called before the first frame update
    void Start()
    {
		cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
		swordAnimation = GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<Animator>();
		swordAnimation.SetBool("Left?", true);
    }

    // Update is called once per frame
    void Update()
    {
		if(attackDelay <= 0)
		{
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				countAttackTime = true;
				startedCounting = true;
				timePassedSinceAttacking = 0f;
			}
			else if (Input.GetKey(KeyCode.Mouse0))
			{
				if (!cameraShake.shaking)
				{
					StartCoroutine(cameraShake.Shake(.2f));
				}
				timePassedSinceAttacking = 0f;
			}
			if (Input.GetKeyUp(KeyCode.Mouse0))
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
		}
		else if (startedCounting == true)
		{
			startedCounting = false;
			Attack(attackHoldTime);
			attackHoldTime = 0;
		}
		timePassedSinceAttacking += Time.deltaTime;

		if(timePassedSinceAttacking >= 1.5f && !swordAnimation.GetBool("Left?"))
		{
			swordAnimation.SetBool("Swing", false);
			swordAnimation.SetBool("ResetPos", true);
			swordAnimation.SetBool("Left?", !swordAnimation.GetBool("Left?"));
		}
		else if(swordAnimation.GetBool("ResetPos") && !swordAnimation.GetCurrentAnimatorStateInfo(0).IsName("SwordSwing"))
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
	}

	void Attack(float time)
	{
		attackDelay = .3f;
		swordAnimation.SetBool("Left?", !swordAnimation.GetBool("Left?"));
		swordAnimation.SetBool("Swing", true);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.transform.root != transform.root) // Checks that it is not colliding with the player
		{
			// Do stuff
		}
	}
}
