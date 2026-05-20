using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    [Header("Puzzle Settings")]
    public int gridSize = 5;
    public float spacing = 0.5f;
    public int shuffleSteps = 25;
    
    [Header("Materials")]
    public Material yellowMaterial;
    public Material greenMaterial;
    
    [Header("UI & Feedback")]
    public GameObject winIndicator;
    public TextMeshPro winText;

    [SerializeField] private List<PuzzleNode> allNodes = new List<PuzzleNode>();

    [ContextMenu("Generate Grid")]
    public void GenerateGrid()
    {
        // Clear existing children
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        allNodes.Clear();

        // Position nodes so the grid is centered on the manager's position
        // Local Z is -0.1f to be in front of the board
        Vector3 startOffset = new Vector3(-(gridSize - 1) * spacing * 0.5f, -(gridSize - 1) * spacing * 0.5f, -0.1f);
        PuzzleNode[,] grid = new PuzzleNode[gridSize, gridSize];

        for (int r = 0; r < gridSize; r++)
        {
            for (int c = 0; c < gridSize; c++)
            {
                GameObject nodeObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                nodeObj.name = $"Node_{r}_{c}";
                nodeObj.transform.SetParent(transform);
                nodeObj.transform.localPosition = startOffset + new Vector3(c * spacing, r * spacing, 0);
                nodeObj.transform.localScale = Vector3.one * (spacing * 0.7f);

                if (nodeObj.GetComponent<BoxCollider>() == null) nodeObj.AddComponent<BoxCollider>();
                nodeObj.AddComponent<XRSimpleInteractable>();
                
                PuzzleNode node = nodeObj.AddComponent<PuzzleNode>();
                grid[r, c] = node;
                allNodes.Add(node);
            }
        }

        // Connect Neighbors
        for (int r = 0; r < gridSize; r++)
        {
            for (int c = 0; c < gridSize; c++)
            {
                List<PuzzleNode> adj = new List<PuzzleNode>();
                if (r > 0) adj.Add(grid[r - 1, c]);
                if (r < gridSize - 1) adj.Add(grid[r + 1, c]);
                if (c > 0) adj.Add(grid[r, c - 1]);
                if (c < gridSize - 1) adj.Add(grid[r, c + 1]);

                grid[r, c].Initialize(this, true, adj, yellowMaterial, greenMaterial); 
            }
        }
        
        if (winIndicator != null) winIndicator.SetActive(false);
        if (winText != null) winText.gameObject.SetActive(false);
        
        Debug.Log($"Generated {gridSize}x{gridSize} grid.");
    }

    [ContextMenu("Shuffle Puzzle")]
    public void ShufflePuzzle()
    {
        if (allNodes.Count == 0) return;

        // Reset all to Green (Solved state)
        foreach (var node in allNodes) 
        {
            if (!node.isGreen) node.Toggle();
        }

        // Simulate random clicks to guarantee solvability
        for (int i = 0; i < shuffleSteps; i++)
        {
            allNodes[Random.Range(0, allNodes.Count)].SimulateClick();
        }
        
        // Safety: If it happens to be solved, shuffle again
        if (allNodes.All(n => n.isGreen)) ShufflePuzzle();
        
        if (winIndicator != null) winIndicator.SetActive(false);
        if (winText != null) winText.gameObject.SetActive(false);
        
        Debug.Log("Puzzle Shuffled and Solvable.");
    }

    public void CheckWinCondition()
    {
        if (allNodes.Count > 0 && allNodes.All(n => n.isGreen))
        {
            Debug.Log("Puzzle Solved!");
            OnPuzzleSolved();
        }
    }

    private void OnPuzzleSolved()
    {
        if (winIndicator != null) winIndicator.SetActive(true);
        if (winText != null) 
        {
            winText.text = "PUZZLE SOLVED!";
            winText.gameObject.SetActive(true);
        }
    }
}
