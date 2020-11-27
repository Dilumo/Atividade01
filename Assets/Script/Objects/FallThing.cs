using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallThing : MonoBehaviour
{
    public FallThingData data;

    public Rigidbody2D body;
    public RectTransform rectTransform;


    public void Spawn(Sprite sprite, float speedFall, int points, AudioClip sfxClick, Vector3 position)
    {
        data.imageThing = sprite;
        GetComponent<Image>().sprite = sprite;
        gameObject.SetActive(true);
        data.speedFall = speedFall;
        body.gravityScale = speedFall;
        data.points = points;
        rectTransform.position = position;
        data.isAtGame = true;
        data.sfxClicked = sfxClick;
        
    }

    public void Colect()
    {
        body.mass = 0;
        rectTransform.localPosition = Vector3.zero;
        data.sfxClicked = null;
        gameObject.SetActive(false);
        data.isAtGame = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != "LostLine") return;
        Colect();
        GameplayController.Instance.countThingsAtGame--;
    }
}
