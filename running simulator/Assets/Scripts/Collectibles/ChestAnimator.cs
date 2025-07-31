using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAnimator : MonoBehaviour
{
    private const string OPEN = "open";

    void Start()
    {
        Chest.OnChestOpened += Chest_OnChestOpened;
    }

    private void Chest_OnChestOpened(object sender, Chest.OnChestOpenedEventArgs e)
    {
        PlayChestAnimation(e.caller);
    }

    private void PlayChestAnimation(Chest caller)
    {
        if (!caller.AnimationPlayed)
        {
            Animator _animator = caller.GetComponent<Animator>();
            _animator.Play(OPEN);
            caller.SetAnimationAsPlayed();
        }
    }
}
