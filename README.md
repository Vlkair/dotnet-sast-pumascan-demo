# Demo: Análisis SAST con PumaScan en .NET 🔍

> 🌐 **[English version available here](README_EN.md)**

Este proyecto demuestra cómo usar **PumaScan** para detectar vulnerabilidades de seguridad en código .NET, específicamente **inyección SQL**.

## 🎯 Objetivo

Mostrar cómo PumaScan detecta automáticamente vulnerabilidades de seguridad durante la compilación del proyecto.

## 📋 Requisitos Previos

- .NET SDK 9.0 o superior
- PowerShell (Windows)

## 🚀 Uso Rápido

### Opción 1: Usar el script automatizado

```powershell
powershell -ExecutionPolicy Bypass -File .\scan.ps1
```

### Opción 2: Manualmente

```powershell
cd PumaScanner
dotnet build
```

> **Nota**: Si tienes problemas de permisos con PowerShell, usa el comando completo con `-ExecutionPolicy Bypass`.

## 📝 ¿Qué detecta?

El archivo `TestVuln.cs` contiene una vulnerabilidad de **SQL Injection**:

```csharp
// ⚠️ VULNERABLE: Concatenación de strings sin parametrizar
string query = "SELECT * FROM Users WHERE Id = " + userInput;
SqlCommand cmd = new SqlCommand(query);
```

PumaScan detectará esto y mostrará:
- **Warning SEC0107**: SQL Injection - ADO.NET method is passed a dynamic SQL statement

## 🛠️ Configuración del Proyecto

El proyecto incluye el analizador PumaScan como paquete NuGet:

```xml
<PackageReference Include="Puma.Security.Rules" Version="2.4.11">
  <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
  <PrivateAssets>all</PrivateAssets>
</PackageReference>
```

## ✅ Solución Recomendada

Para corregir la vulnerabilidad, usa consultas parametrizadas:

```csharp
// ✅ SEGURO: Consulta parametrizada
string query = "SELECT * FROM Users WHERE Id = @UserId";
using (SqlCommand cmd = new SqlCommand(query, connection))
{
    cmd.Parameters.AddWithValue("@UserId", userInput);
    // ...
}
```

## 📚 Recursos

- [PumaScan Official Site](https://www.pumascan.com/)
- [PumaScan Rules](https://www.pumascan.com/rules/)
- [OWASP SQL Injection](https://owasp.org/www-community/attacks/SQL_Injection)

## 📖 Para el Artículo en Dev.to

### Comandos para copiar:

```bash
# Clonar el repositorio
git clone https://github.com/TU-USUARIO/dotnet-sast-pumascan-demo.git
cd dotnet-sast-pumascan-demo

# Ejecutar análisis
powershell -ExecutionPolicy Bypass -File .\scan.ps1
```

## 🔍 Detalles Técnicos

- **Herramienta**: PumaScan (Analizador de Roslyn)
- **Lenguaje**: C# / .NET 9.0
- **Tipo de Análisis**: SAST (Static Application Security Testing)
- **Vulnerabilidad**: CWE-89 (SQL Injection)
- **Regla**: SEC0107

## 📁 Estructura del Proyecto

```
dotnet-sast-pumascan-demo/
├── PumaScanner/               # Proyecto .NET con PumaScan
│   ├── PumaScanner.csproj    # Configuración con Puma.Security.Rules
│   ├── TestVuln.cs           # ⚠️ Código VULNERABLE (para demo)
│   └── SecureExample.cs      # ✅ Código SEGURO (para comparación)
├── scan.ps1                   # Script de análisis automatizado
├── README.md                  # Documentación del proyecto (Español)
├── README_EN.md              # Documentación del proyecto (English)
├── ARTICLE_GUIDE.md          # Guía completa para tu artículo
├── READY_FOR_PUBLISH.md      # Checklist de publicación (Español)
└── READY_FOR_PUBLISH_EN.md   # Checklist de publicación (English)
```

## 🎯 Salida Esperada

Cuando ejecutes el scan, deberías ver:

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

---

💡 **Tip**: PumaScan se ejecuta automáticamente durante `dotnet build`, no necesitas herramientas adicionales.
