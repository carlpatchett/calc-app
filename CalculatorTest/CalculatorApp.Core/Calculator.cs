using CalculatorApp.Core.Interfaces;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace CalculatorApp.Core
{
    /// <summary>
    /// A calculator capable of basic arithmetic.
    /// </summary>
    public class Calculator : ISimpleCalculator, IDiagnosticsWriterWithDatabaseStorage
    {
        private readonly string mDiagnosticsTableName = "DiagnosticsStorage";
        private readonly string mConnectionString = "Server=localhost;Database=CalculatorAppDiagnostics;Integrated Security=True;TrustServerCertificate=Yes;";

        /// <summary>
        /// Adds two numbers together.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number</param>
        /// <returns>The result of adding both numbers together.</returns>
        public int Add(int x, int y)
        {
            var result = x + y;
            this.WriteToDebugger(new Operation(ArithmeticOperators.Add, x, y, result));
            return result;
        }

        /// <summary>
        /// Subtracts two numbers from eachother.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <returns>The result of subtracting the first number from the second.</returns>
        public int Subtract(int x, int y)
        {
            var result = x - y;
            this.WriteToDebugger(new Operation(ArithmeticOperators.Subtract, x, y, result));
            return result;
        }

        /// <summary>
        /// Multiplies two numbers by eachother.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <returns>The result of multiplying the first number by the second number.</returns>
        public int Multiply(int x, int y)
        {
            var result = x * y;
            this.WriteToDebugger(new Operation(ArithmeticOperators.Multiply, x, y, result));
            return result;
        }

        /// <summary>
        /// Divides two numbers by eachother.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <returns>The result of dividing the first number by the second number.</returns>
        public float Divide(int x, int y)
        {
            var result = x / y;
            this.WriteToDebugger(new Operation(ArithmeticOperators.Divide, x, y, result));
            return result;
        }

        public void WriteToDebugger(Operation operation)
        {
            Debug.Write(operation.@operator, "Operator");
            Debug.Write(operation.x, "X");
            Debug.Write(operation.y, "Y");
            Debug.Write(operation.result, "Result");

            this.Store(operation);
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

        public Operation? GetLatest()
        {
            this.EnsureDb();

            Operation? operation = null;
            using (var connection = new SqlConnection(mConnectionString))
            {
                connection.Open();

                using var cmd = connection.CreateCommand();
                cmd.CommandText = $"SELECT TOP(1) * FROM dbo.{mDiagnosticsTableName} ORDER BY ID DESC";

                cmd.ExecuteNonQuery();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    operation = ConstructOperation(reader);
                }

                reader.Close();
            }

            if (operation == null)
            {
                throw new NotImplementedException();
            }

            return (Operation)operation;
        }

        public IEnumerable<Operation> Get()
        {
            this.EnsureDb();

            var operations = new List<Operation>();
            using (var connection = new SqlConnection(mConnectionString))
            {
                connection.Open();

                using var cmd = connection.CreateCommand();
                cmd.CommandText = $"SELECT * FROM dbo.{mDiagnosticsTableName}";

                cmd.ExecuteNonQuery();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    operations.Add(ConstructOperation(reader));
                }

                reader.Close();
            }

            return operations;
        }

        public Operation? Get(int id)
        {
            this.EnsureDb();

            Operation? operation = null;
            using (var connection = new SqlConnection(mConnectionString))
            {
                connection.Open();

                using var cmd = connection.CreateCommand();
                cmd.CommandText = $"SELECT * FROM dbo.{mDiagnosticsTableName} WHERE Id=@id";
                this.CreateAndAddParameter(cmd, "@id", id);

                cmd.ExecuteNonQuery();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    operation = ConstructOperation(reader);
                }

                reader.Close();
            }

            return operation;
        }

        public void Store(Operation operation)
        {
            this.EnsureDb();

            using (var connection = new SqlConnection(mConnectionString))
            {
                connection.Open();

                using var cmd = connection.CreateCommand();
                cmd.CommandText = $"INSERT INTO [{mDiagnosticsTableName}]( [Operator],[X],[Y],[Result] ) VALUES (@operator, @x, @y, @result)";
                this.CreateAndAddParameter(cmd, "@operator", @operation.@operator.ToString());
                this.CreateAndAddParameter(cmd, "@x", @operation.x);
                this.CreateAndAddParameter(cmd, "@y", @operation.y);
                this.CreateAndAddParameter(cmd, "@result", @operation.result);

                cmd.ExecuteNonQuery();
            }
        }

        private static Operation ConstructOperation(SqlDataReader? reader)
        {
            var @operator = (ArithmeticOperators)Enum.Parse(typeof(ArithmeticOperators), reader[1].ToString());
            var x = int.Parse(reader[2].ToString());
            var y = int.Parse(reader[3].ToString());
            var result = float.Parse(reader[4].ToString());

            return new Operation(@operator, x, y, result);
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