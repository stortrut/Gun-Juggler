using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class StunZone : MonoBehaviour
{
    [Header("SoundWave - Drag in")]
    public List<SpriteRenderer> soundWave;
    [SerializeField] List<Animator> soundWaveAnimator;

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
            AudienceSatisfaction.Instance.AudienceHappiness(10);
            Sound.Instance.SoundSet(Sound.Instance.audienceApplauding, 0, .3f);
            Sound.Instance.SoundSet(Sound.Instance.otherPositiveReactions, 3, .3f);
        }
        else if (obj.CompareTag("Enemy"))
        {
            Stun(obj, stunnedEnemy);
            Sound.Instance.SoundSet(Sound.Instance.dizzyPieClown, 0, .8f);
            Sound.Instance.SoundSet(Sound.Instance.audienceApplauding, 0, .3f);

            AudienceSatisfaction.Instance.AudienceHappiness(2);

            //stunned sound!!!
        }
        else if (obj.CompareTag("EnemyNonTargetable"))
        {
            Stun(obj, stunnedBalloon);
            AudienceSatisfaction.Instance.AudienceHappiness(2);
             Sound.Instance.SoundSet(Sound.Instance.landingWithBike, 0 , 0.3f );
        }
        if (hit == true)
        {
            //UpgradeCombo.Instance.hitSinceShot = true;
            //UpgradeCombo.Instance.comboTween.Kill();
        }
    }
    public void SoundWave()
    {
        if(soundWave.Count() == 0) { return; }

        foreach (var wave in soundWave)
        {
            polygonCollider.enabled = true;

            wave.enabled = true;

            var anim = soundWaveAnimator.ElementAt(soundWave.IndexOf(wave));
            anim.SetBool("Wave", true);
            StartCoroutine(Wait(wave, anim));
        }
    }
    private IEnumerator Wait(SpriteRenderer sprite , Animator anim)
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Wave", false);
        SoundWaveOff();
        yield return new WaitForSeconds(0.4f);
        polygonCollider.enabled = false;
        yield return new WaitForSeconds(0.3f);
        sprite.enabled = false;
        
        
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
            StartCoroutine(DelayPop());
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


        }
        IEnumerator DelayPop()
        {
            yield return new WaitForSeconds(0);
            if(damageable != null)
            {
                damageable.ApplyDamage(1);
            }
   
        }
    }
    private void SoundWaveOff()
    {
        if(hit == true)
        {
            Score.Instance.bulletsHit++;
        }
    }
}
