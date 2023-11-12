@echo off
REM Apply last migration created
dotnet ef database update -s ..\InventoryX-CleanArquitecture.API\ -p ..\InventoryX-CleanArquitecture.Infrastructure
