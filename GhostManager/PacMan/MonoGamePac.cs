using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.GameComponents.Player;
using MonoGameLibrary.Util;

namespace GhostManager.PacMan;

public class MonoGamePac : MonoGameLibrary.Sprite.DrawableSprite
{
    internal ConsolePac pac { get; private set; }
    private PlayerController controller;
    private Texture2D pacTexture, deadPacTexture;
    protected PacState pacState { get; private set; }

    public PacState PacState
    {
        get => pacState;
        set
        {
            if (pac.State != value)
            {
                pacState = pac.State = value;
            }
        }
    }
    
    public MonoGamePac(Game game) : base(game)
    {
        pac = new ConsolePac((GameConsole)game.Services.GetService<IGameConsole>() ?? new GameConsole(game));
        controller = new PlayerController(game);
    }

    public override void Initialize()
    {
        base.Initialize();
        pacTexture = Game.Content.Load<Texture2D>("pacManSingle");
        deadPacTexture = Game.Content.Load<Texture2D>("20px_1trans");
        this.spriteTexture = pacTexture;
        this.Origin = new Vector2(this.SpriteTexture.Width / 2, this.SpriteTexture.Height / 2);
        this.Location = new Microsoft.Xna.Framework.Vector2(100, 100);
        this.Speed = 200;
        this.pacState = PacState.Chomping;
    }

    public override void Update(GameTime gameTime)
    {
        //Elapsed time since last update
        var time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        this.controller.Update(gameTime);

        movePac(time);
        ChangePacState();
        KeepPacOnScreen();

        base.Update(gameTime);
    }

    private void movePac(float time)
    {
        this.Location += ((this.controller.Direction * (time / 1000)) * Speed);      //Simple Move 
        this.Rotate = this.controller.Rotate;
    }

    private void ChangePacState()
    {
        if (this.controller.hasInputForMoverment)
        {
            if (pacState != PacState.Spawning && pacState != PacState.SuperPac)
                this.PacState = PacState.Chomping;
        }
        else
        {
            if (pacState != PacState.Spawning && pacState != PacState.SuperPac)
                this.PacState = PacState.Still;
        }
    }

    private void KeepPacOnScreen()
    {
        if (this.Location.X > Game.GraphicsDevice.Viewport.Width - (this.spriteTexture.Width / 2))
        {
            this.Location.X = Game.GraphicsDevice.Viewport.Width - (this.spriteTexture.Width / 2);
        }
        if (this.Location.X < (this.spriteTexture.Width / 2))
            this.Location.X = (this.spriteTexture.Width / 2);

        if (this.Location.Y > Game.GraphicsDevice.Viewport.Height - (this.spriteTexture.Height / 2))
            this.Location.Y = Game.GraphicsDevice.Viewport.Height - (this.spriteTexture.Height / 2);

        if (this.Location.Y < (this.spriteTexture.Height / 2))
            this.Location.Y = (this.spriteTexture.Height / 2);
    }
}