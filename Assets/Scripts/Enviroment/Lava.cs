using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Pet"))
            col.gameObject.GetComponent<Pet>().Lose();
        if(col.gameObject.CompareTag("Enemy"))
            col.gameObject.GetComponent<Enemy>().Death();
    }
}
