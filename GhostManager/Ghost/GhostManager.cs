using GhostManager.PacMan;
using Microsoft.Xna.Framework;

namespace GhostManager.Ghost;

public class GhostManager : GameComponent
{
    private const int numGhosts = 4;
    MonoGhost[] ghosts;

    private readonly MonoGamePac player;
    
    public GhostManager(Game game, MonoGamePac pac) : base(game)
    {
        player = pac;
        ghosts = new MonoGhost[numGhosts];
        for (int i = 0; i < numGhosts; i++)
        {
            ghosts[i] = new MonoGhost(game, new Vector2(0, 0));
            ghosts[i].Initialize();
        }
    }

    public override void Update(GameTime gameTime)
    {
        foreach (MonoGhost ghost in ghosts)
        {
            MoveGhostBasedOnState(ghost);
            ghost.UpdateCollision(player);
            
            ghost.Update(gameTime);
        }
        
        base.Update(gameTime);
    }

    private void MoveGhostBasedOnState(MonoGhost ghost)
    {
        switch (ghost.State)
        {
            case GhostState.Chasing:
                ghost.UpdateGhostChasing(player);
                break;
            case GhostState.Evading:
                ghost.UpdateGhostEvading(player);
                break;
            case GhostState.Roving:
                ghost.UpdateGhostRoving(player);
                break;
            case GhostState.Dead:
                break;
        }
    }
}