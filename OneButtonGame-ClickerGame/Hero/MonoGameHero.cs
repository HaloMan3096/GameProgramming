using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Util;

namespace MonoGame;

public class MonoGameHero : MonoGameLibrary.Sprite.DrawableSprite
{
    public int Health { get; set; }
    public int Attack { get; protected set; }
    private OneButtonPlayerController PlayerController { get; set; }
    public ConsoleHero Hero { get; private set; }
    private bool held = false;
    
    protected HeroState heroState;
    public HeroState HeroState
    {
        get { return this.heroState; }
        set
        {
            if (this.heroState != value)
            {
                this.heroState = this.Hero.State = value;
            }
        }
    }
    
    public MonoGameHero(Game game) : base(game)
    {
        this.PlayerController = new OneButtonPlayerController(game);
        this.Hero = new ConsoleHero((GameConsole)game.Services.GetService<IGameConsole>() ?? new GameConsole(game));
        this.Health = 120;
        this.Attack = 20;
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        this.SpriteTexture = this.Game.Content.Load<Texture2D>("BlueSquare");
        this.Origin = new Vector2(this.SpriteTexture.Width / 2, this.SpriteTexture.Height / 2);
        this.Location = new Vector2(200, 300);
        this.HeroState = HeroState.Idle;
    }

    public override void Update(GameTime gameTime)
    {
        var time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        this.PlayerController.Update(gameTime);

        // Makeshift solution for checking if held (doesn't work)
        if (this.PlayerController.IsKeyPressed && !held)
        {
            Hero.Log("Hero Attack");
            Hero.Attack();
            held = true;
        }
        else
        {
            Hero.Idle();
            held = false;
        }
        
        base.Update(gameTime);
    }

    public void Dmg(int damage)
    {
        this.Health -= damage;
    }
}