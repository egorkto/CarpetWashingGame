using System;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void Delete()
    {
        _animator.SetTrigger("Delete");
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}