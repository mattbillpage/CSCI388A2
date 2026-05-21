using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System.Collections.Generic;

public class PuzzleNode : MonoBehaviour
{
    private MeshRenderer nodeRenderer;
    private Material yellowMaterial;
    private Material greenMaterial;
    
    private List<PuzzleNode> adjacentNodes = new List<PuzzleNode>();
    public bool isGreen { get; private set; }
    
    private PuzzleManager manager;

    public void Initialize(PuzzleManager manager, bool startGreen, List<PuzzleNode> neighbors, Material yellow, Material green)
    {
        this.manager = manager;
        this.isGreen = startGreen;
        this.adjacentNodes = neighbors;
        this.yellowMaterial = yellow;
        this.greenMaterial = green;
        
        if (nodeRenderer == null) nodeRenderer = GetComponent<MeshRenderer>();
        
        UpdateVisuals();
        
        var interactable = GetComponent<XRSimpleInteractable>();
        if (interactable == null) interactable = gameObject.AddComponent<XRSimpleInteractable>();
        
        interactable.selectEntered.RemoveAllListeners();
        interactable.selectEntered.AddListener((args) => {
            Debug.Log($"Select Entered on {gameObject.name} by {args.interactorObject.transform.name}");
            OnInteraction(args);
        });

        interactable.activated.RemoveAllListeners();

        interactable.hoverEntered.RemoveAllListeners();
        interactable.hoverEntered.AddListener((args) => {
            Debug.Log($"Hover Entered on {gameObject.name} by {args.interactorObject.transform.name}");
        });
    }

    private void OnInteraction(BaseInteractionEventArgs args)
    {
        ExecuteToggleLogic();
        if (manager != null) manager.CheckWinCondition();
    }

    public void SimulateClick()
    {
        ExecuteToggleLogic();
    }

    private void ExecuteToggleLogic()
    {
        foreach (var node in adjacentNodes)
        {
            node.Toggle();
        }
    }

    public void Toggle()
    {
        isGreen = !isGreen;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (nodeRenderer == null) nodeRenderer = GetComponent<MeshRenderer>();
        if (nodeRenderer != null && yellowMaterial != null && greenMaterial != null)
        {
            nodeRenderer.sharedMaterial = isGreen ? greenMaterial : yellowMaterial;
        }
    }
}
