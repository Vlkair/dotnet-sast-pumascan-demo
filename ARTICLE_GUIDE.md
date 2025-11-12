# Guía para el Artículo en Dev.to 📝

## Estructura Recomendada del Artículo

### 1. Introducción
```markdown
# Detectando Vulnerabilidades SQL Injection con PumaScan en .NET

¿Sabías que puedes detectar vulnerabilidades de seguridad en tu código .NET automáticamente durante la compilación? En este artículo te mostraré cómo usar **PumaScan**, una herramienta de análisis estático (SAST) que se integra directamente en tu proceso de build.
```

### 2. ¿Qué es SAST?
- **SAST** (Static Application Security Testing): Análisis de código estático sin ejecutar la aplicación
- Detecta vulnerabilidades temprano en el ciclo de desarrollo
- Se integra en el proceso de build

### 3. Configuración del Proyecto

#### Captura de pantalla #1: Estructura del proyecto
Mostrar:
```
dotnet-sast-pumascan-demo/
├── PumaScanner/
│   ├── PumaScanner.csproj
│   └── TestVuln.cs
├── scan.ps1
└── README.md
```

#### Código a mostrar: PumaScanner.csproj
```xml
<PackageReference Include="Puma.Security.Rules" Version="2.4.11">
  <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
  <PrivateAssets>all</PrivateAssets>
</PackageReference>
```

### 4. Código Vulnerable

#### Captura de pantalla #2: TestVuln.cs
```csharp
using System;
using System.Data.SqlClient;

public class VulnerableClass
{
    public void UnsafeQuery(string userInput)
    {
        // ⚠️ SQL Injection vulnerability
        string query = "SELECT * FROM Users WHERE Id = " + userInput;
        SqlCommand cmd = new SqlCommand(query);
    }
}
```

**Explicar por qué es vulnerable:**
- Concatenación directa de entrada del usuario
- Sin validación ni parametrización
- Permite inyectar comandos SQL maliciosos

**Ejemplo de ataque:**
```sql
-- Si userInput = "1 OR 1=1"
SELECT * FROM Users WHERE Id = 1 OR 1=1
-- Retorna TODOS los usuarios
```

### 5. Ejecutando el Análisis

#### Captura de pantalla #3: Terminal ejecutando scan.ps1
```powershell
.\scan.ps1
```

#### Captura de pantalla #4: Resultado mostrando SEC0107
Resaltar la línea:
```
warning SEC0107: SQL Injection - ADO.NET method is passed a dynamic SQL statement.
(https://www.pumascan.com/rules/#sec0107-sql-injection-ado-net)
```

### 6. Entendiendo el Warning

**SEC0107**: SQL Injection en ADO.NET
- **Severidad**: Alta
- **CWE**: CWE-89
- **OWASP**: A03:2021 – Injection

### 7. Solución: Código Seguro

#### Captura de pantalla #5: Código corregido
```csharp
using System;
using Microsoft.Data.SqlClient;

public class SecureClass
{
    public void SafeQuery(string userInput, string connectionString)
    {
        // ✅ Consulta parametrizada - SEGURO
        string query = "SELECT * FROM Users WHERE Id = @UserId";
        
        using (SqlConnection connection = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, connection))
        {
            // Parámetro en vez de concatenación
            cmd.Parameters.AddWithValue("@UserId", userInput);
            
            connection.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"User: {reader[0]}");
                }
            }
        }
    }
}
```

### 8. Ventajas de PumaScan

- ✅ Se ejecuta automáticamente en cada build
- ✅ No requiere configuración compleja
- ✅ Detecta múltiples tipos de vulnerabilidades (XSS, CSRF, etc.)
- ✅ Integración con Visual Studio y VS Code
- ✅ Gratis y open source

### 9. Otros Warnings que Detecta

- **SEC0108**: LDAP Injection
- **SEC0109**: XPath Injection
- **SEC0116**: CSRF (Cross-Site Request Forgery)
- **SEC0102**: Insecure Cookie Configuration
- Y más de 30 reglas...

### 10. Conclusión

```markdown
PumaScan es una herramienta poderosa que debería estar en todo proyecto .NET. 
Con solo agregar un paquete NuGet, obtienes análisis de seguridad automático 
en cada compilación.

## Recursos
- [Código completo en GitHub](tu-repo-url)
- [PumaScan Documentation](https://www.pumascan.com/)
- [OWASP Top 10](https://owasp.org/Top10/)

¿Usas alguna otra herramienta SAST? ¡Déjame saber en los comentarios! 👇
```

## 📸 Capturas de Pantalla Necesarias

1. **Estructura del proyecto** en VS Code
2. **Archivo TestVuln.cs** con el código vulnerable
3. **Terminal ejecutando** `.\scan.ps1`
4. **Resultado mostrando** el warning SEC0107 (resaltado)
5. **Código corregido** con parametrización

## 🎨 Elementos Visuales Sugeridos

- Emoji de escudo 🛡️ para seguridad
- Emoji de advertencia ⚠️ para vulnerabilidades
- Emoji de check ✅ para código seguro
- Colores: Rojo para vulnerable, Verde para seguro

## 📊 Datos a Incluir

- **Tiempo de setup**: ~2 minutos
- **Líneas de configuración**: 1 PackageReference
- **Tipos de vulnerabilidades detectadas**: 30+
- **Costo**: $0 (gratis)

## 🔗 Links Útiles

- Repositorio de ejemplo: (tu GitHub)
- PumaScan Rules: https://www.pumascan.com/rules/
- CWE-89: https://cwe.mitre.org/data/definitions/89.html
- OWASP SQL Injection: https://owasp.org/www-community/attacks/SQL_Injection

---

## 💡 Tips para el Artículo

1. **Usa bloques de código con sintaxis highlighting**
2. **Incluye GIFs** si puedes (ejecutando el scan)
3. **Hashtags sugeridos**: #dotnet #csharp #security #sast #devsecops
4. **Longitud ideal**: 5-8 minutos de lectura
5. **Call to action**: Invita a probar el código del repo

¡Buena suerte con tu artículo! 🚀
