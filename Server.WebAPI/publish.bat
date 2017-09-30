taskkill /f /im w3wp.exe
taskkill /f /im dotnet.exe
rmdir /s /q "C:\EverMoneyBuild\"
mkdir C:\EverMoneyBuild\
set CsProj="%0%\..\Server.WebApi.csproj"
dotnet publish %CsProj% -f netcoreapp2.0 -c Debug -o C:\EverMoneyBuild -v m
mkdir C:\EverMoneyBuild\Logs
wget "https://localhost:5001/api/health" --no-check-certificate
pause