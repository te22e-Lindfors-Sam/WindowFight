using System.Diagnostics;
using System.Numerics;
using Raylib_cs;



int windowY = 1000;
int windowX = 1000;
int moveSpeed = 5;

Raylib.SetWindowPosition(1920 / 2, 1080 / 2);
Raylib.InitWindow(windowX, windowY, "");
Raylib.SetTargetFPS(60);

Vector2 playerPos = new Vector2(500, 500);

List<Bullet> bullets = new List<Bullet>();

Texture2D gun = Raylib.LoadTexture("gun.png");

while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();

    windowY -= 2;
    windowX -= 2;
    int added = 0;



    if (windowX < 150 || windowY < 150)
    {
        break;
    }

    //Player controls
    if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
    {
        playerPos.Y -= moveSpeed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
    {
        playerPos.Y += moveSpeed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
    {
        playerPos.X -= moveSpeed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
    {
        playerPos.X += moveSpeed;
    }

    Vector2 mousePos = Raylib.GetMousePosition();

    Raylib.DrawCircleLines((int)playerPos.X, (int)playerPos.Y, 20, Color.WHITE);

    float xDistance = playerPos.X - mousePos.X;
    float yDistance = playerPos.Y - mousePos.Y;

    float rotation = MathF.Atan(xDistance / yDistance) * 180 / MathF.PI * -1;

    double dir = MathF.Atan2(playerPos.Y - mousePos.Y, mousePos.X - playerPos.Y) * 180.0 / MathF.PI * -1 - 90;
    Raylib.DrawTextureEx(gun, new Vector2((int)playerPos.X, (int)playerPos.Y), (float)dir, 1f, Color.WHITE);

    if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
    {
        Bullet bullet = new Bullet();
        bullet.x = (int)playerPos.X;
        bullet.y = (int)playerPos.Y;
        double velocity_x = 5 * MathF.Cos((float)(dir * -1 - 90) * MathF.PI / 180.0f);
        double velocity_y = -5 * MathF.Sin((float)(dir * -1 - 90) * MathF.PI / 180.0f);
        Console.WriteLine(velocity_x);
        bullet.velocity = new Vector2((float)velocity_x, (float)velocity_y);
        bullets.Add(bullet);

    }


    for (int i = 0; i < bullets.Count; i++)
    {
        bullets[i].x += (int)bullets[i].velocity.X;
        bullets[i].y += (int)bullets[i].velocity.Y;

        Raylib.DrawCircle(bullets[i].x, bullets[i].y, 5, Color.RED);

        if (bullets[i].x <= 0)
        {
            windowX += 50;
            added = 0;
            bullets.Remove(bullets[i]);
            continue;
        }
        if (bullets[i].x >= windowX)
        {
            windowX += 50;
            added = -50;
            bullets.Remove(bullets[i]);
            continue;
        }
        if (bullets[i].y <= 0)
        {
            windowY += 50;
            added = 0;
            bullets.Remove(bullets[i]);
            continue;
        }
        if (bullets[i].y >= windowY)
        {
            windowY += 50;
            added = -50;
            bullets.Remove(bullets[i]);
            continue;
        }
    }

    Raylib.SetWindowSize(windowX, windowY);
    Vector2 windowPos = Raylib.GetWindowPosition();
    Raylib.SetWindowPosition((int)windowPos.X + 1 + added, (int)windowPos.Y + 1 + added);
    Raylib.ClearBackground(Color.BLACK);



    Raylib.EndDrawing();
}

Raylib.CloseWindow(); // Close the window when done

Raylib.UnloadTexture(gun);