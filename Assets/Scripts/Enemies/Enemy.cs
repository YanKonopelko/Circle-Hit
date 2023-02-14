using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    private AIPath ap;
    private const float speed = 2;
    void Start()
    {
        ap = GetComponent<AIPath>();
        GameSceneManager.instance.onGameStart += SetTarget;
        ap.maxSpeed = speed;
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Pet"))
            col.gameObject.GetComponent<Pet>().Lose();
    }

    private void SetTarget()
    {
        Path a;
        ap.target = FindObjectOfType<Pet>().transform;
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        GameSceneManager.instance.onGameStart -= SetTarget;
    }
}
