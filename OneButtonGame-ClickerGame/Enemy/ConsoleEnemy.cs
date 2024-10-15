using MonoGameLibrary.Util;

namespace MonoGame;

// This is really the same as console hero maybe toss it in its own class?
public class ConsoleEnemy : Enemy
{
    GameConsole console;

    public ConsoleEnemy()
    {
        console = null;
    }

    public ConsoleEnemy(GameConsole console)
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