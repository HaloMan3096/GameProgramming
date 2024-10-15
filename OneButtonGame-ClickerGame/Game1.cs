using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.State;
using MonoGameLibrary.Util;

namespace MonoGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont font;
    
    private readonly ScoreService score;
    private MonoGameHero hero;
    private MonoEnemy enemy;
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        var monoHero = new MonoGameHero(this);
        hero = monoHero;
        this.Components.Add(hero);
        
        var monoEnemy = new MonoEnemy(this, hero);
        enemy = monoEnemy;
        this.Components.Add(enemy);
        
        var scoreService = (ScoreService)this.Services.GetService<IScoreService>() ?? new ScoreService(this);
        score = scoreService;
        this.Components.Add(scoreService);
        
#if DEBUG
        FPS fps;
        fps = new FPS(this);
        this.Components.Add(fps);
#endif
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        // Preload content
        Content.Load<Texture2D>("BlueSquare");
        Content.Load<Texture2D>("RedSquare");
        font = Content.Load<SpriteFont>("Arial"); // Load the font
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        _spriteBatch.Begin();
        // Could def. move this to its own class so the game class doesn't hold instances of hero and enemy
        _spriteBatch.DrawString(font, "Press 'E' to attack the enemy", new Vector2(0, 0), Color.Black);
        _spriteBatch.DrawString(
            font, 
            string.Format(
                "Current Score: {0}, Current HP: {1}, Enemy HP: {2}"
                , score.CurrentScore
                , hero.Health
                , enemy.Health)
            , new Vector2(0, 20)
            , Color.Black);
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}