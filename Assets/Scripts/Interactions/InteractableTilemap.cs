using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractableTilemap : MonoBehaviour
{
    public Tilemap tilemap;
    [SerializeField] private EnergyMeterLogic energyMeterLogic;
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
            energyMeterLogic.meter_state += 1;
            tilemap.SetTile(cell, energyMeterLogic.meter_tiles[energyMeterLogic.meter_state]);
            if(energyMeterLogic.meter_state == 8) {
                energyMeterLogic.FinishStage();
            }
        }
        if(tile.name.StartsWith("Tile_boost_")) {
            float coeficient = 1.4f;
            playerMovement.boostSpeedByAmplifying(coeficient);
        }
        return;
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
}
