using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

	public enum Type { Stone }
	public Type type;

	public Tile (Type type) {
		
		this.type = type;
	}

}
