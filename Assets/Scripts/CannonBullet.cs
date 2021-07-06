using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CannonBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 5.0f;
    [SerializeField] private GameObject explosionPVF;    
    private Transform target;
    private Rigidbody2D rigidbody2D;
    private float rotateSpeed = 200f;
    private float objectsDestroyingTime = 3.0f;
    private string PLAYER_LAYER = "Player";

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Player>().transform;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!rigidbody2D.IsTouchingLayers(LayerMask.GetMask(PLAYER_LAYER)))
        {
            DestroyBullet();
        }        
    }

    public void DestroyBullet ()
    {
        GameObject newExplosion = Instantiate(explosionPVF, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(newExplosion, objectsDestroyingTime);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rigidbody2D.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction,transform.up).z;
        rigidbody2D.angularVelocity = -rotateAmount * rotateSpeed;
        rigidbody2D.velocity = transform.up * bulletSpeed;
    }
}
