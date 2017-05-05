using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

	public int width;
	public int height;

	public Tile[,] tiles;

	// Use this for initialization
	void Start () {

		CreateTiles ();
		SubdivideTilesArray ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateTiles () {

		tiles = new Tile[width, height];

		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {



				tiles [i, j] = new Tile (Tile.Type.Stone);
			}
		}
	}

	void SubdivideTilesArray (int i1 = 0, int i2 = 0) {

		if (i1 > tiles.GetLength (0) && i2 > tiles.GetLength (1))
			return;

		//Get size of segment
		Tile[,] segment;

		int sizeX, sizeY;

		if (tiles.GetLength (0) - i1 > 100) {

			sizeX = 100;
		} else
			sizeX = tiles.GetLength (0) - i1;

		if (tiles.GetLength (1) - i2 > 100) {

			sizeY = 100;
		} else
			sizeY = tiles.GetLength (1) - i2;

		segment = new Tile[sizeX,sizeY];

		//Copy tiles onto segment

		for (int i = i1; i < sizeX; i++) {
			for (int j = i2; j < sizeY; j++) {

				segment [i, j] = tiles [i + i1, j + i2];
			}
		}

		GenerateMesh (segment, i1, i2);


		if (tiles.GetLength (0) > i1 + 100) {
			SubdivideTilesArray (i1 + 100, i2);
			return;
		}

		if (tiles.GetLength (1) > i2 + 100) {
			SubdivideTilesArray (0, i2 + 100);
			return;
		}
	}

	void GenerateMesh (Tile[,] tiles_segment, int x, int y) {

		MeshData data = new MeshData (tiles_segment);

		GameObject meshGO = new GameObject ("CHUNK_" + x + "_" + y);
		meshGO.transform.position = new Vector3 (x, y);
		meshGO.transform.SetParent (this.transform);

		MeshFilter filter = meshGO.AddComponent<MeshFilter> ();
		meshGO.AddComponent<MeshRenderer> ();

		Mesh mesh = filter.mesh;

		mesh.vertices = data.vertices.ToArray ();
		mesh.triangles = data.triangles.ToArray ();
	}
}
