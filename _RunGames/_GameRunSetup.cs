
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


public abstract class _GameRunSetup
{
    protected KeyboardState kstate;
    protected MouseState mstate;
    protected GraphicsDeviceManager _graphics;
    protected SpriteBatch _spriteBatch;
    protected Camera2D camera2D;
    protected Texture2D texture;
    public bool Exit = false;
    public _GameRunSetup(GraphicsDeviceManager _graphics, SpriteBatch _spriteBatch, Camera2D camera2D, Texture2D texture)
    {
        this._graphics = _graphics;
        this._spriteBatch = _spriteBatch;
        this.camera2D = camera2D;
        this.texture = texture;
    }
    
    
    public abstract void Update(GameTime gameTime);
    public abstract void Draw();
}
