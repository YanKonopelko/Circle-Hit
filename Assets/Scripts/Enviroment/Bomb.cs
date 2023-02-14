using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Animation _animation;
    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(time);
        _animation.Play("Explosion");
    }

    private void ExplosionStarter() => StartCoroutine(Explosion());

    private void OnEnable() => GameSceneManager.instance.onGameStart += ExplosionStarter;

    private void OnDisable() => GameSceneManager.instance.onGameStart -= ExplosionStarter;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Pet"))
            col.gameObject.GetComponent<Pet>().Lose();
    }
}
