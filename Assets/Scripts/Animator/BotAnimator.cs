using UnityEngine;

public class BotAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Run(bool isRun)
    {
        _animator.SetBool(DataAnimator.Params.Run, isRun);
    }
}
