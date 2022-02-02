using Assets.Scripts.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    public bool canBeDisabled = true;
    public ITriggerable[] triggers;
    public float delayBetweenTriggers = 0.4f;

    bool activated = false;
    Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    bool AnimatorIsPlaying()
    {
        return (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0));
    }

    void Enabling()
    {
        anim.Play("Base Layer.Enabling");
        StartCoroutine(TriggerItems());

        activated = true;
    }

    void Disabling()
    {
        anim.Play("Base Layer.Disabling");

        activated = false;
    }

    IEnumerator TriggerItems()
    {
        foreach (ITriggerable obj in triggers)
        {
            obj.Triggered();

            yield return new WaitForSeconds(delayBetweenTriggers);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile" && !AnimatorIsPlaying())
        {
            if(!activated)
            {
                Enabling();
            }
            else if(canBeDisabled)
            {
                Disabling();
            }
        }
    }
}
