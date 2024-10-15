using Microsoft.Xna.Framework;
using MonoGameLibrary.Util;

namespace MonoGame;

public class ConsoleHero : Hero
{
    GameConsole console;
    public ConsoleHero()
    {
        console = null;
    }

    public ConsoleHero(GameConsole console)
    {
        this.console = console;
    }
    
    public override void Log(string s)
    {
        if (console != null)
        {
            console.GameConsoleWrite(s);
        }
        else
        {
            base.Log(s);
        }
    }
}