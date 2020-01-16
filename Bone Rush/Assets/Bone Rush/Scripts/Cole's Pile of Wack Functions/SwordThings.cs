using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordThings : MonoBehaviour
{

	float attackHoldTime;
	bool countAttackTime;
	bool startedCounting;

	CameraShake cameraShake;

    // Start is called before the first frame update
    void Start()
    {
		cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			countAttackTime = true;
			startedCounting = true;
		}
		else if (Input.GetKey(KeyCode.Mouse0))
		{
			if (!cameraShake.shaking)
			{
				StartCoroutine(cameraShake.Shake(.2f));
			}
		}
		if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			countAttackTime = false;
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
    }

	void Attack(float time)
	{
		
	}
}
