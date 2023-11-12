@echo off
REM Receive the name of new migration as argument
dotnet ef migrations add %1 -p ..\InventoryX-CleanArquitecture.Infrastructure\ -s ..\InventoryX-CleanArquitecture.API\ -o Persistence\Migrations
