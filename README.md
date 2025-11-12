# Demo: Análisis SAST con PumaScan en .NET 🔍

Este proyecto demuestra cómo usar **PumaScan** para detectar vulnerabilidades de seguridad en código .NET, específicamente **inyección SQL**.

## 🎯 Objetivo

Mostrar cómo PumaScan detecta automáticamente vulnerabilidades de seguridad durante la compilación del proyecto.

## 📋 Requisitos Previos

- .NET SDK 9.0 o superior
- PowerShell (Windows)

## 🚀 Uso Rápido

### Opción 1: Usar el script automatizado

```powershell
.\scan.ps1
```

### Opción 2: Manualmente

```powershell
cd PumaScanner
dotnet build
```

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
.\scan.ps1
```

## 🔍 Detalles Técnicos

- **Herramienta**: PumaScan (Analizador de Roslyn)
- **Lenguaje**: C# / .NET 9.0
- **Tipo de Análisis**: SAST (Static Application Security Testing)
- **Vulnerabilidad**: CWE-89 (SQL Injection)
- **Regla**: SEC0107

---

💡 **Tip**: PumaScan se ejecuta automáticamente durante `dotnet build`, no necesitas herramientas adicionales.
