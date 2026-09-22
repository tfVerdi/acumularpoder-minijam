using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractableTilemap : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile[] meter_tiles;
    public int meter_look = 0; // Max = 8
    public GameObject player;
    private PlayerState playerState;
    private Movement playerMovement;
    
    void Start() {
        playerState = player.GetComponent<PlayerState>();
        playerMovement = player.GetComponent<Movement>();
    }

    public void energyPickup(Collider2D collider2D) {   
        playerState = collider2D.gameObject.GetComponent<PlayerState>();
        playerState.playVFX(PlayerState.VFXEnum.EnergyPickup);
        if(playerState.energyStored < playerState.getMaxEnergyStored()) {
            playerState.energyStored += 1;
        }
    }

    private void tileCheckStay(TileBase tile, Vector3Int cell, Collider2D collider2D) {
        if(tile.name == "Tile_energy" && playerState.energyStored < playerState.getMaxEnergyStored()) {
            tilemap.SetTile(cell, null);
            energyPickup(collider2D);
        }
        if(tile.name.StartsWith("Tile_meter_") && tile.name != "Tile_meter_08" && playerState.energyStored > 0) {
            playerState.energyStored -= 1;
            meter_look += 1;
            // Debug.Log("Meter_look = " + meter_look);
            tilemap.SetTile(cell, meter_tiles[meter_look]);
        }
        Debug.Log("Overlapping tile: " + tile.name);
        return;
    }

    private void tileCheckEnter(TileBase tile, Vector3Int cell, Collider2D collider2D) {
        if(tile.name.StartsWith("Tile_boost_")) {
            // Debug.Log(tile.name);
            string direction = tile.name.Split("_").Last();
            // Debug.Log(direction);
            float coeficient = 1.5f;
            Debug.Log("p");
            if(playerMovement.boosted) {
                return;
            }
            StartCoroutine(playerMovement.boostSpeed(coeficient));
            switch(direction) {
                case "up":
                    playerMovement.movementForce += Vector2.up * coeficient;
                    return;
                case "right":
                    playerMovement.movementForce += Vector2.right * coeficient;
                    return;
                case "down":
                    playerMovement.movementForce += Vector2.down * coeficient;
                    return;
                case "left":
                    playerMovement.movementForce += Vector2.left * coeficient;
                    return;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collider2D) {
        if(!collider2D.CompareTag("Player")) {
            return;
        }

        Bounds bounds = collider2D.bounds;
        Vector3Int minCell = tilemap.WorldToCell(bounds.min);
        Vector3Int maxCell = tilemap.WorldToCell(bounds.max);

        for(int x = minCell.x; x <= maxCell.x; x++) {
            for(int y = minCell.y; y <= maxCell.y; y++) {
                Vector3Int cell = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(cell);

                if(tile == null) {
                    continue;
                }
                tileCheckStay(tile, cell, collider2D);
            }
        }
    }    
    
    private void OnTriggerEnter2D(Collider2D collider2D) {
        if(!collider2D.CompareTag("Player")) {
            return;
        }

        Bounds bounds = collider2D.bounds;
        Vector3Int minCell = tilemap.WorldToCell(bounds.min);
        Vector3Int maxCell = tilemap.WorldToCell(bounds.max);

        for(int x = minCell.x; x <= maxCell.x; x++) {
            for(int y = minCell.y; y <= maxCell.y; y++) {
                Vector3Int cell = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(cell);

                if(tile == null) {
                    continue;
                }
                tileCheckEnter(tile, cell, collider2D);
            }
        }
    }
}
