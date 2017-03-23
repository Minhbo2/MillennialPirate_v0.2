using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootInit : MonoBehaviour {

    void Start()
    {

        // Create the initial app
        var app = ResourceManager.Create("App/App");
        if (app)
        {
            app.name = "App";

            // Create the main game
            var game = ResourceManager.Create("Game/Game");
            if (game)
                game.name = "Game";

            // Don't need boot init anymore
            Destroy(gameObject);
        }
    }
}
