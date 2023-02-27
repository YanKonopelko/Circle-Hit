using NTC.Global.Cache;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoCache
{
    private NavMeshAgent agent;
    private const float speed = 7;
    private int priority = 0;
    [SerializeField] private float force = 10;
    private Rigidbody2D rb;

    private Transform myTransform;
    protected override void OnEnabled()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        agent.speed = speed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        myTransform = transform;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Pet"))
            col.gameObject.GetComponent<Pet>().Lose();
        if (col.gameObject.GetComponent<DrowLine>() != null)
        {
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(force*rb.velocity.normalized,ForceMode2D.Impulse);
            rb.AddForce(-1*(force/2*rb.velocity.normalized),ForceMode2D.Impulse);
        }
    }
    
    public void Death()
    {
        Destroy(gameObject);
    }

    protected override void Run()
    {
        var tr = Quaternion.LookRotation(myTransform.forward,GameSceneManager.instanse.pet.transform.position);
        myTransform.rotation = Quaternion.RotateTowards(myTransform.rotation,tr,Time.deltaTime* speed);
    }
}
