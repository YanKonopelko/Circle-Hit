using System.Collections;
using UnityEngine;
public class LineCheck :MonoBehaviour
{
    private Transform _transform;
    private Vector3 lastPos;
    private void Start()
    {
        _transform = transform;
        lastPos = _transform.position;
        StartCoroutine(Check(0.01f));
    }
    private IEnumerator Check(float time)
    {
        yield return new WaitForSeconds(time);
        if (lastPos == _transform.position)
        {
            lastPos = _transform.position;
            GameSceneManager.instanse.NavUptdate();
            StartCoroutine(Check(0.01f));
        }
        else
            StartCoroutine(Check(0.05f));
    }
}
