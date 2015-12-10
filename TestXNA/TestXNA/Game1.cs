using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;

namespace TestXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public struct PlayerData
    {
        public Vector2 Position;
        public bool IsAlive;
        public Color Color;
        public int type;
        public String Direction;

    }
    public class Game1 : Microsoft.Xna.Framework.Game
    {



        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D back;
        int screenWidth;
        int screenHight;
        PlayerData[] players;
        int numberOfPlayers = 10;
        Texture2D carriageTexture;
         Network_Component.Network net;
            
       
        GameTime gt;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 500;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Tank Game - Client";
            net = new Network_Component.Network();
            net.StartConnection();
           

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }
        

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            /* String stri="brick";
             // Create a new SpriteBatch, which can be used to draw textures.
             spriteBatch = new SpriteBatch(GraphicsDevice);
             back = Content.Load<Texture2D>("post-138925-1233469168");
             carriageTexture = Content.Load<Texture2D>(stri);
             screenWidth = graphics.PreferredBackBufferWidth;
             screenHight = graphics.PreferredBackBufferHeight;
             SetUpPlayers();*/
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
        /// 

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            this.gt = gameTime;
            // TODO: Add your update logic here
            //String stri = "brick";
            spriteBatch = new SpriteBatch(GraphicsDevice);
            back = Content.Load<Texture2D>("post-138925-1233469168");
            //carriageTexture = Content.Load<Texture2D>(stri);
            screenWidth = graphics.PreferredBackBufferWidth;
            screenHight = graphics.PreferredBackBufferHeight;
            //DrawPlayers();
            //SetUpPlayers();
            Draw(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            DrawScenery();
            DrawPlayers();
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        private void DrawScenery()
        {
            Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHight);
            spriteBatch.Draw(back, screenRectangle, Color.White);
        }

        private void SetUpPlayers()
        {
            Color[] playerColors = new Color[10];
            playerColors[0] = Color.Red;
            playerColors[1] = Color.Green;
            playerColors[2] = Color.Blue;
            playerColors[3] = Color.Purple;
            playerColors[4] = Color.Orange;
            playerColors[5] = Color.Indigo;
            playerColors[6] = Color.Yellow;
            playerColors[7] = Color.SaddleBrown;
            playerColors[8] = Color.Tomato;
            playerColors[9] = Color.Turquoise;

            players = new PlayerData[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players[i].IsAlive = true;
                players[i].Color = playerColors[i];

            }

            players[0].Position = new Vector2(100, 0);

            players[1].Position = new Vector2(140, 0);

            players[2].Position = new Vector2(180, 0);
            players[3].Position = new Vector2(220, 0);
            players[4].Position = new Vector2(260, 0);
            players[5].Position = new Vector2(300, 0);
            players[6].Position = new Vector2(300, 40);

        }
        public void set(){
            Update(gt);
        }
        private void DrawPlayers()
        {
            /* foreach (PlayerData player in players)
             {
                 if (player.IsAlive)
                 {
                     carriageTexture = Content.Load<Texture2D>("brick");
                     spriteBatch.Draw(carriageTexture, player.Position, Color.White);
                 }
             }*/
           
            
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {

                        if ((net.map[i][j].type) != 0)
                        {
                            PlayerData player = new PlayerData();
                            String str = null;
                            player.type = net.map[i][j].type;
                            player.Direction=net.map[i][j].Direction;
                            player.Position = new Vector2((i * 45), ((j * 45)));
                            if (player.type == 1)
                            {
                                str = "brick";
                            }
                            else if (player.type == 2)
                            {
                                str = "stone";
                            }
                            else if (player.type == 3)
                            {
                                str = "water";
                            }
                            else if (player.type == 4)
                            {
                                
                                 if(player.Direction.Equals("West")){
                                str = "tank-west";}
                                else if(player.Direction.Equals("South")){
                                str = "tank-south";}
                                else if(player.Direction.Equals("East")){
                                str = "tank-east";}
                                 else 
                                 {
                                     str = "tank-north";
                                 }
                            }
                            else if (player.type == 5)
                            {
                                str = "coins";
                            }
                            else if (player.type == 6)
                            {
                                str = "life";
                            }
                            carriageTexture = Content.Load<Texture2D>(str);
                            spriteBatch.Draw(carriageTexture, player.Position, Color.White);

                        }
                        else
                        {
                            PlayerData player = new PlayerData();
                            //String str = null;
                            player.type = net.map[i][j].type;
                            player.Position = new Vector2((i * 45), ((j * 45)));
                            carriageTexture = Content.Load<Texture2D>("blank");
                            try
                            {
                                //spriteBatch.Begin();
                                spriteBatch.Draw(carriageTexture, player.Position, Color.White);
                                //spriteBatch.End();
                            }
                            catch (Exception e) { Console.WriteLine(e); }
                        
                    }
                }

            } 
        }
    }
}


