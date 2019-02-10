using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SelectionAction
{
    joinLobby = 0,
    customize,
    option,
    exit
}

public class SelectionRayCast : MonoBehaviour
{
    [SerializeField]
    internal RawImage cursor;
    private Texture defaultIcon;
    private SelectableItem curSelection; 
    public SelectableItem CurSelection
    {
        get { return CurSelection; }
    }
    private Dictionary<SelectionAction, Texture> selectionTextures = new Dictionary<SelectionAction, Texture>();

    private void Start()
    {
        // Force MenuSelector
        gameObject.layer = LayerMask.NameToLayer("MenuSelector");
        defaultIcon = cursor.texture;

        foreach (int selection in Enum.GetValues(typeof(SelectionAction)))
        {
            Texture selectionIcon = Resources.Load<Texture>("Textures/Icons/Selections/" + Enum.GetName(typeof(SelectionAction), selection));
            selectionTextures.Add((SelectionAction)selection, selectionIcon);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Object collided with should be under menu selection
        SelectableItem item = other.GetComponent<SelectableItem>();
        if (item == null)
        {
            Debug.LogError("Missing SelectableItem component on " + item.name);
        } else if (curSelection == null) //If we're selecting something currently, stay on that selection
        {
            curSelection = item;
            cursor.texture = selectionTextures[item.selection];
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Make sure to get the gameObject.  Each components has it's own ID!
        if (other.gameObject.GetInstanceID() == curSelection.gameObject.GetInstanceID())
        {
            curSelection = null;
            cursor.texture = defaultIcon;
        }
    }

    private IEnumerator IsSelectionStillActive()
    {

        yield return null;
    }
}
