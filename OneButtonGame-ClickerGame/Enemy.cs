using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;

namespace MonoGame;

public class Enemy : DrawableSprite
{
    private int _health;
    
    public Enemy(Game game) : base(game)
    {
        _health = 100;
    }
    
    protected override void LoadContent()
    {
        this.SpriteTexture = this.Game.Content.Load<Texture2D>("RedSquare");
        this.Location = new Vector2(150, 150);
        base.LoadContent();
    }

    public int GetHealth()
    {
        return _health;
    }
    
    public void TakeDamage(int damage)
    {
        _health -= damage;
    }
}