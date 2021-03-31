using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidBehaviour : MonoBehaviour
{
    private Vector3 movement;
    private Rigidbody2D rb;
    private PolygonCollider2D polygonCollider2D;
    private PolygonCollider2D polygonColliderTrigger2D;
    [SerializeField] private float minScaleValue = 0.75f;
    [SerializeField] private float maxScaleValue = 3f;
    [SerializeField] private float minForceValue = 75f;
    [SerializeField] private float maxForceValue = 100f;
    [SerializeField] private float force = 0f;
    // Start is called before the first frame update
    void Start()
    {

        transform.Rotate(0f, 0f, Random.Range(0f, 359f));

        AddComponentsToAstroid();
        SetAstroidScale();
        PushAstroid();
    }

    void HandleAttributes()
    {
        minScaleValue = minScaleValue < 0f ? 0.5f : minScaleValue;
        maxScaleValue = maxScaleValue < 0f ? 4f : maxScaleValue;
        minForceValue = minForceValue < 0f ? 75f : minForceValue;
        maxForceValue = maxForceValue < 0f ? 100f : maxForceValue;
    }

    void AddComponentsToAstroid()
    {
        rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        rb.gravityScale = 0;

        polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>() as PolygonCollider2D;
        polygonColliderTrigger2D = gameObject.AddComponent<PolygonCollider2D>() as PolygonCollider2D;
        polygonColliderTrigger2D.isTrigger = true;
    }

    private void SetAstroidScale()
    {
        float scale = Random.Range(minScaleValue, maxScaleValue);
        transform.localScale = new Vector3(scale, scale, 1);
        rb.mass = transform.localScale.magnitude;
    }

    private void PushAstroid()
    {
        movement = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        force = (maxScaleValue / rb.mass) * (Random.Range(minForceValue, maxForceValue));
        rb.AddForce(movement * force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            
            Destroy(collision.gameObject);
        }
        
    }
}

