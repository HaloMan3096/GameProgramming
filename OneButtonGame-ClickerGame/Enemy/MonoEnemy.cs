using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Util;

namespace MonoGame;

public class MonoEnemy : MonoGameLibrary.Sprite.DrawableSprite
{
    internal ConsoleEnemy _enemy;
    protected EnemyState _state;
    public int Health {get; set;}
    private int Attack {get; set;}

    private ScoreService score {get; set;}
    public EnemyState State
    {
        get { return _state; }
        set
        {
            if (this._state != value)
            {
                this._state = this._enemy.State = value;
            }
        }
    }
    
    private readonly MonoGameHero hero;
    
    public MonoEnemy(Game game, MonoGameHero hero) : base(game)
    {
        this.score = (ScoreService)game.Services.GetService<IScoreService>() ?? new ScoreService(game);
        this._enemy = new ConsoleEnemy((GameConsole)game.Services.GetService<IGameConsole>() ?? new GameConsole(game));
        this.hero = hero;
        this.Health = 100;
        this.Attack = 10;
    }

    public override void Initialize()
    {
        base.Initialize();
        this.SpriteTexture = this.Game.Content.Load<Texture2D>("RedSquare");
        // ReSharper disable twice PossibleLossOfFraction
        this.Origin = new Vector2(this.SpriteTexture.Width / 2, this.SpriteTexture.Height / 2);
        this.Location = new Vector2(500, 200);
        this._state = EnemyState.Idle;
    }

    public override void Update(GameTime gameTime)
    {
        var time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        if (Health <= 0)
        {
            score.CurrentScore++;
            Health = 100;
        }

        UpdateBasedOnState();
        UpdateBasedOnHeroState();

        ChooseState();

        base.Update(gameTime);
    }

    // The worst bot ever :(
    private void ChooseState()
    {
        EnemyState[] states = [EnemyState.Idle, EnemyState.Blocking, EnemyState.Attacking, EnemyState.Healing];
        Random rng = new Random();
        this.State = states[rng.Next(0, states.Length)];
    }

    private void UpdateBasedOnState()
    {
        switch (this.State)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Blocking:
                _enemy.Log("Enemy: Blocking");
                this._enemy.Idle();
                break;
            case EnemyState.Attacking:
                _enemy.Log("Enemy: Attacking");
                hero.Dmg(Attack);
                _enemy.Idle();
                break;
            case EnemyState.Healing:
                _enemy.Log("Enemy: Healing");
                if (Health % 2 == 0)
                {
                    // we clamp the health to 100 to avoid over healing
                    Math.Clamp(this.Health += 10, 0, 100);
                }
                this._enemy.Idle();
                break;
            default:
                this._enemy.Idle();
                break;
        }
    }

    // Switch may be wasteful here but it makes it scalable 
    private void UpdateBasedOnHeroState()
    {
        Console.WriteLine("Hero State: " + hero.Hero.State);
        switch (hero.Hero.State)
        {
            case HeroState.Attacking:
                if (this.State == EnemyState.Blocking)
                {
                    this.Health -= this.hero.Attack;
                }
                else
                {
                    this.Health -= (this.hero.Attack - 10);
                }
                Console.WriteLine($"Hero attacked, Health at #{Health}");
                this.hero.Hero.Idle();
                break;
            case HeroState.Idle:
            default:
                break;
        }
    }
}