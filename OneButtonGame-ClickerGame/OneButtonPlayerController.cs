using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;

namespace MonoGame
{
    public class OneButtonPlayerController : GameComponent
    {
        private readonly InputHandler inputHandler;
        private readonly Keys controllerKey;

        public bool IsKeyPressed { get; private set; }

        public OneButtonPlayerController(Game game) : base(game)
        {
            inputHandler = (InputHandler)game.Services.GetService<IInputHandler>();
            if(inputHandler == null)
            {
                inputHandler = new InputHandler(game);
                game.Components.Add(inputHandler);
            }

            controllerKey = Keys.E;
        }


        public override void Update(GameTime gameTime)
        {
            IsKeyPressed = inputHandler.KeyboardState.IsKeyDown(controllerKey);
            base.Update(gameTime);
        }

    }
}
