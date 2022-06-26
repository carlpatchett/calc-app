using CalculatorApp.Core;
using CalculatorApp.Core.Interfaces;
using Microsoft.Data.SqlClient;

namespace CalculatorApp.Tests
{
    internal class MockDiagnosticsWriterWithDatabaseStorage : IDiagnosticsWriterWithDatabaseStorage
    {
        private readonly string mDiagnosticsTableName = "DiagnosticsStorage";
        private readonly string mConnectionString = "Server=localhost;Database=CalculatorAppDiagnostics;Integrated Security=True;TrustServerCertificate=Yes;";

        public void WriteToDebugger(Operation operation)
        {
            this.StoreOperation(this, operation);
        }

        public void EnsureDb()
        {
            using (var connection = new SqlConnection("Server=localhost;Database=master;Integrated Security=True;TrustServerCertificate=Yes;"))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = "IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE [name] = N'CalculatorAppDiagnostics') CREATE DATABASE CalculatorAppDiagnostics;";
                cmd.ExecuteNonQuery();
            }
            using (var connection = new SqlConnection("Server=localhost;Database=CalculatorAppDiagnostics;Integrated Security=True;TrustServerCertificate=Yes;"))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = $"IF OBJECT_ID(N'[dbo].[{mDiagnosticsTableName}]', N'U') IS NULL CREATE TABLE [dbo].[DiagnosticsStorage] ( ID int IDENTITY(1, 1) PRIMARY KEY, Operator varchar(255), X int, Y int, Result float(53) )";
                cmd.ExecuteNonQuery();
            }
        }

        public Operation RetrieveLatestOperation()
        {
            this.EnsureDb();

            object? operation = null;
            using (var connection = new SqlConnection(mConnectionString))
            {
                connection.Open();

                using var cmd = connection.CreateCommand();
                cmd.CommandText = $"SELECT TOP(1) * FROM dbo.{mDiagnosticsTableName} ORDER BY ID DESC";

                cmd.ExecuteNonQuery();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var @operator = (ArithmeticOperators)Enum.Parse(typeof(ArithmeticOperators), reader[1].ToString());
                    var x = int.Parse(reader[2].ToString());
                    var y = int.Parse(reader[3].ToString());
                    var result = float.Parse(reader[4].ToString());

                    operation = new Operation(@operator, x, y, result);
                }

                reader.Close();
            }

            if (operation == null)
            {
                throw new NotImplementedException();
            }

            return (Operation)operation;
        }

        public void StoreOperation(object? sender, Operation operation)
        {
            this.EnsureDb();

            using (var connection = new SqlConnection(mConnectionString))
            {
                connection.Open();

                using var cmd = connection.CreateCommand();
                cmd.CommandText = $"INSERT INTO [{mDiagnosticsTableName}]( [Operator],[X],[Y],[Result] ) VALUES (@operator, @x, @y, @result)";
                CreateAndAddParameter(cmd, "@operator", @operation.@operator.ToString());
                CreateAndAddParameter(cmd, "@x", @operation.x);
                CreateAndAddParameter(cmd, "@y", @operation.y);
                CreateAndAddParameter(cmd, "@result", @operation.result);

                cmd.ExecuteNonQuery();
            }
        }

        private void CreateAndAddParameter(SqlCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }
    }
}
