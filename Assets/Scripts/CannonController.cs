using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform initialBulletPosition;
    [SerializeField] private float shootingFrequency = 1.0f;
    [SerializeField] private GameObject explosionPVF;
    private Animator animator;
    private bool hasLounchedBullet = false;
    private float objDestroyDefaultTime = 10.0f;

    //string references
    private string SHOOTING_ANIMATION = "IsShooting";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        StartCoroutine(ProcessShooting());
    }

    IEnumerator ProcessShooting()
    {
        if (FindObjectsOfType<CannonBullet>().Length >= 1)
        {
            animator.SetBool(SHOOTING_ANIMATION, false);
            hasLounchedBullet = true;
        }
        else
        {
            hasLounchedBullet = false;
            animator.SetBool(SHOOTING_ANIMATION, true);            
        }

        yield return new WaitForSeconds(shootingFrequency);

    }
    
    public void Fire()
    {           
        GameObject newBullet = Instantiate(bullet, initialBulletPosition.position, initialBulletPosition.rotation);
        Destroy(newBullet, objDestroyDefaultTime);
    }
}
