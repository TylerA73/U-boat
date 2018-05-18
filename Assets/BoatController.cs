using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Tyler Arseneault
 * Due: Feb 13, 2018
 * Description: Handles the actions of the boat
 * The U Boat leads the target, then fires the torpedo
 */
public class BoatController : MonoBehaviour {
    private GameObject[] enemies;
    private Rigidbody enemy;
    private Rigidbody shooter;
    private GameObject[] torpedos;
    private bool isCoroutineExecuting;
    private int enemyNum;
    public float torpedoSpeed;
    public int ammo;
    public float speed;
    private const float TOP_SPEED = 5f;

	/*
	 * Initialization
	 */
	void Start () {
        enemyNum = 0;
        shooter = GetComponent<Rigidbody>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemy = enemies[enemyNum].GetComponent<Rigidbody>();
        speed = 3f;
        torpedos = GameObject.FindGameObjectsWithTag("Torpedo");
        ammo = torpedos.Length;
        torpedoSpeed = 5f;
        isCoroutineExecuting = false;


        foreach (GameObject i in torpedos){
            i.SetActive(false);
        }
	}

    /*
     * Handles updates for each frame
     */
    private void FixedUpdate(){
        shooter.velocity = transform.right * speed;

        if (FindDistance() <= 20)
        {
            shooter.transform.LookAt(InterceptPosition());
            StartCoroutine(Fire());
        }
    }

    /*
     * Find the position that the torpedo needs to intercept
     * Param: None
     * Returns: Vector3
     */
    private Vector3 InterceptPosition(){
        Vector3 enemyPos = enemy.position;
        Vector3 enemyVel = enemy.velocity;
        Vector3 torpedoPos = shooter.position;

        Vector3 interceptDirection = enemyPos - torpedoPos;
        float sqrTorpedoSpeed = torpedoSpeed * torpedoSpeed;
        float sqrEnemySpeed = enemyVel.sqrMagnitude;
        float interceptDotEnemy = Vector3.Dot(interceptDirection, enemyVel);
        float sqrInterceptDirection = interceptDirection.sqrMagnitude;
        float dist = (interceptDotEnemy * interceptDotEnemy) - sqrInterceptDirection * (sqrEnemySpeed - sqrTorpedoSpeed);

        float sqrtDist = Mathf.Sqrt(dist);
        return ((-interceptDotEnemy - sqrtDist) / sqrInterceptDirection) * interceptDirection + enemyVel;
    }

    /*
     * Find how far the sub is from the convoy
     * Param: None
     * Return: float
     */
    private float FindDistance(){
        return Mathf.Abs(shooter.position.z);
    }

    /*
     * Fires each torpedo at a rate of one time per second only if there is still ammo
     * Param: None
     * Return: IEnumerator
     * Source: https://answers.unity.com/questions/796881/c-how-can-i-let-something-happen-after-a-small-del.html
     */
    IEnumerator Fire(){
        
        if (isCoroutineExecuting)
            yield break;

        isCoroutineExecuting = true;

        yield return new WaitForSeconds(1f);

        if (ammo > 0)
        {
            torpedos[ammo - 1].SetActive(true);
            torpedos[ammo - 1].transform.position = shooter.position;
            torpedos[ammo - 1].transform.rotation = shooter.rotation;
            ammo--;
            enemyNum++;
            enemy = enemies[enemyNum].GetComponent<Rigidbody>();
        }

        isCoroutineExecuting = false;
    }


}
