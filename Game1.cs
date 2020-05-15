using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Modeller;
using Pong.Sprites;
using System;
using System.IO;
using System.Collections.Generic;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        // Olika gamestates
        public enum GameState
        {
            MainMenu, 
            Play,
            Pause
        }
        GameState CurrentState = GameState.MainMenu;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int screenBredd;
        public static int screenHöjd;

        public static Random rand;

        private Poäng poäng;
        private List<Sprite> sprites;

        private Texture2D playGameButton;
        private Texture2D pauseButton;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Bredden och höjden på skärmen
            screenBredd = graphics.PreferredBackBufferWidth;
            screenHöjd = graphics.PreferredBackBufferHeight;
            rand = new Random();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var playerTexture = Content.Load<Texture2D>("Bat");
            var bollTexture = Content.Load<Texture2D>("Ball");

            poäng = new Poäng(Content.Load<SpriteFont>("font"));

            playGameButton = Content.Load<Texture2D>("play");
            pauseButton = Content.Load<Texture2D>("pause");

            sprites = new List<Sprite>()
            {
                new Player(playerTexture)
                {
                    Pos = new Vector2(20, (screenHöjd / 2) - (playerTexture.Height / 2)),
                    Input = new Input()
                    {
                        Up = Keys.W,
                        Down = Keys.S,
                    }
                },
                new Player(playerTexture)
                {
                    Pos = new Vector2(screenBredd - 20 - playerTexture.Width, (screenHöjd / 2) - (playerTexture.Height / 2)),
                    Input = new Input()
                    {
                        Up = Keys.Up,
                        Down = Keys.Down,
                    }
                },
                new Boll(bollTexture)
                {
                    Pos = new Vector2((screenBredd / 2) - (bollTexture.Width / 2), (screenHöjd / 2) - (bollTexture.Height / 2)),
                    Poäng = poäng,
                },
            };

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            switch (CurrentState)
            {
                // Byter mellan gamestates
                case GameState.MainMenu:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter)) // Enter för att starta spelet
                        CurrentState = GameState.Play;
                    break;

                case GameState.Play:
                    foreach (var sprite in sprites)
                    {
                        sprite.Update(gameTime, sprites);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.P)) // P för att pausa
                        CurrentState = GameState.Pause;
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape)) // Escape för att komma till main menu
                        CurrentState = GameState.MainMenu;
                    break;

                case GameState.Pause:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space)) // Space för att återuppta
                        CurrentState = GameState.Play;
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape)) // Escape för att komma till main menu
                        CurrentState = GameState.MainMenu;
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            switch (CurrentState)
            {
                // Main menu skärmen
                case GameState.MainMenu:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Draw(playGameButton, new Vector2((screenBredd / 2) - (playGameButton.Width / 2), (screenHöjd / 2) - (playGameButton.Height / 2)), Color.White);
                    break;
                
                // Spel skärmen
                case GameState.Play:
                    GraphicsDevice.Clear(Color.Black);
                    foreach (var sprite in sprites)
                        sprite.Draw(spriteBatch);
                    poäng.Draw(spriteBatch);
                    break;

                // Pause skärmen
                case GameState.Pause:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Draw(pauseButton, new Vector2((screenBredd / 2) - (pauseButton.Width / 2), (screenHöjd / 2) - (pauseButton.Height / 2)), Color.White);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
