using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionButtons : MonoBehaviour {

    private Image buttonImageHUD; //hud button interaction image

    public enum Buttons { E, Space }; //interaction buttons
    private Dictionary<Buttons, string> pathToImage; //path to intereaction buttons

    private void Start()
    {
        buttonImageHUD = GetComponent<Image>(); //find interaction image
        pathToImage = InitializePathDictionary(); //initialize sprite pathes
    }

    //initialize sprite pathes
    private Dictionary<Buttons, string> InitializePathDictionary()
    {
        Dictionary<Buttons, string> newPathToImage = new Dictionary<Buttons, string>();

        newPathToImage.Add(Buttons.E, "Button/button_E"); //path to E button
        newPathToImage.Add(Buttons.Space, "Button/button_Space"); //path to space button

        return newPathToImage;
    }

    //show hud interaction button
    public void ShowInteractionButton(Buttons button)
    {
        var buttonSprite = Resources.Load<Sprite>(pathToImage[button]);
        buttonImageHUD.sprite = buttonSprite;
        buttonImageHUD.enabled = true;
    }

    //hide interaction button
    public void HideInteractionButton()
    {
        buttonImageHUD.enabled = false;
    }
}
