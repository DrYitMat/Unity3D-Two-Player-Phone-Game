using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Globals {
	public static float TOP_COOLDOWN = 1.5f;
	public static float SIDES_COOLDOWN = 6.0f;
	public static float BOTTOM_COOLDOWN = 3.0f;
	public static float WAIT_TIME = 1.5F;
}

public enum ShipTypes {
	fireShip, electripShip, shadowShip
}

public enum AbilityType {
	fire,electric,shadow
}