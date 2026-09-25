using UnityEngine;
using UnityEngine.Tilemaps;

public class EnergyMeterLogic : MonoBehaviour
{
    public int meter_state = 0; // Max = 8 (0-indexed)
    public Tile[] meter_tiles;
}
