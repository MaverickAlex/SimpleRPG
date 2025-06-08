using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SimpleRPG;

public class Game1 : Game
{
  private GraphicsDeviceManager _graphics;
  private SpriteBatch _spriteBatch;

  private Texture2D _playerSprite;
  private Texture2D _walkDown;
  private Texture2D _walkLeft;
  private Texture2D _walkRight;
  private Texture2D _walkUp;

  private Texture2D _background;
  private Texture2D _ball;
  private Texture2D _skull;
  public Game1()
  {
    _graphics = new GraphicsDeviceManager(this);
    Content.RootDirectory = "Content";
    IsMouseVisible = true;
  }

  protected override void Initialize()
  {
    _graphics.PreferredBackBufferWidth = 1280;
    _graphics.PreferredBackBufferHeight = 720;
    _graphics.ApplyChanges();

    base.Initialize();
  }

  protected override void LoadContent()
  {
    _spriteBatch = new SpriteBatch(GraphicsDevice);

    _playerSprite = Content.Load<Texture2D>("Player/player");
    _walkDown = Content.Load<Texture2D>("Player/walkDown");
    _walkLeft = Content.Load<Texture2D>("Player/walkLeft");
    _walkRight = Content.Load<Texture2D>("Player/walkRight");
    _walkUp = Content.Load<Texture2D>("Player/walkUp");
    _background = Content.Load<Texture2D>("background");
    _ball = Content.Load<Texture2D>("ball");
    _skull = Content.Load<Texture2D>("skull");
  }

  protected override void Update(GameTime gameTime)
  {
    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
      Exit();

    // TODO: Add your update logic here

    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime)
  {
    GraphicsDevice.Clear(Color.CornflowerBlue);

    _spriteBatch.Begin();
    _spriteBatch.Draw(_background, new Vector2(-500, -500), Color.White);
    _spriteBatch.End();

    base.Draw(gameTime);
  }
}
