using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationObject : MonoBehaviour
{
    public Animation animationComponent;

    void Start()
    {
        if (animationComponent == null)
        {
            animationComponent = GetComponent<Animation>();
        }
    }

    public void PlayAnimation(string animationName)
    {
        if (animationComponent != null)
        {
            animationComponent.Play(animationName);
        }
        else
        {
            Debug.LogError("Компонент Animation не найден!");
        }
    }
}
