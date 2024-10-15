using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;

namespace MonoGame;

public enum HeroState
{
    Idle,
    Attacking
}
public class Hero
{
    private HeroState _state;
    public HeroState State
    {
        get { return _state; }
        set
        {
            if (_state != value)
            {
                Console.WriteLine(string.Format("{0}'s state was: {1} now is: {2}", this.ToString(), _state, value));
                _state = value;
            }
        } 
    }

    protected Hero()
    {
        this.State = HeroState.Idle;
    }

    public void Attack()
    {
        this.State = HeroState.Attacking;
    }

    public void Idle()
    {
        this.State = HeroState.Idle;
    }
    
    public virtual void Log(string s)
    {
        Console.WriteLine(s);
    }
}