@echo off
REM Remove last migration created
dotnet ef migrations remove  -p ..\InventoryX-CleanArquitecture.Infrastructure\ -s ..\InventoryX-CleanArquitecture.API\
