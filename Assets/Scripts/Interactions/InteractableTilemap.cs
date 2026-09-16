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
    
    void Start() {
        playerState = player.GetComponent<PlayerState>();
    }
    public void energyPickup(Collider2D collider2D)
    {   
        if(!collider2D.CompareTag("Player")) {
            return;
        }
        playerState = collider2D.gameObject.GetComponent<PlayerState>();
        playerState.playVFX(PlayerState.VFXEnum.EnergyPickup);
        if(playerState.energyStored < playerState.getMaxEnergyStored()) {
            playerState.energyStored += 1;
        }
    }

    private void OnTriggerStay2D(Collider2D collider2D) {
        if (!collider2D.CompareTag("Player")) {
            return;
        }

        Bounds bounds = collider2D.bounds;
        Vector3Int minCell = tilemap.WorldToCell(bounds.min);
        Vector3Int maxCell = tilemap.WorldToCell(bounds.max);

        for (int x = minCell.x; x <= maxCell.x; x++) {
            for (int y = minCell.y; y <= maxCell.y; y++) {
                Vector3Int cell = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(cell);

                if (tile == null) {
                    continue;
                }
                if(tile.name == "Tile_energy" && playerState.energyStored < playerState.getMaxEnergyStored()) {
                    tilemap.SetTile(cell, null);
                    energyPickup(collider2D);
                }
                Debug.Log(tile.name);
                Debug.Log(tile.name.StartsWith("Tile_meter_"));
                if(tile.name.StartsWith("Tile_meter_") && tile.name != "Tile_meter_08" && playerState.energyStored > 0) {
                    playerState.energyStored -= 1;
                    meter_look += 1;
                    Debug.Log("Meter_look = " + meter_look);
                    tilemap.SetTile(cell, meter_tiles[meter_look]);
                }
                Debug.Log("Overlapping tile: " + tile.name);
            }
        }
    }
}
