using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct SelectionComponents
{
    public Texture selectionIcon;
    public string selectionText;
    public SelectionMethod selectionMethod;
}
public delegate void SelectionMethod();

public class SelectableItem : MonoBehaviour
{
    public SelectionAction selection;
    private void Start()
    {
        // Force MenuSelector
        gameObject.layer = LayerMask.NameToLayer("MenuSelectable");
    }

        public static SelectionComponents GetSelectionComponents(SelectionAction selection)
    {
        string iconName = "";
        string iconText = "";
        SelectionMethod action = new SelectionMethod(MissingFunc);
        
        switch (selection)
        {
            case SelectionAction.JoinRoom:
                iconText = "Go to work";
                action = new SelectionMethod(JoinRoom);
                break;
            case SelectionAction.customize:
                iconText = "Customize";
                action = new SelectionMethod(Customize);
                break;
            case SelectionAction.option:
                iconText = "Options";
                action = new SelectionMethod(Option);
                break;
            case SelectionAction.exit:
                iconText = "Skip Work";
                action = new SelectionMethod(ExitGame);
                break;
        }

        if (iconName == "")
        {
            iconName = Enum.GetName(typeof(SelectionAction), selection);
        }
        SelectionComponents selectedComponents = new SelectionComponents();
        selectedComponents.selectionIcon = Resources.Load<Texture>("Textures/Icons/Selections/" + iconName);
        selectedComponents.selectionText = iconText;
        selectedComponents.selectionMethod = action;
        return selectedComponents;
    }

    public static void MissingFunc()
    {
        Debug.LogError("action was not defined");
    }

    public static void JoinRoom()
    {
        bool isSearching = RoomFinder.lobbyFinder.FindRoom();
        if (isSearching)
        {
            Debug.Log("Joining Lobby");
        }
        
    }


    public static void Customize()
    {

    }

    public static void Option()
    {

    }

    public static void ExitGame()
    {
        Debug.Log("Leaving Game");
        // TODO: Add Are you sure? Message
        Application.Quit();
    }
}
