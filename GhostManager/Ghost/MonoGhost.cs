using GhostManager.PacMan;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Util;

namespace GhostManager.Ghost;

public class MonoGhost : MonoGameLibrary.Sprite.DrawableSprite
{
    private const float TurnAmount = .04f;
    private ConsoleGhost Ghost { get; set; }
    private GhostState _state { get; set; }
    private Vector2 _spawn { get; set; }

    public GhostState State
    {
        get => _state;
        set
        {
            if (Ghost.State != value)
            {
                _state = Ghost.State = value;
            }
        }
    }
    
    Texture2D ghostTexture, ghostHitTexture;
    
    public MonoGhost(Game game, Vector2 spawnLoc) : base(game)
    {
        Ghost = new ConsoleGhost((GameConsole)game.Services.GetService<IGameConsole>() ?? new GameConsole(game));
        _spawn = spawnLoc;
    }

    public override void Initialize()
    {
        ghostTexture = Game.Content.Load<Texture2D>("PurpleGhost");
        ghostHitTexture = Game.Content.Load<Texture2D>("GhostHit");
        // ReSharper disable PossibleLossOfFraction
        this.Origin = new Vector2(this.SpriteTexture.Width / 2, this.SpriteTexture.Height / 2);
        this.Location = _spawn;
        this.Speed = 200;
        this.State = GhostState.Evading;
        
        base.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
        UpdateTexture();
        Location += ((this.Direction * (lastUpdateTime / 1000)) * Speed);
        base.Update(gameTime);
    }

    public void UpdateGhostChasing(MonoGamePac player)
    {
        this.Direction.Y = player.Location.Y > this.Location.Y 
            ? MathHelper.Clamp(this.Direction.Y += TurnAmount, -1, 1) :
            MathHelper.Clamp(this.Direction.Y -= TurnAmount, -1, 1);

        this.Direction.X = player.Location.X > this.Direction.X 
            ? MathHelper.Clamp(this.Direction.X += TurnAmount, -1, 1) :
            MathHelper.Clamp(this.Direction.X -= TurnAmount, -1, 1);
    }
    
    public void UpdateGhostEvading(MonoGamePac player)
    {
        this.Location.Y = player.Location.Y > this.Location.Y ? -1 : 1;

        this.Location.X = player.Location.X > this.Location.X ? -1 : 1;
    }
    
    public void UpdateGhostRoving(MonoGamePac player)
    {
        //check if ghost can see pacman
        var p = new Vector2(this.Location.X, this.Location.Y);
        while (p.X < this.Game.GraphicsDevice.Viewport.Width &&
               p.X > 0 &&
               p.Y < this.Game.GraphicsDevice.Viewport.Height &&
               p.Y > 0)
        {
            if (player.LocationRect.Contains(new Point((int)p.X, (int)p.Y)))
            {
                this._state = GhostState.Chasing;
                break;
            }
            p += this.Direction;
        }
    }
    
    public void UpdateCollision(MonoGamePac player)
    {
        if (!this.Intersects(player)) return;
        if (this.PerPixelCollision(player))
        {
            this._state = GhostState.Dead;
        }
    }

    private void UpdateTexture()
    {
        switch (this._state)
        {
            case GhostState.Roving :
            case GhostState.Chasing:
                this.SpriteTexture = ghostTexture;
                break;
            case GhostState.Dead:
            case GhostState.Evading:
                this.SpriteTexture = ghostHitTexture;
                break;
        }
    }
}