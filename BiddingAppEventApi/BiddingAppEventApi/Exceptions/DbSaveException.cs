using System;

namespace WebAppEventApi.Exceptions
{
    [Serializable]
    public class DbSaveException : Exception
    {
        public DbSaveException()
        {

        }

        public DbSaveException(string failedId)
            : base(failedId)
        { 

        }
    }
}