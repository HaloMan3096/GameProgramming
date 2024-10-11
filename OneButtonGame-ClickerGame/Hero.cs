using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;

namespace MonoGame;

public class Hero : DrawableSprite
{
    public Hero(Game game) : base(game)
    {
    }
    
    protected override void LoadContent()
    {
        this.SpriteTexture = this.Game.Content.Load<Texture2D>("BlueSquare");
        this.Location = new Vector2(50, 50);
        base.LoadContent();
    }
}