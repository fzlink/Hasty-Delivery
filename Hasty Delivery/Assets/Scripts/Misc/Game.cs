//
// Game.cs
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
using System;

public class Game : MonoBehaviour
{
    /** Speed Increase Value */
    static float SPEED_INCREASE = .1f;

    public int[] seed;
    public int selectedSeed;
    /** Seeded Randomizer */
    static System.Random RND;

    /** Tileholder */
    public GameObject TileHolder;

    /** TileManager */
    private WorldTileManager tileManager;

    private bool canScroll = false;
    public static bool gameIsOver;

    /** On Awake */
    void Awake()
    {
        // 32 is just an arbitrary seed number. Could be anything.
        selectedSeed = seed[UnityEngine.Random.Range(0, seed.Length)];
        RND = new System.Random(selectedSeed);
        this.tileManager = TileHolder.GetComponent<WorldTileManager>();
    }

    /** On Start */
    void Start()
    {
        gameIsOver = false;
        CargoManager.instance.OnLevelFail += StopScroll;
        CargoManager.instance.OnLevelWin += StopScroll;
        InputManager.instance.OnGameStarted += InitRoad;
        this.tileManager.Init();
        Vehicle vehicle = LoadManager.instance.GetVehicleObject();
        Instantiate(vehicle);
    }

    private void InitRoad()
    {
        canScroll = true;
    }

    private void StopScroll()
    {
        canScroll = false;
        gameIsOver = true;
    }

    /** On Update */
    void Update()
    {
        if (!canScroll) return;
        this.tileManager.IncreaseSpeed(SPEED_INCREASE);
        this.tileManager.UpdateTiles(RND);
    }
}