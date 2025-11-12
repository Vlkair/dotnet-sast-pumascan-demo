# SAST Analysis with PumaScan for .NET 🔍

> 🌐 **[Versión en español disponible aquí](README.md)**

This project demonstrates how to use **PumaScan** to detect security vulnerabilities in .NET code, specifically **SQL Injection**.

## 🎯 Objective

Show how PumaScan automatically detects security vulnerabilities during project compilation.

## 📋 Prerequisites

- .NET SDK 9.0 or higher
- PowerShell (Windows)

## 🚀 Quick Start

### Option 1: Use the automated script

```powershell
powershell -ExecutionPolicy Bypass -File .\scan.ps1
```

### Option 2: Manually

```powershell
cd PumaScanner
dotnet build
```

> **Note**: If you have PowerShell permission issues, use the full command with `-ExecutionPolicy Bypass`.

## 📝 What Does It Detect?

The `TestVuln.cs` file contains a **SQL Injection** vulnerability:

```csharp
// ⚠️ VULNERABLE: String concatenation without parameterization
string query = "SELECT * FROM Users WHERE Id = " + userInput;
SqlCommand cmd = new SqlCommand(query);
```

PumaScan will detect this and display:
- **Warning SEC0107**: SQL Injection - ADO.NET method is passed a dynamic SQL statement

## 🛠️ Project Configuration

The project includes the PumaScan analyzer as a NuGet package:

```xml
<PackageReference Include="Puma.Security.Rules" Version="2.4.11">
  <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
  <PrivateAssets>all</PrivateAssets>
</PackageReference>
```

## ✅ Recommended Solution

To fix the vulnerability, use parameterized queries:

```csharp
// ✅ SECURE: Parameterized query
string query = "SELECT * FROM Users WHERE Id = @UserId";
using (SqlCommand cmd = new SqlCommand(query, connection))
{
    cmd.Parameters.AddWithValue("@UserId", userInput);
    // ...
}
```

## 📚 Resources

- [PumaScan Official Site](https://www.pumascan.com/)
- [PumaScan Rules](https://www.pumascan.com/rules/)
- [OWASP SQL Injection](https://owasp.org/www-community/attacks/SQL_Injection)

## 📖 For Dev.to Article

### Commands to copy:

```bash
# Clone the repository
git clone https://github.com/YOUR-USERNAME/dotnet-sast-pumascan-demo.git
cd dotnet-sast-pumascan-demo

# Run analysis
powershell -ExecutionPolicy Bypass -File .\scan.ps1
```

## 🔍 Technical Details

- **Tool**: PumaScan (Roslyn Analyzer)
- **Language**: C# / .NET 9.0
- **Analysis Type**: SAST (Static Application Security Testing)
- **Vulnerability**: CWE-89 (SQL Injection)
- **Rule**: SEC0107

---

💡 **Tip**: PumaScan runs automatically during `dotnet build`, no additional tools needed.

## 📁 Project Structure

```
dotnet-sast-pumascan-demo/
├── PumaScanner/               # .NET Project with PumaScan
│   ├── PumaScanner.csproj    # Configuration with Puma.Security.Rules
│   ├── TestVuln.cs           # ⚠️ VULNERABLE Code (for demo)
│   └── SecureExample.cs      # ✅ SECURE Code (for comparison)
├── scan.ps1                   # Automated analysis script
├── README.md                  # Project documentation (Spanish)
├── README_EN.md              # Project documentation (English)
├── ARTICLE_GUIDE.md          # Complete guide for your article
├── READY_FOR_PUBLISH.md      # Publishing checklist (Spanish)
└── READY_FOR_PUBLISH_EN.md   # Publishing checklist (English)
```

## 🎯 Expected Output

When you run the scan, you should see:

```
======================================
 PumaScan - SAST Security Analysis
======================================

warning SEC0107: SQL Injection - ADO.NET method is passed a dynamic SQL statement.
(https://www.pumascan.com/rules/#sec0107-sql-injection-ado-net)

======================================
 Analysis Complete!
======================================

Look for security warnings above:
  - SEC0107: SQL Injection vulnerability
```

## 🤝 Contributing

Feel free to fork this project and use it for educational purposes. If you find any issues or have suggestions, please open an issue.

## 📄 License

This project is provided as-is for educational and demonstration purposes.

---

Made with ❤️ for the .NET Security community
