//
// WorldTileManager.cs
//
// Author:
//       Devon O. <devon.o@onebyonedesign.com>
//
// Copyright (c) 2017 Devon O. Wolfgang
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldTileManager : MonoBehaviour
{
    /** Max Number of Tiles visible at one time */
    static int MAX_TILES = 10;

    /** Max speed of tiles */
    [Range(0f, 50f)]
    public float maxSpeed = 10f;

    /** Collection of Types of Tiles */
    public GameObject[] tileTypes;

    /** Size of Tiles in z dimension */
    private float tileSize = 10f;

    /** Current Speed of Tiles */
    private float speed;

    /** Collection of active Tiles */
    private List<GameObject> tiles;

    /** Pool of Tiles */
    private TilePool tilePool;

    /** Initialize */
    public void Init()
    {
        this.speed = 0f;
        this.tiles = new List<GameObject>();
        this.tilePool = new TilePool(this.tileTypes, MAX_TILES, this.transform);
        InitTiles();
    }

    /** Increase speed by given amount */
    public void IncreaseSpeed(float amt)
    {
        this.speed += amt;
        if (this.speed > this.maxSpeed)
            this.speed = maxSpeed;
    }

    /** Update Tiles */
    public void UpdateTiles(System.Random rnd)
    {
        for (int i = tiles.Count - 1; i >= 0; i--)
        {
            GameObject tile = tiles[i];
            tile.transform.Translate(0f, 0f, -this.speed * Time.deltaTime);

            // If a tile moves behind the camera release it and add a new one
            if (tile.transform.position.z < Camera.main.transform.position.z)
            {
                this.tiles.RemoveAt(i);
                this.tilePool.ReleaseTile(tile);
                int type = rnd.Next(0, this.tileTypes.Length);
                AddTile(type);
            }
        }
    }

    /** Add a new Tile */
    private void AddTile(int type)
    {
        GameObject tile = this.tilePool.GetTile(type);

        // position tile's z at 0 or behind the last item added to tiles collection
        float zPos = this.tiles.Count == 0 ? 0f : this.tiles[this.tiles.Count - 1].transform.position.z + this.tileSize;
        tile.transform.Translate(0f, 0f, zPos);
        this.tiles.Add(tile);
    }

    /** Initialize Tiles */
    private void InitTiles()
    {
        for (int i = 0; i < MAX_TILES; i++)
        {
            AddTile(0);
        }
    }

    /** Object Pool for World Tiles */
    class TilePool
    {
        /** Pool of Tiles */
        private List<GameObject>[] pool;

        /** Model Transform */
        private Transform transform;

        /** Create a new TilePool */
        public TilePool(GameObject[] types, int size, Transform transform)
        {
            this.transform = transform;
            int numTypes = types.Length;
            this.pool = new List<GameObject>[numTypes];
            for (int i = 0; i < numTypes; i++)
            {
                this.pool[i] = new List<GameObject>(size);
                for (int j = 0; j < size; j++)
                {
                    GameObject tile = (GameObject)Instantiate(types[i],transform);
                    tile.SetActive(false);
                    this.pool[i].Add(tile);
                }
            }
        }

        /** Get a Tile */
        public GameObject GetTile(int type)
        {
            for (int i = 0; i < this.pool[type].Count; i++)
            {
                GameObject tile = this.pool[type][i];
                // Ignore active tiles until we find the 1st inactive in appropriate list
                if (tile.activeInHierarchy)
                    continue;

                // reset the tile's transform to match the model transform
                tile.transform.position = this.transform.position;
                tile.transform.rotation = this.transform.rotation;

                // set to active and return
                tile.SetActive(true);
                return tile;
            }

            // This will never be reached, but compiler requires it
            return null;
        }

        /** Release a Tile */
        public void ReleaseTile(GameObject tile)
        {
            // Inactivate the released tile
            tile.SetActive(false);
        }
    }
}