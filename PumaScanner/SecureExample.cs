// SecureExample.cs - Código corregido para comparación
using System;
using Microsoft.Data.SqlClient;

public class SecureClass
{
    /// <summary>
    /// Ejemplo de consulta SQL segura usando parámetros
    /// </summary>
    public void SafeQuery(string userInput, string connectionString)
    {
        // ✅ SEGURO: Consulta parametrizada
        string query = "SELECT * FROM Users WHERE Id = @UserId";
        
        using (SqlConnection connection = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, connection))
        {
            // Usar parámetros en vez de concatenar strings
            cmd.Parameters.AddWithValue("@UserId", userInput);
            
            connection.Open();
            
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"User ID: {reader["Id"]}");
                    Console.WriteLine($"User Name: {reader["Name"]}");
                }
            }
        }
    }

    /// <summary>
    /// Ejemplo con múltiples parámetros
    /// </summary>
    public void SafeQueryMultipleParams(string name, string email, string connectionString)
    {
        string query = @"
            SELECT * FROM Users 
            WHERE Name = @Name 
            AND Email = @Email";
        
        using (SqlConnection connection = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, connection))
        {
            // Agregar múltiples parámetros
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Email", email);
            
            connection.Open();
            
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                // Procesar resultados...
            }
        }
    }

    /// <summary>
    /// Ejemplo con StoredProcedure (aún más seguro)
    /// </summary>
    public void SafeStoredProcedure(string userId, string connectionString)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("GetUserById", connection))
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", userId);
            
            connection.Open();
            
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                // Procesar resultados...
            }
        }
    }
}

/*
 * DIFERENCIAS CLAVE:
 * 
 * ❌ INSEGURO (TestVuln.cs):
 *    string query = "SELECT * FROM Users WHERE Id = " + userInput;
 *    - Concatenación directa
 *    - Vulnerable a SQL Injection
 *    
 * ✅ SEGURO (Este archivo):
 *    string query = "SELECT * FROM Users WHERE Id = @UserId";
 *    cmd.Parameters.AddWithValue("@UserId", userInput);
 *    - Usa parámetros
 *    - El driver escapa automáticamente caracteres peligrosos
 *    - Imposible inyectar código SQL
 */
