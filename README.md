# Gurps Companion
A web application for the gamemaster and the players to store data und supply some convenience functions.  
This applies to the pen and paper game GURPS. 

## Commands
Migrate existing Database with Scaffold-Database:

```
Scaffold-DbContext "DataSource=C:\Users\Leckmich\Desktop\Repos\GurpsCompanion\data.sqlite3" Microsoft.EntityFrameworkCore.Sqlite -OutputDir DataContext -Force -Context DataContext
```

Refresh Browser after rebuild from `dotnet watch run` with the npm package browser-sync:

`browser-sync start --proxy http://localhost:5000 --files '**/*.CopyComplete'`