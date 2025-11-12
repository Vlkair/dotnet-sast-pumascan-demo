// TestVuln.cs - Vulnerable code for SAST demo
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
