# .NET SAST Demo with Puma Scan

This repository contains a minimal C# file with a SQL injection vulnerability, used to demonstrate static analysis with Puma Scan.

## How to scan
Run this command in PowerShell:
```powershell
puma -s . -o report.html
