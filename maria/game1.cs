using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Squared.Tiled;
using System.IO;
using Maria.Sprites;
using Maria.Models;
using Maria.Managers;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;


namespace Maria
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {   
        
        public static Game1 Instance { get; private set; }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public SpriteFont File;
        Map map;
        Vector2 viewportPosition;
        Song song;
        public List<SoundEffect> soundeffects;
        public List<Texture2D> bg;
        Texture2D block;
        


        public Player player;

        public Vector2 spawnPoint;

        #region Camera
        // Camera
        Camera camera;

        // Mainmenu
        MainMenu mainmenu;

        public Rectangle spriteRectangle;

        #endregion

        public SpriteManager spriteManager;
        private float timer = 0;

        public Game1()
        {
            var fps = 1;
            Instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 320*2;
            graphics.PreferredBackBufferHeight = 240*2;
            graphics.ApplyChanges();
            soundeffects = new List<SoundEffect>();
            mainmenu = new MainMenu();
            bg = new List<Texture2D>();
            this.Activated += (sender, args) => { this.Window.Title = "Maria Jumpu "; };
            this.Deactivated += (sender, args) => { this.Window.Title = "Maria Jumpu (unactive)"; };
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
            camera = new Camera(GraphicsDevice.Viewport);
            spriteManager = new SpriteManager();
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            File = Content.Load<SpriteFont>("File");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var animations = new Dictionary<string, Animation>()
            {
                { "bunny", new Animation(Content.Load<Texture2D>("Player/bunnyjung"), 3) },
            };

            spriteManager.AddSprite(new Player(animations, 10f)
            {

                Position = new Vector2(0, 100),
                Input = new Input()
                {
                    Jump = Keys.Space,
                    ChangeBlockA = Keys.A,
                    ChangeBlockB = Keys.S,
                    ChangeBlockC = Keys.D
                },
            });

            spriteManager.DebugBox.Add(Content.Load<Texture2D>("debugbox"));
            
            player = (Player)spriteManager.List[0];
            // TODO: use this.Content to load your game content here
            
            LoadObj();

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            File = null;
            // TODO: Unload any non ContentManager content here
        }

        public void LoadMap (string mapName)
        {
            SpriteManager.Instance.DestroyMap();
            map = Map.Load(Path.Combine(Content.RootDirectory, "maps/" + mapName + ".tmx"), Content);
            player.Reset();
            map.SetupSprite();
            map.SetPlayerLocation();
            
        }
        
        public void LoadSong (string songName)
        {
            song = Content.Load<Song>(Path.Combine("Music/" + songName));
        }

        public void LoadSfx(string sfxName)
        {
           soundeffects.Add(Content.Load<SoundEffect>(Path.Combine("sfx/" + sfxName)));
        }

        public void LoadBg(string bgName)
        {
            bg.Add(Content.Load<Texture2D>(Path.Combine("bg/" + bgName)));
        }
        
        public void LoadObj()
        {
            //sfx
            LoadSfx("jump");
            LoadSfx("hit");
            LoadSfx("crystal");
            LoadSfx("changeblock");
            LoadSfx("levelselect");


            //song
            LoadSong("0");

            //block
            block = Content.Load<Texture2D>(Path.Combine("tiled/blocks"));

            //bg
            LoadBg("bg0");
            LoadBg("bg1");
            LoadBg("bg2");
            LoadBg("bg3");
            LoadBg("bg4");
        }

        public void PlayLevel (int index)
        {
            player.Reset();
            LoadMap(LevelList.list[index].map);            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                if (LevelList.list[0].map != "level1")
                    LevelList.list[0].play1 = false;
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    if (!mainmenu.Active)
                    {
                        mainmenu.Active = true;
                        mainmenu.ChangeStage(2);
                    }
                }

                if (mainmenu.Active)
                {
                    mainmenu.Update(gameTime);
                }
                else
                {
                    spriteManager.Update(gameTime);

                    if (player.Texture != null)
                        spriteRectangle = new Rectangle((int)player.Position.X, (int)player.Position.Y,
                                       player.Texture.Width, player.Texture.Height);
                    camera.Update(gameTime, this);
                }

                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            
            // TODO: Add your drawing code here
            // Render map

            //map.Draw(spriteBatch, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), viewportPosition);
            //map.Draw(spriteBatch, new Rectangle(0, 0, 200, 100), viewportPosition);
            // Render sprite

            if (mainmenu.Active)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                    mainmenu.Draw(gameTime, spriteBatch);
                spriteBatch.End();
            } else {
                spriteBatch.Begin();
                {
                    if (LevelList.list[0].play1)
                        spriteBatch.Draw(bg[1], new Rectangle(0, 0, 640, 480), Color.White);
                    if (LevelList.list[0].play2)
                        spriteBatch.Draw(bg[2], new Rectangle(0, 0, 640, 480), Color.White);
                    if (LevelList.list[0].play3)
                        spriteBatch.Draw(bg[3], new Rectangle(0, 0, 640, 480), Color.White);
                    if (LevelList.list[0].play4)
                        spriteBatch.Draw(bg[4], new Rectangle(0, 0, 640, 480), Color.White);
                }
                spriteBatch.End();
                
                spriteBatch.Begin(SpriteSortMode.Deferred,
                    BlendState.AlphaBlend,
                    null, null, null, null,
                    camera.tranform
                    );

                spriteManager.Draw(gameTime, spriteBatch);

                spriteBatch.End();

                spriteBatch.Begin();

                spriteBatch.DrawString(File, "Score: " + player.score, new Vector2(520, 20), Color.Black, 0, new Vector2(0, 0), 1.5f, 0, 0);
                spriteBatch.Draw(block, new Rectangle(10, 10, 50, 50), 
                    new Rectangle( (int)player.blockType * block.Height, 0, block.Height, block.Height), Color.White);
                
                spriteBatch.End();

            }

           
            base.Draw(gameTime);

        }
    }
}
