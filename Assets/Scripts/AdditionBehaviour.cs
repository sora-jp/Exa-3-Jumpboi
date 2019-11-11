using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AdditionBehaviour : MonoBehaviour
{
    // ReSharper disable once SuggestBaseTypeForParameter, because unity message
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name + " -> " + gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            OnPlayerCollision(other.transform.GetComponent<Player>());
        }
    }

    protected abstract void OnPlayerCollision(Player player);
    public abstract void PositionOnPlatform(Transform platform);
}
