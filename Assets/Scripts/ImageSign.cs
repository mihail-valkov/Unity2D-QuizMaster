using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSign : MonoBehaviour
{

    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite incorrectAnswerSprite;

    Image image;

    // Start is called before the first frame update
    void Start()
    {
        //fint the image component
        image = GetComponent<Image>();
        ResetImage();
    }

    public void ResetImage()
    {
        //set color to transparent
        image.color = new Color(1, 1, 1, 0);
        image.sprite = null;
    }

    public void SetCorrect()
    {
        //set the image to the correct sprite
        image.sprite = correctAnswerSprite;
        //set the color to white
        image.color = Color.white;
    }

    public void SetIncorrect()
    {
        //set the image to the incorrect sprite
        image.sprite = incorrectAnswerSprite;
        //set the color to white
        image.color = Color.white;
    }
}
