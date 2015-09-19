using UnityEngine;
using System.Collections;

public static class Globals {
	public static float TOP_COOLDOWN = 1.5f;
	public static float SIDES_COOLDOWN = 6.0f;
	public static float BOTTOM_COOLDOWN = 3.0f;
	public static float WAIT_TIME = 1.5F;
}

public enum ShipTypes {
	typeOne, typeTwo, typeThree
}

public enum AbilityType {
	defense,attack,passive
}

public struct Ability {
	private AbilityType abilityType;
	private string myString;
	private int myInt;
}