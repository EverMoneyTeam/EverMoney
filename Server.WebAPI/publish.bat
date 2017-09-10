taskkill /f /im w3wp.exe
rmdir /s /q "C:\EverMoneyBuild\"
mkdir C:\EverMoneyBuild\
set CsProj="%0%\..\Server.WebApi.csproj"
dotnet publish %CsProj% -f netcoreapp2.0 -c Debug -o C:\EverMoneyBuild -v m
mkdir C:\EverMoneyBuild\Logs
pause