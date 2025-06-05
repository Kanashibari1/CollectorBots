using UnityEngine;

public class DataAnimator : MonoBehaviour
{
    public class Params 
    {
        public static readonly int Run = Animator.StringToHash(nameof(Run));
    }
}