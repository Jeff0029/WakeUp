using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum SelectionAction
{
    JoinRoom = 0,
    customize,
    option,
    exit
}

public class SelectionRayCast : MonoBehaviour
{
    [SerializeField]
    internal RawImage cursor;
    [SerializeField]
    internal TextMeshProUGUI cursorText;
    private Texture defaultIcon;
    private SelectionMethod curSelectionMethod;
    private SelectableItem curSelection; 
    public SelectableItem CurSelection
    {
        get { return CurSelection; }
    }
    //private Dictionary<SelectionAction, Texture> selectionTextures = new Dictionary<SelectionAction, Texture>();

    private void Start()
    {
        // Force MenuSelector
        gameObject.layer = LayerMask.NameToLayer("MenuSelector");
        defaultIcon = cursor.texture;
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && curSelectionMethod != null)
        {
            curSelectionMethod();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Object collided with should be under menu selection
        SelectableItem item = other.GetComponent<SelectableItem>();
        if (item == null)
        {
            Debug.LogError("Missing SelectableItem component on " + item.name);
        } else {
            curSelection = item;
            SelectionComponents components = SelectableItem.GetSelectionComponents(item.selection);
            cursor.texture = components.selectionIcon;
            cursorText.text = components.selectionText;
            curSelectionMethod = components.selectionMethod;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Make sure to get the gameObject.  Each components has it's own ID!
        if (curSelection != null && other.gameObject.GetInstanceID() == curSelection.gameObject.GetInstanceID())
        {
            curSelection = null;
            cursor.texture = defaultIcon;
            cursorText.text = "";
            curSelectionMethod = null;
        }
    }

    private IEnumerator IsSelectionStillActive()
    {

        yield return null;
    }
}
