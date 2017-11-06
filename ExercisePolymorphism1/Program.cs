using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercisePolymorphism1
{
    public abstract class DbConnection
    {
        //private string _connectionString;
        //private TimeSpan Timeout;

        private string ConnectionString { get; set; }
        private TimeSpan Timeout { get; set; }

        protected DbConnection(string connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
                throw new ArgumentException("Connection string is NULL or empty");

            ConnectionString = connectionString;

        }

        public abstract bool OpenConnection();

        public abstract bool CloseConnection();

    }

    public class DbCommand
    {
        protected readonly DbConnection _dbConnection;
        protected readonly string _commandInstruction;


        public DbCommand(DbConnection connection, string instruction)
        {
            if (connection is null)
                throw new ArgumentNullException("Connection is null");

            if (string.IsNullOrEmpty(instruction))
                throw new ArgumentException("instruction is null or empty");

            _dbConnection = connection;
            _commandInstruction = instruction;
        }

        public void Execute()
        {
            _dbConnection.OpenConnection();
            Console.WriteLine($"{_commandInstruction} executed");
            _dbConnection.CloseConnection();
        }
    }

    public class SqlConnection : DbConnection
    {
        public SqlConnection(string connectionString) : base(connectionString)
        {
        }

        public override bool OpenConnection()
        {
            Console.WriteLine("Opening SQL connection");
            return true;
        }

        public override bool CloseConnection()
        {
            Console.WriteLine("Closing SQL connection");
            return true;
        }
        
    }

    class Program
    {
        static void Main(string[] args)
        {
            DbCommand SqlCommand = new DbCommand(new SqlConnection("SQL_DB_01"), "The SQL command");
            SqlCommand.Execute();
        }
    }
}
