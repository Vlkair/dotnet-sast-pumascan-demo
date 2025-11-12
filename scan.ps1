# scan.ps1
Write-Host "🚀 Running Puma Scan on current folder..."
puma -s . -o puma-report.html
Write-Host "✅ Report saved as puma-report.html"
