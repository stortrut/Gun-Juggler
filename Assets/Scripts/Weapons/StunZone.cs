using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class StunZone : MonoBehaviour
{
    [Header("SoundWave - Drag in")]
    public SpriteRenderer soundWave;
    [SerializeField] Animator soundWaveAnimator;

    [Header("Stun values for different Objects")]
    [SerializeField] private float stunnedBalloon;
    [SerializeField] private float stunnedEnemy;

    private PolygonCollider2D polygonCollider;
    private Dictionary<IStunnable, Coroutine> stunCoroutines = new Dictionary<IStunnable, Coroutine>();
    private bool hit = false;

    private void Start()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D obj)
    {
        hit = false;
        if (obj == null) { return; }

        if (obj.CompareTag("EnemyBullet"))
        {
            var enemyBullet = obj.GetComponent<IAim>();
            enemyBullet.Deflected();
        }
        else if (obj.CompareTag("Enemy"))
        {
            Stun(obj, stunnedEnemy);
        }
        else if (obj.CompareTag("EnemyNonTargetable"))
        {
            Stun(obj, stunnedBalloon);
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
        if (stunnable == null) { Debug.Log("Object is hit but no stunnable on it"); return; }
        //Debug.Log(stunnable);
        foreach (var stun in stunnable)
        {
            stun.isStunnable = true;
            //Debug.Log(obj.gameObject.name);
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
    }
}
