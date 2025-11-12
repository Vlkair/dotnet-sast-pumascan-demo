# scan.ps1 - PumaScan SAST Analysis Script
Write-Host "======================================" -ForegroundColor Cyan
Write-Host " PumaScan - SAST Security Analysis" -ForegroundColor Cyan
Write-Host "======================================" -ForegroundColor Cyan
Write-Host ""

# Build the project with PumaScan analysis
Set-Location PumaScanner
dotnet clean | Out-Null
dotnet build

Write-Host ""
Write-Host "======================================" -ForegroundColor Green
Write-Host " Analysis Complete!" -ForegroundColor Green
Write-Host "======================================" -ForegroundColor Green
Write-Host ""
Write-Host "Look for security warnings above:" -ForegroundColor Yellow
Write-Host "  - SEC0107: SQL Injection vulnerability" -ForegroundColor Yellow
Write-Host ""
Set-Location ..
