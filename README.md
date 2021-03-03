# Gurps Companion
- A web application for the gamemaster and the players to store data und supply some convenience functions.  
This applies to the pen and paper game GURPS. 
- **Beware I applied a custom ruleset**, which alters the way of how initiative is calculated and applied. Furthermore FP do not exist, instead ST is used by half the FP value. To compensate this VP is a new stat, which replenishs HP and ST according to the appropriate reg-stats.

## Commands
Migrate existing Database with Scaffold-Database:

```
Scaffold-DbContext "DataSource=C:\Users\Leckmich\Desktop\Repos\GurpsCompanion\data.sqlite3" Microsoft.EntityFrameworkCore.Sqlite -OutputDir DataContext -Force -Context DataContext
```
## Legal Notice

GURPS is a trademark of Steve Jackson Games, and its rules and art are copyrighted by Steve Jackson Games. All rights are reserved by Steve Jackson Games. This game aid is a original creation and is released for free distribution, and not for resale, under the permissions granted in the <a href="http://www.sjgames.com/general/online_policy.html">Steve Jackson Games Online Policy</a>.
