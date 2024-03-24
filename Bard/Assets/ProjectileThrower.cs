using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileThrower : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Launch(Vector3 targetPos) {
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        newProjectile.transform.rotation = Quaternion.LookRotation(transform.forward, targetPos - transform.position);
        newProjectile.GetComponent<Rigidbody2D>().velocity = newProjectile.transform.up * speed;
        newProjectile.transform.rotation = Quaternion.identity;
        Destroy(newProjectile,15);
    }
}
