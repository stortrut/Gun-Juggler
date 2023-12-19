using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class StunZone : MonoBehaviour
{
    [SerializeField] Animator soundWaveAnimator;

    //    [HideInInspector]
    public List<GameObject> objectsInField;
    public SpriteRenderer soundWave;
    private bool hit = false;
    [SerializeField] private PolygonCollider2D polygonCollider;
    private Dictionary<IStunnable, Coroutine> stunCoroutines = new Dictionary<IStunnable, Coroutine>();

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    objectsInField.Add(other.gameObject);
    //}
    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    objectsInField.Remove(other.gameObject);
    //   // StartCoroutine(Delay(other));
    //}
    private void OnTriggerEnter2D(Collider2D obj)
    {

        hit = false;


        if (obj == null) { return; }

        if (obj.CompareTag("EnemyBullet"))
        {
            var enemyBullet = obj.GetComponent<IAim>();
            enemyBullet.Deflected();

            //add bool so that enemy bullets now can damage enemies,
        }
        else if (obj.CompareTag("Enemy"))
        {
            Stun(obj, 4);

        }
        else if (obj.CompareTag("EnemyNonTargetable"))
        {
            Stun(obj, 1);
        }

        if (hit == true)
        {
            UpgradeCombo.Instance.hitSinceShot = true;
            UpgradeCombo.Instance.comboTween.Kill();
        }
    }

    public void SoundWave()
    {
        polygonCollider.enabled = true;
        soundWave.enabled = true;
        soundWaveAnimator.SetBool("Wave", true);
        StartCoroutine(nameof(Wait));
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        soundWaveAnimator.SetBool("Wave", false);

        yield return new WaitForSeconds(0.7f);
        soundWave.enabled = false;
        polygonCollider.enabled = false;
    }
    private void Stun(Collider2D obj, float time)
    {


        var stunnable = obj.GetComponents<IStunnable>();
        var damageable = obj.GetComponent<Health>();

        hit = true;
        if (stunnable == null) { return; }
        //Debug.Log(stunnable);
        foreach (var stun in stunnable)
        {
            stun.isStunnable = true;
            if (stunCoroutines.ContainsKey(stun))
            {
                // Coroutine is already running, add time to it
                StopCoroutine(stunCoroutines[stun]);
            }

            stunCoroutines[stun] = StartCoroutine(UnFreeze(time, stun, damageable));
           


        }
        IEnumerator UnFreeze(float timeStunned, IStunnable stunnable, IDamageable damageable)
        {
           
            yield return new WaitForSeconds(timeStunned);
            stunnable.isStunnable = false;
            damageable.ApplyDamage(1);

        }
        //private IEnumerator Delay(Collider2D other)
        //{
        //    yield return new WaitForSeconds(0.5f);
        //    objectsInField.Remove(other.gameObject);
        //}
    }
}
